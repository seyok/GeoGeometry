using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWM.GeoGeometry
{
    public class sg_plane
    {
        public enum PlaneState
        {
            Horizon,
            Vertical,
            UnKnow
        };

        public double Width
        {
            get;
            private set;
        }
        public double Height
        {
            get;
            private set;
        }
        sg_Vector3 certenPt;
        sg_Vector3 _v;
        PlaneState __state;

        public sg_plane(double angle,double dip)
        {
            certenPt = new sg_Vector3(0,0,0);
            
            Width = -1;
            Height = -1;
            
            if (sg_math.isZero(angle))
            {
                __state = PlaneState.Horizon;
            }
            if (sg_math.isEquel(angle,90))
            {
                __state = PlaneState.Vertical;
            }
            
            _v = new sg_Vector3(angle,dip);
 	}
 
 	public sg_plane( sg_Vector3 v)
 	{
        certenPt = new sg_Vector3(0, 0, 0);
 
 		Width = -1;
 		Height = -1;

        __state = PlaneState.UnKnow;
 
 		_v = new sg_Vector3(v);
 	}
 
 	public sg_plane( sg_Vector3 pt1,  sg_Vector3 pt2,  sg_Vector3 pt3)
 	{
 		sg_Vector3 v = sg_Vector3.getNormal(pt1, pt2, pt3);
 		_v = new sg_Vector3(v);
 
 		double dip = v.getDip();
 		double aaa = v.getAngle();

        certenPt = new sg_Vector3( (pt1.x + pt3.x) / 2.0,(pt1.y + pt3.y) / 2.0,(pt1.z + pt3.z) / 2.0);
 
 		Width = sg_math.getDist(pt1, pt2);
 		Height = sg_math.getDist(pt2, pt3);
 	}
 
 	public void sizeit(sg_Vector3 pt, double width, double height)
 	{
 		certenPt = pt;
 		Width = width;
 		Height = height;
 	}
 
 	public sg_Vector3 get3DPoint(sg_Vector3 pt)
 	{
 		return get3DPoint(pt.x,pt.y,0);
 	}
// 
// 	POINT3D sg_plane::get3DPoint(POINT3D pt) const
// 	{
// 		return get3DPoint(pt.x, pt.y, pt.z);
// 	}
 
 	public sg_Vector3 get3DPoint(double x, double y, double z) 
 	{
 		sg_Vector3 v= new sg_Vector3(x, y, z);
 		sg_Transformation m = new sg_Transformation(_v, certenPt);
 		sg_Vector3 newv = m.apply(v);
 		return new sg_Vector3( newv.x, newv.y, newv.z);
 	}

      public bool isSized() 
      {
          return (Width>0&&Height>0);
      }
 
 	public void Move(sg_Vector3 v)
 	{
 		if (!isSized())
 		{
 			return;
 		}
 		sg_Vector3 pt1 =new sg_Vector3(v.x,v.y,v.z);
 		sg_Vector3 pt2 = get3DPoint(pt1);
 		certenPt = pt2;
 	}
 
 	public bool get2DPoint(sg_Vector3 pt, out sg_Vector3 retPt)
 	{
        sg_Vector3 v = new sg_Vector3(pt.x, pt.y, pt.z);
        retPt = new sg_Vector3(0, 0, 0);
 		sg_Transformation m = new sg_Transformation(_v, certenPt);
 		sg_Vector3 newv = m.inverse(v);
 		if (!sg_math.isZero(newv.z))
 		{
 			return false;
 		}
        retPt = new sg_Vector3(newv);
 		return true;
 	}
 
 	public sg_Vector3 getPt1()
 	{
 		sg_Vector3 pt = new sg_Vector3(-0.5*Width, 0.5*Height,0);
 		return get3DPoint(pt);
 	}
 
 	public sg_Vector3 getPt2()
 	{
 		sg_Vector3 pt = new sg_Vector3( 0.5*Width, 0.5*Height,0);
 		return get3DPoint(pt);
 	}
 
 	public sg_Vector3 getPt3() 
 	{
 		sg_Vector3 pt =new sg_Vector3( 0.5*Width, -0.5*Height,0);
 		return get3DPoint(pt);
 	}
 
 	public sg_Vector3 getPt4() 
 	{
 		sg_Vector3 pt =new sg_Vector3(-0.5*Width, -0.5*Height,0);
 		return get3DPoint(pt);
 	}
 

 
 	public bool isVertical()
 	{
 		if (__state == PlaneState.UnKnow)
 		{
 			cal_state();
 		}
        return (__state == PlaneState.Vertical);
 	}
 
 	private void cal_state()
 	{
 		if (_v.isParallel(new sg_Vector3(0,0,1)))
 		{
            __state = PlaneState.Vertical;
 		}
 		if (_v.isVertical(new sg_Vector3(0, 0, 1)))
 		{
            __state = PlaneState.Horizon;
 		}
 	}
 
 	public bool isHorizontal()
 	{
 		if (__state == PlaneState.UnKnow)
 		{
 			cal_state();
 		}
 		return (__state == PlaneState.Horizon);
 	}
 
 	public double getDist(sg_Vector3 pt)
 	{
 		sg_Vector3 v = new sg_Vector3(pt);
 		sg_Transformation m = new sg_Transformation(_v, certenPt);
 		sg_Vector3 newv = m.inverse(v);
 		return Math.Abs(newv.z);
 	}
// 
// 	void sg_plane::setDip(SG_numValue d)
// 	{
// 		_state = NoInit;
// 	}
// 
// 	void sg_plane::setAngle(SG_numValue a)
// 	{
// 		_state = NoInit;
// 	}
// 
 	public void setNormal( sg_Vector3 v)
 	{
 		_v = new sg_Vector3(v);
 	}
// 
// 	SG_numValue getDip()
// 	{
// 		return 0.0;
// 	}
// 
// 	SG_numValue getAngle()
// 	{
// 		return 0.0;
// 	}
    }
}
