using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWM.GeoGeometry
{
    public class SGBox3D
    {
        sg_Vector3 _pt1, _pt2;
        public bool contain(sg_Vector3 pt)
        {
            return (sg_math.isInCloseinterval(pt.x, _pt1.x, _pt2.x) &&
                sg_math.isInCloseinterval(pt.y, _pt1.y, _pt2.y) &&
                sg_math.isInCloseinterval(pt.z, _pt1.z, _pt2.z));
        }

        public SGBox3D(sg_Vector3 pt1, sg_Vector3 pt2)
        {
            _pt1 = pt1;
            _pt2 = pt2;
        }
        public SGBox3D(SGBox3D box)
        {
            _pt1 = box._pt1;
            _pt2 = box._pt2;
        }
        public SGBox3D()
        {
            _pt1 = new sg_Vector3(0, 0, 0);
            _pt2 = new sg_Vector3(0, 0, 0);
        }

        public static SGBox3D operator +(SGBox3D box1, SGBox3D box2)
        {
            double minx = getmin(box1._pt1.x, box1._pt2.x, box2._pt1.x, box2._pt2.x);
            double maxx = getmax(box1._pt1.x, box1._pt2.x, box2._pt1.x, box2._pt2.x);

            double miny = getmin(box1._pt1.y, box1._pt2.y, box2._pt1.y, box2._pt2.y);
            double maxy = getmax(box1._pt1.y, box1._pt2.y, box2._pt1.y, box2._pt2.y);

            double minz = getmin(box1._pt1.z, box1._pt2.z, box2._pt1.z, box2._pt2.z);
            double maxz = getmax(box1._pt1.z, box1._pt2.z, box2._pt1.z, box2._pt2.z);

            sg_Vector3 pt1 = new sg_Vector3(minx, miny, minz);
            sg_Vector3 pt2 = new sg_Vector3(maxx, maxy, maxz);

            return new SGBox3D(pt1, pt2);
        }

        private static double getmin(double x1, double x2, double x3, double x4)
        {
            double minx = x1;
            if (minx > x2)
            {
                minx = x2;
            }
            if (minx > x3)
            {
                minx = x3;
            }
            if (minx > x4)
            {
                minx = x4;
            }
            return minx;
        }

        private static double getmax(double x1, double x2, double x3, double x4)
        {
            double maxx = x1;
            if (maxx < x2)
            {
                maxx = x2;
            }
            if (maxx < x3)
            {
                maxx = x3;
            }
            if (maxx < x4)
            {
                maxx = x4;
            }
            return maxx;
        }
    }
}
