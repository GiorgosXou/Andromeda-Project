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

package com.google.devtools.simple.runtime.components.impl.android;

import com.google.devtools.simple.runtime.components.ComponentContainer;
import com.google.devtools.simple.runtime.events.EventDispatcher;
import com.google.devtools.simple.runtime.components.BReceiver;
import com.google.devtools.simple.runtime.components.impl.ComponentImpl;
import android.content.BroadcastReceiver;
import android.content.Intent;
import android.content.Context;
import android.os.Handler;
import android.content.IntentFilter;  
import com.google.devtools.simple.runtime.android.ApplicationImpl;
import android.util.Log; 

public final class BReceiverImpl extends ComponentImpl implements BReceiver {

  private BroadcastReceiver InfoReceiver;
  public BReceiverImpl(ComponentContainer container) {
    super(container);
  }

	
  @Override
  public void Received(String msg) {
    EventDispatcher.dispatchEvent(this, "Received",msg);
  }
  
  @Override
  public void RegisterReceiver(String name,String bcontent) {
    final String Bname=name;
	final String BCont=bcontent;
    InfoReceiver = new BroadcastReceiver() {
		//@Override  
        public void onReceive(Context context, Intent intent)   
        {  
			if(intent.getAction().equals("vb4a")){
				String resv = intent.getExtras().getString(BCont);
				Received(resv);
				Log.i("vb4a", "MyReceiver onReceive--->");  
			}
        }
	};
	IntentFilter filter = new IntentFilter();  
    filter.addAction("vb4a");  
    ApplicationImpl.getContext().registerReceiver(InfoReceiver, filter);  
	Log.i("vb4a", "Registed");  
  }
}
