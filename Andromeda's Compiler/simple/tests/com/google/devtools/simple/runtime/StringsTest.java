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

import com.google.devtools.simple.runtime.parameters.StringReferenceParameter;

import junit.framework.TestCase;

/**
 * Tests for {@link Strings}.
 *
 * @author Herbert Czymontek
 */
public class StringsTest extends TestCase {

  public StringsTest(String testName) {
    super(testName);
  }

  /**
   * Tests {@link Strings#InStr(String, String, int)}.
   */
  public void testInStr() {
    // Check that if the first string argument is null that a NullPointerException will be thrown.
    // Note that if the function is called from Simple code that the compiler will generate
    // exception handlers to convert NullPointerExceptions into the equivalent runtime error
    try {
      Strings.InStr(null, "", 0);
      fail();
    } catch (NullPointerException expected) {
    }

    // Check that if the second string argument is null that a NullPointerException will be thrown
    try {
      Strings.InStr("", null, 0);
      fail();
    } catch (NullPointerException expected) {
    }

    // Check that if the start index argument is out of range that an IllegalArgumentException
    // will be thrown (Simple runtime error equivalent of IllegalArgumentError)
    try {
      Strings.InStr("", "", -1);
      fail();
    } catch (IllegalArgumentException expected) {
    }
    try {
      Strings.InStr("", "", 1);
      fail();
    } catch (IllegalArgumentException expected) {
    }

    // Empty string is always found at the start index position
    assertEquals(0, Strings.InStr("", "", 0));
    assertEquals(0, Strings.InStr("foo", "", 0));
    assertEquals(1, Strings.InStr("foo", "", 1));

    // Check that a negative value will be returned if the second string cannot be found within the
    // first
    assertTrue(Strings.InStr("foo", "bar", 0) < 0);
    assertTrue(Strings.InStr("foobar", "bar", 5) < 0);

    // Check that the first occurrence of the search string is found if the search starting index
    // is less than start index of the first occurrence
    assertEquals(0, Strings.InStr("foofoo", "foo", 0));

    // Check that the second occurrence of the search string is found if the search starting index
    // is greater than start index of the first occurrence
    assertEquals(3, Strings.InStr("foofoo", "foo", 1));
  }

  /**
   * Tests {@link Strings#InStrRev(String, String, int)}.
   */
  public void testInStrRev() {
    // Check that if the first string argument is null that a NullPointerException will be thrown
    try {
      Strings.InStrRev(null, "", 0);
      fail();
    } catch (NullPointerException expected) {
    }

    // Check that if the second string argument is null that a NullPointerException will be thrown
    try {
      Strings.InStrRev("", null, 0);
      fail();
    } catch (NullPointerException expected) {
    }

    // Check that if the start index argument is out of range that an IllegalArgumentException
    // will be thrown
    try {
      Strings.InStrRev("", "", -1);
      fail();
    } catch (IllegalArgumentException expected) {
    }

    try {
      Strings.InStrRev("", "", 1);
      fail();
    } catch (IllegalArgumentException expected) {
    }

    // Empty string is always found at the start index position
    assertEquals(0, Strings.InStrRev("", "", 0));
    assertEquals(0, Strings.InStrRev("foo", "", 0));
    assertEquals(1, Strings.InStrRev("foo", "", 1));

    // Check that a negative value will be returned if the second string cannot be found within the
    // first
    assertTrue(Strings.InStrRev("foo", "bar", 0) < 0);
    assertTrue(Strings.InStrRev("foobar", "bar", 2) < 0);

    // Check that the first occurrence of the search string is found if the search starting index
    // is less than start index of the first occurrence
    assertEquals(3, Strings.InStrRev("foofoo", "foo", 3));

    // Check that the second occurrence of the search string is found if the search starting index
    // is greater than start index of the first occurrence
    assertEquals(0, Strings.InStrRev("foofoo", "foo", 2));
  }

  /**
   * Tests {@link Strings#LCase(StringReferenceParameter)}.
   */
  public void testLCase() {
    StringReferenceParameter strRef = new StringReferenceParameter(null);
    // Check that if the string argument is null that a NullPointerException will be thrown
    try {
      Strings.LCase(strRef);
      fail();
    } catch (NullPointerException expected) {
    }

    // Empty string
    strRef.set("");
    Strings.LCase(strRef);
    assertEquals("", strRef.get());

    // All UPPER should become all lower
    strRef.set("FOO");
    Strings.LCase(strRef);
    assertEquals("foo", strRef.get());
  }

  /**
   * Tests {@link Strings#UCase(StringReferenceParameter)}.
   */
  public void testUCase() {
    StringReferenceParameter strRef = new StringReferenceParameter(null);
    // Check that if the string argument is null that a NullPointerException will be thrown
    try {
      Strings.UCase(strRef);
      fail();
    } catch (NullPointerException expected) {
    }

    // Empty string
    strRef.set("");
    Strings.UCase(strRef);
    assertEquals("", strRef.get());

    // All lower should become all UPPER
    strRef.set("foo");
    Strings.UCase(strRef);
    assertEquals("FOO", strRef.get());
  }

  /**
   * Tests {@link Strings#Left(String, int)}.
   */
  public void testLeft() {
    // Check that if the string argument is null that a NullPointerException will be thrown
    try {
      Strings.Left(null, 0);
      fail();
    } catch (NullPointerException expected) {
    }

    // Length argument out of range (negative or greater than string length)
    try {
      Strings.Left("foo", -2);
      fail();
    } catch (IllegalArgumentException expected) {
    }

    // Left of empty string
    assertEquals("", Strings.Left("", 0));
    assertEquals("", Strings.Left("foo", 0));

    // Left of string
    assertEquals("fo", Strings.Left("foo", 2));
    assertEquals("foo", Strings.Left("foo", 20));
  }

  /**
   * Tests {@link Strings#Right(String, int)}.
   */
  public void testRight() {
    // Check that if the string argument is null that a NullPointerException will be thrown
    try {
      Strings.Right(null, 0);
      fail();
    } catch (NullPointerException expected) {
    }

    // Length argument out of range (negative or greater than string length)
    try {
      Strings.Right("foo", -2);
      fail();
    } catch (IllegalArgumentException expected) {
    }

    // Right of empty string
    assertEquals("", Strings.Right("", 0));
    assertEquals("", Strings.Right("foo", 0));

    // Right of string
    assertEquals("oo", Strings.Right("foo", 2));
    assertEquals("foo", Strings.Right("foo", 20));
  }

  /**
   * Tests {@link Strings#Mid(String, int, int)}.
   */
  public void testMid() {
    // Check that if the string argument is null that a NullPointerException will be thrown
    try {
      Strings.Mid(null, 0, 0);
      fail();
    } catch (NullPointerException expected) {
    }

    // Check that if the start index argument is out of range that an IllegalArgumentException
    // will be thrown
    try {
      Strings.Mid("", -1, 0);
      fail();
    } catch (IllegalArgumentException expected) {
    }
    try {
      Strings.Mid("", 1, 0);
      fail();
    } catch (IllegalArgumentException expected) {
    }

    // Length argument out of range (negative or greater than string length)
    try {
      Strings.Mid("foo", 0, -2);
      fail();
    } catch (IllegalArgumentException expected) {
    }

    // Mid of empty string
    assertEquals("", Strings.Mid("", 0, 0));
    assertEquals("", Strings.Mid("", 0, 10));

    // Mid of string
    assertEquals("", Strings.Mid("foo", 1, 0));
    assertEquals("oo", Strings.Mid("foo", 1, 2));
    assertEquals("oo", Strings.Mid("foo", 1, 20));
  }

  /**
   * Tests {@link Strings#Len(String)}.
   */
  public void testLen() {
    // Check that if the string argument is null that a NullPointerException will be thrown
    try {
      Strings.Len(null);
      fail();
    } catch (NullPointerException expected) {
    }

    // Length of strings
    assertEquals(0, Strings.Len(""));
    assertEquals(3, Strings.Len("foo"));
  }

  /**
   * Tests {@link Strings#RTrim(StringReferenceParameter)}.
   */
  public void testRTrim() {
    StringReferenceParameter strRef = new StringReferenceParameter(null);

    // Check that if the string argument is null that a NullPointerException will be thrown
    try {
      Strings.RTrim(strRef);
      fail();
    } catch (NullPointerException expected) {
    }

    // Trim spaces of strings
    strRef.set("");
    Strings.RTrim(strRef);
    assertEquals("", strRef.get());

    strRef.set("  foo bar  ");
    Strings.RTrim(strRef);
    assertEquals("  foo bar", strRef.get());
  }

  /**
   * Tests {@link Strings#LTrim(StringReferenceParameter)}.
   */
  public void testLTrim() {
    StringReferenceParameter strRef = new StringReferenceParameter(null);

    // Check that if the string argument is null that a NullPointerException will be thrown
    try {
      Strings.LTrim(strRef);
      fail();
    } catch (NullPointerException expected) {
    }

    // Trim spaces of strings
    strRef.set("");
    Strings.LTrim(strRef);
    assertEquals("", strRef.get());

    strRef.set("  foo bar  ");
    Strings.LTrim(strRef);
    assertEquals("foo bar  ", strRef.get());
  }

  /**
   * Tests {@link Strings#Trim(StringReferenceParameter)}.
   */
  public void testTrim() {
    StringReferenceParameter strRef = new StringReferenceParameter(null);

    // Check that if the string argument is null that a NullPointerException will be thrown
    try {
      Strings.Trim(strRef);
      fail();
    } catch (NullPointerException expected) {
    }

    // Trim spaces of strings
    strRef.set("");
    Strings.Trim(strRef);
    assertEquals("", strRef.get());

    strRef.set("  foo bar  ");
    Strings.Trim(strRef);
    assertEquals("foo bar", strRef.get());
  }

  /**
   * Tests {@link Strings#Replace(StringReferenceParameter, String, String, int, int)}.
   */
  public void testReplace() {
    StringReferenceParameter strRef = new StringReferenceParameter(null);

    // Check that if any of the string arguments are null that a NullPointerException will be
    // thrown
    try {
      Strings.Replace(strRef, "", "", 0, 0);
      fail();
    } catch (NullPointerException expected) {
    }

    strRef.set("");
    try {
      Strings.Replace(strRef, null, "", 0, 0);
      fail();
    } catch (NullPointerException expected) {
    }

    try {
      Strings.Replace(strRef, "", null, 0, 0);
      fail();
    } catch (NullPointerException expected) {
    }

    // Check that if the start index argument is out of range that an IllegalArgumentException
    // will be thrown
    try {
      Strings.Replace(strRef, "", "", -1, 0);
      fail();
    } catch (IllegalArgumentException expected) {
    }
    try {
      Strings.Replace(strRef, "", "", 1, 0);
      fail();
    } catch (IllegalArgumentException expected) {
    }

    // Some fun with empty strings
    strRef.set("");
    Strings.Replace(strRef, "", "", 0, -1);
    assertEquals("", strRef.get());

    strRef.set("");
    Strings.Replace(strRef, "", "", 0, 0);
    assertEquals("", strRef.get());

    strRef.set("");
    Strings.Replace(strRef, "", "", 0, 10);
    assertEquals("", strRef.get());

    strRef.set("foo");
    Strings.Replace(strRef, "", "", 0, -1);
    assertEquals("foo", strRef.get());

    strRef.set("foo");
    Strings.Replace(strRef, "", "", 0, 1);
    assertEquals("foo", strRef.get());

    strRef.set("foo");
    Strings.Replace(strRef, "", "", 1, 1);
    assertEquals("foo", strRef.get());

    strRef.set("foo");
    Strings.Replace(strRef, "", "f", 2, 2);
    assertEquals("foffo", strRef.get());

    // Make sure reg expr characters in the find expression are ignored
    strRef.set("f.o.o");
    Strings.Replace(strRef, ".", "!", 2, 1);
    assertEquals("f.o!o", strRef.get());

    // Enough special cases, now some easy replacements
    strRef.set("foo");
    Strings.Replace(strRef, "o", "i", 0, -1);
    assertEquals("fii", strRef.get());

    strRef.set("foo");
    Strings.Replace(strRef, "o", "i", 0, 0);
    assertEquals("foo", strRef.get());

    strRef.set("foo");
    Strings.Replace(strRef, "o", "i", 0, 1);
    assertEquals("fio", strRef.get());

    strRef.set("foo");
    Strings.Replace(strRef, "o", "i", 0, 10);
    assertEquals("fii", strRef.get());

    strRef.set("foo");
    Strings.Replace(strRef, "o", "i", 2, -1);
    assertEquals("foi", strRef.get());

    strRef.set("foo");
    Strings.Replace(strRef, "o", "i", 2, 0);
    assertEquals("foo", strRef.get());

    strRef.set("foo");
    Strings.Replace(strRef, "o", "i", 2, 1);
    assertEquals("foi", strRef.get());

    strRef.set("foo");
    Strings.Replace(strRef, "o", "i", 2, 10);
    assertEquals("foi", strRef.get());
  }

  /**
   * Tests {@link Strings#StrComp(String, String)}.
   */
  public void testStrComp() {
    // Check that if either of the string arguments is null that a NullPointerException will be
    // thrown
    try {
      Strings.StrComp(null, "");
      fail();
    } catch (NullPointerException expected) {
    }
    try {
      Strings.StrComp("", null);
      fail();
    } catch (NullPointerException expected) {
    }

    // Empty strings
    assertTrue(Strings.StrComp("", "") == 0);
    assertTrue(Strings.StrComp("foo", "") > 0);
    assertTrue(Strings.StrComp("", "foo") < 0);

    // Other strings
    assertTrue(Strings.StrComp("foo", "foo") == 0);
    assertTrue(Strings.StrComp("fooo", "foo") > 0);
    assertTrue(Strings.StrComp("foo", "bar") > 0);
    assertTrue(Strings.StrComp("foo", "fooo") < 0);
    assertTrue(Strings.StrComp("bar", "foo") < 0);
  }

  /**
   * Tests {@link Strings#StrReverse(StringReferenceParameter)}.
   */
  public void testStrReverse() {
    // Check that if the string argument is null that a NullPointerException will be thrown
    StringReferenceParameter strRef = new StringReferenceParameter(null);
    try {
      Strings.StrReverse(strRef);
      fail();
    } catch (NullPointerException expected) {
    }

    // Trim spaces of strings
    strRef.set("");
    Strings.StrReverse(strRef);
    assertEquals("", strRef.get());

    strRef.set("foo");
    Strings.StrReverse(strRef);
    assertEquals("oof", strRef.get());
  }
}
