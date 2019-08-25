using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWM.GeoGeometry
{
    public class sg_polyline
    {
        List<sg_line> _lines = new List<sg_line>();
        SGBox3D _box;
        sg_Vector3 _lastpt;
        double _l;
        public sg_polyline(sg_Vector3 pt1,sg_Vector3 pt2)
         	{
                _lines.Clear();
         		_box = new SGBox3D(pt1,pt1);
         		_lastpt = pt1;
         		_l= -1;
         		appendPoint(pt2);
         	}
         
         	public void appendPoint(sg_Vector3 pt)
         	{
         		sg_line line = new sg_line(_lastpt,pt);
         		_lines.Add(line);
         		_lastpt = pt;
         		_box = _box + line.Box;
         	}
         
         
         
         	public bool IsInclude(sg_Vector3 pt,out double pathlength)
         	{
         		pathlength = 0;
         		if (!_box.contain(pt))
         		{
         			return false;
         		}
          		double l = 0;


                for (int i = 0; i < _lines.Count;i++ )
                {
                    sg_line line = _lines[i];
                    if (line.IsInclude(pt,out l))
                    {
                        pathlength = pathlength + l;
                        return true;
                    }
                    else
                    {
                        pathlength = pathlength + line.getLength();
                    }
                }  
         		return false;
         	}
         
         	public bool IsBeforePt1(sg_Vector3 pt,out double length)
         	{
                length = 0;
         		if (_lines.Count == 0)
         		{
         			return false;
         		}
         		sg_line line = _lines[0];
         		return line.IsBeforePt1(pt,out length);
         	}
         
         public	bool IsAfterLastPt(sg_Vector3 pt,out double length)
         	{
                length = 0;
         		if (_lines.Count == 0)
         		{
         			return false;
         		}
         		sg_line line  = _lines[_lines.Count-1];
         		return line.IsAfterPt2(pt,out length);
         	}
         
         	public double getLength()
         	{
         		if (_l>0)
         		{
         			return _l;
         		}
         		_l = 0.0;
                for (int i = 0; i < _lines.Count;i++ )
                {
                    sg_line line = _lines[i];
                    _l += line.getLength();
                }
         		return _l;
         	}
         
         	public double getLength(int index)
         	{
         		int maxindex = (int)_lines.Count-1;
         		if (index <= 0)
         		{
         			return 0;
         		}
         		if (index>maxindex)
         		{
         			return getLength();
         		}
         		double length = 0.0;
         		for (int i = 1; i <= index;i++)
         		{
         			sg_line line = _lines[i-1];
         			length += line.getLength();
         		}
         		return length;
         	}
         
         	public int getVerCount()
         	{
         		return _lines.Count+1;
         	}
         
         	public sg_Vector3 getPointAt(int index)
         	{
         		sg_Vector3 pt=new sg_Vector3(-999999,-999999,-999999);
         		if (index == 0)
         		{
         			return _lines[0].StartPoint;
         		}
         		if (index <= (_lines.Count))
         		{
         			return _lines[index-1].EndPoint;
         		}
         		return pt;
         	}
         
         	public sg_Vector3 getPoint(double length)
         	{
         		if (sg_math.isZero(length))
         		{
                    return _lines[0].StartPoint;
         		}
         		double l = getLength();
         		if (sg_math.isEquel(length,l))
         		{
         			return _lastpt;
         		}
         		double newlth = 0.0;
         		sg_line line = findLine(length,out newlth);
         		return line.getPoint(newlth);		
         	}
         
         	private sg_line findLine(double length, out double newlength)
         	{
                newlength = 0;
         		if (length <0)
         		{
         			newlength = length;
         			return _lines[0];
         		}
         		double l = getLength();
         		if (length >l)
         		{
         			sg_line line = _lines[_lines.Count -1];
         			newlength = line.getLength() + length - l;
         			return line;
         		}
         		double leng1 = 0.0;
         		double leng2 = 0.0;
                for (int i = 0; i < _lines.Count;i++ )
                {
                    sg_line line = _lines[i];
                    leng2 = leng1 + line.getLength();
                    if (length < leng2)
                    {
                        newlength = length - leng1;
                        return line;
                    }
                    leng1 = leng2;
                }
         		newlength = 99999999;
         		return null;
         	}
    }
    
}
