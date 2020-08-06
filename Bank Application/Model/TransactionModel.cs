using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Application.Model
{
	public class TransactionModel
	{
		public long Account_Number { get; set; }
		public string Transaction_Type { get; set; }
		public decimal Balance { get; set; }
		public int Amount { get; set; }
		public DateTime Date_of_Trans { get; set; }
	}
}
