using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SummerWorkProject
{
    internal class Account
    {
        /// <summary>
        /// Метод находит сальдо за определенный период
        /// </summary>
        /// <param name="receiptMoney"></param>
        /// <param name="startTime"></param>
        /// <param name="finishTime"></param>
        /// <returns>Возвращает сумму сальдо за период</returns>
        public double GetAmountMoneyOnPeriod(ReceiptMoney[] receiptMoney, DateTime startTime, DateTime finishTime)
        {
            double money = 0;

            if (startTime > finishTime)
            {
                (startTime, finishTime) = (finishTime, startTime);
            }

            money += ReceiptMoney.GetReceiptMoneyOnPeriod(receiptMoney, startTime, finishTime);
            money -= Record.GetTotalSumForPeriod(startTime, finishTime);

            return money;
        }

        /// <summary>
        /// Метод возвращает потраченную сумму по месяцам за текущий год
        /// </summary>
        /// <returns>Массив где храниться потраченная сумма за каждый месяц</returns>
        public double[] spendingMoneyThisYear()
        {

            double[] spendMoney = new double[12];

            DateTime curentDate = new DateTime(DateTime.Today.Year, 1, 1);

            for (int i = 0; i < 12; i++)
            {
                if (curentDate > DateTime.Now)
                {
                    break;
                }

                // возможно будет тут ошибка в добавлении месяца
                spendMoney[curentDate.Month] = (Record.GetTotalSumForPeriod(curentDate, curentDate.AddMonths(1)));
                curentDate = curentDate.AddMonths(1);
            }

            return spendMoney;
        }

        /// <summary>
        /// Метод возвращает общую потраченную сумму за каждый месяц, за все время учета 
        /// </summary>
        /// <returns>Массив double где храниться потраченная сумма по каждому месяцу</returns>
        public static double[] GetSpendingMoneyAllTime()
        {
            double[] spendMoney = new double[12];

            DateTime startTime = Record.GetMinDateSpend;
            DateTime finishTime = Record.GetLastDateSpend;
            DateTime currentTime = startTime;

            for (int i = startTime.Year; i < finishTime.Year; i++)
            {
                while (currentTime.Month < 12)
                {
                    spendMoney[currentTime.Month] = (Record.GetTotalSumForPeriod(currentTime, currentTime.AddMonths(1)));
                    currentTime = currentTime.AddMonths(1);
                }
            }

            return spendMoney;
        }
    }
}
