using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SWM.GeoGeometry
{
    public class sg_Vector3
    {
        public double x
        {
            get;
            private set;
        }

        public double y
        {
            get;
            private set;
        }

        public double z
        {
            get;
            private set;
        }

        public double length
        {
            get
            {
                sg_Vector3 pt = new sg_Vector3(0, 0, 0);
                return sg_math.getDist(this, pt);
            }
        }

        public sg_Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public sg_Vector3(Point3D pt)
            : this(pt.x, pt.y, pt.z)
        {

        }

        public sg_Vector3(sg_Vector3 v)
            : this(v.x, v.y, v.z)
        {

        }

        public sg_Vector3(double angle, double dip)
        {
            sg_Vector3 v = getNormal(angle, dip);
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }

        public bool isZero()
        {
            return sg_math.isZero(this.x) && sg_math.isZero(this.y) &&
                sg_math.isZero(this.z);
        }

        public void Normalize()
        {
            if (isZero())
            {
                return;
            }
            double l = length;
            //             x /= l; y /= l; y /= l;
            x /= l; y /= l; z /= l;
        }
        public static sg_Vector3 operator +(sg_Vector3 p1, sg_Vector3 p)
        {
            return new sg_Vector3(p1.x + p.x,
                p1.y + p.y,
                p1.z + p.z);
        }

        //          	public sg_Vector3 operator *(double v) 
        //          	{
        //          		return new sg_Vector3(this.x*v,
        //          			this.y*v,
        //          			this.z*v);
        //          	}

        public static sg_Vector3 operator -(sg_Vector3 p1, sg_Vector3 p)
        {
            return new sg_Vector3(p1.x - p.x,
                p1.y - p.y,
                p1.z - p.z);
        }

        public sg_Vector3 crossMul(sg_Vector3 v)
        {
            double x = ((this.y * v.z) - (this.z * v.y));
            double y = ((this.z * v.x) - (this.x * v.z));
            double z = ((this.x * v.y) - (this.y * v.x));
            return new sg_Vector3(x, y, z);
        }

        public double dotMul(sg_Vector3 v)
        {
            return (this.x * v.x + this.y * v.y + this.z * v.z);
        }

        public bool isParallel(sg_Vector3 v)
        {
            if (isZero() || v.isZero())
            {
                return false;
            }
            sg_Vector3 newv = this.crossMul(v);
            return newv.isZero();
        }
        public bool isVertical(sg_Vector3 v)
        {
            if (isZero() || v.isZero())
            {
                return false;
            }
            return (dotMul(v) == 0);
        }

        public double getInterAngle(sg_Vector3 v)
        {
            double a = dotMul(v) / (length * v.length);
            return sg_math.ArcToAngle(Math.Acos(a));
        }

        public sg_Vector3 getNormal(double angle, double dip)
        {
            double Degreeangle = sg_math.AngleToArc(angle);
            double DegreedDip = sg_math.AngleToArc(dip);
            double d = Math.Sin(Degreeangle);
            return new sg_Vector3(d * Math.Sin(DegreedDip), d * Math.Cos(DegreedDip), Math.Cos(Degreeangle));
        }

        public static double getAngle(sg_Vector3 v)
        {
            if (v.isZero())
            {
                return -99999;
            }
            return sg_math.ArcToAngle(Math.Acos(v.z / v.length));
        }

        public static double getDip(sg_Vector3 v)
        {
            double a = getAngle(v);
            double l = v.length * Math.Sin(sg_math.AngleToArc(a));
            if (sg_math.isZero(l))
            {
                return 0.0;
            }
            double opp = v.x / l;
            if (opp > 1.0 || opp < -1.0)
            {
                opp = (int)(opp);
            }
            return sg_math.ArcToAngle(Math.Asin(opp));
        }

        public double getAngle()
        {
            return getAngle(this);
        }

        public double getDip()
        {
            return getDip(this);
        }

        public bool is_Viald()
        {
            sg_Vector3 v = new sg_Vector3(-999999,-999999,-999999);
            double l = sg_math.getDist(this, v);
            return sg_math.isZero(l);        
        }

        public static sg_Vector3 getNormal(sg_Vector3 p1, sg_Vector3 p2, sg_Vector3 p3)
        {
            sg_Vector3 v = p3 - p1;
            sg_Vector3 ret = v.crossMul(p2 - p1);
            ret.Normalize();
            return ret;
        }

    }
}
