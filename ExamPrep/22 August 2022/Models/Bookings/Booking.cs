using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Utilities.Messages;

namespace BookingApp.Models.Bookings
{
    public class Booking :IBooking
    {
        //IRoom room, int residenceDuration, int adultsCount, int childrenCount, int bookingNumber+
        private IRoom room;
        private int residenceDuration;
        private int adultsCount;
        private int childrenCount;
        private int bookingNumber;

        public Booking(IRoom room, int residenceDuration,int adultsCount,int childrenCount,int bookingNumber)
        {
            ResidenceDuration = residenceDuration;
            AdultsCount = adultsCount;
            ChildrenCount = childrenCount;
            this.bookingNumber = bookingNumber;
            this.Room = room;
        }
        public IRoom Room { get; private set; }
        //o	ResidenceDuration must be greater than zero. If NOT, throw a new ArgumentException with the message: "Duration cannot be negative or zero!".
        public int ResidenceDuration
        {
            get=>residenceDuration;
            private set
            {
                if (value <= 0)
                {
                    string.Format(ExceptionMessages.DurationZeroOrLess);
                }
                residenceDuration = value;
            }
        }
        //•	The count of Adults cannot be less than 1. If so, throw a new ArgumentException with the message: "Adults count cannot be negative or zero!".
        public int AdultsCount
        {
            get=> adultsCount;
            private set
            {
                if (value <= 0)
                {
                    string.Format(ExceptionMessages.AdultsZeroOrLess);
                }
                adultsCount = value;
            }
        }
        //•	The count of Children cannot be less than 0. If so, throw a new ArgumentException with the message: "Children count cannot be negative!".
        public int ChildrenCount
        {
            get => childrenCount;
            private set
            {
                if (value < 0)
                {
                    string.Format(ExceptionMessages.ChildrenNegative);
                }
                childrenCount = value;
            }
        }
        public int BookingNumber => bookingNumber;
        public string BookingSummary()
        {
           //"Booking number: {BookingNumber}
           // Room type: {RoomType}
           // Adults: {AdultsCount} Children: {ChildrenCount}
           // Total amount paid: {TotalPaid():F2} $"
           // HINT: TotalPaid() => MathRound(ResidenceDuration*PricePerNight, 2),  print TotalPaid() on the Console with two decimal places after the decimal point.
           var sb = new StringBuilder();
           sb.AppendLine($"Booking number: {BookingNumber}");
           sb.AppendLine($"Room type: {Room.GetType().Name}");
           sb.AppendLine($"Adults: {AdultsCount} Children: {ChildrenCount}");
           sb.AppendLine($"Total amount paid: {TotalPaid():F2} $");
           return sb.ToString().TrimEnd();

        }

        private double TotalPaid() => Math.Round(this.residenceDuration * Room.PricePerNight, 2);
    }
}
