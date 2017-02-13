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
import com.google.devtools.simple.runtime.components.FrameLayout;
import android.view.View;  
import android.view.Gravity;
import android.app.Activity;
/**
 * Frame layout for Simple components.
 * 
 * @author David Foster
 */
public class FrameLayoutImpl extends LayoutImpl implements FrameLayout {
  
  /**
   * Creates a new frame layout.
   * 
   * @param container  view container
   */
  FrameLayoutImpl(ViewComponentContainer container) {
    super(new android.widget.FrameLayout(ApplicationImpl.getContext()), container);
  }

//尝试使用xy布局之FrameLayout http://www.myexception.cn/mobile/409387.html
  
  @Override
  public void addComponent(ViewComponent component) {
	
	android.widget.FrameLayout.LayoutParams parms=new android.widget.FrameLayout.LayoutParams(
		android.widget.FrameLayout.LayoutParams.WRAP_CONTENT, 
		android.widget.FrameLayout.LayoutParams.WRAP_CONTENT);
	getLayoutManager().addView(component.getView(), parms);
	/*
	parms.gravity = Gravity.LEFT|Gravity.TOP;
	parms.leftMargin = xp;
	parms.topMargin = yp;
	component.getView().setLayoutParams(parms);
	*/
  }
}
