using System;
using System.Collections.Generic;
using System.Text;
using Bank_Application.Model;

namespace Bank_Application.Services
{
	class TransactionInfoArgs:EventArgs
	{
		public TransactionModel transaction;
		public TransactionInfoArgs(TransactionModel model) => transaction = model;
	}
}
