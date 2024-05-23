using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Repositories.Contracts;

namespace BookingApp.Repositories
{
    public class BookingRepository :IRepository<IBooking>
    {
        private List<IBooking> books;

      
        public BookingRepository()
        {
            books = new List<IBooking>();
        }
        public void AddNew(IBooking model)
        {
           books.Add(model);
        }
        //•	Returns a booking which has the given bookingNumber or returns a default value.
        public IBooking Select(string criteria)
            => books.FirstOrDefault(x => x.BookingNumber.ToString() == criteria);

        public IReadOnlyCollection<IBooking> All()
            => books;
    }
}
