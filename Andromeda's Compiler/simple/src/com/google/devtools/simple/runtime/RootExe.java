/*     */ package com.google.devtools.simple.runtime;
/*     */ 
import com.google.devtools.simple.runtime.annotations.SimpleFunction;
import com.google.devtools.simple.runtime.annotations.SimpleObject;
//THIS FILE IS DECOMPILED FROM E4A
/*     */ import java.io.BufferedReader;
/*     */ import java.io.DataInputStream;
/*     */ import java.io.DataOutputStream;
/*     */ import java.io.File;
/*     */ import java.io.IOException;
/*     */ import java.io.InputStream;
/*     */ import java.io.InputStreamReader;
/*     */ import java.util.ArrayList;
/*     */ import java.util.List;
/*     */ 
/*     */ @SimpleObject
/*     */ public final class RootExe
/*     */ {
/*  22 */   private static boolean haveRoot = false;
/*     */   private static DataInputStream inputStream;
/*     */   private static DataOutputStream outputStream;
/*     */   private static Process process;
/*     */ 
/*     */   @SimpleFunction
/*     */   public static boolean IfRooted()
/*     */   {
/*  30 */     if (haveRoot == true) {
/*  31 */       return true;
/*     */     }
/*  33 */     int i = RunCMD("echo test");
/*  34 */     if (i != -1)
/*  35 */       haveRoot = true;
/*     */     else {
/*  37 */       haveRoot = false;
/*     */     }
/*  39 */     return haveRoot;
/*     */   }
/*     */ 
/*     */   @SimpleFunction
/*     */   public static String RunCMD2(String cmd) {
/*  44 */     String str1 = "";
/*     */     try {
/*  46 */       process = Runtime.getRuntime().exec("su");
/*  47 */       outputStream = new DataOutputStream(process.getOutputStream());
/*  48 */       inputStream = new DataInputStream(process.getInputStream());
/*  49 */       outputStream.writeBytes(cmd + "\n");
/*  50 */       outputStream.flush();
/*  51 */       outputStream.writeBytes("exit\n");
/*  52 */       outputStream.flush();
/*  53 */       String str2 = null;
/*  54 */       while ((str2 = inputStream.readLine()) != null) {
/*  55 */         if (!str1.equals(""))
/*  56 */           str1 = str1 + "\n" + str2;
/*     */         else {
/*  58 */           str1 = str2;
/*     */         }
/*     */       }
/*  61 */       process.waitFor();
/*     */     } catch (Exception e) {
/*  63 */       e.printStackTrace();
/*     */     } finally {
/*  65 */       if (outputStream != null) {
/*     */         try {
/*  67 */           outputStream.close();
/*     */         } catch (IOException e2) {
/*  69 */           e2.printStackTrace();
/*     */         }
/*     */       }
/*  72 */       if (inputStream != null) {
/*     */         try {
/*  74 */           inputStream.close();
/*     */         } catch (IOException e3) {
/*  76 */           e3.printStackTrace();
/*     */         }
/*     */       }
/*     */     }
/*  80 */     return str1;
/*     */   }
/*     */ 
/*     */   @SimpleFunction
/*     */   public static int RunCMD(String cmd) {
/*  85 */     int i = -1;
/*     */     try {
/*  87 */       Process process = Runtime.getRuntime().exec("su");
/*  88 */       outputStream = new DataOutputStream(process.getOutputStream());
/*  89 */       outputStream.writeBytes(cmd + "\n");
/*  90 */       outputStream.flush();
/*  91 */       outputStream.writeBytes("exit\n");
/*  92 */       outputStream.flush();
/*  93 */       process.waitFor();
/*  94 */       i = process.exitValue();
/*     */     } catch (Exception e) {
/*  96 */       e.printStackTrace();
/*     */     } finally {
/*  98 */       if (outputStream != null) {
/*     */         try {
/* 100 */           outputStream.close();
/*     */         } catch (IOException e2) {
/* 102 */           e2.printStackTrace();
/*     */         }
/*     */       }
/*     */     }
/* 106 */     return i;
/*     */   }
/*     */ 
/*     */   @SimpleFunction
/*     */   public static void RunCMD3(String cmd) {
/*     */     try {
/* 112 */       if ((process == null) || (outputStream == null)) {
/* 113 */         process = Runtime.getRuntime().exec("su");
/* 114 */         outputStream = new DataOutputStream(process.getOutputStream());
/*     */       }
/* 116 */       outputStream.writeBytes(cmd + "\n");
/* 117 */       outputStream.flush();
/*     */     } catch (Exception e) {
/* 119 */       e.printStackTrace();
/*     */     }
/*     */   }
/*     */ 
/*     */   @SimpleFunction
/*     */   public static void SendKey(int Keycode) {
/* 125 */     if (IfRooted() == true)
/* 126 */       RunCMD3("input keyevent " + Keycode);
/*     */   }
/*     */ 
/*     */   @SimpleFunction
/*     */   public static void SendInput(String txtContent)
/*     */   {
/* 132 */     if (IfRooted() == true)
/* 133 */       RunCMD3("input text '" + txtContent + "'");
/*     */   }
/*     */ 
/*     */   @SimpleFunction
/*     */   public static void SendTouch(int x, int y)
/*     */   {
/* 139 */     if (IfRooted() == true)
/*     */     {
/* 146 */       RunCMD3("input tap " + x + " " + y);
/*     */     }
/*     */   }
/*     */ 
/*     */   @SimpleFunction
/*     */   public static void SendMove(int x1, int y1, int x2, int y2) {
/* 152 */     if (IfRooted() == true)
/* 153 */       RunCMD3("input swipe " + x1 + " " + y1 + " " + x2 + " " + y2);
/*     */   }
/*     */ 
/*     */   @SimpleFunction
/*     */   public static boolean SilentInstall(String apkname)
/*     */   {
/* 218 */     int result = -1;
/* 219 */     if (IfRooted() == true) {
/* 220 */       result = RunCMD("pm install " + apkname);
/*     */     }
/* 222 */     if (result != -1) {
/* 223 */       return true;
/*     */     }
/* 225 */     return false;
/*     */   }
/*     */ 
/*     */   @SimpleFunction
/*     */   public static boolean SilentUninstall(String apkname)
/*     */   {
/* 231 */     int result = -1;
/* 232 */     if (IfRooted() == true) {
/* 233 */       result = RunCMD("pm uninstall " + apkname);
/*     */     }
/* 235 */     if (result != -1) {
/* 236 */       return true;
/*     */     }
/* 238 */     return false;
/*     */   }


/*     */   @SimpleFunction
/*     */   public static void ScreenCap(String path) {
/* 281 */     if (IfRooted() == true)
/* 282 */       RunCMD3("screencap " + path);
/*     */   }
/*     */ 
/*     */   @SimpleFunction
/*     */   public static boolean ForceStop(String packagename)
/*     */   {
/* 288 */     int resultcmd = -1;
/* 289 */     if (IfRooted() == true) {
/* 290 */       resultcmd = RunCMD("am force-stop " + packagename);
/*     */     }
/* 292 */     return resultcmd != -1;
/*     */   }
/*     */ }

/* Location:           D:\快盘\VB4A\E4ARuntime.jar
 * Qualified Name:     com.e4a.runtime.Rootæéæä½
 * JD-Core Version:    0.6.2
 */