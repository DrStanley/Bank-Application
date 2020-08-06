using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Application.Services
{
	interface IBankTransactions
	{
		public void Deposit();
		public void Withdraw();

		public void CreateAccount();

		public void Options();
	}
}
