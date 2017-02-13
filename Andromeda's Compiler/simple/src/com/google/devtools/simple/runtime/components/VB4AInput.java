/*
 * Copyright 2009 Google Inc.
 * Lu Chengwei 2013, VB4A
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

import com.google.devtools.simple.runtime.annotations.SimpleDataElement;
import com.google.devtools.simple.runtime.annotations.SimpleEvent;
import com.google.devtools.simple.runtime.annotations.SimpleObject;

/**
 * VB4A CONSTS.
 * 
 * @author Lu Chengwei 2013, CUIT
 */
@SimpleObject
public interface VB4AInput {


  @SimpleDataElement
  static final int DATETIME = 4;
  @SimpleDataElement
  static final int NUMBER = 3;
  @SimpleDataElement
  static final int PHONENUMBER = 2;
  @SimpleDataElement
  static final int PHONE = 2;
  @SimpleDataElement
  static final int TEXTS = 1;
  @SimpleDataElement
  static final int NULL = 0;
  @SimpleDataElement
  static final int MULTILINE = 131072;

  /**
   * Default Initialize event handler.
   */
  @SimpleEvent
  void Initialize();
}
