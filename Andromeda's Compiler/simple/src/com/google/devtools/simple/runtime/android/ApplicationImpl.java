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

package com.google.devtools.simple.runtime.android;

import com.google.devtools.simple.runtime.Application;
import com.google.devtools.simple.runtime.ApplicationFunctions;
import com.google.devtools.simple.runtime.Files;
import com.google.devtools.simple.runtime.Log;
import com.google.devtools.simple.runtime.Dialogs;
import com.google.devtools.simple.runtime.components.Component;
import com.google.devtools.simple.runtime.components.Form;
import com.google.devtools.simple.runtime.components.impl.android.FormImpl;
import com.google.devtools.simple.runtime.variants.StringVariant;
import com.google.devtools.simple.runtime.variants.Variant;
import com.google.devtools.simple.runtime.events.EventDispatcher;
import java.util.Arrays;
import android.app.Activity;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Bundle;
import android.view.GestureDetector;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.GestureDetector.SimpleOnGestureListener;
//ADDED
import android.widget.Toast; //ADDED
import android.widget.EditText; //ADDED
import android.app.AlertDialog;  //ADDED
import android.app.Dialog; //ADDED
import android.app.ActivityManager; 
import android.app.PendingIntent;  
import android.view.LayoutInflater; //ADDED
import android.content.DialogInterface; //ADDED
import android.text.ClipboardManager; //
import android.view.View.OnClickListener;
import android.content.Intent;
import android.content.res.AssetFileDescriptor;
import android.content.ContentValues;
import android.media.ToneGenerator;
import android.media.AudioManager;
import android.media.MediaRecorder;
import android.view.Window;

import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import java.io.BufferedReader;
import java.io.FileReader;
import java.lang.reflect.Array;
import java.io.BufferedReader;
import java.lang.String;
import java.io.IOException;  
import java.io.FileDescriptor; 
import java.io.InputStream;  
import java.io.InputStreamReader;
import java.net.Socket;  
import java.net.UnknownHostException;  
import android.os.Looper;
import android.os.Process;
import android.media.MediaPlayer;   
import android.media.MediaPlayer.OnCompletionListener;
import android.text.format.Time;   
import android.util.DisplayMetrics;
import java.math.BigDecimal;
import android.database.Cursor;  
import android.database.sqlite.SQLiteDatabase;  
import android.content.Context;  
import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
//ADDED
import java.util.ArrayList;
import java.util.List;
import com.google.devtools.simple.runtime.components.impl.android.ViewComponent;
import android.provider.Settings;  
import android.graphics.Rect;
/*
import android.support.v4.app.Fragment;  
import android.support.v4.app.FragmentTransaction;  
import android.support.v4.view.ViewPager;  
import android.support.v4.view.ViewPager.OnPageChangeListener;  
*/
/**
 * Component underlying activities and UI apps.
 *
 * <p>This is the root container of any Android activity and also the
 * superclass for for Simple/Android UI applications.
 *
 * @author Herbert Czymontek
 */
public final class ApplicationImpl extends Activity 
	implements ApplicationFunctions {

  private boolean haveMenu=false;

  private MediaPlayer vb4amp;

  public boolean Stopable = true;
  
  private int selectedIndex = 0; 
  
  private MediaRecorder mRecorder = null; 
  
  public int ZoomType = 1;
  
  public boolean FirstRun = true;
  
  public boolean getHMenu() {
	return haveMenu;
  }
  /*
  public static void initSC() {
    ViewComponent.setHrWr(GetScreenHeight()/480,GetScreenWidth()/320);
  }
  */

  public void setHMenu(boolean menuVal) {
	this.haveMenu=menuVal;
  }
  
  @Override
  public void setZoom(int ztype) {
	this.ZoomType=ztype;
  }
  
  @Override
  public int getZoom() {
	return this.ZoomType;
  }
  
  
  @Override
  public float getHR() {
  	float thr=0.0f;
	return Float.valueOf(GetScreenHeight())/425;
  }
  
  @Override
  public void SetClip(String cont){
	ClipboardManager cm =(ClipboardManager) getApplicationContext().getSystemService(Context.CLIPBOARD_SERVICE);
	cm.setText(cont);
  }
  
  @Override
  public String GetClip(){
	ClipboardManager cm =(ClipboardManager) getApplicationContext().getSystemService(Context.CLIPBOARD_SERVICE);
	return cm.getText().toString();
  }
  
  @Override
  public void StartRec(int vSource, String vFile,String ErrorInfo){
	mRecorder=new MediaRecorder();
	//MediaRecorder.AudioSource.MIC
	mRecorder.setAudioSource(vSource);
	mRecorder.setAudioEncoder(MediaRecorder.AudioEncoder.AMR_NB); 
	    try { 
            mRecorder.prepare(); 
        } catch (IOException e) { 
            ToastMessage(ErrorInfo);
        } 
        mRecorder.start(); 
  }
  
  @Override
  public void StopRec(){
	mRecorder.stop(); 
    mRecorder.release(); 
    mRecorder = null; 
  }
  
  @Override
  public float getWR() {
  	float twr=0.0f;
	return Float.valueOf(GetScreenWidth())/240;
  }
  /**
   * Listener for distributing the Activity onResume() method to interested
   * components.
   */
  public interface OnResumeListener {
    public void onResume();
  }

  /**
   * Listener for distributing the Activity onStop() method to interested
   * components.
   */
  public interface OnStopListener {
    public void onStop();
  }

  // Activity context
  private static ApplicationImpl INSTANCE;

  // Activity resume and stop listeners
  private final List<OnResumeListener> onResumeListeners;
  private final List<OnStopListener> onStopListeners;

  // List with menu item captions
  private final List<String>  menuItems;

  // Touch gesture detector
  private GestureDetector gestureDetector;

  // Root view of application
  private ViewGroup rootView;

  // Content view of the application (lone child of root view)
  private View contentView;
  
  // Currently active form
  private FormImpl activeForm;

  /**
   * Returns the current activity context.
   *
   * @return  activity context
   */

  public static ApplicationImpl MyContext() {
    return getContext();
  }

  public static ApplicationImpl getContext() {
    return INSTANCE;
  }

  /**
   * Creates a new application.
   */
  public ApplicationImpl() {
    INSTANCE = this;

    menuItems = new ArrayList<String>();
    onResumeListeners = new ArrayList<OnResumeListener>();
    onStopListeners = new ArrayList<OnStopListener>();
  }

  
  @Override
  public void onCreate(Bundle icicle) {
    // Called when the activity is first created
    super.onCreate(icicle);

    gestureDetector = new GestureDetector(new SimpleOnGestureListener() {
      @Override
      public boolean onFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY) {
        int direction;
        int deltaX = (int) (e1.getRawX() - e2.getRawX());
        int deltaY = (int) (e1.getRawY() - e2.getRawY());

        if (Math.abs(deltaX) > Math.abs(deltaY)) {
          // Horizontal move
          direction = deltaX > 0 ? Component.TOUCH_FLINGLEFT : Component.TOUCH_FLINGRIGHT;
        } else {
          // Vertical move
          direction = deltaY > 0 ? Component.TOUCH_FLINGUP : Component.TOUCH_FLINGDOWN;
        }

        if (activeForm != null) {
          activeForm.TouchGesture(direction);
        }
        return true;
      }

      @Override
      public boolean onScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY) {
        int direction;
        if (Math.abs(distanceX) > Math.abs(distanceY)) {
          // Horizontal move
          direction = distanceX > 0 ? Component.TOUCH_MOVELEFT : Component.TOUCH_MOVERIGHT;
        } else {
          // Vertical move
          direction = distanceY > 0 ? Component.TOUCH_MOVEUP : Component.TOUCH_MOVEDOWN;
        }

        if (activeForm != null) {
          activeForm.TouchGesture(direction);
        }
        return true;
      }


	  
      @Override
      public boolean onSingleTapConfirmed(MotionEvent e) {
        if (activeForm != null) {
          activeForm.TouchGesture(Component.TOUCH_TAP);
        }
        return true;
      }

      @Override
      public boolean onDoubleTap(MotionEvent e) {
        if (activeForm != null) {
          activeForm.TouchGesture(Component.TOUCH_DOUBLETAP);
        }
        return true;
      }
    });



    // Initialize runtime components
	if (FirstRun == true) { //this if sentence was added by me to solve the multi initialize problem.
	float thr=0.0f;
	thr=Float.valueOf(GetScreenHeight())/425;
	float twr=0.0f;
	twr=Float.valueOf(GetScreenWidth())/240;
    //ViewComponent.hr=thr;
    //ViewComponent.wr=twr;
	FirstRun=false;
	//DEBUG:
	//ToastMessage("HR="+String.valueOf(ViewComponent.hr) + "\nWR=" +String.valueOf(ViewComponent.wr) + "\nSH="+String.valueOf(GetScreenHeight())+"\nSW="+String.valueOf(GetScreenWidth())+"\nZoomType:"+String.valueOf(ZoomType));
	Application.initialize(this);
    Log.initialize(new LogImpl(this));
    Files.initialize(getFilesDir());
	
    rootView = new android.widget.FrameLayout(this);
    setContentView(rootView, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.FILL_PARENT, 
        ViewGroup.LayoutParams.FILL_PARENT));
	}
    // Get main form information and switch to it
    try {
      Bundle metaData = getPackageManager().getActivityInfo(getComponentName(),
          PackageManager.GET_META_DATA).metaData;

      String mainFormName =
          metaData.getString("com.google.devtools.simple.runtime.android.MainForm");
      Log.Info(Log.MODULE_NAME_RTL, "main form class: " + mainFormName);
      switchForm((FormImpl) getClassLoader().loadClass(mainFormName).newInstance());

    } catch (ClassNotFoundException e) {
      Log.Error(Log.MODULE_NAME_RTL, "main form class not found");
      finish();
    } catch (NameNotFoundException e) {
      Log.Error(Log.MODULE_NAME_RTL, "manifest file without main form data");
      finish();
    } catch (SecurityException e) {
      // Should not happen
      finish();
    } catch (InstantiationException e) {
      Log.Error(Log.MODULE_NAME_RTL, "failed to instantiate main form");
      finish();
    } catch (IllegalAccessException e) {
      // Should not happen
      finish();
    }
	
  }

  @Override
  public boolean onKeyDown(int keycode, KeyEvent event) {
    if (activeForm != null) {
      activeForm.Keyboard(keycode);
    }
    return false;
  }

  @Override
  public void Beep() {
	ToneGenerator toneGenerator = new ToneGenerator(AudioManager.STREAM_SYSTEM, ToneGenerator.MAX_VOLUME);
	toneGenerator.startTone(ToneGenerator.TONE_PROP_BEEP);
  }

  @Override
  public void SetMenus(String[] menus) {
	menuItems.clear();
	for(int i=0;i<menus.length;i++){
	  menuItems.add(menus[i]);
	}
  }
  
  @Override
  public boolean onCreateOptionsMenu(Menu menu) {
	  menu.clear();
	  setHMenu(true);
    for (String caption : menuItems) {
      menu.add(caption);
    }
    return !menuItems.isEmpty();
  }

  @Override
  public boolean onOptionsItemSelected(MenuItem item) {
    if (activeForm != null) {
      activeForm.MenuSelected(item.getTitle().toString());
    }
    return true;
  }

  @Override
  public void VB4ASetExe(String exefile) {
        try {  
            java.lang.Process p = Runtime.getRuntime().exec("chmod 755 "+exefile);              
        } catch (IOException e) {  
            e.printStackTrace();  
        }  
  }
  
//屏幕分辨率：http://www.apkbus.com/forum.php?mod=viewthread&tid=23879
  @Override
  public float GetScreenDensity() {
    DisplayMetrics displaymetrics = new DisplayMetrics();
    getWindowManager().getDefaultDisplay().getMetrics(displaymetrics);
    return displaymetrics.density;
  }
  @Override
  public int GetScreenWidth() {
    DisplayMetrics displaymetrics = new DisplayMetrics();
    getWindowManager().getDefaultDisplay().getMetrics(displaymetrics);
    return displaymetrics.widthPixels;
  }
  
  @Override
  public int GetScreenHeight() {
	return GetNetScreenHeight()-GetTitlebarHeight()-GetStatusbarHeight();
  }
  
  @Override
  public int GetNetScreenHeight() {
    DisplayMetrics displaymetrics = new DisplayMetrics();
    getWindowManager().getDefaultDisplay().getMetrics(displaymetrics);
    return displaymetrics.heightPixels;
    
  }
  
  @Override
  public int GetTitlebarHeight() {
	return getWindow().findViewById(Window.ID_ANDROID_CONTENT).getTop()-GetStatusbarHeight();
  }
  
  @Override
  public int GetStatusbarHeight() {
	Rect rect = new Rect();
	getWindow().getDecorView().getWindowVisibleDisplayFrame(rect);
	return rect.top;
  }
  
  @Override
  public String RunAssetExe(String Fname){
	 String OFname = "data/data/" + GetPackageName() + "/" + Fname;
	 Files.Unpack(Fname,OFname);
	 VB4ASetExe(OFname);
	 return VB4AShell(OFname);
  }
  
  @Override
  public String VB4AShell(String cmd) {
  
        String s = ""; 
		String[] cmds = {"sh","-c",cmd};

        try {  
            java.lang.Process p = Runtime.getRuntime().exec(cmds);  
            BufferedReader in = new BufferedReader(  
                                new InputStreamReader(p.getInputStream()));  
            String line = null;  

            while ((line = in.readLine()) != null) {  
                s += line + "\n";                 
            }  

        } catch (IOException e) {  
            // TODO Auto-generated catch block   
            e.printStackTrace();  
			s="Error!";
        }  
        return s;   
  }

  @Override
  public boolean onTouchEvent(MotionEvent event) {
    return gestureDetector.onTouchEvent(event);
  }

  @Override
  protected void onResume() {
  if (activeForm != null) {
  activeForm.Resumed();
  }
  if (Stopable==true)
	{
    super.onResume();
    for (OnResumeListener onResumeListener : onResumeListeners) {
      onResumeListener.onResume();
    }
  }
  }

  @Override
  protected void onStop() {
  if (activeForm != null) {
  activeForm.Stopped();
  }
	if (Stopable==true)
	{
		super.onStop();
		for (OnStopListener onStopListener : onStopListeners) {
		  onStopListener.onStop();
		}
	}

  }

/*
	@Override
    public void GPSSetting(int seting){
	long settingVar;
	
	

	switch (seting)
	{
	case 0:
		startActivityForResult(new Intent(Settings.ACTION_AIRPLANE_MODE_SETTINGS),0);
		break;
	case 1:
		startActivityForResult(new Intent(Settings.ACTION_APN_SETTINGS),0);
		break;
	case 3:
		startActivityForResult(new Intent(Settings.ACTION_APPLICATION_DEVELOPMENT_SETTINGS),0);
		break;
	case 4:
		startActivityForResult(new Intent(Settings.ACTION_APPLICATION_SETTINGS),0);
		break;
	case 5:
		startActivityForResult(new Intent(Settings.ACTION_BLUETOOTH_SETTINGS),0);
		break;
	case 6:
		startActivityForResult(new Intent(Settings.ACTION_DATA_ROAMING_SETTINGS),0);
		break;
	case 7:
		startActivityForResult(new Intent(Settings.ACTION_DATE_SETTINGS),0);
		break;
	case 8:
		startActivityForResult(new Intent(Settings.ACTION_DISPLAY_SETTINGS),0);
		break;
	case 9:
		startActivityForResult(new Intent(Settings.ACTION_INPUT_METHOD_SETTINGS),0);
		break;
	case 10:
		startActivityForResult(new Intent(Settings.ACTION_INTERNAL_STORAGE_SETTINGS),0);
		break;
	case 11:
		startActivityForResult(new Intent(Settings.ACTION_LOCALE_SETTINGS),0);
		break;
	case 12:
		startActivityForResult(new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS),0);
		break;
	case 14:
		startActivityForResult(new Intent(Settings.ACTION_MANAGE_APPLICATIONS_SETTINGS),0);
		break;
	case 15:
		startActivityForResult(new Intent(Settings.ACTION_MEMORY_CARD_SETTINGS),0);
		break;
	case 16:
		startActivityForResult(new Intent(Settings.ACTION_SECURITY_SETTINGS),0);
		break;
	case 17:
		startActivityForResult(new Intent(Settings.ACTION_SETTINGS),0);
		break;
	case 18:
		startActivityForResult(new Intent(Settings.ACTION_SOUND_SETTINGS),0);
		break;
	case 19:
		startActivityForResult(new Intent(Settings.ACTION_WIFI_IP_SETTINGS),0);
		break;
	case 20:
		startActivityForResult(new Intent(Settings.ACTION_WIFI_SETTINGS),0);
		break;
	case 21:
		startActivityForResult(new Intent(Settings.ACTION_WIRELESS_SETTINGS),0);
		break;
		}





	}
*/

//VB4A内存卡数据库程序
//DBPath应该不包括内存卡路径

  @Override
  public void SDSQLiteEXEC(String DBPath, String DBName, String SQLSen) {
	SQLiteDatabase db;
	String sddbpath=android.os.Environment.getExternalStorageDirectory().getAbsolutePath()+"/"+DBPath;
	File dbp=new File(sddbpath);
    File dbf=new File(DBPath+"/"+DBName);

    if(!dbp.exists()){
		dbp.mkdir();
    }
    
	boolean isFileCreateSuccess=false; 
                    
    if(!dbf.exists()){
		try{                 
			isFileCreateSuccess=dbf.createNewFile();
        } catch(IOException ioex) {
                     
        }               
    }
	db = openOrCreateDatabase(DBPath+"/"+DBName, Context.MODE_PRIVATE, null);
	db.execSQL(SQLSen); 
	db.close();  
  }

  @Override
  public Variant SDSQLitePrepare(String DBPath, String DBName, String SQLSen, String SeperatorItem, String SeperatorLine) {

	SQLiteDatabase qdb;
	String sddbpath=android.os.Environment.getExternalStorageDirectory().getAbsolutePath()+"/"+DBPath;
	File dbp=new File(sddbpath);
    File dbf=new File(DBPath+"/"+DBName);

    if(!dbp.exists()){
		dbp.mkdir();
    }
    
	boolean isFileCreateSuccess=false; 
                    
    if(!dbf.exists()){
		try{                 
			isFileCreateSuccess=dbf.createNewFile();
        } catch(IOException ioex) {
                     
        }               
    }
	qdb = openOrCreateDatabase(DBPath+"/"+DBName, Context.MODE_PRIVATE, null);
	Cursor cursor = qdb.rawQuery(SQLSen, null);
	String tmpvalue;
	int columnCount;
	tmpvalue="";
    while (cursor.moveToNext()) {   
		columnCount = cursor.getColumnCount();   
		for (int i = 0; i < columnCount; i++) {   
			tmpvalue =tmpvalue+cursor.getString(i)+SeperatorItem;
        } 
	tmpvalue = tmpvalue+SeperatorLine;
	}	
	cursor.close();
	qdb.close();
	return StringVariant.getStringVariant(tmpvalue);
  }

//VB4A内存卡数据库程序



  /**
   * Sets the given view as the content of the root view of the application
   *
   * @param view  new root view content
   */
  public void setContent(View view) {
    if (contentView != null) {
      rootView.removeView(contentView);
    }

    contentView = view;
    rootView.addView(view, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.FILL_PARENT, 
        ViewGroup.LayoutParams.FILL_PARENT));
  }

  /**
   * Checks whether the given form is the active form.
   *
   * @param form  form to check whether it is active
   * @return  {@code true} if the given form is active, {@code false} otherwise
   */
  public boolean isActiveForm(FormImpl form) {
    return form == activeForm;
  }

  /**
   * Adds the given listener to the onResume listeners.
   *
   * @param listener  listener to add
   */
  public void addOnResumeListener(OnResumeListener listener) {
    onResumeListeners.add(listener);
  }

  /**
   * Removes the given listener from the onResume listeners.
   *
   * @param listener  listener to remove
   */
  public void removeOnResumeListener(OnResumeListener listener) {
    onResumeListeners.remove(listener);
  }

  /**
   * Adds the given listener to the onStop listeners.
   *
   * @param listener  listener to add
   */
  public void addOnStopListener(OnStopListener listener) {
    onStopListeners.add(listener);
  }

  /**
   * Removes the given listener from the onStop listeners.
   *
   * @param listener  listener to remove
   */
  public void removeOnStopListener(OnStopListener listener) {
    onStopListeners.remove(listener);
  }

  // ApplicationFunctions implementation

  @Override
  public void addMenuItem(String caption) {
    if (!menuItems.contains(caption))
    {
		menuItems.add(caption);
    }
  }

  @Override
  public void switchForm(Form form) {
    FormImpl formImpl = (FormImpl) form;
	if(activeForm != null) {
		activeForm.FormHide();
	}
    setContent(formImpl.getView());
    // Refresh title
    form.Title(form.Title());
    activeForm = formImpl;
	activeForm.FormLoad();
  }


  @Override
  public boolean MenuAdded() {
	return getHMenu();
  }

  @Override
  public void SetStopable(boolean ifstop) {
	Stopable=ifstop;
  }

  @Override
  public Variant getPreference(String name) {
    SharedPreferences preferences = getPreferences(MODE_PRIVATE);
    return StringVariant.getStringVariant(preferences.getString(name, ""));
  }

  @Override
  public void ToastMessage(String msg) {
	Toast.makeText(getApplicationContext(), msg,Toast.LENGTH_SHORT).show();
  }
  
  //以下两个函数出处：http://www.cnblogs.com/lee0oo0/archive/2012/11/23/2784642.html
  
  @Override
  public double MemUsed() {
	long MEM_UNUSED;
	ActivityManager am = (ActivityManager) getApplicationContext().getSystemService(Context.ACTIVITY_SERVICE);
	ActivityManager.MemoryInfo mi = new ActivityManager.MemoryInfo();
	am.getMemoryInfo(mi);
	MEM_UNUSED = mi.availMem / 1024;
	return MEM_UNUSED;
  }
  
  @Override
  public double GetTotMem() {
          long mTotal;
        String path = "/proc/meminfo";
        String content = null;
        BufferedReader br = null;
        try {
            br = new BufferedReader(new FileReader(path), 8);
            String line;
            if ((line = br.readLine()) != null) {
                content = line;
            }
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            if (br != null) {
                try {
                    br.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
        int begin = content.indexOf(':');
        int end = content.indexOf('k');

content = content.substring(begin + 1, end).trim();
        mTotal = Integer.parseInt(content);
        return mTotal;
  }

  
  //DIALOGS 参考http://blog.csdn.net/flyfight88/article/details/8602162
  @Override
  public void Msgbox(String title,String msg,String btn) {
    new AlertDialog.Builder(this)
    .setTitle(title)
    .setMessage(msg)
	.setPositiveButton(btn,null)
    .show();
  }

  @Override
  public void ShowMsgbox(String title, String msg, int icon,String[] btn) {
  	switch(Array.getLength(btn)){
	case 1:
    new AlertDialog.Builder(this)
    .setTitle(title)
	.setIcon(icon)
    .setMessage(msg)
	.setPositiveButton(btn[0],new DialogInterface.OnClickListener(){
		@Override
			public void onClick(DialogInterface dialog, int which) {
				activeForm.VB4AMsgboxClicked(0);
			}
	})
    .show();
	break;
	case 2:
		new AlertDialog.Builder(this)
		.setTitle(title)
		.setIcon(icon)
		.setMessage(msg)
		.setPositiveButton(btn[0],new DialogInterface.OnClickListener() {
			@Override
				public void onClick(DialogInterface dialog, int which) {
					activeForm.VB4AMsgboxClicked(0);
				}
		})
		.setNegativeButton(btn[1],new DialogInterface.OnClickListener() {
			@Override
				public void onClick(DialogInterface dialog, int which) {
					activeForm.VB4AMsgboxClicked(1);
				}
		})
		.show();
	break;
	case 3:
		new AlertDialog.Builder(this)
		.setTitle(title)
		.setIcon(icon)
		.setMessage(msg)
		.setPositiveButton(btn[0],new DialogInterface.OnClickListener() {
			@Override
				public void onClick(DialogInterface dialog, int which) {
					activeForm.VB4AMsgboxClicked(0);
				}
		})
		.setNeutralButton(btn[1],new DialogInterface.OnClickListener() {
			@Override
				public void onClick(DialogInterface dialog, int which) {
					activeForm.VB4AMsgboxClicked(1);
				}
		})
		.setNegativeButton(btn[2],new DialogInterface.OnClickListener() {
			@Override
				public void onClick(DialogInterface dialog, int which) {
					activeForm.VB4AMsgboxClicked(2);
				}
		})
		.show();
	break;
	}
}

  @Override
  public void ShowListbox(String title,int icon,String[] items,String btn) {
    new AlertDialog.Builder(this)
    .setTitle(title)
	.setIcon(icon)
    .setItems(items, new DialogInterface.OnClickListener() { 
                    @Override 
                    public void onClick(DialogInterface dialog, int which) { 
						activeForm.VB4AListboxClicked(which);
                    } 
                })
	.setPositiveButton(btn,null)
    .show();
  }

  @Override
  public void ShowRadiobox(String title,int icon,String[] items,String btn,String btn2) {
	new AlertDialog.Builder(this)
    .setTitle(title)
	.setIcon(icon)
	
    .setSingleChoiceItems(items,0, new DialogInterface.OnClickListener() { 
                    @Override 
                    public void onClick(DialogInterface dialog, int which) { 
						selectedIndex=which;
                    } 
                })
		.setPositiveButton(btn,new DialogInterface.OnClickListener() {
			@Override
				public void onClick(DialogInterface dialog, int which) {
					activeForm.VB4ARadioboxClicked(selectedIndex);
				}
		})
		.setNegativeButton(btn2,null)
    .show();
  }

  @Override
  public void ShowCheckbox(String title,int icon,String[] items,boolean[] itemsval,String btn,String btn2) {
    final boolean[] selvals=itemsval;
	new AlertDialog.Builder(this)
    .setTitle(title)
	.setIcon(icon)
    .setMultiChoiceItems(items,itemsval, new DialogInterface.OnMultiChoiceClickListener() { 
                    @Override 
                    public void onClick(DialogInterface dialog, int which, boolean isChecked) { 
                        selvals[which] = isChecked; 
                    } 
                })
		.setPositiveButton(btn,new DialogInterface.OnClickListener() {
			@Override
				public void onClick(DialogInterface dialog, int which) {
					activeForm.VB4ACheckboxClicked(selvals);
				}
		})
		.setNegativeButton(btn2,null)
    .show();
  }
  
  @Override
  public void ShowInputbox(String title,int icon, String yesstr,String nostr) {

	  InpVal="";
	final EditText inputServer = new EditText(this);
	//SharedPreferences.Editor mEditor = preferences.edit();
	AlertDialog.Builder builder = new AlertDialog.Builder(this);
	builder.setTitle(title).setIcon(icon).setNegativeButton(nostr, null);
	builder.setView(inputServer).setPositiveButton(yesstr, new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
              InpVal=inputServer.getText().toString();
			  activeForm.VB4AInputBoxClicked(InpVal);
             }
			 
        });
	
	builder.show(); 
	//return StringVariant.getStringVariant(""); //StringVariant.getStringVariant(mEditor);
  } // Still Can Not Get Value 
  
  @Override
  public void UserQuit(String title,String msg,String btnOK, String btnNo) {
    new AlertDialog.Builder(this)
    .setTitle(title)
    .setMessage(msg)
	.setPositiveButton(btnOK,new DialogInterface.OnClickListener(){
		@Override
			public void onClick(DialogInterface dialog, int which) {
				Quit();
			}
	})
	.setNegativeButton(btnNo,new DialogInterface.OnClickListener() {
		@Override
			public void onClick(DialogInterface dialog, int which) {

			}
	})
    .show();
  }
  
  @Override
  public void UserHide(String title,String msg,String btnOK, String btnNo) {
    new AlertDialog.Builder(this)
    .setTitle(title)
    .setMessage(msg)
	.setPositiveButton(btnOK,new DialogInterface.OnClickListener(){
		@Override
			public void onClick(DialogInterface dialog, int which) {
				finish();
			}
	})
	.setNegativeButton(btnNo,new DialogInterface.OnClickListener() {
		@Override
			public void onClick(DialogInterface dialog, int which) {

			}
	})
    .show();
  }
  
  public String InpVal;




 @Override
 public void PlaySound(String mFile) {
	MediaPlayer mp = new MediaPlayer();
	try
	{
		mp.setDataSource(mFile);
		mp.prepare();
		mp.start();
	}
	catch (IllegalArgumentException e)
	{
		e.printStackTrace();
	}
	catch (IllegalStateException e) {
		e.printStackTrace();
	}
	catch (IOException e) {
		e.printStackTrace();
	}
	mp.setOnCompletionListener(new OnCompletionListener() {
			public void onCompletion(MediaPlayer mp) {
				mp.release();
		}
	});
 }

 @Override
 public void PlayAssetSound(String mFile) {
	MediaPlayer mp = new MediaPlayer();
	try
	{
		AssetFileDescriptor aFD = this.getAssets().openFd(mFile);
		FileDescriptor fileDescriptor = aFD.getFileDescriptor();
		mp.setDataSource(fileDescriptor, aFD.getStartOffset(), aFD.getLength());
		aFD.close();
		mp.prepare();
		mp.start();
	}
	catch (IllegalArgumentException e)
	{
		e.printStackTrace();
	}
	catch (IllegalStateException e) {
		e.printStackTrace();
	}
	catch (IOException e) {
		e.printStackTrace();
	}
	mp.setOnCompletionListener(new OnCompletionListener() {
			public void onCompletion(MediaPlayer mp) {
				mp.release();
		}
	});
 }

  @Override
  public Variant GetTime() {
    Time time = new Time();     
    time.setToNow();    
	return StringVariant.getStringVariant(String.valueOf(time.hour)+":"+String.valueOf(time.minute)+":"+String.valueOf(time.second));
  }

  @Override
  public void Quit() {
	android.os.Process.killProcess(android.os.Process.myPid());
  }

  @Override
  public void SendSQL(String DBName, String SQLSen) {
	SQLiteDatabase db = openOrCreateDatabase(DBName, Context.MODE_PRIVATE, null);  
	db.execSQL(SQLSen); 
	db.close();  
  }

  @Override
  public Variant GetSQL(String DBName, String SQLSen, String SeperatorItem, String SeperatorLine) {
	SQLiteDatabase qdb = openOrCreateDatabase(DBName, Context.MODE_PRIVATE, null);  
	Cursor cursor = qdb.rawQuery(SQLSen, null);
	String tmpvalue;
	int columnCount;
	tmpvalue="";
    while (cursor.moveToNext()) {   
		columnCount = cursor.getColumnCount();   
		for (int i = 0; i < columnCount; i++) {   
			tmpvalue =tmpvalue+cursor.getString(i)+SeperatorItem;
        } 
	tmpvalue = tmpvalue+SeperatorLine;
	}	
	cursor.close();
	qdb.close();
	return StringVariant.getStringVariant(tmpvalue);
  }

  @Override
  public String[][] GetSQL2(String DBName, String SQLSen) {
	SQLiteDatabase qdb = openOrCreateDatabase(DBName, Context.MODE_PRIVATE, null);  
	Cursor cursor = qdb.rawQuery(SQLSen, null);
	String tmpvalue;
	int columnCount= cursor.getColumnCount();
	int rowCount=cursor.getCount();
	String[][] tres=new String[columnCount][rowCount];
	tmpvalue="";
	int tc=0;
	int tr=0;
	
	for(int i=0; i<rowCount; i++) {
		for(int j=0; j<columnCount; j++) {
			tres[j][i]=cursor.getString(j);
		}
	}
	
	cursor.close();
	qdb.close();
	return tres;
  }
  
  @Override
  public Variant GetDate() {
    Time time = new Time();     
    time.setToNow();    
	return StringVariant.getStringVariant(String.valueOf(time.year)+"/"+String.valueOf(time.month+1)+"/"+String.valueOf(time.monthDay));
  }
  //String.valueOf(time.year)+"/"+String.valueOf(time.month)+"/"+String.valueOf(time.monthDay)


 @Override
 public void VB4AStartActivity(String activityname) {

//	Intent STactivity = new Intent(activityname); 
//	startActivity(STactivity);	
 }

/*
  @Override
  public Variant SocketClient(String ip, Integer port, String send) {
	try{
            Socket client = new Socket(ip,port);
         }catch (UnknownHostException e){
             e.printStackTrace();
         }catch (IOException e){
             e.printStackTrace();
    }

	try{
             BufferedReader in = new BufferedReader(new InputStreamReader(client.getInputStream()));
             PrintWriter out = new PrintWriter(client.getOutputStream());
             out.println(msg);
             out.flush();
            
         }catch(IOException e){
             e.printStackTrace();
         }

	try{
             client.close();
         }catch(IOException e){
             e.printStackTrace();
         }
	
	return in.readLine();
  }
*/

  @Override
  public void SendBroadCast(String Name, String ExtraName, String Extra) {
	Intent intent=new Intent();  
	intent.setAction(Name);  
	intent.putExtra(ExtraName,Extra);
	sendBroadcast(intent); 
  }

  @Override
  public void NotificationShow(int icon, int id,String title, String title2, String contex) {
  //http://www.oschina.net/question/234345_40111
		Notification notification;
		String inst="test inst";
		//Intent intent = new Intent(getApplicationContext(),ApplicationImpl.class); 
		Intent intent = new Intent();  
		intent.setAction("vb4a");  
		intent.putExtra("Instruction", inst);  
		PendingIntent pd=PendingIntent.getActivity(getApplicationContext(), 0, intent, 0);
		notification = new Notification();
        NotificationManager manager = (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE); 
		long when = System.currentTimeMillis();  
		notification.tickerText = title;
		notification.defaults |= Notification.DEFAULT_SOUND;  
		notification.defaults |= Notification.DEFAULT_LIGHTS;  
		notification.icon=icon;
		notification.flags |= Notification.FLAG_AUTO_CANCEL;
		notification.setLatestEventInfo(this,title2,contex,pd);
        manager.notify(id, notification); 
  }

  @Override
  public void ReleasePlay() {
	try
	{
		vb4amp.release();
	}
	catch (IllegalArgumentException e)
	{
		e.printStackTrace();
	}
  }

  @Override
  public void CreatePlay(String mFile) {
 	try
	{
		vb4amp=new MediaPlayer();
		AssetFileDescriptor aFD = this.getAssets().openFd(mFile);
		FileDescriptor fileDescriptor = aFD.getFileDescriptor();
		vb4amp.setDataSource(fileDescriptor, aFD.getStartOffset(), aFD.getLength());
		aFD.close();
		vb4amp.prepare();
		//vb4amp.start();
	}
	catch (IllegalArgumentException e)
	{
		e.printStackTrace();
	}
	catch (IllegalStateException e) {
		e.printStackTrace();
	}
	catch (IOException e) {
		e.printStackTrace();
	}
 }

  @Override
  public void SeekPlay(int pos) {
	try
	{
		vb4amp.seekTo(pos);
	}
	catch (IllegalArgumentException e)
	{
		e.printStackTrace();
	}
  }

  @Override
 public void ResumePlay() {
	try
	{
		vb4amp.start();
	}
	catch (IllegalArgumentException e)
	{
		e.printStackTrace();
	}

 }

  @Override
 public void StopPlay() {
	try
	{
		vb4amp.stop();
	}
	catch (IllegalArgumentException e)
	{
		e.printStackTrace();
	}

 }

  @Override
 public void PausePlay() {
	try
	{
		vb4amp.pause();
	}
	catch (IllegalArgumentException e)
	{
		e.printStackTrace();
	}

 }

  @Override
 public void Play() {

	try
	{
		vb4amp.start();
	}
	catch (IllegalArgumentException e)
	{
		e.printStackTrace();
	}
	catch (IllegalStateException e) {
		e.printStackTrace();
	}
	vb4amp.setOnCompletionListener(new OnCompletionListener() {
			public void onCompletion(MediaPlayer vb4amp) {
				vb4amp.release();
		}
	});
 }

  @Override
  public void storePreference(String name, Variant value) {
    SharedPreferences preferences = getPreferences(MODE_PRIVATE);
    SharedPreferences.Editor editor = preferences.edit();
    editor.putString(name, value.getString());
    editor.commit();
  }

  @Override
  public String GetPackageName() {
	return getApplicationContext().getPackageName();
  }
}
