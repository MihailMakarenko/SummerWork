using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// КЛАСС НИГДЕ НЕ ИСПОЛЬЗУЕТСЯ (ЕЩЕ НЕ ДОДЕЛАН)
namespace SummerWorkProject
{
    internal class ReceiptMoney
    {
        private double _amount;
        private DateTime _dateTime;
        private bool _isCard;
        private string _description;

        public ReceiptMoney(double amount, DateTime dateTime, bool isCard, string description)
        {
            _amount = amount;
            _dateTime = dateTime;
            _isCard = isCard;
            _description = description;
        }

        public static double GetReceiptMoneyOnPeriod(ReceiptMoney[] receiptMoneyArray, DateTime startTime, DateTime finishTime)
        {
            if (startTime > finishTime)
            {
                (startTime, finishTime) = (finishTime, startTime);
            }
            double totalMoney = receiptMoneyArray.Where(item => (item._dateTime >= startTime && item._dateTime <= finishTime)).Sum(item => item._amount);
            return totalMoney;
        }
    }
}
