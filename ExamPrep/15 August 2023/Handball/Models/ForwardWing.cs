using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class ForwardWing : Player
    {
        private const double rating = 5.5;
        private const double increaseIndex = 1.25;
        private const double decreaseIndex = 0.75;
        public ForwardWing(string name) : base(name, rating)
        {
        }

        public override void IncreaseRating()
        {
            base.Rating += increaseIndex;
            if (base.Rating > 10) base.Rating = 10;
        }

        public override void DecreaseRating()
        {
            base.Rating -= decreaseIndex;
            if (base.Rating < 1) base.Rating = 1;
        }
    }
}
