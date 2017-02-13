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
import com.google.devtools.simple.runtime.components.Button;
import com.google.devtools.simple.runtime.components.ComponentContainer;
import com.google.devtools.simple.runtime.components.impl.android.util.ImageUtil;
import com.google.devtools.simple.runtime.components.impl.android.util.TextViewUtil;
import com.google.devtools.simple.runtime.events.EventDispatcher;
import android.view.MotionEvent;
import android.graphics.drawable.Drawable;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnFocusChangeListener;
import android.view.View.OnLongClickListener;
import android.view.View.OnTouchListener;
import android.widget.TextView;

import java.io.IOException;

/**
 * Android implementation of Simple Button component.
 *
 * @author Herbert Czymontek
 */
public final class ButtonImpl extends TextViewComponent
    implements Button, OnClickListener, OnFocusChangeListener, OnLongClickListener,OnTouchListener {

  // Background image path (if any)
  private String backgroundImage;
  private String UImage="null";
  private String DImage="null";

  /**
   * Creates a new Button component.
   *
   * @param container  container which will hold the component (must not be
   *                   {@code null}
   */
  public ButtonImpl(ComponentContainer container) {
    super(container);
  }

  @Override
  protected View createView() {
    View view = new android.widget.Button(ApplicationImpl.getContext());

    // Listen to clicks and focus changes
    view.setOnClickListener(this);
    view.setOnFocusChangeListener(this);
	view.setOnLongClickListener(this);
	view.setOnTouchListener(this);

    return view;
  }

  // Button implementation

  @Override
  public void Click() {
    EventDispatcher.dispatchEvent(this, "Click");
  }

  @Override
  public void PressDown() {
    EventDispatcher.dispatchEvent(this, "PressDown");
  }

  @Override
  public void PressUp() {
    EventDispatcher.dispatchEvent(this, "PressUp");
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
  public String Image() {
    return backgroundImage;
  }

  @Override
  public void SetUDImages(String UpImage, String DownImage) {
	Image(UpImage);
	UImage=UpImage;
	DImage=DownImage;
  }
  
  @Override
  public void Image(String imagePath) {
    backgroundImage = imagePath;
    Drawable drawable = null;
    try {
      drawable = ImageUtil.getDrawable(imagePath, ApplicationImpl.getContext(),
          android.R.drawable.ic_dialog_email, true);
    } catch (IOException ioe) {
      Log.e("ButtonImpl", "IOException", ioe);
    }
    if (drawable != null) {
      View view = getView();
      view.setBackgroundDrawable(drawable);
	  view.invalidate();

    }
  }

  @Override
  public boolean Enabled() {
    return TextViewUtil.isEnabled((TextView) getView());
  }

  @Override
  public void Enabled(boolean enabled) {
    TextViewUtil.setEnabled((TextView) getView(), enabled);
  }

  // OnClickListener implementation

  @Override
  public void onClick(View view) {
    Click();
  }


//VB4A LongClick 20130620
  @Override
  public boolean onLongClick(View view) {
	LongClick();
	return true;
  }

  @Override
  public void LongClick() {
    EventDispatcher.dispatchEvent(this, "LongClick");
  }
//VB4A LongClick 20130620

  // OnFocusChangeListener implementation

  @Override
  public boolean onTouch(View v, MotionEvent event) {
	if(event.getAction() == MotionEvent.ACTION_DOWN){
		if(DImage != "null") Image(DImage);
		PressDown();
	}else if(event.getAction() == MotionEvent.ACTION_UP){
		if(UImage != "null") Image(UImage);
		PressUp();
	}
	return false;
  }
  
  @Override
  public void onFocusChange(View previouslyFocused, boolean gainFocus) {
    if (gainFocus) {
      GotFocus();
    } else {
      LostFocus();
    }
  }
}
