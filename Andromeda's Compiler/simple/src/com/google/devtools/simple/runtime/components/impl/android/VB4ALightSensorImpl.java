package com.google.devtools.simple.runtime.components.impl.android;

import com.google.devtools.simple.runtime.android.ApplicationImpl;
import com.google.devtools.simple.runtime.components.ComponentContainer;
import com.google.devtools.simple.runtime.components.VB4ALightSensor;
import com.google.devtools.simple.runtime.components.impl.ComponentImpl;
import com.google.devtools.simple.runtime.events.EventDispatcher;

import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;

import java.util.List;

public final class VB4ALightSensorImpl extends ComponentImpl
    implements VB4ALightSensor, SensorEventListener {

  private final SensorManager sensors;

  private boolean enabled;
  private float newvalue1;

  public VB4ALightSensorImpl(ComponentContainer container) {
    super(container);

    sensors = (SensorManager) ApplicationImpl.getContext().getSystemService(Context.SENSOR_SERVICE);
    sensors.registerListener(this, sensors.getDefaultSensor(Sensor.TYPE_LIGHT),
        SensorManager.SENSOR_DELAY_GAME);
  }

  @Override
  public void LightChanged(float newvalue) {
	EventDispatcher.dispatchEvent(this, "LightChanged", newvalue1);
  }

  @Override
  public boolean Available() {
    List<Sensor> sensorList = sensors.getSensorList(Sensor.TYPE_LIGHT);
    return sensorList != null && !sensorList.isEmpty();
  }

  @Override
  public boolean Enabled() {
    return enabled;
  }

  @Override
  public float Light() {
	return newvalue1;
  }
  @Override
  public void Enabled(boolean enable) {
    enabled = enable;
  }

 
  // SensorEventListener implementation

  @Override
  public void onSensorChanged(SensorEvent event) {
    if (event.sensor.getType() == Sensor.TYPE_LIGHT && enabled) {
      newvalue1 = event.values[0];
	  LightChanged(newvalue1);
    }
  }

  @Override
  public void onAccuracyChanged(Sensor sensor, int accuracy) {
  }
}
