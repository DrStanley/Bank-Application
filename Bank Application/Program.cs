using System;
using System.Collections.Generic;
using System.Linq;
using Bank_Application.Services;


namespace Bank_Application
{
	class Program
	{
		static void Main(string[] args)
		{
			/*this program perform tansactions with AccountNum as each 
			 * account identifier
			*/
			IBankTransactions bank = new BankTransacService();
			bank.Options();
			var names = new List<string> { "james","anabel"};
			var nam =(from n in names
					  where n.StartsWith(' ')
					  orderby n
					  select n);
		}
	}
}
