using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Repositories.Contracts;
using BankLoan.Utilities.Messages;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private IRepository<ILoan> loans;
        private IRepository<IBank> banks;

        public Controller()
        {
            loans = new LoanRepository();
            banks = new BankRepository();
        }

        public string AddBank(string bankTypeName, string name)
        {
            IBank bank;
            if (bankTypeName == nameof(BranchBank)) bank = new BranchBank(name);
            else if(bankTypeName  == nameof(CentralBank)) bank = new CentralBank(name);
            else
            {
                return string.Format(ExceptionMessages.BankTypeInvalid);
            }
            banks.AddModel(bank);
            return string.Format(OutputMessages.BankSuccessfullyAdded, bankTypeName);
        }
        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            IClient client;
            if (clientTypeName == nameof(Adult))
                client = new Adult(clientName, id, income);
            else if (clientTypeName == nameof(Student))
                client = new Student(clientName, id, income);
            else
            {
                return string.Format(ExceptionMessages.ClientTypeInvalid);
            }

            IBank bank = banks.FirstModel(bankName);
            if ((banks.GetType().Name == nameof(BranchBank) && clientTypeName != nameof(Student))
                ||
                (banks.GetType().Name == nameof(CentralBank) && clientTypeName != nameof(Adult)))
            {
                return string.Format(OutputMessages.UnsuitableBank);
            }
            bank.AddClient(client);
            return string.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);
        }

        public string AddLoan(string loanTypeName)
        {
            ILoan loan;
            if (loanTypeName == nameof(MortgageLoan)) 
                loan = new MortgageLoan();
            else if(loanTypeName == nameof(StudentLoan))
                loan = new StudentLoan();
            else
            {
                throw new ArgumentException(ExceptionMessages.LoanTypeInvalid);

            }
            loans.AddModel(loan);
            return string.Format(OutputMessages.LoanSuccessfullyAdded, loanTypeName);
        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            ILoan loan = loans.FirstModel(loanTypeName);
            if (loan == null)
            {
                return string.Format(ExceptionMessages.MissingLoanFromType,loanTypeName);
            }
            IBank bank = banks.FirstModel(bankName);
            bank.AddLoan(loan);
            loans.RemoveModel(loan);
            return string.Format(OutputMessages.LoanReturnedSuccessfully, loanTypeName, bankName);
        }

       

        public string FinalCalculation(string bankName)
        {
            IBank bank = banks.Models.FirstOrDefault(n => n.Name == bankName);
            var sumLoans = bank.Loans.Sum(a => a.Amount);
            var sumClients = bank.Clients.Sum(c => c.Income);
            var sumFunds = (sumClients + sumLoans).ToString("0.00");
            return string.Format(OutputMessages.BankFundsCalculated, bankName, sumFunds);
        }

        public string Statistics()
        {
            //Returns information about each bank. You can use Bank's GetStatistics() method to implement the current functionality.
           
            var sb = new StringBuilder();
            foreach (var bank in banks.Models)
            {
                sb.AppendLine(bank.GetStatistics());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
