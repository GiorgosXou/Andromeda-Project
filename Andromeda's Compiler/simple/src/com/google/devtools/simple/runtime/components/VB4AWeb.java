/*
 * Copyright 2013 Lu Chengwei
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
import com.google.devtools.simple.runtime.annotations.SimpleObject;
import com.google.devtools.simple.runtime.annotations.SimpleProperty;
import com.google.devtools.simple.runtime.annotations.UsesPermissions;
import com.google.devtools.simple.runtime.annotations.SimpleFunction;
import com.google.devtools.simple.runtime.annotations.SimpleEvent;

/**
 * WebView Component for VB4A 
 *
 * @author Lu Chengwei,2013,05,06
 */
@SimpleComponent
@SimpleObject

@UsesPermissions(permissionNames = "android.permission.INTERNET")
public interface VB4AWeb extends VisibleComponent {

  @SimpleFunction
  public void LoadURL(String URL);

  @SimpleFunction
  public void LoadURL2(String URL); //Ìø×ªÄ¬ÈÏä¯ÀÀÆ÷

  @SimpleFunction
  public void GoBack();

  @SimpleFunction
  public void GoForward();

  @SimpleFunction
  public void Reload();

  @SimpleFunction
  public void Stop();

//  @SimpleProperty
  boolean SavePassword();

  @SimpleProperty(type = SimpleProperty.PROPERTY_TYPE_BOOLEAN,
                  initializer = "True")
  void SavePassword(boolean value);

//  @SimpleProperty
  boolean SaveFormData();

  @SimpleProperty(type = SimpleProperty.PROPERTY_TYPE_BOOLEAN,
                  initializer = "True")
  void SaveFormData(boolean value);
  
//  @SimpleProperty
  boolean JSEnabled();

  @SimpleProperty(type = SimpleProperty.PROPERTY_TYPE_BOOLEAN,
                  initializer = "True")
  void JSEnabled(boolean value);

//  @SimpleProperty
  boolean ZoomEnabled();

  @SimpleProperty(type = SimpleProperty.PROPERTY_TYPE_BOOLEAN,
                  initializer = "True")
  void ZoomEnabled(boolean value);

//  @SimpleProperty
  boolean BuildinZoom();

//  @SimpleProperty
  short Progress();

//  @SimpleEvent
  void ProgressChanged(int prgs);

  @SimpleProperty(type = SimpleProperty.PROPERTY_TYPE_BOOLEAN,
                  initializer = "True")
  void BuildinZoom(boolean value);
 
}
