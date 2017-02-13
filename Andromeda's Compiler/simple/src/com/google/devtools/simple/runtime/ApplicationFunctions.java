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

package com.google.devtools.simple.runtime;

import com.google.devtools.simple.runtime.components.Form;
import com.google.devtools.simple.runtime.variants.Variant;

/**
 * Interface for application related functions. Not accessible to Simple
 * programmers.
 * 
 * @author Herbert Czymontek
 */
public interface ApplicationFunctions {

  /**
   * Creates a new menu item with the given caption.
   *
   * <p>The caption will also be used to identify the menu item in the menu
   * event handler.
   *
   * @param caption  menu item caption
   */
  void addMenuItem(String caption);

  /**
   * Display a different form.
   *
   * @param form  form to display
   */
  void switchForm(Form form);

  /**
   * Terminates this application.
   */
  void finish();

  void StartRec(int vSource, String vFile,String ErrorInfo);
  
  void StopRec();
  
  double MemUsed();
  
  double GetTotMem();
  
  String GetPackageName();

  void Quit();

  int GetScreenWidth();
  
  int GetScreenHeight();
  
  int GetNetScreenHeight();
  
  int GetTitlebarHeight();
  
  int GetStatusbarHeight();
  
  float GetScreenDensity();
  
  void SetMenus(String[] menus);
  
  void Beep();
  
  float getHR();
  
  float getWR();
  
  void setZoom(int ztype);
  
  int getZoom();

  void SeekPlay(int pos);

  String VB4AShell(String cmd);

  String RunAssetExe(String Fname);
  
  void VB4ASetExe(String exefile);

  void SetStopable(boolean ifstop);

  void ToastMessage(String msg);

  void SDSQLiteEXEC(String DBPath, String DBName, String SQLSen);

  Variant SDSQLitePrepare(String DBPath, String DBName, String SQLSen, String SeperatorItem, String SeperatorLine);

  void Msgbox(String title,String msg,String btn);
  
  void UserQuit(String title,String msg,String btnYes, String btnNo);
  void UserHide(String title,String msg,String btnYes, String btnNo);
  void NotificationShow(int icon, int id, String title, String title2, String contex);

//  void GPSSetting(int setting);
//  Variant Shell(String shellscr);

//DIALOGS

  void ShowMsgbox(String title, String msg, int icon,String[] btn);
  void ShowInputbox(String title,int icon,String yesstr,String nostr);
  void ShowListbox(String title,int icon,String[] items,String btn);
  void ShowRadiobox(String title,int icon,String[] items,String btn,String btn2);
  void ShowCheckbox(String title,int icon,String[] items,boolean[] itemsval,String btn,String btn2);
  /**
   * Retrieves the value of a previously stored preference (even from previous
   * of the same program).
   *
   * @param name  name which was used to store the value under
   * @return  value associated with name
   */
  void SendBroadCast(String Name, String ExtraName, String Extra);
   
  Variant getPreference(String name);

  Variant GetTime();

  Variant GetDate();

  Variant GetSQL(String DBName, String SQLSen, String SeperatorItem, String SeperatorLine);

  String[][] GetSQL2(String DBName, String SQLSen);
  
  void SendSQL(String DBName, String SQLSen);

  void PlaySound(String mFile);

  void PlayAssetSound(String mFile);

  boolean MenuAdded();

  void CreatePlay(String mFile);

  void ReleasePlay();

  void ResumePlay();

  void StopPlay();

  void Play();

  void PausePlay();
  
  void SetClip(String cont);
  
  String GetClip();

//  void PlayMedia(String Mfile);

 // Variant SocketClient(String ip, Integer port, String send);

  void VB4AStartActivity(String activityname);

  /**
   * Stores the given value under given name. The value can be retrieved using
   * the given name any time (even on subsequent runs of the program).
   * 
   * @param name  name to store value under
   * @param value  value to store (must be a primitive value, objects not
   *               allowed)
   */
  void storePreference(String name, Variant value);
}
