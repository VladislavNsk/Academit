using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public abstract class Shape : IShape
    {
        public virtual double GetArea()
        {
            return 0;
        }

        public virtual double GetHeight()
        {
            return 0;
        }

        public virtual double GetPerimeter()
        {
            return 0;
        }

        public virtual double GetWidth()
        {
            return 0;
        }
    }
}
