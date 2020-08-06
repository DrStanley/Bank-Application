using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Bank_Application.Model;

namespace Bank_Application.Services
{
	class BankTransacService : IBankTransactions
	{

		List<CustomerModel> allCustomer = new List<CustomerModel>();

		List<TransactionModel> allTransaction = new List<TransactionModel>();


		DataBaseComands baseComands;



		public BankTransacService()
		{
			baseComands = new DataBaseComands();
			Console.WriteLine("*** Welcome To Astonish Bank ***");

		}

		public void CreateAccount()
		{
			int age = 0, bal = 0;
			CustomerModel customerModel = new CustomerModel();

			Console.WriteLine("Enter Your full name");
			customerModel.Full_Name = Console.ReadLine();

			Console.WriteLine("Enter Your Email");
			customerModel.Email = Console.ReadLine();

			Console.WriteLine("Enter Your Phone Number");
			customerModel.Phone_Number = Console.ReadLine();

			Console.WriteLine("Enter Your Age");
			Int32.TryParse(Console.ReadLine(), out age);
			customerModel.Age = age;

			Console.WriteLine("Enter Your opening Balance");
			Int32.TryParse(Console.ReadLine(), out bal);
			customerModel.Balance = bal;

			Console.WriteLine("Enter Your Account Type (Savings,Current,Fixed)");
			customerModel.Account_Type = Console.ReadLine();

			customerModel.Date_Created = DateTime.Now;

			System.Random rnd = new System.Random();
			customerModel.Account_Number = rnd.Next(1000, 10000);

			Console.WriteLine($"Your Account number is: {customerModel.Account_Number}");

			baseComands.InsertCustormer(customerModel);

			Console.WriteLine("Account Successfully Created!");
			Options();


		}

		public void Deposit()
		{
			int amount = 0;
			long accNum = 0;

			Console.WriteLine("Enter Account Number");
			long.TryParse(Console.ReadLine(), out accNum);

			Console.WriteLine("Enter Deposit amount");
			Int32.TryParse(Console.ReadLine(), out amount);

			baseComands.MakeDeposit(accNum, amount);

			Options();

		}

		public void Withdraw()
		{
			int amount = 0;
			long accNum = 0;

			Console.WriteLine("Enter Account Number");
			long.TryParse(Console.ReadLine(), out accNum);

			Console.WriteLine("Enter Withdrawal amount");
			Int32.TryParse(Console.ReadLine(), out amount);

			baseComands.MakeWithdraw(accNum, amount);

			Options();
		}

		public void Options()
		{
			char check = ' ';
			Console.WriteLine("Choose an Input Option");
			Console.WriteLine("'c' to Create Account \t\t\t\t 'd' to deposit to an Account ");
			Console.WriteLine("'w' to withdral from an Account \t\t 'b' to see balance from an Account ");
			Console.WriteLine("'s' to statement of an Account \t\t\t 'x' to show all Account ");
			Console.WriteLine("'0' to Exit \t\t\t 't' to transfer to another Account");
			Console.Write(":");
			Char.TryParse(Console.ReadLine(), out check);

			switch (check)
			{
				case 'c':
					CreateAccount();
					break;

				case 'd':
					Deposit();
					break;

				case 'w':
					Withdraw();
					break;

				case 'b':
					showBal();
					break;

				case 's':
					StatementOfAccount();
					break;

				case 'x':
					ShowAllAccount();

					break;
				case 't':
					Tranfer();

					break;
				case '0':
					System.Environment.Exit(0);
					break;

				default:
					Console.WriteLine("Incorrect Input");
					Options();
					break;
			}


		}

		private void Tranfer()
		{
			int amount = 0;
			long accNum1 = 0;
			long accNum2 = 0;

			Console.WriteLine("Enter Sender Account Number");
			long.TryParse(Console.ReadLine(), out accNum1);

			Console.WriteLine("Enter Recievers Account Number");
			long.TryParse(Console.ReadLine(), out accNum2);

			Console.WriteLine("Enter amount");
			Int32.TryParse(Console.ReadLine(), out amount);

			baseComands.MakeTransfer(accNum1, accNum2, amount);

			Options();
		}


		public void StatementOfAccount()
		{
			long accNum = 0;

			Console.WriteLine("Enter Account Number");
			long.TryParse(Console.ReadLine(), out accNum);

			baseComands.ShowStatement(accNum);

			Options();

		}

		public void showBal()
		{
			long accNum = 0;

			Console.WriteLine("Enter Account Number");
			long.TryParse(Console.ReadLine(), out accNum);
			baseComands.GetBalance(accNum);
			Options();
		}


		public void ShowAllAccount()
		{
			Console.WriteLine("**** Astonish Bank Customers ****");
			//Console.WriteLine("Account Full_Name\t\tAccount Number\t\tAge\t\tBalance\t\tAccount Type\t\tDate of Opening");
			baseComands.GetAllCustomers();
			Options();

		}
	}
}