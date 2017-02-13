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
import com.google.devtools.simple.runtime.components.VB4AImageButton;
import com.google.devtools.simple.runtime.components.impl.android.util.ImageUtil;
import com.google.devtools.simple.runtime.errors.NoSuchFileError;
import com.google.devtools.simple.runtime.events.EventDispatcher;

import android.view.View.OnClickListener;
import android.view.View.OnLongClickListener;
import android.graphics.drawable.Drawable;
import android.view.View;
import android.widget.ImageButton;
import android.view.MotionEvent;

import java.io.IOException;

/**
 * Component for displaying images and animations.
 *
 * @author Herbert Czymontek
 */
public final class VB4AImageButtonImpl extends ViewComponent implements VB4AImageButton, OnClickListener, OnLongClickListener {

  // Backing for background color
  private int backgroundColor;

  /**
   * Creates a new Image component.
   *
   * @param container  container which will hold the component (must not be
   *                   {@code null}
   */
  public VB4AImageButtonImpl(ComponentContainer container) {
    super(container);
  }


  @Override
  protected View createView() {
    View view = new ImageButton(ApplicationImpl.getContext()) {
      
	  @Override
      public boolean verifyDrawable(Drawable dr) {
        super.verifyDrawable(dr);
        return true;
      }

    @Override
    public boolean onTouchEvent(MotionEvent event) {

	    switch (event.getAction()) {   
        case MotionEvent.ACTION_DOWN:   
			TouchDown();
            break;   
		case MotionEvent.ACTION_UP:
			TouchUp();
			break;
        default:   
            break;   
        }   
	  return true;
    }


    };




    // Adds the component to its designated container
    view.setFocusable(true);
	view.setOnClickListener(this);
	view.setOnLongClickListener(this);
    return view;
  }

  // Image implementation

  @Override
  public int BackgroundColor() {
    return backgroundColor;
  }

  @Override
  public void TouchDown() {
	EventDispatcher.dispatchEvent(this, "TouchDown");
  }

  @Override
  public void TouchUp() {
	EventDispatcher.dispatchEvent(this, "TouchUp");
  }

  @Override
  public void BackgroundColor(int argb) {
    backgroundColor = argb;
    ImageButton view = (ImageButton) getView();
    view.setBackgroundColor(argb);
    view.invalidate();
  }

  @Override
  public void Click() {
    EventDispatcher.dispatchEvent(this, "Click");
  }

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

  @Override
  public void SetAlpha(int value) {
    ImageButton view = (ImageButton) getView();
	view.setAlpha(value);
  }

  @Override
  public void Picture(String imagePath) {
    try {
      Drawable drawable = ImageUtil.getDrawable(imagePath, ApplicationImpl.getContext(),
          android.R.drawable.picture_frame, true);
      if (drawable != null) {
        ImageButton view = (ImageButton) getView();
        view.setImageDrawable(drawable);
        view.setAdjustViewBounds(true);
      }
    } catch (IOException ioe) {
      throw new NoSuchFileError(imagePath);
    }
  }

}
