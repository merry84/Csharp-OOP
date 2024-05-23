using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;


namespace ChristmasPastryShop.Core.Contracts
{
    public class Controller : IController
    {
        private IRepository<IBooth> booths;

        public Controller()
        {
            booths = new BoothRepository();
        }
        public string AddBooth(int capacity)
        {
            int boothId = booths.Models.Count + 1;

            IBooth booth = new Booth(boothId, capacity);
            booths.AddModel(booth);

            return string.Format(OutputMessages.NewBoothAdded, boothId, capacity);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if (delicacyTypeName != nameof(Gingerbread)
                && delicacyTypeName != nameof(Stolen))
            {

                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId  == boothId);
            if (booths.Models.Any(d => d.DelicacyMenu.Models.Any(x => x.Name == delicacyName)))
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);

            IDelicacy delicacy;
            if (delicacyTypeName == nameof(Gingerbread))
                delicacy = new Gingerbread(delicacyName);
            else
            {
                delicacy = new Stolen(delicacyName);
            }

            booth.DelicacyMenu.AddModel(delicacy);
            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if (cocktailTypeName != nameof(Hibernation)
                && cocktailTypeName != nameof(MulledWine))
            {

                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }
            //("Small", "Middle", "Large"), 
            if (size != "Small" && size != "Middle" && size != "Large")
                return string.Format(OutputMessages.InvalidCocktailSize, size);

            if (booths.Models.Any(x => x.CocktailMenu.Models
                    .Any(x => x.Name == cocktailName && x.Size == size)))
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);

            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            ICocktail cocktail = booth.CocktailMenu.Models.FirstOrDefault(c => c.Size == size && c.Name == cocktailName);
            
            if (cocktailTypeName == nameof(Hibernation))
                cocktail = new Hibernation(cocktailName, size);
            else if(cocktailTypeName == nameof(MulledWine))
            
                cocktail = new MulledWine(cocktailName, size);
           
            booth.CocktailMenu.AddModel(cocktail);
            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string ReserveBooth(int countOfPeople)
        {
            //•	Order all booths from the BoothRepository, which are not reserved &&
            //their capacity is enough for the number of people provided, by capacity ascending, and the by boothId, decsending
            var booth = this.booths.Models
                .Where(x => x.IsReserved == false && x.Capacity >= countOfPeople)
                .OrderBy(x => x.Capacity)
                .ThenByDescending(x => x.BoothId)
                .FirstOrDefault();//Take the first available Booth.
            if (booth == null)
            { return string.Format(OutputMessages.NoAvailableBooth, countOfPeople); }

            booth.ChangeStatus();
            return string.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            IBooth booth = this.booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            string[] orders = order.Split("/");

            bool isCocktail = false;

            string itemTypeName = orders[0];
            // Finds the booth with the given boothId and finds the item from the given type with the given name
            if (itemTypeName != nameof(MulledWine) &&
                itemTypeName != nameof(Hibernation) &&
                itemTypeName != nameof(Gingerbread) &&
                itemTypeName != nameof(Stolen))
            {
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }
            string itemName = orders[1];
            //If an item with the given itemName is not added in the according IRepository yet,
            //return the following message: "There is no {itemTypeName} {itemName} available!"
            if (!booth.CocktailMenu.Models.Any(x => x.Name == itemName)
                && !booth.DelicacyMenu.Models.Any(x => x.Name == itemName))
            {
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
            }

            int countPieces = int.Parse(orders[2]);
            //•	If the item is cocktail:
            if (itemTypeName == nameof(Hibernation)
                || itemTypeName == nameof(MulledWine))
            {
                isCocktail = true;
            }
            //Check if cocktail from the given itemTypeName, with the given itemName and the desired size is available:
            if (isCocktail)
            {
                string size = orders[3];
                ICocktail cocktailDesired = booth.CocktailMenu.Models
                    .FirstOrDefault(x=>x.GetType().Name== itemTypeName 
                && x.Size == size
                && x.Name == itemName);
                if (cocktailDesired == null)
                {
                    //There is no {size} {itemName} available!"
                    return string.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                }
                //o	If all the validations pass, the CurrentBill is increased with the price of the desired item
                booth.UpdateCurrentBill(cocktailDesired.Price * countPieces);
                return string.Format(OutputMessages.SuccessfullyOrdered, boothId, countPieces, itemName);
            }
            else
            {
                //o	Check if delicacy from the given itemTypeName and the given itemName is available:
                IDelicacy delicacyDesired = booth.DelicacyMenu.Models
                    .FirstOrDefault(x => x.GetType().Name == itemTypeName && x.Name == itemName);
                if (delicacyDesired == null)
                {
                    //"There is no {itemTypeName} {itemName} available!"
                    return string.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);
                }
                booth.UpdateCurrentBill(delicacyDesired.Price * countPieces);
                return string.Format(OutputMessages.SuccessfullyOrdered, boothId, countPieces, itemName);
            }
        }
        public string LeaveBooth(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            var sb = new StringBuilder();
            //"Bill {currentBill:f2} lv"
            // "Booth {boothId} is now available!"
            sb.AppendLine($"Bill {booth.CurrentBill:f2} lv");
            booth.Charge();
            booth.ChangeStatus();
            sb.AppendLine($"Booth {boothId} is now available!");
            return sb.ToString().TrimEnd();
        }

        public string BoothReport(int boothId)
        {
            return this.booths.Models.FirstOrDefault(b => b.BoothId == boothId)
                .ToString()
                .TrimEnd();
        }

    }
}
