/*
 * Lu Chengwei 2013, VB4A
 *
 */

package com.google.devtools.simple.runtime.components.impl.android;

import com.google.devtools.simple.runtime.android.ApplicationImpl;
import com.google.devtools.simple.runtime.components.ComponentContainer;
import com.google.devtools.simple.runtime.components.VB4AProp;
import com.google.devtools.simple.runtime.components.impl.ComponentImpl;

import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Vibrator;

import android.os.Build;

import android.app.Activity;
import android.app.PendingIntent;
import android.content.Intent;
import android.os.Bundle;
import android.telephony.PhoneNumberUtils;
import android.telephony.gsm.SmsManager; 
import android.telephony.TelephonyManager;  
import android.widget.Toast; 
import android.provider.Settings.Secure;

import java.io.BufferedReader;  
import java.io.BufferedWriter;  
import java.io.InputStreamReader;  
import java.io.OutputStreamWriter;  
import java.io.PrintWriter;  
import java.net.Socket;  
import java.io.IOException;  

public final class VB4APropImpl extends ComponentImpl implements VB4AProp {


  public VB4APropImpl(ComponentContainer container) {
    super(container);
  }

  @Override
  public String GetIMEI() {
	TelephonyManager tele = (TelephonyManager) ApplicationImpl.getContext().getSystemService(Context.TELEPHONY_SERVICE);
	return tele.getDeviceId();
  }

  @Override
  public String GetModel() {
	Build bd = new Build();   
	return bd.MODEL;
  }

  @Override
  public String GetSoftwareVersion() {
	TelephonyManager tele = (TelephonyManager) ApplicationImpl.getContext().getSystemService(Context.TELEPHONY_SERVICE);
	return tele.getDeviceSoftwareVersion();
  }

  @Override
  public String GetPhoneNum() {
	TelephonyManager tele = (TelephonyManager) ApplicationImpl.getContext().getSystemService(Context.TELEPHONY_SERVICE);
	return tele.getLine1Number();
  }

  @Override
  public String GetSimSerial() {
	TelephonyManager tele = (TelephonyManager) ApplicationImpl.getContext().getSystemService(Context.TELEPHONY_SERVICE);
	return tele.getSimSerialNumber();
  }

  @Override
  public String GetID() {
	return Secure.getString(ApplicationImpl.getContext().getContentResolver(), Secure.ANDROID_ID);
  }
/*
  @Override
  public String GetABI() {

	return android.os.Build.CPUABI;
  }
*/

}
