using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_T1
{
    public class Account
    {
        public double Balance { get; private set; }
        public double InterestRate { get; private set; }

        public Account(double initialBalance, double interest)
        {
            if (double.IsNaN(initialBalance))
                throw new Exception($"Initial Balance can't be NaN!");
            if(double.IsInfinity(initialBalance))
                throw new Exception($"Initial Balance can't be negative or positive infinity!");
            if(initialBalance < 0)
                throw new Exception($"Balance can't be less than 0");

            if (double.IsNaN(interest))
                throw new Exception($"Initial interest can't be NaN!");
            if (double.IsInfinity(interest))
                throw new Exception($"Initial interest can't be negative or positive infinity!");
            if (interest < 0)
                throw new Exception($"interest can't be less than 0");

            Balance = initialBalance;
            InterestRate = interest;
        }

        public void Deposit(double amount)
        {
            if(double.IsNaN(amount))
                throw new Exception($"Cannot deposit NaN!");
            if(double.IsInfinity(amount))
                throw new Exception($"Cannot deposit negative or positive infinity!");
            if(amount <= 0)
                throw new Exception($"Cannot deposit less or equal to 0kr");

            Balance += amount;

            Console.WriteLine($"Your deposit of {amount}kr has been executed successfully.\nCurrent Balance: {Balance}");
        }

        public void Withdraw(double amount)
        {
            if(double.IsNaN(amount))
                throw new Exception($"Cannot Withdraw NaN!");
            if(double.IsInfinity(amount))
                throw new Exception($"Cannot withdraw negative or positive infinity!");
            if(Balance < amount)
                throw new Exception($"The amount requested to withdraw is less than amount in account\nAccount Balance: {Balance}kr.\nAmount requested to withdraw: {amount}kr.");
            if(amount <= 0)
                throw new Exception($"Cannot withdraw less or equal to 0kr");

            Balance -= amount;
            Console.WriteLine($"Your withdraw of {amount}kr has been executed successfully.\nCurrent Balance: {Balance}");
        }

        public bool Transfer(Account targetAccount, double TransferedAmount)
        {
            if (targetAccount == this)
                throw new Exception($"Target account cannot be the same as sender.");
            Withdraw(TransferedAmount);
            targetAccount.Deposit(TransferedAmount);
            return true;
        }

        public double CalculateInterest()
        {
            var interestAmount = Balance * InterestRate;

            Balance = Balance * (InterestRate + 1);

            return interestAmount;
        }
    }
}
