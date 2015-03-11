using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asymmetric
{
	class Program
	{
		static void Main(string[] args)
		{
			int n = int.Parse(Console.ReadLine());
			int counter = 0;

            // there are that many assymetric numbers
            // in the given interval 1 ≤ N ≤ 9999999
			if (n > 544320)
			{
				Console.WriteLine("-1");
				return;
			}

            // from the first to the last assymetric number
            string lastNumber = null;
			for (int i = 1023455; i < 9876544; i++)
			{
				HashSet<char> uniqueDigits = new HashSet<char>();
				foreach (var digit in i.ToString())
				{
                    uniqueDigits.Add(digit);
				}
				if (uniqueDigits.Count == 7)
				{
                    lastNumber = i.ToString();
                    counter++;
				}

                if (counter == n)
                {
                    break;
                }
			}

            if (string.IsNullOrEmpty(lastNumber))
            {
                Console.WriteLine("-1");
            }
            Console.WriteLine(lastNumber);
		}
	}
}
