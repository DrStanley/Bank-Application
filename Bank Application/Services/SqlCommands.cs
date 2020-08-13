using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Bank_Application.Services
{
	class SqlCommands
	{

		public static void  UpdateSql(long accNum, decimal bal, SqlConnection con)
		{
			con.Close();
			con.Open();
			try
			{
				SqlCommand cmd2 = new SqlCommand("UPDATE Customers SET Balance = " + bal + " where Account_Number = " + accNum, con);
				cmd2.ExecuteNonQuery();

			}
			catch (Exception ex)
			{
				Console.WriteLine("Error message: " + ex.Message + " \nReasons: " + ex.StackTrace);
			}
		}


		public static bool CheckAccount(long accNum, SqlConnection con) {
			con.Close();
			con.Open();

			try
			{
				SqlCommand cmd = new SqlCommand("select * from Customers where Account_Number = " + accNum, con);

				SqlDataReader reader = cmd.ExecuteReader();

				return reader.HasRows;
			}catch(Exception ex)
			{
				Console.WriteLine("Error Mesaage: " + ex.Message + " \nReasons: " + ex.StackTrace);

				return false;
			}
		}

		public static SqlDataReader SelectSql(long accNum , SqlConnection con)
		{
			con.Close();
			con.Open();

			SqlCommand cmd = new SqlCommand("select * from Customers where Account_Number = " + accNum, con);

			SqlDataReader reader = cmd.ExecuteReader();
			try
			{
				if (reader.HasRows)
				{
					return reader;

				}
				else
				{
					Console.WriteLine("Account Number Doesnt Exist");
					return null;
				}

			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public static SqlDataReader SelectSqlT(long accNum , SqlConnection con)
		{
			con.Close();
			con.Open();

			SqlCommand cmd = new SqlCommand("select * from Transactions where Account_Number = " + accNum, con);

			SqlDataReader reader = cmd.ExecuteReader();
			try
			{
				if (reader.HasRows)
				{
					return reader;

				}
				else
				{
					Console.WriteLine("Account Number Doesnt Exist");
					return null;
				}

			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
