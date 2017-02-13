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
import com.google.devtools.simple.runtime.components.Combobox;
import com.google.devtools.simple.runtime.events.EventDispatcher;
import com.google.devtools.simple.runtime.parameters.BooleanReferenceParameter;

import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.view.View.OnFocusChangeListener;
import android.widget.Spinner;
import android.widget.TextView;
import android.text.TextUtils; 

import android.widget.AdapterView;   
import android.widget.AdapterView.OnItemSelectedListener;

import android.widget.ArrayAdapter;   
import android.widget.ArrayAdapter.*;   
import android.os.Bundle;   


/**
 * Android implementation of Simple text box component.
 *
 * @author Herbert Czymontek
 */
public final class VB4ASpinnerImpl extends TextViewComponent implements Combobox, OnItemSelectedListener {
  public ArrayAdapter<String> adapter;
  public VB4ASpinnerImpl(ComponentContainer container) {
    super(container);
  }

  @Override
  protected View createView() {
    Spinner view = new Spinner(ApplicationImpl.getContext());
    
    
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

	view.setOnItemSelectedListener(this);

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
  public void ItemSelected(int item) {
	EventDispatcher.dispatchEvent(this, "ItemSelected",item);
  }

  @Override
  public void NothingSelected() {
	EventDispatcher.dispatchEvent(this, "NothingSelected");
  }


  @Override
  public void onItemSelected(AdapterView<?> arg0, View arg1, int arg2,long arg3) {   
	ItemSelected(arg2);
  } 

  @Override
  public void onNothingSelected(AdapterView<?> arg0) {  
	NothingSelected();
  }

  @Override
  public void SetItem(String[] array) {
	Spinner view = (Spinner) getView();
	adapter = new ArrayAdapter<String>(ApplicationImpl.getContext(),android.R.layout.simple_spinner_item, array);
	adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
	view.setAdapter(adapter);  
  }

  @Override
  public String Value() {
	Spinner view = (Spinner) getView();
	return view.getSelectedItem().toString();
  }

  @Override
  public void Index(int index) {
	Spinner view = (Spinner) getView();
	view.setSelection(index,true);
  }

  @Override
  public int Index() {
  	Spinner view = (Spinner) getView();
	return view.getSelectedItemPosition();
  }



}
