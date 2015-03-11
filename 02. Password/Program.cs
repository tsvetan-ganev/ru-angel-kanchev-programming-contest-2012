using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password
{
	class Program
	{
		static void Main(string[] args)
		{
			string password = Console.ReadLine();
			int lowerCaseCount = 0;
			int upperCaseCount = 0;
			int digitsCount = 0;

			for (int i = 0; i < password.Length; i++)
			{
				if (password[i] >= 'A' && password[i] <= 'Z')
				{
					upperCaseCount++;
				}
				else if (password[i] >= 'a' && password[i] <= 'z')
				{
					lowerCaseCount++;
				}
				else if (password[i] >= '1' && password[i] <= '9')
				{
					digitsCount++;
				}
			}

			if (lowerCaseCount > 0 && upperCaseCount > 0 
				&& digitsCount > 0 && password.Length >= 8)
			{
				Console.WriteLine("YES");
				Console.WriteLine("{0} {1}", lowerCaseCount, upperCaseCount);
			}
			else
			{
				Console.WriteLine("NO");
			}
		}
	}
}
