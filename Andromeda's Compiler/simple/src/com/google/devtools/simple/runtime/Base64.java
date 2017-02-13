/*     */ package com.google.devtools.simple.runtime;
/*     */ //This file is directly decompiled from e4a
/*     */ import java.io.UnsupportedEncodingException;
/*     */ 
/*     */ public class Base64
/*     */ {
/*  34 */   private static char[] base64EncodeChars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/' };
/*     */ 
/*  44 */   private static byte[] base64DecodeChars = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 62, -1, -1, -1, 63, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, -1, -1, -1, -1, -1, -1, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -1, -1, -1, -1, -1, -1, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -1, -1, -1, -1, -1 };
/*     */ 
/*     */   public static String B64Encode(byte[] data)
/*     */   {
/*  55 */     StringBuffer sb = new StringBuffer();
/*  56 */     int len = data.length;
/*  57 */     int i = 0;
/*     */ 
/*  59 */     while (i < len) {
/*  60 */       int b1 = data[(i++)] & 0xFF;
/*  61 */       if (i == len) {
/*  62 */         sb.append(base64EncodeChars[(b1 >>> 2)]);
/*  63 */         sb.append(base64EncodeChars[((b1 & 0x3) << 4)]);
/*  64 */         sb.append("==");
/*  65 */         break;
/*     */       }
/*  67 */       int b2 = data[(i++)] & 0xFF;
/*  68 */       if (i == len) {
/*  69 */         sb.append(base64EncodeChars[(b1 >>> 2)]);
/*  70 */         sb.append(base64EncodeChars[((b1 & 0x3) << 4 | (b2 & 0xF0) >>> 4)]);
/*  71 */         sb.append(base64EncodeChars[((b2 & 0xF) << 2)]);
/*  72 */         sb.append("=");
/*  73 */         break;
/*     */       }
/*  75 */       int b3 = data[(i++)] & 0xFF;
/*  76 */       sb.append(base64EncodeChars[(b1 >>> 2)]);
/*  77 */       sb.append(base64EncodeChars[((b1 & 0x3) << 4 | (b2 & 0xF0) >>> 4)]);
/*  78 */       sb.append(base64EncodeChars[((b2 & 0xF) << 2 | (b3 & 0xC0) >>> 6)]);
/*  79 */       sb.append(base64EncodeChars[(b3 & 0x3F)]);
/*     */     }
/*  81 */     return sb.toString();
/*     */   }
/*     */ 
/*     */   public static byte[] B64Decode(String str) throws UnsupportedEncodingException {
/*  85 */     int remainder = str.length() % 4;
/*     */ 
/*  87 */     if (remainder == 2)
/*  88 */       str = str + "==";
/*  89 */     else if (remainder == 3) {
/*  90 */       str = str + "=";
/*     */     }
/*     */ 
/*  93 */     StringBuffer sb = new StringBuffer();
/*  94 */     byte[] data = str.getBytes("US-ASCII");
/*  95 */     int len = data.length;
/*  96 */     int i = 0;
/*     */ 
/*  98 */     while (i < len) {
/*     */       int b1;
/*     */       do
/* 101 */         b1 = base64DecodeChars[data[(i++)]];
/* 102 */       while ((i < len) && (b1 == -1));
/* 103 */       if (b1 == -1)
/*     */         break;
/*     */       int b2;
/*     */       do b2 = base64DecodeChars[data[(i++)]];
/*     */ 
/* 108 */       while ((i < len) && (b2 == -1));
/* 109 */       if (b2 == -1) break; sb.append((char)(b1 << 2 | (b2 & 0x30) >>> 4));
/*     */       int b3;
/*     */       do {
/* 113 */         b3 = data[(i++)];
/* 114 */         if (b3 == 61) return sb.toString().getBytes("iso8859-1");
/* 115 */         b3 = base64DecodeChars[b3];
/* 116 */       }while ((i < len) && (b3 == -1));
/* 117 */       if (b3 == -1) break; sb.append((char)((b2 & 0xF) << 4 | (b3 & 0x3C) >>> 2));
/*     */       int b4;
/*     */       do {
/* 121 */         b4 = data[(i++)];
/* 122 */         if (b4 == 61) return sb.toString().getBytes("iso8859-1");
/* 123 */         b4 = base64DecodeChars[b4];
/* 124 */       }while ((i < len) && (b4 == -1));
/* 125 */       if (b4 == -1) break;
/* 126 */       sb.append((char)((b3 & 0x3) << 6 | b4));
/*     */     }
/* 128 */     return sb.toString().getBytes("iso8859-1");
/*     */   }
/*     */ }

/* Location:           D:\快盘\VB4A\E4ARuntime.jar
 * Qualified Name:     com.e4a.runtime.Base64
 * JD-Core Version:    0.6.2
 */