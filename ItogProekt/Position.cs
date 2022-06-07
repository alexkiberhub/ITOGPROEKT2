using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ItogProekt
{
    public class Position
    {
        PointF position1 = new PointF();
        PointF position2 = new PointF();
        public Position()
        {
          
            position1 = new PointF(350, 400);// первая точка
            position2 = new PointF(1000, 400);// вторая точка
        }
    }
}
