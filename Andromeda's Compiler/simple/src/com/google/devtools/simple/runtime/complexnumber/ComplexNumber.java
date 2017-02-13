/*
  VB4A ComplexNumber Lu Chengwei 2014 03 13
*/

package com.google.devtools.simple.runtime.complexnumber;

import com.google.devtools.simple.runtime.annotations.SimpleFunction;
import com.google.devtools.simple.runtime.annotations.SimpleObject;
import com.google.devtools.simple.runtime.annotations.SimpleProperty;
import com.google.devtools.simple.runtime.errors.IndexOutOfBoundsError;
import com.google.devtools.simple.runtime.variants.Variant;

@SimpleObject
public class ComplexNumber {
 private double realPart;
 private double imaginaryPart;
 
 public ComplexNumber() {
  this.realPart = 0.0;
  this.imaginaryPart = 0.0;
 }
 
 public ComplexNumber(double a,double b) {
  this.realPart = a;
  this.imaginaryPart = b;
 }
 
@SimpleProperty
	public void RealPart(double rpt) {
		this.realPart=rpt;
	}
	
@SimpleProperty
	public void ImaginaryPart(double ipt) {
		this.imaginaryPart=ipt;
	}
	
@SimpleProperty
	public double RealPart() {
		return this.realPart;
	}
	
@SimpleProperty
	public double ImaginaryPart() {
		return imaginaryPart;
	}

@SimpleFunction
	public void SetComplex(double rpt,double ipt){
		this.realPart=rpt;
		this.imaginaryPart=ipt;
	}

@SimpleProperty
 public double GetMod() {
	return java.lang.Math.sqrt(this.realPart*this.realPart+this.imaginaryPart*this.imaginaryPart);
 }
	
@SimpleProperty
public double GetArg() {
	double r=this.realPart;
	double i=this.imaginaryPart;
	double arg=0;
	if(r==0 && i==0){
		arg=0;
	}
	if(r==0 && i>0){
		arg=java.lang.Math.PI/2;
	}
	if(r==0 && i<0){
		arg=-1*java.lang.Math.PI/2;
	}
	if(r<0 && i<0){
		arg=java.lang.Math.atan(i/r)-java.lang.Math.PI;
	}	
	if(r<0 && i>=0){
		arg=java.lang.Math.atan(i/r)+java.lang.Math.PI;
	}	
	if(r>0){
		arg=java.lang.Math.atan(i/r);
	}	
	return arg;
}

@SimpleProperty
 public String toString() {
  if(this.imaginaryPart<0){
  return this.realPart + "" + this.imaginaryPart + "i";
  } else {
  return this.realPart + "+" + this.imaginaryPart + "i";
  }
 }
 
@SimpleFunction
public ComplexNumber Add(ComplexNumber aComNum){
  if(aComNum==null)
  {
   return new ComplexNumber();
  }
  return new ComplexNumber(this.realPart + aComNum.RealPart(),this.imaginaryPart + aComNum.ImaginaryPart());
}

@SimpleFunction
 public ComplexNumber Minus(ComplexNumber aComNum)
 {
  if(aComNum==null)
  {
   return new ComplexNumber();
  }
  return new ComplexNumber(this.realPart - aComNum.RealPart(),this.imaginaryPart - aComNum.ImaginaryPart());
 }

@SimpleFunction
public ComplexNumber Multiply(ComplexNumber aComNum)
 {
  if(aComNum==null)
  {
   return new ComplexNumber();
  }
  double newReal = this.realPart * aComNum.RealPart() - this.imaginaryPart * aComNum.ImaginaryPart();
  double newImaginary = this.realPart * aComNum.ImaginaryPart() + this.imaginaryPart * aComNum.RealPart();
  ComplexNumber result = new ComplexNumber(newReal,newImaginary);
  return result;
 }
 
 @SimpleFunction
 public ComplexNumber Divide(ComplexNumber aComNum)
 {
  if(aComNum==null)
  {
   return new ComplexNumber();
  }
  if(aComNum.RealPart() == 0 && aComNum.ImaginaryPart() == 0)
  {
   return new ComplexNumber();
  }
  
  double temp = aComNum.RealPart() * aComNum.RealPart() +
  aComNum.ImaginaryPart() * aComNum.ImaginaryPart();
  double crealpart = (this.realPart * aComNum.RealPart()+
    this.imaginaryPart * aComNum.ImaginaryPart())/temp;
  double cimaginarypart = (this.imaginaryPart * aComNum.RealPart() -
    this.realPart * aComNum.ImaginaryPart())/temp;
  return new ComplexNumber(crealpart,cimaginarypart);
  
 }
 
}