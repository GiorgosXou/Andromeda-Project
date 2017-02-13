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

import com.google.devtools.simple.runtime.android.ApplicationImpl;
import com.google.devtools.simple.runtime.components.ComponentContainer;
import com.google.devtools.simple.runtime.components.Listview;
import com.google.devtools.simple.runtime.events.EventDispatcher;
import com.google.devtools.simple.runtime.parameters.BooleanReferenceParameter;

import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.view.View.OnFocusChangeListener;
import android.widget.TextView;
import android.text.TextUtils; 


import android.widget.ListView;
import android.widget.SimpleAdapter;  


import android.widget.AdapterView;   
import android.widget.AdapterView.OnItemClickListener;
import android.widget.AdapterView.OnItemLongClickListener;

import android.widget.ArrayAdapter;   
import android.widget.ArrayAdapter.*;   
import android.os.Bundle;   


/**
 * Android implementation of Simple text box component.
 *
 * @author Herbert Czymontek
 */
public final class VB4AListViewImpl extends TextViewComponent implements Listview,OnItemClickListener,OnItemLongClickListener { 
  public ArrayAdapter<String> adapter;
  public int tid=-1;
  public int tid2=-1;
  public VB4AListViewImpl(ComponentContainer container) {
    super(container);
  }

  @Override
  protected View createView() {
    ListView view = new ListView(ApplicationImpl.getContext());
    
    // Listen to focus changes
    view.setOnFocusChangeListener(new OnFocusChangeListener() {
      @Override
      public void onFocusChange(View previouslyFocused, boolean gainFocus) {
        if (gainFocus) {
          GotFocus();
        } else {
          LostFocus();
        }
      }      
    });

	view.setOnItemClickListener(this);
	view.setOnItemLongClickListener(this);
    return view;
  }

  @Override
  public void GotFocus() {
    EventDispatcher.dispatchEvent(this, "GotFocus");
  }

  @Override
  public void LostFocus() {
    EventDispatcher.dispatchEvent(this, "LostFocus");
  }

  @Override
  public boolean Enabled() {
    return getView().isEnabled();
  }

  @Override
  public void Enabled(boolean enabled) {
    View view = getView();
    view.setEnabled(enabled);
    view.setFocusable(enabled);
    view.setFocusableInTouchMode(enabled);
    view.invalidate();
  }
  
  @Override
  public void SetItem(String[] array) {
	ListView view = (ListView) getView();
	adapter = new ArrayAdapter<String>(ApplicationImpl.getContext(),android.R.layout.simple_expandable_list_item_1, array);
	view.setAdapter(adapter);  
  }

  @Override
  public void ItemClicked(int item) {
	EventDispatcher.dispatchEvent(this, "ItemClicked",item);
  }

  @Override
  public void ItemLongClicked(int item) {
	EventDispatcher.dispatchEvent(this, "ItemLongClicked",item);
  }
  
  @Override
  public String Value() {
	ListView view = (ListView) getView();
	if(tid==-1){
		return "Null";
	} else {
		return view.getAdapter().getItem(tid).toString();
	}
  }
  
  @Override
  public String LongClickValue() {
	ListView view = (ListView) getView();
	if(tid==-1){
		return "Null";
	} else {
		return view.getAdapter().getItem(tid2).toString();
	}
  }
  
  @Override
  public int Index() {
	return tid;
  }
  @Override
  public int LongClickIndex() {
	return tid2;
  }
  @Override
  public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,long arg3) {
  	tid=arg2;
	ItemClicked(arg2);
  }
  
  @Override
  public boolean onItemLongClick(AdapterView<?> arg0, View arg1,final int arg2, long arg3) {
  	tid2=arg2;
	ItemLongClicked(arg2);
	return true;
  }
}
