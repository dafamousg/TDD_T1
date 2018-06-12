using System;
using Xunit;
using TDD_T1;

namespace xUnit_Test
{
    public class UnitTest1
    {
        public Account CreateAccount(double balance, double interest)
        {
            return new Account(balance, interest);
        }

        #region Account_Test

        #region Balance_Test
        [Theory]
        [InlineData(-10)]
        [InlineData(double.NaN)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity)]
        public void Invalid_BalanceInput_Throws_Exception(double balance)
        {
            Assert.Throws<Exception>(() => CreateAccount(balance, 0.02));
        }

        [Fact]
        public void If_AccountBalance_Is_Valid()
        {
            Account account = CreateAccount(300,0.01);
            double expectedBalance = 300;

            Assert.Equal(expectedBalance, account.Balance);
        }
        #endregion

        #region Interest_Test
        [Theory]
        [InlineData(-0.02)]
        [InlineData(double.NaN)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity)]
        public void Invalid_InterestInput_Throws_Exception(double interest)
        {
            Assert.Throws<Exception>(() => CreateAccount(300, interest));
        }

        [Fact]
        public void If_AccountInterest_Is_Valid()
        {
            Account account = CreateAccount(300, 0.01);
            double expectedBalance = 0.01;

            Assert.Equal(expectedBalance, account.InterestRate);
        }
        #endregion

        #endregion

        #region Deposit_Test
        [Theory]
        [InlineData(0)]
        [InlineData(double.NaN)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity)]
        public void Deposit_Work(double amount)
        {
            Account account = CreateAccount(300, 0.02);

            Assert.Throws<Exception>(() => account.Deposit(amount));
        }

        [Fact]
        public void If_DepositAmount_Is_Valid()
        {
            var amount = 100;
            Account account = CreateAccount(100, 0.01);

            double expectedBalance = account.Balance + amount;
            account.Deposit(amount);

            Assert.Equal(expectedBalance, account.Balance);
        }
        #endregion

        #region Withdraw_Test
        [Theory]
        [InlineData(0)]
        [InlineData(double.NaN)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity)]
        public void Invalid_WithdrawInput_Throws_Exception(double amount)
        {
            Account account = CreateAccount(300, 0.01);

            Assert.Throws<Exception>(() => account.Withdraw(amount));

        }

        [Theory]
        [InlineData(200)]
        public void LessBalance_Than_WithdrawAmount(double amount)
        {
            Account account = CreateAccount(100, 0.01);

            Assert.Throws<Exception>(() => account.Withdraw(amount));
        }

        [Fact]
        public void If_WithdrawAmount_Is_Valid()
        {
            var amount = 100;
            Account account = CreateAccount(200, 0.01);

            double expectedAmount = account.Balance - amount;
            account.Withdraw(amount);

            Assert.Equal(expectedAmount, account.Balance);
        }
        #endregion

        #region Transfer_Test
        [Theory]
        [InlineData(0)]
        [InlineData(200)]
        [InlineData(double.NaN)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity)]
        public void Invalid_Transfer_Throws_Exception(double transferedAmount)
        {
            Account account = CreateAccount(100, 0.01);
            Account targetAccount = CreateAccount(100, 0.01);

            Assert.Throws<Exception>(() => account.Transfer(targetAccount,transferedAmount));
        }
        
        [Fact]
        public void If_TransferedAmount_Is_Valid()
        {
            Account account = CreateAccount(100, 0.01);
            Account targetAccount = CreateAccount(100, 0.01);

            var amount = 100;

            double expectedWithdrawBalance = account.Balance - amount;
            double expected_TargetAccount_Balance = targetAccount.Balance + amount;

            account.Transfer(targetAccount, amount);

            Assert.Equal(expectedWithdrawBalance, account.Balance);
            Assert.Equal(expected_TargetAccount_Balance, targetAccount.Balance);
        }

        [Fact]
        public void If_TargetAndReciever_Is_TheSame()
        {
            Account account = CreateAccount(100, 0.01);
            var amount = 100;

            Assert.Throws<Exception>(() => account.Transfer(account, amount));
        }
        #endregion

        [Fact]
        public void CalculateInterest_Test()
        {
            Account account = CreateAccount(100, 0.02);

            var expectedInterestBalance = account.Balance * account.InterestRate;

            double interestAmount = account.CalculateInterest();

            Assert.Equal(expectedInterestBalance, interestAmount);
        }
    }
}
