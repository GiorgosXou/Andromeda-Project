/*
 * Copyright 2013 Lu Chengwei
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
import com.google.devtools.simple.runtime.components.VB4AWeb;
import com.google.devtools.simple.runtime.errors.NoSuchFileError;
import com.google.devtools.simple.runtime.events.EventDispatcher;
import com.google.devtools.simple.runtime.annotations.SimpleFunction;
import com.google.devtools.simple.runtime.annotations.SimpleProperty;

import android.graphics.drawable.Drawable;
import android.view.View;
import android.webkit.WebView;
import android.webkit.WebSettings;  
import android.webkit.WebViewClient;   
import java.io.IOException;
import android.webkit.WebChromeClient;


/**
 * Component for WebView in VB4A
 *
 * @author Lu Chengwei
 */
public final class VB4AWebImpl extends ViewComponent implements VB4AWeb {

  // Backing for background color
  private int backgroundColor;

  /**
   * Creates a new Image component.
   *
   * @param container  container which will hold the component (must not be
   *                   {@code null}
   */
  public VB4AWebImpl(ComponentContainer container) {
    super(container);
  }


  @Override
  protected View createView() {
    WebView view = new WebView(ApplicationImpl.getContext()) {
      @Override
      public boolean verifyDrawable(Drawable dr) {
        super.verifyDrawable(dr);
        return true;
      }
    };

    // Adds the component to its designated container
    view.setFocusable(true);

    return view;
  }

  // Image implementation

  @Override
  public int BackgroundColor() {
    return backgroundColor;
  }

  @Override
  public void BackgroundColor(int argb) {
    backgroundColor = argb;
    WebView view = (WebView) getView();
    view.setBackgroundColor(argb);
    view.invalidate();
  }


  @Override
  public void LoadURL(String URL) {
	WebView view = (WebView) getView();
//	view.loadUrl(URL);  
	view.setWebViewClient(new WebViewClient(){
                @Override
                public boolean shouldOverrideUrlLoading(WebView view2, String url){
                        view2.loadUrl(url);
                        return false;
                }
        });
	try
	{
		view.loadUrl(URL);  
	}
	catch (Exception ex)
	{
		ex.printStackTrace(); 
	}
  }

  @Override
  public void LoadURL2(String URL) {
	WebView view = (WebView) getView();
	try
	{
		view.loadUrl(URL);  
	}
	catch (Exception ex)
	{
		ex.printStackTrace(); 
	}

  }

  @Override
  public void GoBack() {
	WebView view = (WebView) getView();
	view.goBack(); 
  }

  @Override
  public void Reload() {
	WebView view = (WebView) getView();
	view.reload(); 
  }

  @Override
  public void GoForward() {
	WebView view = (WebView) getView();
	view.goForward(); 
  }

  @Override
  public void Stop() {
	WebView view = (WebView) getView();
	view.stopLoading(); 
  }

  @Override
  public boolean SavePassword() {
	WebView view = (WebView) getView();
	return true;
  }

  @Override
  public void SavePassword(boolean value) {
	WebView view = (WebView) getView();
	WebSettings webSettings = view.getSettings();  
	webSettings.setSavePassword(value);  
  }

  @Override
  public boolean SaveFormData() {
	WebView view = (WebView) getView();
		return true;
  }

  @Override
  public void SaveFormData(boolean value) {
	WebView view = (WebView) getView();
	WebSettings webSettings = view.getSettings();  
	webSettings.setSaveFormData(value);  
  }

  @Override
  public boolean JSEnabled() {
	WebView view = (WebView) getView();
		return true;
  }

  @Override
  public void JSEnabled(boolean value) {
	WebView view = (WebView) getView();
	WebSettings webSettings = view.getSettings();  
	webSettings.setJavaScriptEnabled(value);  
  }

  @Override
  public boolean ZoomEnabled() {
	WebView view = (WebView) getView();
		return true;
  }

  @Override
  public void ZoomEnabled(boolean value) {
	WebView view = (WebView) getView();
	WebSettings webSettings = view.getSettings();  
	webSettings.setSupportZoom(value);  
  }

  @Override
  public void BuildinZoom(boolean value) {
	WebView view = (WebView) getView();
	WebSettings webSettings = view.getSettings();  
	webSettings.setBuiltInZoomControls(value);  
  }


  @Override
  public boolean BuildinZoom() {
	WebView view = (WebView) getView();
	return true;
  }

  @Override
  public short Progress() {
	WebView view = (WebView) getView();
	return 0;
  }

  @Override
  public void ProgressChanged(int prgs){
	WebView view = (WebView) getView();
	EventDispatcher.dispatchEvent(this, "ProgressChanged", prgs);
  }
}
