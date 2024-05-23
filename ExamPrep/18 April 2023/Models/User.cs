using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Models
{
    public class User :IUser
    {
        private string firstName;
        private string lastName;
        private double rating;
        private string drivingLicenseNumber;
        private bool isBlocked;
        public User(string firstName,string lastName,string drivingLicenseNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            this.rating = 0;
            DrivingLicenseNumber = drivingLicenseNumber;
            this.isBlocked = false;
        }

        public string FirstName
        {
            get => firstName;
            private set
            {
                if(string.IsNullOrWhiteSpace(value)) 
                throw new ArgumentException(string.Format(ExceptionMessages.FirstNameNull));

                firstName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.LastNameNull));

                lastName = value;
            }
        }

        public double Rating => this.rating;

        public string DrivingLicenseNumber
        {
            get => drivingLicenseNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.DrivingLicenseRequired));
                }
                drivingLicenseNumber = value;
            }
        }
        public bool IsBlocked => isBlocked;
        public void IncreaseRating()
        {
            if (rating < 10)
            {
                rating += 0.50;
            }
        }

        public void DecreaseRating()
        {
            if (rating < 2)
            {
                isBlocked =true;
                rating = 0;
            }
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} Driving license: {drivingLicenseNumber} Rating: {rating}";
        }
    }
}
