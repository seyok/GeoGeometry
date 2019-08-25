using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWM.GeoGeometry
{
    public class sg_math
    {
        public static bool isInOpeninterval(double x,double limit1,double limit2)
        {
            return ((x>limit1&&x<limit2)||(x<limit1&&x>limit2));
        }

        public static bool isInCloseinterval(double x,double limit1,double limit2)
        {
            return (isInOpeninterval(x,limit1,limit2)||isEquel(x,limit1)||isEquel(x,limit2));
        }
        
        public static bool isEquel(double x1,double x2)
        {
            return (Math.Abs(x1-x2)<ZERO);
        }
        
        public static bool isZero(double x)
        {
            return isEquel(x,0.0);
        }
	
        public static double getDist(sg_Vector3 pt1,sg_Vector3 pt2)
        {
            double dx = pt1.x - pt2.x;
            double dy = pt1.y - pt2.y;
            double dz = pt1.z - pt2.z;

            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
        
        public static double ArcToAngle(double arc)
        {
            return arc * 180 / Math.PI;
        }

        public static double AngleToArc(double angle)
        {
            return angle * Math.PI/ 180;
        }

    const double ZERO = 5e-3;


    }
}
