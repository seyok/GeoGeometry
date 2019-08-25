using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWM.GeoGeometry
{
    public class sg_line
    {
        sg_Vector3 _pt1;
        sg_Vector3 _pt2;
        bool _needCal;
        SGBox3D _box;
        double _l;

        public sg_Vector3 StartPoint
        {
            get
            {
                return _pt1;
            }
        }
        public sg_Vector3 EndPoint
        {
            get
            {
                return _pt2;
            }
        }
        public SGBox3D Box
        {
            get
            {
                return new SGBox3D(_box);
            }
        }
        public sg_line(sg_Vector3 pt1, sg_Vector3 pt2)
        {
            _pt1 = pt1;
            _pt2 = pt2;
            _box = new SGBox3D(_pt1, _pt2);
            _needCal = true;
        }

        public bool IsInclude(sg_Vector3 pt, out double pathlength)
        {
            pathlength = 0;
            if (!(_box.contain(pt)))
            {
                return false;
            }

            double l1 = sg_math.getDist(_pt1, pt);
            double l2 = sg_math.getDist(pt, _pt2);

            double d = getDistToPoint(l1, l2);

            if (sg_math.isZero(d))
            {
                if ((_l > l1 && _l > l2) ||
                    (sg_math.isEquel(l1, _l) && sg_math.isEquel(0.0, l2)) ||
                    (sg_math.isEquel(l2, _l) && sg_math.isEquel(0.0, l1)))
                {
                    pathlength = l1;
                    return true;
                }
            }
            return false;
        }

        private void _cal_l()
        {
            _l = sg_math.getDist(_pt1, _pt2);
            _needCal = false;
        }

        public double getLength()
        {
            if (_needCal)
            {
                _cal_l();
            }
            return _l;
        }

        public bool IsBeforePt1(sg_Vector3 pt, out double length)
        {
            length = 0;
            if ((_box.contain(pt)))
            {
                return false;
            }
            if (_needCal)
            {
                _cal_l();
            }
            double l1 = sg_math.getDist(_pt1, pt);
            double l2 = sg_math.getDist(pt, _pt2);
            double d = getDistToPoint(l1, l2);

            if (sg_math.isZero(d))
            {
                if (l2 > _l && l2 > l1)
                {
                    length = l1;
                    return true;
                }
            }
            return false;
        }

        public bool IsAfterPt2(sg_Vector3 pt, out double length)
        {
            length = 0;
            if ((_box.contain(pt)))
            {
                return false;
            }
            if (_needCal)
            {
                _cal_l();
            }
            double l1 = sg_math.getDist(_pt1, pt);
            double l2 = sg_math.getDist(pt, _pt2);

            double d = getDistToPoint(l1, l2);

            if (sg_math.isZero(d))
            {
                if (l1 > _l && l1 > l2)
                {
                    length = l2;
                    return true;
                }
            }
            return false;
        }

        public sg_Vector3 getPoint(double length)
        {
            sg_Vector3 pt = new sg_Vector3(-999999, -999999, -999999);
            if (sg_math.isZero(length))
            {
                return _pt1;
            }
            double l = getLength();
            if (sg_math.isEquel(length, l))
            {
                return _pt2;
            }
            if (sg_math.isZero(l))
            {
                return pt;
            }
            double lanbda = (l - length) / length;
            return getLanbudaPoint(lanbda);
        }

        private sg_Vector3 getLanbudaPoint(double l)
        {
            double x = getPointCoord(l, _pt2.x, _pt1.x);
            double y = getPointCoord(l, _pt2.y, _pt1.y);
            double z = getPointCoord(l, _pt2.z, _pt1.z);

            return new sg_Vector3(x, y, z);
        }

        private double getPointCoord(double l, double x1, double x2)
        {
            return (l * x2 + x1) / (l + 1);
        }

        private double getDistTo(sg_Vector3 pt)
        {
            double l1 = sg_math.getDist(_pt1, pt);
            double l2 = sg_math.getDist(pt, _pt2);

            return getDistToPoint(l1, l2);
        }

        public double getDistToPoint(double l1, double l2)
        {
            double l = getLength();
            // 海伦公式
            double p = (l1 + l2 + l) * 0.5;
            double ss = p * (p - l1) * (p - l2) * (p - l);
            if (sg_math.isZero(ss))
            {
                return 0.0;
            }
            double s = Math.Sqrt(ss);
            return 2 * s / l;
        }

        public bool IsOnLine(sg_Vector3 pt)
        {
            double d = getDistTo(pt);
            return sg_math.isZero(d);
        }

        public sg_Vector3 getPointFormX(double x)
        {
            sg_Vector3 ret_pt = new sg_Vector3(-999999, -999999, -999999);
            if (sg_math.isEquel(_pt1.x, _pt2.x))
            {
                return ret_pt;
            }
            if (sg_math.isEquel(_pt1.x, x))
            {
                return _pt1;
            }
            if (sg_math.isEquel(_pt2.x, x))
            {
                return _pt2;
            }
            double t = (x - _pt1.x) / (_pt2.x - _pt1.x);

            return new sg_Vector3(x, _pt1.y + t * (_pt2.y - _pt1.y), _pt1.z + t * (_pt2.z - _pt1.z));
        }
    }
}
