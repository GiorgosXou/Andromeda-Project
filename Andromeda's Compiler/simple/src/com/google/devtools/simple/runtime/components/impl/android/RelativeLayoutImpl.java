/*
	LuChengwei 20140306 VB4A
 */

package com.google.devtools.simple.runtime.components.impl.android;

import com.google.devtools.simple.runtime.android.ApplicationImpl;
import com.google.devtools.simple.runtime.components.Component;
import com.google.devtools.simple.runtime.components.RelativeLayout;
import android.view.View;  
import android.view.ViewGroup;
import android.view.ViewGroup.MarginLayoutParams;  

/**
 * Linear layout for placing components horizontally or vertically.
 *
 * @author Herbert Czymontek
 */
public final class RelativeLayoutImpl extends LayoutImpl implements RelativeLayout {

  private int tid=0;
  android.widget.RelativeLayout layoutManager;
   
  RelativeLayoutImpl(ViewComponentContainer container) {
    super(new android.widget.RelativeLayout(ApplicationImpl.getContext()), container);
    layoutManager = (android.widget.RelativeLayout) getLayoutManager();
	//ApplicationImpl.initSC();
  }

  /*
  @Override
  public void Orientation(int newOrientation) {
    ((android.widget.RelativeLayout) getLayoutManager()).setOrientation(
        newOrientation == Component.LAYOUT_ORIENTATION_HORIZONTAL ?
            android.widget.RelativeLayout.HORIZONTAL :
            android.widget.RelativeLayout.VERTICAL);
  }
  */

  //关于布局的位置确定来自于：http://lovesong.blog.51cto.com/3976862/1183335
  //						  http://blog.csdn.net/tabactivity/article/details/9128271
  
  public void Reposition(ViewComponent component,int xp, int yp, int ww, int hh) {
	View view = component.getView();
	android.widget.RelativeLayout.LayoutParams params = new android.widget.RelativeLayout.LayoutParams(
	  ww,hh);
	/*
	  ViewGroup.LayoutParams.WRAP_CONTENT, 
	  ViewGroup.LayoutParams.WRAP_CONTENT
	);
	*/
	params.leftMargin = xp;
	params.topMargin = yp;
	view.setLayoutParams(params);
  }
  
  @Override
  public void addComponent(ViewComponent component) {
	View view = component.getView();
	android.widget.RelativeLayout.LayoutParams params = new android.widget.RelativeLayout.LayoutParams(
	  ViewGroup.LayoutParams.WRAP_CONTENT, 
	  ViewGroup.LayoutParams.WRAP_CONTENT
	);
	layoutManager.addView(view, params);
  }
}
