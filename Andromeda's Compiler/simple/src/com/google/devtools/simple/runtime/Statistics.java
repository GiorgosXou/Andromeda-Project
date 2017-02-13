/*
	VB4A统计学函数库，LuChengwei 2014 03 12
	除参考资料外，感谢Dengfeng Zhang的帮助。
 */

package com.google.devtools.simple.runtime;

import com.google.devtools.simple.runtime.annotations.SimpleDataElement;
import com.google.devtools.simple.runtime.annotations.SimpleFunction;
import com.google.devtools.simple.runtime.annotations.SimpleObject;
import com.google.devtools.simple.runtime.variants.IntegerVariant;
import com.google.devtools.simple.runtime.variants.Variant;
import java.util.Random;
import java.security.SecureRandom;
/**
	参考：http://blog.csdn.net/derpvailzhangfan/article/details/1880072
	          "最大值："+GetMax(testData)
              "最小值："+GetMin(testData)
              "计数："+GetCount(testData)
              "求和："+GetSum(testData)
              "求平均："+GetAverage(testData)
              "方差："+GetVariance(testData)
              "标准差："+GetStandardDiviation(testData)
 */
 
@SimpleObject
public final class Statistics {

  private final static Random randomGenerator = new SecureRandom();

  private Statistics() {  // COV_NF_LINE
  }                   // COV_NF_LINE
  
  @SimpleFunction
	public static double[] AsArray(String str) {
		String[] items=str.split("\\Q" + "," + "\\E");
		int titems=items.length;
		double[] ditems;
		ditems=new double[titems];
		for(int i=0;i<titems;i++){
		ditems[i]=Double.parseDouble(items[i]);
		}
		return ditems;
	}
  
    @SimpleFunction
	public static String AsString(double[] inputData) {
	  if (inputData == null || inputData.length == 0)
	   return "Null";
	  
    StringBuilder sb = new StringBuilder();
    String sep = "";
    for (double a : inputData) {
     sb.append(sep).append(a);
     sep = ",";
    }
    return sb.toString();
	}
  
  @SimpleFunction
	 public static double GetMax(double[] inputData) {
	  if (inputData == null || inputData.length == 0)
	   return -1;
	  int len = inputData.length;
	  double max = inputData[0];
	  for (int i = 0; i < len; i++) {
	   if (max < inputData[i])
		max = inputData[i];
	  }
	  return max;
	 }
	 
  @SimpleFunction
	 public static double GetMin(double[] inputData) {
	  if (inputData == null || inputData.length == 0)
	   return -1;
	  int len = inputData.length;
	  double min = inputData[0];
	  for (int i = 0; i < len; i++) {
	   if (min > inputData[i])
		min = inputData[i];
	  }
	  return min;
	 }
	 
  @SimpleFunction
	public static double GetSum(double[] inputData) {
	  if (inputData == null || inputData.length == 0)
	   return -1;
	  int len = inputData.length;
	  double sum = 0;
	  for (int i = 0; i < len; i++) {
	   sum = sum + inputData[i];
	  }
	  return sum;
	 }
	 
  @SimpleFunction
	 public static int GetCount(double[] inputData) {
	  if (inputData == null)
	   return -1;
	  return inputData.length;
	 }

  @SimpleFunction
	 public static double GetAverage(double[] inputData) {
	  if (inputData == null || inputData.length == 0)
	   return -1;
	  int len = inputData.length;
	  double result;
	  result = GetSum(inputData) / len;
	  return result;
	 }
	 
  @SimpleFunction
	 public static double GetSquareSum(double[] inputData) {
	  if(inputData==null||inputData.length==0)
		  return -1;
		 int len=inputData.length;
	  double sqrsum = 0.0;
	  for (int i = 0; i <len; i++) {
	   sqrsum = sqrsum + inputData[i] * inputData[i];
	  }
	  return sqrsum;
	 }
	 
  @SimpleFunction
	public static double GetVariance(double[] inputData) {
	  int count = GetCount(inputData);
	  double sqrsum = GetSquareSum(inputData);
	  double average = GetAverage(inputData);
	  double result;
	  result = (sqrsum - count * average * average) / count;
		 return result;
	 }

  @SimpleFunction
	 public static double GetStandardDeviation(double[] inputData) {
	  double result;
	  result = java.lang.Math.sqrt(java.lang.Math.abs(GetVariance(inputData)));
	  return result;
	 }
	 
	 
  @SimpleFunction
  public static void Sort(double a[], int left, int right) {
        int i, j;
		double temp;
        i = left;
        j = right;
        if (left > right)
            return;
        temp = a[left];
        while (i != j)/* 找到最终位置 */
        {
            while (a[j] >= temp && j > i)
                j--;
            if (j > i)
                a[i++] = a[j];
            while (a[i] <= temp && j > i)
                i++;
            if (j > i)
                a[j--] = a[i];

        }
        a[i] = temp;
        Sort(a, left, i - 1);/* 递归左边 */
        Sort(a, i + 1, right);/* 递归右边 */
		
		//http://www.cnblogs.com/hubcarl/archive/2011/04/07/2007823.html
  }
  
  @SimpleFunction
  public static double[] GenNorm(int count, double mean, double sd) {
  //Dengfeng Zhang:http://tieba.baidu.com/p/2874642159
  //Recode in java:Lu Chengwei 
	double[] temp1;
	double[] temp2;
	double[] result;
	double R;
	double T;
	int tempn;
	int i;
	int j=0;
	result=new double[count];
	if(count<=0){
		return result;
	}
  
	if(count%2==0){
		tempn=count/2;
		temp1 = new double[tempn];
		temp2 = new double[tempn];
		for(i=0; i <tempn; i++){
			temp1[i]=randomGenerator.nextDouble();
			temp2[i]=randomGenerator.nextDouble();
			R=java.lang.Math.sqrt(-2*java.lang.Math.log(temp1[i]));
			T=2*java.lang.Math.PI*temp2[i];
			result[j]=mean+R*java.lang.Math.cos(T)*sd;
			j=j+1;
			result[j]=mean+R*java.lang.Math.sin(T)*sd;
			j=j+1;
		}
	}
	
	if(count%2==1){
		tempn=(count+1)/2;
		temp1 = new double[tempn];
		temp2 = new double[tempn];
		temp1[0]=randomGenerator.nextDouble();
		temp2[0]=randomGenerator.nextDouble();
		R=java.lang.Math.sqrt(-2*java.lang.Math.log(temp1[0]));
		T=2*java.lang.Math.PI*temp2[0];
		result[j]=mean+R*java.lang.Math.cos(T)*sd;
		j=j+1;
		
		if(count>1){
			tempn=(count-1)/2;
			temp1=new double[tempn];
			temp2=new double[tempn];
			for(i=0; i <tempn; i++){
				temp1[i]=randomGenerator.nextDouble();
				temp2[i]=randomGenerator.nextDouble();
				R=java.lang.Math.sqrt(-2*java.lang.Math.log(temp1[i]));
				T=2*java.lang.Math.PI*temp2[i];
				result[j]=mean+R*java.lang.Math.cos(T)*sd;
				j=j+1;
				result[j]=mean+R*java.lang.Math.sin(T)*sd;
				j=j+1;
			}
		}
		
	}
	
	return result;
  }
 /*

	If count>1 Then
		tempn=(count-1)/2
		temp1=New Double(tempn)
		temp2=New Double(tempn)
		For i=0 To (tempn-1)
		temp1(i)=Rnd()
		temp2(i)=Rnd()
		R=Sqr(-2*Log(temp1(i)))
		T=2*PI*temp2(i)
		result(j)=mean+R*Cos(T)*sd
		j=j+1
		result(j)=mean+R*Sin(T)*sd
		j=j+1
		Next i
		rnorm=result
	End If
End If
End Function

*/ 
 
  
  @SimpleFunction
  public static double GetMedian(double[] data) {
	int limit,limit2,i;
	double res=-9999;
	limit=data.length;
	limit2=limit-1;
	if(limit%2==0){
		Sort(data,0,limit2);
		res=(data[limit/2]+data[limit/2-1])/2;
	}
	if(limit%2==1){
		Sort(data,0,limit2);
		res=data[(limit-1)/2];
	}
	return res;
  }
  
	 
}
