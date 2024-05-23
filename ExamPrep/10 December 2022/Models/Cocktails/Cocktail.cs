using System;
using System.Collections.Generic;
using System.Text;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail : ICocktail
    {
        private string name;
        
        private double price;

        protected Cocktail(string name, string size, double price)
        {
            Name = name;
            Size = size;
            Price = price;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    string.Format(ExceptionMessages.NameNullOrWhitespace);
                name = value;
            }
        }

        public string Size { get; private set; }

        public double Price
            // If the Size is set to "Large", the Price is set to be equal to the passed value
            // If the Size is set to "Middle", the Price is equal to ⅔ of the passed value (example: 2/3 * 13.50 = 9.00)
            // If the Size is set to "Small", the Price is equal to ⅓ of the passed value (example: 1/3* 10.50 = 3.50)
            // 
        {
            get => price;
            private set
            {
                if (Size == "Large")
                {
                    price = value;
                }
                else if (Size == "Middle")
                {
                    price = 2.0 / 3 * value;
                }
                else if (Size == "Small")
                {
                    price = 1.0 / 3 * value;
                }
            }
        }

        public override string ToString()
        {
            return $"{Name} ({Size}) - {Price:f2} lv";
        }
    }
}


