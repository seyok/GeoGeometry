using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWM.GeoGeometry
{
    public class sg_Transformation
    {
        double[,] _r = new double[3,3];
        double[] _t = new double[3];
         public void unit()
         {
             _r[0,0] = 1; _r[0,1] = 0; _r[0,2] = 0;
             _r[1,0] = 0; _r[1,1] = 1; _r[1,2] = 0;
             _r[2,0] = 0; _r[2,1] = 0; _r[2,2] = 1;
             _t[0] = 0; _t[1] = 0; _t[2] = 0;
         }
        public sg_Transformation()
        {
            unit();
        }
 
//  	public sg_Transformation(sg_Vector3 v0,  sg_Vector3 v1,  sg_Vector3 v2)
//  	{
//  		sg_Vector3 W1 = v1 - v0;
//  		sg_Vector3 W3 = W1.crossMul(v2 - v0);
//  		sg_Vector3 W2 = W3.crossMul(W1);
//  
//  		W1.Normalize();
//  		W2.Normalize();
//  		W3.Normalize();
//  
//  		_r[0,0] = W1.x; _r[0,1] = W2.x; _r[0,2] = W3.x;
//  		_r[1,0] = W1.y; _r[1,1] = W2.y; _r[1,2] = W3.y;
//  		_r[2,0] = W1.z; _r[2,1] = W2.z; _r[2,2] = W3.z;
//  
//  		_t[0] = v0.x; _t[1] = v0.y; _t[2] = v0.z;
//  		
//  	}
//  
 	public sg_Transformation(  sg_Vector3 n,  sg_Vector3 cenPt )
 	{
 		sg_Vector3 newZ = new sg_Vector3(n);	
 		sg_Vector3 newX = new sg_Vector3(90, n.getDip() - 90);
 		sg_Vector3 newY = newZ.crossMul(newX);
 
 		newX.Normalize();
 		newY.Normalize();
 		newZ.Normalize();
 
 		_r[0,0] = newX.x; _r[0,1] = newY.x; _r[0,2] = newZ.x;
 		_r[1,0] = newX.y; _r[1,1] = newY.y; _r[1,2] = newZ.y;
 		_r[2,0] = newX.z; _r[2,1] = newY.z; _r[2,2] = newZ.z;
 
 		_t[0] = cenPt.x; _t[1] = cenPt.y; _t[2] = cenPt.z;
 	}
 
 	public sg_Transformation( sg_Vector3 n1,  sg_Vector3 n2,  sg_Vector3 cenPt)
 	{
 		sg_Vector3 newZ = new sg_Vector3(n1);
 		sg_Vector3 newY = newZ.crossMul(n2);
 		sg_Vector3 newX = newY.crossMul(newZ);
 
 		newX.Normalize();
 		newY.Normalize();
 		newZ.Normalize();
 
 		_r[0,0] = newX.x; _r[0,1] = newY.x; _r[0,2] = newZ.x;
 		_r[1,0] = newX.y; _r[1,1] = newY.y; _r[1,2] = newZ.y;
 		_r[2,0] = newX.z; _r[2,1] = newY.z; _r[2,2] = newZ.z;
 
 		_t[0] = cenPt.x; _t[1] = cenPt.y; _t[2] = cenPt.z;
 
 
 	}
 
 	public sg_Vector3 apply( sg_Vector3 v) 
 	{
 		double[] input = { v.x, v.y, v.z };
 		double[] output = { 0, 0, 0 };
 		for (int m = 0; m < 3; m++)
 			for (int n = 0; n < 3; n++)
 				output[m] += (double)(input[n] * _r[m,n]);
 		return new sg_Vector3(output[0]+ _t[0], output[1] +_t[1], output[2] +_t[2]);
 	}
 
 	public sg_Vector3 inverse( sg_Vector3 v)
 	{
 		double[] input = { v.x, v.y, v.z };
 		double[] output = { 0, 0, 0 };
 
 		input[0] = input[0] - _t[0];
 		input[1] = input[1] - _t[1];
 		input[2] = input[2] - _t[2];
 		
 
 		for (int m = 0; m < 3; m++)
 			for (int n = 0; n < 3; n++)
 				output[m] += (double)(input[n] * _r[n,m]);
 	
 		return new sg_Vector3(output[0], output[1], output[2]);
 	}

    }
}
