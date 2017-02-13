/*
 * 2014 03 10 Lu Chengwei VB4A
 * Code From:http://blog.csdn.net/jia20003/article/details/7990681
 */

package com.google.devtools.simple.runtime;

import com.google.devtools.simple.runtime.annotations.SimpleFunction;
import com.google.devtools.simple.runtime.annotations.SimpleObject;
import com.google.devtools.simple.runtime.variants.Variant;

import java.lang.reflect.Array;
import java.util.HashSet;
import java.util.Set;

/**
 * Implementation of various array related runtime functions.
 * 
 * @author Herbert Czymontek
 */
@SimpleObject
public final class Matrix {

  private Matrix() {  // COV_NF_LINE
  }                   // COV_NF_LINE

 	public final static int OPERATION_ADD = 1;
	public final static int OPERATION_SUB = 2;
	public final static int OPERATION_MUL = 4;
  
 	public static boolean legalOperation(double[][] a, double[][] b, int type) {
		boolean legal = true;
		if(type == OPERATION_ADD || type == OPERATION_SUB)
		{
			if(a.length != b.length || a[0].length != b[0].length) {
				legal = false;
			}
		} 
		else if(type == OPERATION_MUL)
		{
			if(a[0].length != b.length) {
				legal = false;
			}
		}
		return legal;
	}
	
  @SimpleFunction
	public static double[][] Add(double[][] matrixa, double[][] matrixb) {
		double[][] tma = new double[matrixa.length][matrixb[0].length];
		if(legalOperation(matrixa, matrixb, OPERATION_ADD)) {
			for(int i=0; i<matrixa.length; i++) {
				for(int j=0; j<matrixa[0].length; j++) {
					tma[i][j] = matrixa[i][j] + matrixb[i][j];
				}
			}
		}
		return tma;
	}
	
  @SimpleFunction
	public static double[][] Subtract(double[][] matrixa, double[][] matrixb) {
		double[][] tma = new double[matrixa.length][matrixb[0].length];
		if(legalOperation(matrixa, matrixb, OPERATION_SUB)) {
			for(int i=0; i<matrixa.length; i++) {
				for(int j=0; j<matrixa[0].length; j++) {
					tma[i][j] = matrixa[i][j] - matrixb[i][j];
				}
			}
		}
		return tma;
	}
	
  //乘法使用代码：http://www.jb51.net/article/35158.htm
  @SimpleFunction
  public static double[][] Multiply(double[][] a, double[][] b) {
        double [][] result = new double[a.length][b[0].length];
        for (int i = 0; i<a.length; i++) {
            for (int j = 0; j<b[0].length; j++) {
                for (int k = 0; k<a[0].length; k++) {
                    result[i][j]= result[i][j]+a[i][k]*b[k][j];
                }
            }
        }
        return result; 
  }
	
  @SimpleFunction
	public static double[][] ScalarMultiply(double[][] matrixa, double b) {
		double[][] tma = new double[matrixa.length][matrixa[0].length];
		for(int i=0; i<matrixa.length; i++) {
			for(int j=0; j<matrixa[0].length; j++) {
				tma[i][j] = matrixa[i][j] * b;
			}
		}
		return tma;
	}
	
  //以下代码参考http://wlh0706-163-com.iteye.com/blog/1224208
  @SimpleFunction
     public static double[][] GetComp(double[][] data, int h, int v) {  
        //代数余子式
		int H = data.length;  
        int V = data[0].length;  
        double[][] newData = new double[H - 1][V - 1];  
  
        for (int i = 0; i < newData.length; i++) {  
  
            if (i < h - 1) {  
                for (int j = 0; j < newData[i].length; j++) {  
                    if (j < v - 1) {  
                        newData[i][j] = data[i][j];  
                    } else {  
                        newData[i][j] = data[i][j + 1];  
                    }  
                }  
            } else {  
                for (int j = 0; j < newData[i].length; j++) {  
                    if (j < v - 1) {  
                        newData[i][j] = data[i + 1][j];  
                    } else {  
                        newData[i][j] = data[i + 1][j + 1];  
                    }  
                }  
  
            }  
        }  
        return newData;  
    }  

  @SimpleFunction
  public static double GetDet(double[][] data) {  
            if (data.length == 2) {  
                return data[0][0] * data[1][1] - data[0][1] * data[1][0];  
            }  
      
            double total = 0;  
            // 根据data 得到行列式的行数和列数  
            int num = data.length;  
            // 创建一个大小为num 的数组存放对应的展开行中元素求的的值  
            double[] nums = new double[num];  
      
            for (int i = 0; i < num; i++) {  
                if (i % 2 == 0) {  
                    nums[i] = data[0][i] * GetDet(GetComp(data, 1, i + 1));  
                } else {  
                    nums[i] = -data[0][i] * GetDet(GetComp(data, 1, i + 1));  
                }  
            }  
            for (int i = 0; i < num; i++) {  
                total += nums[i];  
            }  
            System.out.println("total=" + total);  
            return total;  
        }  
	
	
  @SimpleFunction
     public static double[][] Inverse(double[][] data) {  
        double A = GetDet(data);  
        double[][] newData = new double[data.length][data.length];  
        for (int i = 0; i < data.length; i++) {  
            for (int j = 0; j < data.length; j++) {  
                double num;  
                if ((i + j) % 2 == 0) {  
                    num = GetDet(GetComp(data, i + 1, j + 1));  
                } else {  
                    num = -GetDet(GetComp(data, i + 1, j + 1));  
                }  
  
                newData[i][j] = num / A;  
            }  
        }  
        newData = Transpose(newData);  
        return newData;  
    }  
	
  @SimpleFunction
  public static double[][] Transpose(double[][] matrixa) {
	double[][] tma = new double[matrixa[0].length][matrixa.length];
		for(int i=0; i<matrixa.length; i++) {
			for(int j=0; j<matrixa[0].length; j++) {
				tma[j][i] = matrixa[i][j];
			}
		}
		return tma;
  }

	
  @SimpleFunction
  public static double[][] AsMatrix(String inma) {
	String[] lines=inma.split("\\Q" + ";" + "\\E");
	String[] cols;
	double[][] tMa;
	int dx=lines.length;
	String[] tc=lines[0].split("\\Q" + "," + "\\E");
	int dy=tc.length;
	tMa=new double[dx][dy];
	for(int i=0;i<lines.length;i++){
		cols=lines[i].split("\\Q" + "," + "\\E");
		for(int j=0;j<cols.length;j++){
			tMa[i][j]=Double.parseDouble(cols[j]);
		}
	}
	return tMa;
  }
  


}
