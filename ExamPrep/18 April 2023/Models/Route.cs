using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Models
{
    public class Route :IRoute
    {
        private string startPoints;
        private string endPoints;
        private double length;
        private int routeId;
        private bool isLocked;

        public Route(string startPoint, string endPoint, double length, int routeId)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Length = length;
            this.routeId = routeId;
            isLocked = false;

        }
        public string StartPoint
        {
            get =>startPoints;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.StartPointNull);
                }
                startPoints = value;
            }
        }

        public string EndPoint {
            get => endPoints;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.EndPointNull);
                }
                endPoints = value;
            }
        }
        public double Length {
            get => length;
            private set
            {
                if (value<1)
                {
                    throw new ArgumentException(ExceptionMessages.RouteLengthLessThanOne);
                }
                length = value;
            }
        }

        public int RouteId => routeId;
        
        public bool IsLocked => isLocked;
        public void LockRoute()
        {
            isLocked =true;
        }
    }
}
