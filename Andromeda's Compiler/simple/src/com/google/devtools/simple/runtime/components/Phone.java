/*
 * Copyright 2009 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package com.google.devtools.simple.runtime.components;

import com.google.devtools.simple.runtime.annotations.SimpleComponent;
import com.google.devtools.simple.runtime.annotations.SimpleFunction;
import com.google.devtools.simple.runtime.annotations.SimpleObject;
import com.google.devtools.simple.runtime.annotations.SimpleProperty;
import com.google.devtools.simple.runtime.annotations.UsesPermissions;

/**
 * Simple Phone component.
 *
 * <p>Allows access to phone functionality.
 *
 * @author Herbert Czymontek
 */
@SimpleComponent
@SimpleObject
@UsesPermissions(permissionNames = "android.permission.VIBRATE,"+"android.permission.CALL_PHONE,"+"android.permission.SEND_SMS,"+"android.permission.INTERNET") //,"+"android.permission.ACCESS_COARSE_LOCATION")
public interface Phone extends Component {

  /**
   * Available property getter method (read-only property).
   *
   * @return {@code true} indicates that phone functionality is available,
   *         {@code false} that it isn't
   */
  @SimpleProperty
  boolean Available();

  //POST:http://blog.csdn.net/redoffice/article/details/7552137; And Some codes from E4A, thanks ͹-(^_^)-͹.
  @SimpleFunction
  String VB4APost(String url, String params, int Ctimeout, int Rtimeout,String charset);
  
  @SimpleFunction
  String VB4AGet(String url,String codec);
  
  /**
   * Places a call to the given phone number.
   *
   * @param phoneNumber  phone number in the form of numbers only (no spaces,
   *                     no dashes etc.)
   */
  @SimpleFunction
  void Call(String phoneNumber);

  @SimpleFunction
  void SendSMS(String phoneNumber,String text,String warnings);

  @SimpleFunction
  String SocketSend(String ip,int port,String data);

  @SimpleFunction
  String GetURL(String url,String codec);

  @SimpleFunction
  void SendMail(String Address, String mailtitle,String mailtext);

  @SimpleFunction
  void JumpURL(String url);
//  @SimpleFunction
//  void SendSMS(String phoneNumber, String text);

  /**
   * Vibrates the phone.
   *
   * @param duration  duration in milliseconds
   */
  @SimpleFunction
  void Vibrate(int duration);

  //@SimpleFunction
  //String GetIMEI();
}
