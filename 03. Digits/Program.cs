using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digits
{
	class Program
	{
		static void Main(string[] args)
		{
			string number = Console.ReadLine();
			var digitsOccurances = new Dictionary<char, int>()
			{
				{'0', 0}, {'1', 0}, {'2', 0}, {'3', 0}, {'4', 0},
				{'5', 0}, {'6', 0}, {'7', 0}, {'8', 0}, {'9', 0}
			};

			foreach (var digit in number)
			{
				digitsOccurances[digit]+=1;
			}

			var query = digitsOccurances
				.OrderBy(c => c.Value)
				.ThenBy(c => c.Key);

			foreach (var item in query)
			{
				for (int i = 0; i < item.Value; i++)
				{
					Console.Write(item.Key);
				}
			}
            Console.WriteLine();
		}
	}
}
