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
import com.google.devtools.simple.runtime.components.Progressbar;

import android.view.View;
import android.widget.ProgressBar;
import android.graphics.drawable.Drawable;

/**
 * Label containing a text string or an image.
 *
 * @author Herbert Czymontek
 */
public final class ProgressbarImpl extends ViewComponent implements Progressbar {

  /**
   * Creates a new Label component.
   *
   * @param container  container which will hold the component (must not be
   *                   {@code null}
   */
  private int backgroundColor;

  public ProgressbarImpl(ComponentContainer container) {
    super(container);
  }

  @Override
  protected View createView() {
    android.widget.ProgressBar view = new android.widget.ProgressBar(ApplicationImpl.getContext(),null,android.R.attr.progressBarStyleHorizontal);
	view.setIndeterminate(false);
	view.setMax(100);
	view.setProgress(0);
	return view;
  }

  @Override
  public int Max() {
	ProgressBar view = (ProgressBar) getView();
	return view.getMax();
  }

  @Override
  public void Max(int MaxVal) {
	ProgressBar view = (ProgressBar) getView();
	view.setMax(MaxVal);
  }

  @Override
  public int Value() {
	ProgressBar view = (ProgressBar) getView();
	return view.getProgress();
  }

  @Override
  public void Value(int CValue) {
	ProgressBar view = (ProgressBar) getView();
	view.setProgress(CValue);
  }

  @Override
  public int BackgroundColor() {
    return backgroundColor;
  }

  @Override
  public void BackgroundColor(int argb) {
    backgroundColor = argb;
    ProgressBar view = (ProgressBar) getView();
    view.setBackgroundColor(argb);
    view.invalidate();
  }

}
