package com.google.devtools.simple.runtime.components.impl.android;

import com.google.devtools.simple.runtime.android.ApplicationImpl;
import com.google.devtools.simple.runtime.components.ComponentContainer;
import com.google.devtools.simple.runtime.components.VB4ATemperatureSensor;
import com.google.devtools.simple.runtime.components.impl.ComponentImpl;
import com.google.devtools.simple.runtime.events.EventDispatcher;

import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;

import java.util.List;

public final class VB4ATemperatureSensorImpl extends ComponentImpl
    implements VB4ATemperatureSensor, SensorEventListener {

  private final SensorManager sensors;

  private boolean enabled;
  private float temperature;

  public VB4ATemperatureSensorImpl(ComponentContainer container) {
    super(container);

    sensors = (SensorManager) ApplicationImpl.getContext().getSystemService(Context.SENSOR_SERVICE);
    sensors.registerListener(this, sensors.getDefaultSensor(Sensor.TYPE_TEMPERATURE),
        SensorManager.SENSOR_DELAY_GAME);
  }

  @Override
  public void TemperatureChanged(float newtemperature) {
	EventDispatcher.dispatchEvent(this, "TemperatureChanged", newtemperature);
  }

  @Override
  public boolean Available() {
    List<Sensor> sensorList = sensors.getSensorList(Sensor.TYPE_TEMPERATURE);
    return sensorList != null && !sensorList.isEmpty();
  }

  @Override
  public boolean Enabled() {
    return enabled;
  }

  @Override
  public float Temperature() {
	return temperature;
  }
  @Override
  public void Enabled(boolean enable) {
    enabled = enable;
  }

 
  // SensorEventListener implementation

  @Override
  public void onSensorChanged(SensorEvent event) {
    if (event.sensor.getType() == Sensor.TYPE_TEMPERATURE && enabled) {
      temperature = event.values[0];
	  TemperatureChanged(temperature);
    }
  }

  @Override
  public void onAccuracyChanged(Sensor sensor, int accuracy) {
  }
}
