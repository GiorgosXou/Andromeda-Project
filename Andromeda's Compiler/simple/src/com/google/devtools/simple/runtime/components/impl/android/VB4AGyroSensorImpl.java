package com.google.devtools.simple.runtime.components.impl.android;

import com.google.devtools.simple.runtime.android.ApplicationImpl;
import com.google.devtools.simple.runtime.components.ComponentContainer;
import com.google.devtools.simple.runtime.components.VB4AGyroSensor;
import com.google.devtools.simple.runtime.components.impl.ComponentImpl;
import com.google.devtools.simple.runtime.events.EventDispatcher;

import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;

import java.util.List;

public final class VB4AGyroSensorImpl extends ComponentImpl
    implements VB4AGyroSensor, SensorEventListener {

  private final SensorManager sensors;

  private boolean enabled;
  private float gtx;
  private float gty;
  private float gtz;

  public VB4AGyroSensorImpl(ComponentContainer container) {
    super(container);

    sensors = (SensorManager) ApplicationImpl.getContext().getSystemService(Context.SENSOR_SERVICE);
    sensors.registerListener(this, sensors.getDefaultSensor(Sensor.TYPE_GYROSCOPE),
        SensorManager.SENSOR_DELAY_NORMAL);
  }

  @Override
  public void GyroChanged(float ggx, float ggy, float ggz) {
	EventDispatcher.dispatchEvent(this, "GyroChanged", ggx,ggy,ggz);
  }

  @Override
  public boolean Available() {
    List<Sensor> sensorList = sensors.getSensorList(Sensor.TYPE_GYROSCOPE);
    return sensorList != null && !sensorList.isEmpty();
  }

  @Override
  public boolean Enabled() {
    return enabled;
  }

  @Override
  public float GyroX() {
	return gtx;
  }
  
  @Override
  public float GyroY() {
	return gty;
  }
  
  @Override
  public float GyroZ() {
	return gtz;
  }
  
  @Override
  public void Enabled(boolean enable) {
    enabled = enable;
  }

 
  // SensorEventListener implementation

  @Override
  public void onSensorChanged(SensorEvent event) {
    if (event.sensor.getType() == Sensor.TYPE_GYROSCOPE && enabled) {
      gtx = event.values[0];
	  gty = event.values[1];
	  gtz = event.values[2];
	  GyroChanged(gtx,gty,gtz);
    }
  }

  @Override
  public void onAccuracyChanged(Sensor sensor, int accuracy) {
  }
}
