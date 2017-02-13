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
import com.google.devtools.simple.runtime.annotations.SimpleEvent;
import com.google.devtools.simple.runtime.annotations.SimpleFunction;
import com.google.devtools.simple.runtime.annotations.SimpleObject;
import com.google.devtools.simple.runtime.annotations.SimpleProperty;
import com.google.devtools.simple.runtime.annotations.UsesPermissions;
/**
 * Simple Canvas component.
 *
 * @author Herbert Czymontek
 */
@SimpleComponent
@SimpleObject
@UsesPermissions(permissionNames = "android.permission.WRITE_EXTERNAL_STORAGE,"
								  +"android.permission.MOUNT_UNMOUNT_FILESYSTEMS")
public interface Canvas extends VisibleComponent {

  // Overriding default property values

  @Override
  @SimpleProperty(type = SimpleProperty.PROPERTY_TYPE_COLOR,
                  initializer = Component.COLOR_WHITE + "")
  void BackgroundColor(int argb);

  // Component declaration

  /**
   * Handler for touch events
   *
   * @param x  x-coordinate of the touched point
   * @param y  y-coordinate of the touched point
   */
  @SimpleEvent
  void VB4ADown(int x, int y);

  @SimpleEvent
  void VB4AUp(int x, int y);

  @SimpleEvent
  void VB4AMove(int lastx, int lasty, int currentx, int currenty);


  @SimpleFunction
  void SetVB4APaintSize(int size);
  /**
   * BackgroundImage property setter method: sets a background image for the
   * form.
   *
   * @param imagePath  path and name of image
   */
  @SimpleProperty(type = SimpleProperty.PROPERTY_TYPE_ASSET,
                  initializer = "\"\"")
  void BackgroundImage(String imagePath);

  /**
   * PaintColor property getter method.
   *
   * @return  paint RGB color with alpha
   */
  @SimpleProperty
  int PaintColor();

  @SimpleFunction
  boolean SavePicture(String imagePath);
  /**
   * PaintColor property setter method.
   *
   * @param argb  paint RGB color with alpha
   */
  @SimpleProperty(type = SimpleProperty.PROPERTY_TYPE_COLOR,
                  initializer = Component.COLOR_BLACK + "")
  void PaintColor(int argb);

//VB4A 20130611
  @SimpleFunction
  void VB4ADrawText(int x, int y, String text);

  @SimpleFunction
  void VB4ADrawRect(int x1, int y1, int x2, int y2);

  @SimpleFunction
  void VB4ARotate(int rot);

  @SimpleFunction
  void VB4ADrawArc(int x1, int y1, int x2, int y2, int angst, int anged, boolean ucenter);

  @SimpleFunction
  void VB4ADrawRoundRect(int x1, int y1, int x2, int y2, int rx, int ry);

  @SimpleFunction
  void VB4AInvalidate();


  //@SimpleFunction
  void VB4ADrawPic(String imagePath, int x, int y);

//VB4A 20130611

  /**
   * Clears the canvas (fills it with the background color).
   */
  @SimpleFunction
  void Clear();

  /**
   * Draws a point at the given coordinates on the canvas.
   *
   * @param x  x coordinate
   * @param y  y coordinate
   */
  @SimpleFunction
  void DrawPoint(int x, int y);

  /**
   * Draws a circle at the given coordinates on the canvas, with the given radius
   *
   * @param x  x coordinate
   * @param y  y coordinate
   * @param r  radius
   */
  @SimpleFunction
  void DrawCircle(int x, int y, float r);

  /**
   * Draws a line between the given coordinates on the canvas.
   *
   * @param x1  x coordinate of first point
   * @param y1  y coordinate of first point
   * @param x2  x coordinate of second point
   * @param y2  y coordinate of second point
   */
  @SimpleFunction
  void DrawLine(int x1, int y1, int x2, int y2);

  // TODO: add more drawing primitives
}
