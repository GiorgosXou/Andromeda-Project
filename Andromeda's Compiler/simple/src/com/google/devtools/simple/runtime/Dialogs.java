/*
 * Copyright 2014 VB4A.
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

package com.google.devtools.simple.runtime;

import com.google.devtools.simple.runtime.annotations.SimpleFunction;
import com.google.devtools.simple.runtime.annotations.SimpleObject;
import com.google.devtools.simple.runtime.annotations.SimpleEvent;
import com.google.devtools.simple.runtime.events.EventDispatcher;
import android.app.AlertDialog; 
import android.app.Dialog;
import android.app.Activity;
import android.content.DialogInterface;
import android.view.View.OnClickListener;
import java.lang.reflect.Array;
import com.google.devtools.simple.runtime.android.ApplicationImpl;
/**
 * Implementation of various dialogs in vb4a.
 * 
 * @author Lu Chengwei
 */
@SimpleObject
public class Dialogs {

  private Dialogs() {
  }
  
@SimpleFunction
public void ShowMsgbox(String title, String msg, int icon,String[] btn) {
	switch(Array.getLength(btn)){
	case 1:
    new AlertDialog.Builder(ApplicationImpl.getContext())
    .setTitle(title)
    .setMessage(msg)
	.setIcon(icon)
	.setPositiveButton(btn[0],new DialogInterface.OnClickListener(){
		@Override
			public void onClick(DialogInterface dialog, int which) {
				MsgboxClicked(which);
			}
	})
    .show();
	break;
	case 2:
		new AlertDialog.Builder(ApplicationImpl.getContext())
		.setTitle(title)
		.setMessage(msg)
		.setPositiveButton(btn[0],new DialogInterface.OnClickListener(){
			@Override
				public void onClick(DialogInterface dialog, int which) {
					MsgboxClicked(which);
				}
		})
		.setNegativeButton(btn[1],new DialogInterface.OnClickListener() {
			@Override
				public void onClick(DialogInterface dialog, int which) {
					MsgboxClicked(which);
				}
		})
		.show();
	break;
	case 3:
	break;
	}
}

@SimpleEvent
public void MsgboxClicked(int bid) {
  EventDispatcher.dispatchEvent(this, "MsgboxClicked",bid);
}

@SimpleFunction
public void ShowInputbox() {

}

@SimpleEvent
public void InputboxClicked() {
  EventDispatcher.dispatchEvent(this, "InputboxClicked");
}

@SimpleFunction
public void ShowRadiobox() {

}

@SimpleEvent
public void RadioboxClicked() {
  EventDispatcher.dispatchEvent(this, "RadioboxClicked");
}

@SimpleFunction
public void ShowCheckbox() {

}

@SimpleEvent
public void CheckboxClicked() {
  EventDispatcher.dispatchEvent(this, "CheckboxClicked");
}

@SimpleFunction
public void ShowListbox() {

}

@SimpleEvent
public void ListboxClicked() {
  EventDispatcher.dispatchEvent(this, "ListboxClicked");
}

}
