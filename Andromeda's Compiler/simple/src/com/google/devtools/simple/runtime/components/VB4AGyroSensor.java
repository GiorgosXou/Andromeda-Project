/*
	LuChengwei 20140224 VB4A, based on google simple.
 */

package com.google.devtools.simple.runtime.components;

import com.google.devtools.simple.runtime.annotations.SimpleComponent;
import com.google.devtools.simple.runtime.annotations.SimpleEvent;
import com.google.devtools.simple.runtime.annotations.SimpleObject;
import com.google.devtools.simple.runtime.annotations.SimpleProperty;

@SimpleComponent
@SimpleObject
public interface VB4AGyroSensor extends SensorComponent {


  @SimpleEvent
  public void GyroChanged(float ggx, float ggy, float ggz);

  /**
   * Available property getter method (read-only property).
   *
   * @return {@code true} indicates that an orientation sensor is available,
   *         {@code false} that it isn't
   */
  @SimpleProperty
  boolean Available();

  @SimpleProperty
  float GyroX();
  @SimpleProperty
  float GyroY();
  @SimpleProperty
  float GyroZ();
  /**
   * Enabled property getter method.
   *
   * @return {@code true} indicates that the sensor generates events,
   *         {@code false} that it doesn't
   */
  @SimpleProperty
  boolean Enabled();

  /**
   * Enabled property setter method.
   *
   * @param enabled  {@code true} enables sensor event generation,
   *                 {@code false} disables it
   */
  @SimpleProperty(type = SimpleProperty.PROPERTY_TYPE_BOOLEAN,
                  initializer = "True")
  void Enabled(boolean enabled);

 
}
