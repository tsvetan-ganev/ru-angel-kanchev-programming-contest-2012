using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    class Contestant
    {
        public string Number { get; set; }
        public string ClubNumber { get; set; }
        public string LastName { get; set; }
        public char Sex { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan Finish { get; set; }
        public TimeSpan TotalTime 
        { 
            get
            {
                return Finish - Start;
            }
        }
    }

    class Program
    {
        /// <summary>
        /// Finds the number of winners to be displayed. 
        /// </summary>
        /// <param name="contestantsRanking">Array with contestants.</param>
        /// <param name="entries">The maximum number of top contestants to be picked if there are no equal scores.</param>
        /// <returns>The number of winners to be displayed as Integer.</returns>
        private static int FindWinnersCount(Contestant[] contestantsRanking, int entries = 3)
        {
            int winnersCount = 0;
            for (int i = 0; i < contestantsRanking.Length; i++)
            {
                if (i < entries)
                {
                    winnersCount++;
                }
                else
                {
                    if (contestantsRanking[i].TotalTime == contestantsRanking[i - 1].TotalTime)
                    {
                        winnersCount++;
                    }
                }
            }
            return winnersCount;
        }

        /// <summary>
        /// Displays the winners to the console in the required format.
        /// </summary>
        /// <param name="winners">Array with contestants.</param>
        /// <param name="count">The number of winners to be displayed.</param>
        private static void DisplayWinners(Contestant[] winners, int count)
        {
            StringBuilder answer = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                answer.Append(winners[i].Number + " ");
            }
            Console.WriteLine(answer.ToString().TrimEnd(' '));
        }

        static void Main(string[] args)
        {
            var firstLine = Console.ReadLine().Split();
            int contestantsCount = int.Parse(firstLine[0]);
            int clubsCount = int.Parse(firstLine[1]);

            #region Handling contestants input
            List<Contestant> contestants = new List<Contestant>();
            for (int i = 0; i < contestantsCount; i++)
            {
                var input = Console.ReadLine().Split();

                // handling input
                string number = input[0].Substring(0, 2);
                string club = input[0].Substring(2, 2);
                string name = input[0].Substring(4, input[0].Length - 4);
                char sex = input[1].First();
                string start = input[2];
                string finish = input[3];

                // saving the data into a new Contestant instance
                var contestant = new Contestant();
                contestant.Number = number;
                contestant.ClubNumber = club;
                contestant.LastName = name;
                contestant.Sex = sex;
                contestant.Start = new TimeSpan(
                    int.Parse(start.Substring(0, 2)), // hours
                    int.Parse(start.Substring(3, 2)), // minutes
                    int.Parse(start.Substring(6, 2))); // seconds
                contestant.Finish = new TimeSpan(
                    int.Parse(finish.Substring(0, 2)), // hours
                    int.Parse(finish.Substring(3, 2)), // minutes
                    int.Parse(finish.Substring(6, 2))); // seconds

                contestants.Add(contestant);
            }
            #endregion

            #region Finding male winners and displaying them
            var maleRanking = contestants
                .Where(c => c.Sex == 'm')
                .OrderBy(c => c.TotalTime)
                .ThenBy(c => int.Parse(c.Number))
                .ToArray();
            int maleWinnersCount = FindWinnersCount(maleRanking, 3);
            DisplayWinners(maleRanking, maleWinnersCount);
            #endregion

            #region Finding female winners and displaying them
            var femaleRanking = contestants
                .Where(c => c.Sex == 'w')
                .OrderBy(c => c.TotalTime)
                .ThenBy(c => int.Parse(c.Number))
                .ToArray();
            int femaleWinnersCount = FindWinnersCount(femaleRanking, 3);
            DisplayWinners(femaleRanking, femaleWinnersCount);
            #endregion

            #region Finding the winning club(s) and displaying them
            // joins the winners from the male and female rankings
            var allWinners = femaleRanking.
                Take(femaleWinnersCount).
                Concat(maleRanking.Take(maleWinnersCount));

            // creates a dictionary with 'Club Number' as key and 'Wins' as value
            Dictionary<String, int> clubsWinners = new Dictionary<string,int>();
            foreach (var winner in allWinners)
	        {
                if (clubsWinners.Keys.Contains(winner.ClubNumber))
                {
                    clubsWinners[winner.ClubNumber]++;
                }
                else
                {
                    clubsWinners.Add(winner.ClubNumber, 1);
                }
            }

            // converts the dictionary to a sorted array
            var winningClubs = clubsWinners
                .OrderByDescending(c => c.Value)
                .ThenBy(c => int.Parse(c.Key))
                .Select(c => new { Wins = c.Value, Number = c.Key })
                .ToArray();

            // handles the correct output if there are clubs with equal wins
            StringBuilder clubsAnswer = new StringBuilder();
            for (int i = 0; i < winningClubs.Length; i++)
            {
                if (i == 0) // minimum one club winner is displayed
                {
                    clubsAnswer.Append(winningClubs[i].Number + " ");
                }
                else
                {
                    if (winningClubs[i].Wins == winningClubs[i - 1].Wins)
                    {
                        clubsAnswer.Append(winningClubs[i].Number + " ");
                    }
                    else break;
                } 
            }
           
            Console.WriteLine(clubsAnswer.ToString().TrimEnd(' '));
            #endregion
        }
    }
}
