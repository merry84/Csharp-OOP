using System;
using System.Collections.Generic;
using System.Text;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        private int boothId;
        private int capacity;
        private readonly IRepository<IDelicacy> delicacyMenu;
        private readonly IRepository<ICocktail> cocktailMenu;
        private double currentBill;
        private double turnover;
       
        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;
            currentBill = 0;
            turnover = 0;
            this.delicacyMenu = new DelicacyRepository();
            this.cocktailMenu = new CocktailRepository();
           

        }
        public int BoothId
        {
            get => boothId;
            private set => boothId = value;
        }

        public int Capacity
        {
            get => capacity;
            private set
            {
                if (value <1)
                {
                    string.Format(ExceptionMessages.CapacityLessThanOne);
                }
                capacity = value;
            }

        }

        public IRepository<IDelicacy> DelicacyMenu => delicacyMenu;
        public IRepository<ICocktail> CocktailMenu => cocktailMenu;
        public double CurrentBill => currentBill;
        public double Turnover => turnover;
        public bool IsReserved { get; private set; }
        public void UpdateCurrentBill(double amount)
        {
            currentBill += amount;
        }

        public void Charge()
        {
            turnover += currentBill;
            currentBill = 0;
        }

        public void ChangeStatus()
        {
            if (IsReserved)
            {
                IsReserved = false;
                return;
            }
            IsReserved = true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            
            sb.AppendLine($"Booth: {BoothId}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Turnover: {Turnover:f2} lv");
            sb.AppendLine("-Cocktail menu:");
            foreach (var item in cocktailMenu.Models)
            {
                sb.AppendLine($"--{item}");
            }

            sb.AppendLine("-Delicacy menu:");
            foreach (var item in delicacyMenu.Models)
            {
                sb.AppendLine($"--{item}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
