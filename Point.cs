using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWM.GeoGeometry
{
    public class Point2D
    {
        public double x, y;
        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class Point3D:Point2D
    {
        public double z;
        public Point3D(double x,double y,double z):base(x,y)
        {
            this.z = z;
        }
    }
}
