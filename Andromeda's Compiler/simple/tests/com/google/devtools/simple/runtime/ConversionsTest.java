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
import com.google.devtools.simple.runtime.variants.DoubleVariant;
import com.google.devtools.simple.runtime.variants.IntegerVariant;
import com.google.devtools.simple.runtime.variants.StringVariant;

import junit.framework.TestCase;

/**
 * Tests for {@link Conversions}.
 *
 * @author Herbert Czymontek
 */
public class ConversionsTest extends TestCase {

  public ConversionsTest(String testName) {
    super(testName);
  }

  /**
   * Tests {@link Conversions#Asc(String)}.
   */
  public void testAsc() {
    // Check that if the argument is null that a NullPointerException will be thrown.
    // Note that if the function is called from Simple code that the compiler will generate
    // exception handlers to convert NullPointerExceptions into the equivalent runtime error
    try {
      Conversions.Asc(null);
      fail();
    } catch (NullPointerException expected) {
    }

    // Empty string
    try {
      Conversions.Asc("");
      fail();
    } catch (IllegalArgumentException expected) {
    }

    // Other strings
    assertEquals(48, Conversions.Asc("0"));
    assertEquals(48, Conversions.Asc("0123"));
  }

  /**
   * Tests {@link Conversions#Chr(int)}.
   */
  public void testChr() {
    assertEquals("0", Conversions.Chr(48));
  }

  /**
   * Tests {@link Conversions#Hex(com.google.devtools.simple.runtime.variants.Variant)}.
   */
  public void testHex() {
    // Variants containing numeric value
    assertEquals("123ABC", Conversions.Hex(IntegerVariant.getIntegerVariant(0x123ABC)));
    assertEquals("FFFFFFFFFFFFFFFF", Conversions.Hex(IntegerVariant.getIntegerVariant(-1)));
    assertEquals("A", Conversions.Hex(DoubleVariant.getDoubleVariant(10.3)));

    // Non-numeric variants 
    try {
      assertEquals("", Conversions.Hex(StringVariant.getStringVariant("")));
      fail();
    } catch (ConversionError expected) {
    }
  }
}
