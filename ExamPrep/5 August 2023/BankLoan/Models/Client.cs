using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;

namespace BankLoan.Models
{
    public abstract class Client :IClient
    {
        private string name;
        private string id;
        private int interest;
        private double income;

        public Client(string name ,string id,int interest,double income)
        {
            Name = name;
            Id = id;
            Interest = interest;
            Income = income;    
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) string.Format(ExceptionMessages.ClientNameNullOrWhitespace);
                name = value;
            }
        }

        public string Id
        {
            get => id;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) string.Format(ExceptionMessages.ClientIdNullOrWhitespace);
                id = value;
            }
        }

        public int Interest { get=>interest;
            protected set=> interest= value;
        }

        public double Income
        {
            get => income;
            private set
            {
                if (value <= 0) string.Format(ExceptionMessages.ClientIncomeBelowZero);
                income = value;
            }
        }

        public virtual void IncreaseInterest()
        {

        }
       
    }
}
