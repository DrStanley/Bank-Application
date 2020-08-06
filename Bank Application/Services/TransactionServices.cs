using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Application.Services
{
	class TransactoionServices
	{
        public void NewAlert(object sender, TransactionInfoArgs e)
        {

            Console.WriteLine("**** Astonish Bank Alert ****");

            Console.WriteLine($"Transaction: {e.transaction.Transaction_Type}\n" +
                $"Account Number: {e.transaction.Account_Number}\n" +
                $"Amount: {e.transaction.Amount}\n" +
                $"Balance: {e.transaction.Balance}\n" +
                $"Date: {e.transaction.Date_of_Trans} ");
        }



    }
}
