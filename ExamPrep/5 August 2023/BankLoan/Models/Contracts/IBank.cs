using System;
using System.Collections.Generic;
using System.Text;

namespace BankLoan.Models.Contracts
{
    public interface IBank
    {
        public string Name { get; }
        public int Capacity { get; }
        public IReadOnlyCollection<ILoan> Loans { get; }
        public IReadOnlyCollection<IClient> Clients { get; }

        public double SumRates();

        public void AddClient(IClient client);
        public void RemoveClient(IClient client);
        public void AddLoan(ILoan loan);
        public string GetStatistics();
    }
}
