using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Application.Model
{
	public class CustomerModel
	{
		public long Account_Number { get; set; }
		public int Age { get; set; }
		public string Full_Name { get; set; }
		public string Phone_Number { get; set; }
		public string Account_Type { get; set; }
		public string Email { get; set; }
		public decimal Balance { get; set; }
		public DateTime Date_Created { get; set; }
	}
}
