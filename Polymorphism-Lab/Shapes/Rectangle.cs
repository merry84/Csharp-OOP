using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Rectangle : Shape
    {
        //o	height and width for Rectangle
        private double height;
        private double width;

        public Rectangle(double height, double width)
        {
            Height = height;
            Width = width;
        }

        public double Height { get; private set; }
        public double Width { get; private set; }

        public override double CalculateArea()=>Height*Width;
        

        public override double CalculatePerimeter()=> 2*Width + 2* Height;
        public override string Draw()=> base.Draw()+ nameof(Rectangle);
        

    }
}
