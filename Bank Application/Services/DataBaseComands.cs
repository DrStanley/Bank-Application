using System;
using System.Data.SqlClient;
using Bank_Application.Model;

namespace Bank_Application.Services
{
	class DataBaseComands
	{
		SqlConnection con = new SqlConnection();
		public event EventHandler<TransactionInfoArgs> NewTransactionAlert;

		public DataBaseComands()
		{
			con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
						+ @"C:\Users\Stanley\source\repos\Bank Application\Bank Application\Bank.mdf"
						+ ";Integrated Security=True";
		}

		public void InsertCustormer(CustomerModel customerModel)
		{
			con.Open();

			SqlCommand cmd = con.CreateCommand();

			cmd.CommandText = "Insert into Customers " +
				"(Full_Name,Email,Phone_Number,Age,Account_Type,Account_Number,Balance,Date_Created) " +
				"values (@Fn,@Em,@Pn,@Ag,@At,@An,@Bl,@Dc)";

			cmd.Parameters.AddWithValue("@Fn", customerModel.Full_Name);
			cmd.Parameters.AddWithValue("@Em", customerModel.Email);
			cmd.Parameters.AddWithValue("@Pn", customerModel.Phone_Number);
			cmd.Parameters.AddWithValue("@Ag", customerModel.Age);
			cmd.Parameters.AddWithValue("@At", customerModel.Account_Type);
			cmd.Parameters.AddWithValue("@An", customerModel.Account_Number);
			cmd.Parameters.AddWithValue("@Bl", customerModel.Balance);
			cmd.Parameters.AddWithValue("@Dc", customerModel.Date_Created);


			try
			{
				cmd.ExecuteNonQuery();

				Console.WriteLine("Successfully saved");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}
			con.Close();

		}

		public void Inserttransaction(TransactionModel transactionModel)
		{
			con.Close();
			con.Open();

			SqlCommand cmd = con.CreateCommand();

			cmd.CommandText = "Insert into Transactions " +
				"(Account_Number,Transaction_Type,Balance,Amount,Date_of_Trans) " +
				"values (@An,@Tt,@Bl,@Am,@Dt)";

			cmd.Parameters.AddWithValue("@An", transactionModel.Account_Number);
			cmd.Parameters.AddWithValue("@Tt", transactionModel.Transaction_Type);
			cmd.Parameters.AddWithValue("@Bl", transactionModel.Balance);
			cmd.Parameters.AddWithValue("@Am", transactionModel.Amount);
			cmd.Parameters.AddWithValue("@Dt", transactionModel.Date_of_Trans);


			try
			{
				cmd.ExecuteNonQuery();

				Console.WriteLine("Successfully saved");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}
			con.Close();

		}

		public void GetBalance(long accNum)
		{

			con.Close();
			con.Open();
			try
			{
				SqlCommand cmd2 = new SqlCommand("select * from Customers where Account_Number = " + accNum, con);
				SqlDataReader reader = cmd2.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						Console.WriteLine("------------------------------------------------------------");
						Console.WriteLine("Account Ballance: " + reader.GetValue(7));
						Console.WriteLine("------------------------------------------------------------");

					}

				}
				else
				{
					Console.WriteLine("Account Number Doesnt Exist");
				}


			}
			catch (Exception ex)
			{
				Console.WriteLine("Error Message: " + ex.Message + " \nReasons: " + ex.StackTrace);
			}
		}

		public void MakeDeposit(long accNum, int amt)
		{
			con.Close();
			con.Open();
			try
			{
				SqlCommand cmd = new SqlCommand("select * from Customers where Account_Number = " + accNum, con);
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{

					reader.Read();
					decimal bal = reader.GetDecimal(7);

					bal += amt;
					con.Close();
					con.Open();
					SqlCommand cmd2 = new SqlCommand("UPDATE Customers SET Balance = " + bal + " where Account_Number = " + accNum, con);
					cmd2.ExecuteNonQuery();

					Inserttransaction(transactions(accNum, "Credit", bal, amt));
					TrasactionDone(transactions(accNum, "Credit", bal, amt));

				}
				else
				{
					Console.WriteLine("Account Number Does not Exist");

				}


			}

			catch (Exception ex)
			{
				Console.WriteLine("Error message: " + ex.Message + " \nReasons: " + ex.StackTrace);

			}
		}
		public void MakeTransfer(long accNumS, long accNumR, int amt)
		{
			con.Close();
			con.Open();
			try
			{
				SqlCommand cmd = new SqlCommand("select * from Customers where Account_Number = " + accNumR, con);
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{
					con.Close();
					con.Open();
					SqlCommand cmd2 = new SqlCommand("select * from Customers where Account_Number = " + accNumS, con);
					SqlDataReader reader2 = cmd2.ExecuteReader();
					if (reader2.HasRows)
					{
						MakeWithdraw(accNumS, amt);
						MakeDeposit(accNumR, amt);

					}
					else
					{
						Console.WriteLine("Reciever Account Number Does not Exist");
					}


				}
				else
				{
					Console.WriteLine("Sender Account Number Does not Exist");

				}


			}

			catch (Exception ex)
			{
				Console.WriteLine("Error message: " + ex.Message + " \nReasons: " + ex.StackTrace);

			}
		}

		public void MakeWithdraw(long accNum, int amt)
		{
			con.Close();
			con.Open();
			try
			{
				SqlCommand cmd = new SqlCommand("select * from Customers where Account_Number = " + accNum, con);
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.HasRows)
				{

					reader.Read();
					decimal bal = reader.GetDecimal(7);
					if (bal > amt)
					{
						bal -= amt;
						con.Close();
						con.Open();
						SqlCommand cmd2 = new SqlCommand("UPDATE Customers SET Balance = " + bal + " where Account_Number = " + accNum, con);
						cmd2.ExecuteNonQuery();

						Inserttransaction(transactions(accNum, "Debit", bal, amt));
						TrasactionDone(transactions(accNum, "Debit", bal, amt));
					}
					else
					{
						Console.WriteLine("Withdrawal attempt failed.\n Insuficient Fund");
					}


				}
				else
				{
					Console.WriteLine("Account Number Does not Exist");

				}


			}

			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message + " \nReasons: " + ex.StackTrace);

			}
		}

		public void GetAllCustomers()
		{
			con.Close();
			con.Open();


			try
			{
				SqlCommand cmd = new SqlCommand("select * from Customers", con);
				SqlDataReader reader = cmd.ExecuteReader();
				int i = 0;
				while (reader.Read())
				{
					i++;
					Console.WriteLine("------------------------------------------------------------");
					if (i == 2)
					{
						Console.Write(i + ". " + reader.GetValue(1) + "\t\t");

					}
					else
					{
						Console.Write(i + ". " + reader.GetValue(1) + "\t");

					}
					Console.Write(reader.GetValue(2) + "\t");
					Console.Write(reader.GetValue(3) + "\t");
					Console.Write(reader.GetValue(4) + "\t");
					Console.Write(reader.GetValue(5) + "\t");
					Console.Write(reader.GetValue(6) + "\t");
					Console.Write(reader.GetValue(7) + "\t");
					Console.Write(reader.GetValue(8) + "\n");
					Console.WriteLine("------------------------------------------------------------\n");

				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("Error Mesaage: " + ex.Message + " \nReasons: " + ex.StackTrace);
			}
		}
		public void ShowStatement(long accNum)
		{
			con.Close();
			con.Open();


			try
			{
				SqlCommand cmd = new SqlCommand("select * from Transactions where Account_Number = " + accNum, con);
				SqlDataReader reader = cmd.ExecuteReader();
				int i = 0;
				Console.WriteLine("Account number\t  Transaction Type\t Balance\t"
					+ "Amount\t  Date of Transaction");
				while (reader.Read())
				{
					i++;
					Console.WriteLine("------------------------------------------------------------------------------------------------");
					Console.Write(i + ". " + reader.GetValue(1) + "\t\t ");
					Console.Write(reader.GetValue(2) + "\t ");
					Console.Write(reader.GetValue(3) + "\t \t");
					Console.Write(reader.GetValue(4) + " \t  ");
					Console.Write(reader.GetValue(5) + "\t ");
					Console.WriteLine("\n-----------------------------------------------------------------------------------------------\n");

				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("Error Mesaage: " + ex.Message + " \nReasons: " + ex.StackTrace);
			}
		}

		public void TrasactionDone(TransactionModel transactionModel)
		{
			TransactoionServices services = new TransactoionServices();
			NewTransactionAlert = services.NewAlert;
			NewTransactionAlert?.Invoke(this, new TransactionInfoArgs(transactionModel));


		}

		public TransactionModel transactions(long acc, string type, decimal bal, int amt)
		{

			TransactionModel transaction = new TransactionModel();
			transaction.Account_Number = acc;
			transaction.Transaction_Type = type;
			transaction.Balance = bal;
			transaction.Amount = amt;
			transaction.Date_of_Trans = DateTime.Now;
			return transaction;
		}
	}

}
