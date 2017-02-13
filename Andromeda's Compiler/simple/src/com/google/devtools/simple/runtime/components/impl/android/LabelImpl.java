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
import com.google.devtools.simple.runtime.components.Label;
import com.google.devtools.simple.runtime.events.EventDispatcher;

import android.view.View;
import android.widget.TextView;
import android.view.View.OnClickListener;
import android.view.View.OnLongClickListener;

/**
 * Label containing a text string or an image.
 *
 * @author Herbert Czymontek
 */
public final class LabelImpl extends TextViewComponent 
	implements Label, OnClickListener, OnLongClickListener {

  /**
   * Creates a new Label component.
   *
   * @param container  container which will hold the component (must not be
   *                   {@code null}
   */
  public LabelImpl(ComponentContainer container) {
    super(container);
  }

  @Override
  protected View createView() {
	    
	TextView view = new TextView(ApplicationImpl.getContext());
	view.setOnClickListener(this);
	view.setOnLongClickListener(this);
    return view;
  }

  @Override
  public void Click() {
    EventDispatcher.dispatchEvent(this, "Click");
  }
  //VB4A LongClick 20130620
  @Override
  public boolean onLongClick(View view) {
	LongClick();
	return true;
  }

  @Override
  public boolean Enabled() {
    return getView().isEnabled();
  }

  @Override
  public void Enabled(boolean enabled) {
    View view = getView();
    view.setEnabled(enabled);
    view.invalidate();
  }
  
  @Override
  public void LongClick() {
    EventDispatcher.dispatchEvent(this, "LongClick");
  }
//VB4A LongClick 20130620
  @Override
  public void onClick(View view) {
    Click();
  }
}
