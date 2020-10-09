using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    class AreaComparer : IComparer<Shape>
    {
        public int Compare(Shape shape1, Shape shape2)
        {
            if (shape1.GetArea() > shape2.GetArea())
            {
                return -1;
            }

            if (shape1.GetArea() < shape2.GetArea())
            {
                return 1;
            }

            return 0;
        }
    }
}
