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
import com.google.devtools.simple.runtime.parameters.StringReferenceParameter;

import java.text.DecimalFormat; 
import java.lang.String;
import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

import java.net.URLEncoder;
import java.net.URLDecoder;

/**
 * Implementation of various string related runtime functions.
 * 
 * @author Herbert Czymontek
 */
@SimpleObject
public final class Strings {

  private Strings() {
  }

  /*
   * Verifies that a string index argument is within string boundaries.
   */
  private static void checkIndex(String function, int index, int max) {
    if (index < 0 || index > max) {
      throw new IllegalArgumentException("Index for " + function + "() out of range. " +
          "Should be between 0 and " + max + ", but is " + index);
    }
  }

  /*
   * Verifies that a string length argument is positive.
   */
  private static void checkLength(String function, int len) {
    if (len < 0) {
      throw new IllegalArgumentException("Length for " + function + "() out of range. " +
          "Should be greater than 0, but is " + len);
    }
  }

  /*
   * Helper method for extracting substrings.
   */
  private static String mid(String function, String str, int start, int len) {
    checkIndex(function, start, str.length());
    checkLength(function, len);

    int end = start + len;
    if (end > str.length()) {
      end = str.length();
    }

    return str.substring(start, end);
  }

  /**
   * Searches for a string in another string. 
   * 
   * @param str1  string to search in
   * @param str2  string to search for
   * @param start  search start index within {@code str1}
   * @return  index at which {@code str2} was found within {@code str1} or a
   *          negative value if {@code str2} was not found within {@code str1}
   */
  @SimpleFunction
  public static int InStr(String str1, String str2, int start) {
    checkIndex("InStr", start, str1.length());

    return str1.indexOf(str2, start);
  }

  @SimpleFunction
  public static long AscW(String str) {
  char[] arr = str.toCharArray();
  long value = (long)arr[0];
  return value;
  }

  //MD5 http://www.android100.org/html/201203/18/354.html
  @SimpleFunction
	public static String MD5(String string) {
		byte[] hash;
		try {
			hash = MessageDigest.getInstance("MD5").digest(string.getBytes("UTF-8"));
		} catch (NoSuchAlgorithmException e) {
			throw new RuntimeException("Huh, MD5 should be supported?", e);
		} catch (UnsupportedEncodingException e) {
			throw new RuntimeException("Huh, UTF-8 should be supported?", e);
		}

		StringBuilder hex = new StringBuilder(hash.length * 2);
		for (byte b : hash) {
			if ((b & 0xFF) < 0x10) hex.append("0");
			hex.append(Integer.toHexString(b & 0xFF));
		}
		return hex.toString();
	}
  
  @SimpleFunction
  public static String ChrW(long value) {
  char c = (char)value;
  return String.valueOf(c);
  }
  
  @SimpleFunction
  public static byte[] ToBytes(String str, String codec) throws UnsupportedEncodingException{
	return str.getBytes(codec);
  }
  
  @SimpleFunction
  public static String ToString(byte[] bs, String codec) throws UnsupportedEncodingException{
    return new String(bs,codec);
  }
  /**
   * Searches for a string in another string starting at the end of that
   * string.
   * 
   * @param str1  string to search in
   * @param str2  string to search for
   * @param start  search start index within {@code str1}
   * @return  index at which {@code str2} was found within {@code str1} or a
   *          negative value if {@code str2} was not found within {@code str1}
   */
  @SimpleFunction
  public static int InStrRev(String str1, String str2, int start) {
    checkIndex("InStrRev", start, str1.length());

    return str1.lastIndexOf(str2, start);
  }

  @SimpleFunction
  public static String Format(String str, String formatstr) {
		DecimalFormat decimalFormat = new DecimalFormat(); 
		decimalFormat.applyPattern( formatstr ); 
		return decimalFormat.format(Float.parseFloat(str)); 
  }
  /**
   * Converts the given string to all lowercase.
   * 
   * @param str  string to convert to lowercase
   */
  @SimpleFunction
  public static String LCase(StringReferenceParameter str) {
    return (str.get().toLowerCase());
  }

  /**
   * Converts the given string to all uppercase.
   * 
   * @param str  string to convert to uppercase
   */
  @SimpleFunction
  public static String UCase(StringReferenceParameter str) {
    return (str.get().toUpperCase());
  }

  /**
   * Returns the specified number of characters from the start of the given
   * string.
   * 
   * @param str  string to return characters from
   * @param len  number of characters to return
   * @return  substring of the given string
   */
  @SimpleFunction
  public static String Left(String str, int len) {
    return mid("Left", str, 0, len);
  }
	

  /**
   * Returns the specified number of characters from the end of the given
   * string.
   * 
   * @param str  string to return characters from
   * @param len  number of characters to return
   * @return  substring of the given string
   */
  @SimpleFunction
  public static String Right(String str, int len) {
    int start = str.length() - len;
    return mid("Right", str, start < 0 ? 0 : start, len);
  }

  /**
   * Returns the specified number of characters from the given string starting
   * from the given index.
   * 
   * @param str  string to return characters from
   * @param start  start index within {@code str}
   * @param len  number of characters to return
   * @return  substring of the given string
   */
  @SimpleFunction
  public static String Mid(String str, int start, int len) {
    return mid("Mid", str, start, len);
  }

  /**
   * Returns the number of characters in the given string.
   * 
   * @param str  string to get length of
   * @return  number of characters (length) of the given string
   */
  @SimpleFunction
  public static int Len(String str) {
    return str.length();
  }

  /**
   * Removes leading and trailing space characters from the given string.
   * 
   * @param str  string to trim
   */
  @SimpleFunction
  public static String Trim(StringReferenceParameter str) {
    StringReferenceParameter strTmp;
	strTmp=str;
	RTrim2(strTmp);
	return LTrim(strTmp);
  }


/*
  @SimpleFunction
  public static String UTF8toGBK(String str) {
    String tmp1 = new String(str.getBytes("UTF-8"));
	String tmp2 = new String(tmp1.getBytes("GBK"));
	return tmp2;
  }
*/

  /**
   * Removes leading space characters from the given string.
   * 
   * @param str  string to trim
   */
  @SimpleFunction
  public static String LTrim(StringReferenceParameter str) {
    char[] chars = str.get().toCharArray();
    int count = 0;
    for (int i = 0; i < chars.length && chars[i] == ' '; i++, count++) {
    }

    if (count > 0) {
      return (new String(chars, count, chars.length - count));
    } else {
	  return (new String(chars, count, chars.length));
	}
  }

  public static void LTrim2(StringReferenceParameter str) {
    char[] chars = str.get().toCharArray();
    int count = 0;
    for (int i = 0; i < chars.length && chars[i] == ' '; i++, count++) {
    }

    if (count > 0) {
      str.set (new String(chars, count, chars.length - count));
    }
  }
  /**
   * Removes trailing space characters from the given string.
   * 
   * @param str  string to trim
   */
  @SimpleFunction
  public static String RTrim(StringReferenceParameter str) {
    char[] chars = str.get().toCharArray();
    int count = 0;
    for (int i = chars.length - 1; i > 0 && chars[i] == ' '; i--, count++) {
    }

    if (count > 0) {
      return (new String(chars, 0, chars.length - count));
    } else {
	  return (new String(chars, 0, chars.length));
	}
  }
  public static void RTrim2(StringReferenceParameter str) {
    char[] chars = str.get().toCharArray();
    int count = 0;
    for (int i = chars.length - 1; i > 0 && chars[i] == ' '; i--, count++) {
    }

    if (count > 0) {
      str.set(new String(chars, 0, chars.length - count));
    }
  }
  /**
   * Replaces occurrences of one string with another string in the given
   * string.
   * 
   * @param str  string to modify
   * @param find  string to find
   * @param replace  string to replace found string with
   * @param start  start index within {@code str}
   * @param count  number of times to perform the replacement (-1 to replace
   *               all occurrences)
   */
  @SimpleFunction
  public static String Replace(StringReferenceParameter str, String find, String replace,
      int start, int count) {
    String s = str.get();

    checkIndex("Replace", start, s.length());
    if (find == null || replace == null) {
      // None of the following code minds find or replace being null, but we do
      throw new NullPointerException();
    }

    // Escape reg expr characters
    find = "\\Q" + find + "\\E";

    String s1 = s.substring(0, start);
    String s2 = s.substring(start);

    if (count == -1) {
      s2 = s2.replaceAll(find, replace);
    } else {
      while (--count >= 0) {
        s2 = s2.replaceFirst(find, replace);
      }
    }

    //str.set(s1 + s2);
	return s1 + s2;
  }

  @SimpleFunction
  public static String RC4(String aInput,String aKey) {   
        int[] iS = new int[256];   
        byte[] iK = new byte[256];   
          
        for (int i=0;i<256;i++)   
            iS[i]=i;   
              
        int j = 1;   
          
        for (short i= 0;i<256;i++)   
        {   
            iK[i]=(byte)aKey.charAt((i % aKey.length()));   
        }   
          
        j=0;   
          
        for (int i=0;i<255;i++)   
        {   
            j=(j+iS[i]+iK[i]) % 256;   
            int temp = iS[i];   
            iS[i]=iS[j];   
            iS[j]=temp;   
        }   
      
      
        int i=0;   
        j=0;   
        char[] iInputChar = aInput.toCharArray();   
        char[] iOutputChar = new char[iInputChar.length];   
        for(short x = 0;x<iInputChar.length;x++)   
        {   
            i = (i+1) % 256;   
            j = (j+iS[i]) % 256;   
            int temp = iS[i];   
            iS[i]=iS[j];   
            iS[j]=temp;   
            int t = (iS[i]+(iS[j] % 256)) % 256;   
            int iY = iS[t];   
            char iCY = (char)iY;   
            iOutputChar[x] =(char)( iInputChar[x] ^ iCY) ;      
        }   
          
        return new String(iOutputChar);   
                  
    } 


  /**
   * Compares the two given strings lexicographically.
   * 
   * @param str1  first string of comparison
   * @param str2  second string of comparison
   * @return  0 if the strings are equal, a negative number if {@code str2}
   *          follows {@code str1} and a positive number if {@code str1}
   *          follows {@code str2}
   */
  @SimpleFunction
  public static int StrComp(String str1, String str2) {
    return str1.compareTo(str2);
  }

  /**
   * Reverses the given string.
   * 
   * @param str  string to reverse
   */
  @SimpleFunction
  public static String StrReverse(StringReferenceParameter str) {
    return (new StringBuffer(str.get()).reverse().toString());
  }
  
  //×Ö·û±àÂë£º²Î¿¼http://blog.csdn.net/chow__zh/article/details/8943355
  @SimpleFunction
  public static String ReCodec(String str, String incodec, String outcodec) throws UnsupportedEncodingException {
	if(str != null) {
		byte[] bs=str.getBytes(incodec);
		return new String(bs,outcodec);
	}
	return null;
  }
  
  @SimpleFunction
  public static String URLencode(String str, String codec)  throws UnsupportedEncodingException {
    String res=URLEncoder.encode(str, codec);
	return res;
  }
  
  @SimpleFunction
  public static String URLdecode(String str, String codec)  throws UnsupportedEncodingException {
    String res=URLDecoder.decode(str, codec);
	return res;
  }
}
