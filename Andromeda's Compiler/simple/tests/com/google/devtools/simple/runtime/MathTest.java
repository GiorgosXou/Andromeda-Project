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

import com.google.devtools.simple.runtime.errors.ConversionError;
import com.google.devtools.simple.runtime.variants.BooleanVariant;
import com.google.devtools.simple.runtime.variants.DoubleVariant;
import com.google.devtools.simple.runtime.variants.IntegerVariant;
import com.google.devtools.simple.runtime.variants.ObjectVariant;
import com.google.devtools.simple.runtime.variants.SingleVariant;
import com.google.devtools.simple.runtime.variants.StringVariant;

import junit.framework.TestCase;

/**
 * Tests for {@link Math}.
 *
 * @author Herbert Czymontek
 */
public class MathTest extends TestCase {

  public MathTest(String testName) {
    super(testName);
  }

  /**
   * Tests {@link Math#Abs(com.google.devtools.simple.runtime.variants.Variant)}.
   */
  public void testAbs() {
    // Variants containing numeric value
    assertEquals(123, Math.Abs(IntegerVariant.getIntegerVariant(123)).getInteger());
    assertEquals(123, Math.Abs(IntegerVariant.getIntegerVariant(-123)).getInteger());

    assertEquals(123.456, Math.Abs(DoubleVariant.getDoubleVariant(123.456)).getDouble());
    assertEquals(123.456, Math.Abs(DoubleVariant.getDoubleVariant(-123.456)).getDouble());

    // Non-numeric variants
    try {
      assertEquals("", Math.Abs(StringVariant.getStringVariant("")));
      fail();
    } catch (ConversionError expected) {
    }
  }

  /**
   * Tests {@link Math#Atn(double)}.
   */
  public void testAtn() {
    // The trigonometric (and some other mathematical) functions are just wrappers around their
    // Java runtime equivalent. Nevertheless we do some limited testing to ensure basic
    // functionality.
    assertEquals(java.lang.Math.atan(0), Math.Atn(0));
    assertEquals(java.lang.Math.atan(Math.PI / 4), Math.Atn(Math.PI / 4));
    assertEquals(java.lang.Math.atan(Math.PI / 2), Math.Atn(Math.PI / 2));
    assertEquals(java.lang.Math.atan(3 * (Math.PI / 4)), Math.Atn(3 * (Math.PI / 4)));
    assertEquals(java.lang.Math.atan(Math.PI), Math.Atn(Math.PI));
  }

  /**
   * Tests {@link Math#Cos(double)}.
   */
  public void testCos() {
    assertEquals(java.lang.Math.cos(0), Math.Cos(0));
    assertEquals(java.lang.Math.cos(Math.PI / 4), Math.Cos(Math.PI / 4));
    assertEquals(java.lang.Math.cos(Math.PI / 2), Math.Cos(Math.PI / 2));
    assertEquals(java.lang.Math.cos(3 * (Math.PI / 4)), Math.Cos(3 * (Math.PI / 4)));
    assertEquals(java.lang.Math.cos(Math.PI), Math.Cos(Math.PI));
  }

  /**
   * Tests {@link Math#DegreesToRadians(double)}.
   */
  public void testDegreesToRadians() {
    assertEquals(java.lang.Math.toRadians(0), Math.DegreesToRadians(0));
    assertEquals(java.lang.Math.toRadians(45), Math.DegreesToRadians(45));
    assertEquals(java.lang.Math.toRadians(90), Math.DegreesToRadians(90));
    assertEquals(java.lang.Math.toRadians(135), Math.DegreesToRadians(135));
    assertEquals(java.lang.Math.toRadians(180), Math.DegreesToRadians(180));
  }

  /**
   * Tests {@link Math#RadiansToDegrees(double)}.
   */
  public void testRadiansToDegrees() {
    assertEquals(java.lang.Math.toDegrees(0), Math.RadiansToDegrees(0));
    assertEquals(java.lang.Math.toDegrees(Math.PI / 4),
                 Math.RadiansToDegrees(Math.PI / 4));
    assertEquals(java.lang.Math.toDegrees(Math.PI / 2),
                 Math.RadiansToDegrees(Math.PI / 2));
    assertEquals(java.lang.Math.toDegrees(3 * (Math.PI / 4)),
                 Math.RadiansToDegrees(3 * (Math.PI / 4)));
    assertEquals(java.lang.Math.toDegrees(Math.PI),
                 Math.RadiansToDegrees(Math.PI));
  }

  /**
   * Tests {@link Math#Exp(double)}.
   */
  public void testExp() {
    assertEquals(java.lang.Math.exp(0), Math.Exp(0));
    assertEquals(java.lang.Math.exp(1), Math.Exp(1));
    assertEquals(java.lang.Math.exp(10), Math.Exp(10));
  }

  /**
   * Tests {@link Math#Int(com.google.devtools.simple.runtime.variants.Variant)}.
   */
  public void testInt() {
    assertEquals(0, Math.Int(BooleanVariant.getBooleanVariant(false)));
    assertEquals(-1, Math.Int(BooleanVariant.getBooleanVariant(true)));

    assertEquals(-10, Math.Int(IntegerVariant.getIntegerVariant(-10)));
    assertEquals(0, Math.Int(IntegerVariant.getIntegerVariant(0)));
    assertEquals(10, Math.Int(IntegerVariant.getIntegerVariant(10)));

    assertEquals(-10, Math.Int(SingleVariant.getSingleVariant(-10.123f)));
    assertEquals(0, Math.Int(SingleVariant.getSingleVariant(-0.123f)));
    assertEquals(0, Math.Int(SingleVariant.getSingleVariant(0.123f)));
    assertEquals(10, Math.Int(SingleVariant.getSingleVariant(10.123f)));

    assertEquals(-10, Math.Int(DoubleVariant.getDoubleVariant(-10.123)));
    assertEquals(0, Math.Int(DoubleVariant.getDoubleVariant(-0.123)));
    assertEquals(0, Math.Int(DoubleVariant.getDoubleVariant(0.123)));
    assertEquals(10, Math.Int(DoubleVariant.getDoubleVariant(10.123)));

    assertEquals(-10, Math.Int(StringVariant.getStringVariant("-10")));
    assertEquals(0, Math.Int(StringVariant.getStringVariant("0")));
    assertEquals(10, Math.Int(StringVariant.getStringVariant("10")));

    try {
      Math.Int(StringVariant.getStringVariant("abc"));
      fail();
    } catch (ConversionError expected) {
    }

    try {
      Math.Int(ObjectVariant.getObjectVariant(null));
      fail();
    } catch (ConversionError expected) {
    }
  }

  /**
   * Tests {@link Math#Log(double)}.
   */
  public void testLog() {
    assertEquals(java.lang.Math.log(0), Math.Log(0));
    assertEquals(java.lang.Math.log(1), Math.Log(1));
    assertEquals(java.lang.Math.log(10), Math.Log(10));
    assertEquals(java.lang.Math.log(Math.E), Math.Log(Math.E));
  }

  /**
   * Tests {@link Math#Max(com.google.devtools.simple.runtime.variants.Variant,
   *   com.google.devtools.simple.runtime.variants.Variant)}.
   */
  public void testMax() {
    assertFalse(Math.Max(BooleanVariant.getBooleanVariant(false),
        BooleanVariant.getBooleanVariant(false)).getBoolean());
    assertTrue(Math.Max(BooleanVariant.getBooleanVariant(true),
        BooleanVariant.getBooleanVariant(true)).getBoolean());
    assertFalse(Math.Max(BooleanVariant.getBooleanVariant(true),
        BooleanVariant.getBooleanVariant(false)).getBoolean());
    assertFalse(Math.Max(BooleanVariant.getBooleanVariant(false),
        BooleanVariant.getBooleanVariant(true)).getBoolean());

    assertEquals(-10, Math.Max(IntegerVariant.getIntegerVariant(-10),
        IntegerVariant.getIntegerVariant(-10)).getInteger());
    assertEquals(10, Math.Max(IntegerVariant.getIntegerVariant(-10),
        IntegerVariant.getIntegerVariant(10)).getInteger());
    assertEquals(10, Math.Max(IntegerVariant.getIntegerVariant(10),
        IntegerVariant.getIntegerVariant(-10)).getInteger());
    assertEquals(10, Math.Max(IntegerVariant.getIntegerVariant(10),
        IntegerVariant.getIntegerVariant(10)).getInteger());

    assertEquals(-10.123, Math.Max(DoubleVariant.getDoubleVariant(-10.123),
        DoubleVariant.getDoubleVariant(-10.123)).getDouble());
    assertEquals(10.123, Math.Max(DoubleVariant.getDoubleVariant(-10.123),
        DoubleVariant.getDoubleVariant(10.123)).getDouble());
    assertEquals(10.123, Math.Max(DoubleVariant.getDoubleVariant(10.123),
        DoubleVariant.getDoubleVariant(-10.123)).getDouble());
    assertEquals(10.123, Math.Max(DoubleVariant.getDoubleVariant(10.123),
        DoubleVariant.getDoubleVariant(10.123)).getDouble());

    assertEquals(-10.123, Math.Max(StringVariant.getStringVariant("-10.123"),
        StringVariant.getStringVariant("-10.123")).getDouble());
    assertEquals(10.123, Math.Max(StringVariant.getStringVariant("-10.123"),
        StringVariant.getStringVariant("10.123")).getDouble());
    assertEquals(10.123, Math.Max(StringVariant.getStringVariant("10.123"),
        StringVariant.getStringVariant("-10.123")).getDouble());
    assertEquals(10.123, Math.Max(StringVariant.getStringVariant("10.123"),
        StringVariant.getStringVariant("10.123")).getDouble());

    try {
      Math.Max(StringVariant.getStringVariant("abc"), StringVariant.getStringVariant("def"));
      fail();
    } catch (ConversionError expected) {
    }

    try {
      Math.Max(IntegerVariant.getIntegerVariant(10), StringVariant.getStringVariant("def"));
      fail();
    } catch (ConversionError expected) {
    }

    try {
      Math.Max(StringVariant.getStringVariant("abc"), IntegerVariant.getIntegerVariant(10));
      fail();
    } catch (ConversionError expected) {
    }

    try {
      Math.Max(IntegerVariant.getIntegerVariant(10), ObjectVariant.getObjectVariant(null));
      fail();
    } catch (ConversionError expected) {
    }
  }

  /**
   * Tests {@link Math#Min(com.google.devtools.simple.runtime.variants.Variant,
   *   com.google.devtools.simple.runtime.variants.Variant)}.
   */
  public void testMin() {
    assertFalse(Math.Min(BooleanVariant.getBooleanVariant(false),
        BooleanVariant.getBooleanVariant(false)).getBoolean());
    assertTrue(Math.Min(BooleanVariant.getBooleanVariant(true),
        BooleanVariant.getBooleanVariant(true)).getBoolean());
    assertTrue(Math.Min(BooleanVariant.getBooleanVariant(true),
        BooleanVariant.getBooleanVariant(false)).getBoolean());
    assertTrue(Math.Min(BooleanVariant.getBooleanVariant(false),
        BooleanVariant.getBooleanVariant(true)).getBoolean());

    assertEquals(-10, Math.Min(IntegerVariant.getIntegerVariant(-10),
        IntegerVariant.getIntegerVariant(-10)).getInteger());
    assertEquals(-10, Math.Min(IntegerVariant.getIntegerVariant(-10),
        IntegerVariant.getIntegerVariant(10)).getInteger());
    assertEquals(-10, Math.Min(IntegerVariant.getIntegerVariant(10),
        IntegerVariant.getIntegerVariant(-10)).getInteger());
    assertEquals(10, Math.Min(IntegerVariant.getIntegerVariant(10),
        IntegerVariant.getIntegerVariant(10)).getInteger());

    assertEquals(-10.123, Math.Min(DoubleVariant.getDoubleVariant(-10.123),
        DoubleVariant.getDoubleVariant(-10.123)).getDouble());
    assertEquals(-10.123, Math.Min(DoubleVariant.getDoubleVariant(-10.123),
        DoubleVariant.getDoubleVariant(10.123)).getDouble());
    assertEquals(-10.123, Math.Min(DoubleVariant.getDoubleVariant(10.123),
        DoubleVariant.getDoubleVariant(-10.123)).getDouble());
    assertEquals(10.123, Math.Min(DoubleVariant.getDoubleVariant(10.123),
        DoubleVariant.getDoubleVariant(10.123)).getDouble());

    assertEquals(-10.123, Math.Min(StringVariant.getStringVariant("-10.123"),
        StringVariant.getStringVariant("-10.123")).getDouble());
    assertEquals(-10.123, Math.Min(StringVariant.getStringVariant("-10.123"),
        StringVariant.getStringVariant("10.123")).getDouble());
    assertEquals(-10.123, Math.Min(StringVariant.getStringVariant("10.123"),
        StringVariant.getStringVariant("-10.123")).getDouble());
    assertEquals(10.123, Math.Min(StringVariant.getStringVariant("10.123"),
        StringVariant.getStringVariant("10.123")).getDouble());

    try {
      Math.Min(StringVariant.getStringVariant("abc"), StringVariant.getStringVariant("def"));
      fail();
    } catch (ConversionError expected) {
    }

    try {
      Math.Min(IntegerVariant.getIntegerVariant(10), StringVariant.getStringVariant("def"));
      fail();
    } catch (ConversionError expected) {
    }

    try {
      Math.Min(StringVariant.getStringVariant("abc"), IntegerVariant.getIntegerVariant(10));
      fail();
    } catch (ConversionError expected) {
    }

    try {
      Math.Min(IntegerVariant.getIntegerVariant(10), ObjectVariant.getObjectVariant(null));
      fail();
    } catch (ConversionError expected) {
    }
  }

  /**
   * Tests {@link Math#Rnd()}.
   */
  public void testRnd() {
    // That's pretty hard to test. Nevertheless we want to at least invoke the function to make
    // sure it doesn't crash.
    Math.Rnd();
  }

  /**
   * Tests {@link Math#Sgn(double)}.
   */
  public void testSgn() {
    assertEquals(-1, Math.Sgn(-10.123));
    assertEquals(0, Math.Sgn(0));
    assertEquals(1, Math.Sgn(10.123));
  }

  /**
   * Tests {@link Math#Sin(double)}.
   */
  public void testSin() {
    assertEquals(java.lang.Math.sin(0), Math.Sin(0));
    assertEquals(java.lang.Math.sin(Math.PI / 4), Math.Sin(Math.PI / 4));
    assertEquals(java.lang.Math.sin(Math.PI / 2), Math.Sin(Math.PI / 2));
    assertEquals(java.lang.Math.sin(3 * (Math.PI / 4)), Math.Sin(3 * (Math.PI / 4)));
    assertEquals(java.lang.Math.sin(Math.PI), Math.Sin(Math.PI));
  }

  /**
   * Tests {@link Math#Sqr(double)}.
   */
  public void testSqr() {
    assertEquals(java.lang.Math.sqrt(0), Math.Sqr(0));
    assertEquals(java.lang.Math.sqrt(2), Math.Sqr(2));
    assertEquals(java.lang.Math.sqrt(4), Math.Sqr(4));
    assertEquals(java.lang.Math.sqrt(10), Math.Sqr(10));
  }

  /**
   * Tests {@link Math#Tan(double)}.
   */
  public void testTan() {
    assertEquals(java.lang.Math.tan(0), Math.Tan(0));
    assertEquals(java.lang.Math.tan(Math.PI / 4), Math.Tan(Math.PI / 4));
    assertEquals(java.lang.Math.tan(Math.PI / 2), Math.Tan(Math.PI / 2));
    assertEquals(java.lang.Math.tan(3 * (Math.PI / 4)), Math.Tan(3 * (Math.PI / 4)));
    assertEquals(java.lang.Math.tan(Math.PI), Math.Tan(Math.PI));
  }
}
