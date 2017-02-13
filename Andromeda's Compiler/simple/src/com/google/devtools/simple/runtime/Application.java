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

import com.google.devtools.simple.runtime.annotations.SimpleFunction;
import com.google.devtools.simple.runtime.annotations.SimpleObject;
import com.google.devtools.simple.runtime.components.Form;
import com.google.devtools.simple.runtime.variants.Variant;
import com.google.devtools.simple.runtime.annotations.SimpleEvent;
import com.google.devtools.simple.runtime.annotations.SimpleProperty;
/**
 * Implementation of various application related runtime functions.
 * 
 * @author Herbert Czymontek
 */
@SimpleObject
public abstract class Application {

  private static ApplicationFunctions applicationFunctions;

  private Application() {
  }

  /**
   * Initializes the application functionality of the application library.
   *
   * @param functions  implementation of application functions
   */
  public static void initialize(ApplicationFunctions functions) {
    applicationFunctions = functions;
  }

  /**
   * Creates a new menu item with the given caption.
   *
   * <p>The caption will also be used to identify the menu item in the menu
   * event handler.
   *
   * @param caption  menu item caption
   */
  @SimpleFunction
  public static void AddMenuItem(String caption) {
    applicationFunctions.addMenuItem(caption);
  }

  @SimpleFunction
  public static void StartRec(int vSource, String vFile,String ErrorInfo){
	applicationFunctions.StartRec(vSource,vFile,ErrorInfo);
  }
  
  @SimpleFunction 
  public static void StopRec(){
	applicationFunctions.StopRec();
  }
  
  @SimpleFunction
  public static void SetMenus(String[] menus) {
	applicationFunctions.SetMenus(menus);
  }

  @SimpleFunction
  public void SetZoom(int ztype) {
	applicationFunctions.setZoom(ztype);  
  }
  
  @SimpleFunction
  public int GetZoom(int ztype) {
    return applicationFunctions.getZoom();
  }

  @SimpleFunction
  public static void SetStopable(boolean ifstop) {
	applicationFunctions.SetStopable(ifstop);
  }

  @SimpleFunction
  public static String GetPackageName() {
	return applicationFunctions.GetPackageName();
  }

  @SimpleFunction
  public static void SeekPlay(int pos) {
    applicationFunctions.SeekPlay(pos);
  }

  @SimpleFunction
  public static void PlayMedia2(String mFile) {
    applicationFunctions.PlaySound(mFile);
  }

  @SimpleFunction
  public static String VB4AShell(String cmd) {
    return applicationFunctions.VB4AShell(cmd);
  }
  
  @SimpleFunction
  public static void SetClip(String cont){
	applicationFunctions.SetClip(cont);
  }
  @SimpleFunction
  public static String GetClip(){
	return applicationFunctions.GetClip();
  }
  
  @SimpleFunction
  public static int GetScreenWidth() {
    return applicationFunctions.GetScreenWidth();
  }
  @SimpleFunction
  public static float GetScreenDensity() {
    return applicationFunctions.GetScreenDensity();
  }
  @SimpleFunction
  public static int GetScreenHeight() {
    return applicationFunctions.GetScreenHeight();
  }

  @SimpleFunction
  public static int GetFullScreenHeight() {
    return applicationFunctions.GetNetScreenHeight();
  }
  
  @SimpleFunction
  public static int GetTitlebarHeight() {
    return applicationFunctions.GetTitlebarHeight();
  }
  
  @SimpleFunction
  public static String RunAssetExe(String Fname){
	return applicationFunctions.RunAssetExe(Fname); 
  }
  
  @SimpleFunction
  public static int GetStatusbarHeight() {
    return applicationFunctions.GetStatusbarHeight();
  }
  
  
  /*
  @SimpleProperty
  public static int FillType() {
	return applicationFunctions.getZoom();
  };

  @SimpleProperty(type = SimpleProperty.PROPERTY_TYPE_INTEGER,
                  initializer = "1")
  public static void FillType(int ztype){
    applicationFunctions.setZoom(ztype);  
  };
  */

  @SimpleFunction
  public static float ZoomRatioH() {
	return applicationFunctions.getHR();
  }
  
  @SimpleFunction
  public static float ZoomRatioW() {
	return applicationFunctions.getWR();
  }
  
  @SimpleFunction
  public static void VB4ASetExe(String exefile) {
	applicationFunctions.VB4ASetExe(exefile);
  }

  @SimpleFunction
  public static void SQLEXEC(String DBName, String SQLSen) {
	  // public void SendSQL(String DBName, String SQLSen)
	applicationFunctions.SendSQL(DBName,SQLSen);
  }

  @SimpleFunction
  public static Variant SQLPREPARE(String DBName, String SQLSen, String SeperatorItem, String SeperatorLine) {
	return applicationFunctions.GetSQL(DBName, SQLSen, SeperatorItem, SeperatorLine);
  }

  @SimpleFunction
  public static String[][] SQLPREPARE2(String DBName, String SQLSen) {
	return applicationFunctions.GetSQL2(DBName, SQLSen);
  }
  
  @SimpleFunction
  public static void SDSQLiteEXEC(String DBPath, String DBName, String SQLSen) {
	applicationFunctions.SDSQLiteEXEC(DBPath, DBName, SQLSen);
  }

  @SimpleFunction
  public static Variant SDSQLitePrepare(String DBPath, String DBName, String SQLSen, String SeperatorItem, String SeperatorLine) {
	return applicationFunctions.SDSQLitePrepare(DBPath, DBName, SQLSen, SeperatorItem, SeperatorLine);
  }

  @SimpleFunction
  public static void PlayMedia(String mFile) {
    applicationFunctions.PlayAssetSound(mFile);
  }

  @SimpleFunction
  public static void Beep() {
	applicationFunctions.Beep();
  }

  /**
   * Display a different form.
   *
   * @param form  form to display
   */
  @SimpleFunction
  public static void SwitchForm(Form form) {
    applicationFunctions.switchForm(form);
  }
  
  
  @SimpleFunction
  public static void ToastMessage(String msg) {
	applicationFunctions.ToastMessage(msg);
  }

  @SimpleFunction
  public static void Msgbox(String title,String msg,String btn) {
	applicationFunctions.Msgbox(title,msg,btn);
  }
  
  @SimpleFunction
  public static void UserQuit(String title,String msg,String btnYes, String btnNo) {
	applicationFunctions.UserQuit(title,msg,btnYes,btnNo);
  }
  @SimpleFunction
  public static void UserHide(String title,String msg,String btnYes, String btnNo) {
	applicationFunctions.UserHide(title,msg,btnYes,btnNo);
  }

  @SimpleFunction
  public static void VB4ANotify(int icon, int id, String title, String title2, String contex) {
	applicationFunctions.NotificationShow(icon, id, title, title2, contex);
  }

//DIALOGS
  @SimpleFunction
  public static void ShowInputbox(String title,int icon,String yesstr,String nostr) {
	applicationFunctions.ShowInputbox(title,icon,yesstr,nostr);
  }
  @SimpleFunction
  public static void ShowMsgbox(String title, String msg, int icon,String[] btn) {
	applicationFunctions.ShowMsgbox(title,msg,icon,btn);
  }
  @SimpleFunction
  public static void ShowListbox(String title,int icon,String[] items,String btn) {
	applicationFunctions.ShowListbox(title,icon,items,btn);
  }
  @SimpleFunction
  public static void ShowRadiobox(String title,int icon,String[] items,String btn,String btn2) {
	applicationFunctions.ShowRadiobox(title,icon,items,btn,btn2);
  }
  @SimpleFunction
  public static void ShowCheckbox(String title,int icon,String[] items,boolean[] itemsval,String btn,String btn2) {
	applicationFunctions.ShowCheckbox(title,icon,items,itemsval,btn,btn2);
  }
/*
  @SimpleFunction
  public Variant SocketClient(String ip, Integer port, String send) {
	return applicationFunctions.SocketClient(ip,port,send);
  }
  */

//  @SimpleFunction
  public static void VB4AStartActivity(String activityname) {
	applicationFunctions.VB4AStartActivity(activityname);
  }

 /*
  @SimpleFunction
  public static void VB4ASetup(int seting) {
	applicationFunctions.GPSSetting(seting);
  }
*/
  @SimpleFunction
  public static void Quit() {
	applicationFunctions.Quit();
  }

  /**
   * Terminates this application.
   */
  @SimpleFunction
  public static void Hide() {
    applicationFunctions.finish();
  }

  @SimpleFunction
  public static void Finish() {
    applicationFunctions.finish();
  }
  /**
   * Retrieves the value of a previously stored preference (even from previous
   * of the same program).
   *
   * @param name  name which was used to store the value under
   * @return  value associated with name
   */
  @SimpleFunction
  public static Variant GetPreference(String name) {
    return applicationFunctions.getPreference(name);
  }

  @SimpleFunction
  public static Variant GetTime() {
    return applicationFunctions.GetTime();
  }
  
  @SimpleFunction
  public static Variant GetDate() {
    return applicationFunctions.GetDate();
  }

  @SimpleFunction
  public static void SendBroadCast(String Name, String ExtraName, String Extra) {
	applicationFunctions.SendBroadCast(Name, ExtraName, Extra);
  }
  
  @SimpleFunction
  public static boolean MenuAdded() {
	return applicationFunctions.MenuAdded();
  }
  /**
   * Stores the given value under given name. The value can be retrieved using
   * the given name any time (even on subsequent runs of the program).
   * 
   * @param name  name to store value under
   * @param value  value to store (must be a primitive value, objects not
   *               allowed)
   */
  @SimpleFunction
  public static void StorePreference(String name, Variant value) {
    applicationFunctions.storePreference(name, value);
  }

  @SimpleFunction
  public static void CreatePlay(String mFile) {
    applicationFunctions.CreatePlay(mFile);
  }

  @SimpleFunction
  public static void ReleasePlay(){
	applicationFunctions.ReleasePlay();
  }

  @SimpleFunction
  public static void Play() {
    applicationFunctions.Play();
  }
//  @SimpleFunction
  public static void ResumePlay() {
    applicationFunctions.ResumePlay();
  }
  @SimpleFunction
  public static void StopPlay() {
    applicationFunctions.StopPlay();
  }

  @SimpleFunction
  public static void PausePlay() {
	applicationFunctions.PausePlay();
  }
  
  @SimpleFunction
  public static double GetFreeMem() {
	return applicationFunctions.MemUsed();
  }
  
  @SimpleFunction
  public static double GetTotMem() {
	return applicationFunctions.GetTotMem();
  }
}
