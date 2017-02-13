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

import com.google.devtools.simple.runtime.variants.ArrayVariant;
import com.google.devtools.simple.runtime.variants.Variant;

import java.util.List;

import junit.framework.TestCase;

/**
 * Tests for {@link Arrays}.
 *
 * @author Herbert Czymontek
 */
public class ArraysTest extends TestCase {

  public ArraysTest(String testName) {
    super(testName);
  }

  /**
   * Tests {@link Arrays#Filter(String[], String, boolean)}.
   */
  public void testFilter() {
    // Check that if the array argument is null that a NullPointerException will be thrown.
    // Note that if the function is called from Simple code that the compiler will generate
    // exception handlers to convert NullPointerExceptions into the equivalent runtime error
    try {
      Arrays.Filter(null, "", false);
      fail();
    } catch (NullPointerException expected) {
    }
    
    // Check that if the string argument is null that a NullPointerException will be thrown.
    try {
      Arrays.Filter(new String[0], null, false);
      fail();
    } catch (NullPointerException expected) {
    }

    // Some regular filtering
    String[] array = new String[] { "foo", "foobar", "bar", "foo" };

    String[] includeResult = Arrays.Filter(array, "foo", true);
    assertEquals(2, includeResult.length);
    assertContentsAnyOrder(java.util.Arrays.asList(includeResult), "foo", "foobar");

    String[] excludeResult = Arrays.Filter(array, "foo", false);
    assertEquals(1, excludeResult.length);
    assertEquals("bar", excludeResult[0]);    
  }

  /**
   * Tests {@link Arrays#Join(String[], String)}.
   */
  public void testJoin() {
    // Check that if the array argument is null that a NullPointerException will be thrown.
    try {
      Arrays.Join(null, "");
      fail();
    } catch (NullPointerException expected) {
    }
    
    // Check that if the string argument is null that a NullPointerException will be thrown.
    try {
      Arrays.Join(new String[0], null);
      fail();
    } catch (NullPointerException expected) {
    }

    // Some regular joining
    assertEquals("foo", Arrays.Join(new String[] { "foo" }, ", "));
    assertEquals("foo, bar", Arrays.Join(new String[] { "foo", "bar" }, ", "));
  }

  /**
   * Tests {@link Arrays#Split2(String, String, int)}.
   */
  public void testSplit() {
    // Check that if the array argument is null that a NullPointerException will be thrown.
    try {
      Arrays.Split2(null, "", 1);
      fail();
    } catch (NullPointerException expected) {
    }

    // Check that if the string argument is null that a NullPointerException will be thrown.
    try {
      Arrays.Split2("", null, 1);
      fail();
    } catch (NullPointerException expected) {
    }

    // Check that if the count argument less or equal to 0 that an  IllegalArgumentException will
    // be thrown.
    try {
      Arrays.Split2("", "", Integer.MIN_VALUE);
      fail();
    } catch (IllegalArgumentException expected) {
    }

    try {
      Arrays.Split2("", "", 0);
      fail();
    } catch (IllegalArgumentException expected) {
    }

    // Some regular splitting
    String[] result = Arrays.Split2("", "", 1); 
    assertEquals(1, result.length);
    assertEquals("", result[0]);

    result = Arrays.Split2("", "", Integer.MAX_VALUE);
    assertEquals(1, result.length);
    assertEquals("", result[0]);

    result = Arrays.Split2("", "foo", 1);
    assertEquals(1, result.length);
    assertEquals("", result[0]);

    result = Arrays.Split2("", "foo", Integer.MAX_VALUE);
    assertEquals(1, result.length);
    assertEquals("", result[0]);

    result = Arrays.Split2("foo", "", 1);
    assertEquals(1, result.length);
    assertEquals("foo", result[0]);

    result = Arrays.Split2("foo", "", 3);
    assertEquals(3, result.length);
    assertContentsInOrder(java.util.Arrays.asList(result), "", "f", "oo");
  
    result = Arrays.Split2("foo", "", Integer.MAX_VALUE);
    assertEquals(5, result.length);
    assertContentsInOrder(java.util.Arrays.asList(result), "", "f", "o", "o", "");

    result = Arrays.Split2("foobarfoobarfoo", "foo", 1);
    assertEquals(1, result.length);
    assertEquals("foobarfoobarfoo", result[0]);

    result = Arrays.Split2("foobarfoobarfoo", "foo", 3);
    assertEquals(3, result.length);
    assertContentsInOrder(java.util.Arrays.asList(result), "", "bar", "barfoo");
  
    result = Arrays.Split2("foobarfoobarfoo", "foo", Integer.MAX_VALUE);
    assertEquals(4, result.length);
    assertContentsInOrder(java.util.Arrays.asList(result), "", "bar", "bar", "");

    result = Arrays.Split2("barfoobarfoobar", "foo", 1);
    assertEquals(1, result.length);
    assertEquals("barfoobarfoobar", result[0]);

    result = Arrays.Split2("barfoobarfoobar", "foo", 2);
    assertEquals(2, result.length);
    assertContentsInOrder(java.util.Arrays.asList(result), "bar", "barfoobar");
  
    result = Arrays.Split2("barfoobarfoobar", "foo", Integer.MAX_VALUE);
    assertEquals(3, result.length);
    assertContentsInOrder(java.util.Arrays.asList(result), "bar", "bar", "bar");  
  }

  /**
   * Tests {@link Arrays#UBound(Variant, int)}.
   */
  public void testUBound() {
    // Check that if the array argument is null that a NullPointerException will be thrown.
    try {
      Arrays.UBound(null, 1);
      fail();
    } catch (NullPointerException expected) {
    }

    final int FIRST_DIMENSION = 3;
    final int SECOND_DIMENSION = 5;
    Variant singleDimArray = ArrayVariant.getArrayVariant(new int[FIRST_DIMENSION]);
    Variant multiDimArray =
        ArrayVariant.getArrayVariant(new int[FIRST_DIMENSION][SECOND_DIMENSION]);

    // Negative dimension
    try {
      Arrays.UBound(singleDimArray, -1);
      fail();
    } catch (IllegalArgumentException expected) {
    }

    // Zero dimension
    try {
      Arrays.UBound(singleDimArray, 0);
      fail();
    } catch (IllegalArgumentException expected) {
    }

    // Dimension too large
    try {
      Arrays.UBound(singleDimArray, FIRST_DIMENSION + 1);
      fail();
    } catch (IllegalArgumentException expected) {
    }

    // Single-dimension array
    assertEquals(FIRST_DIMENSION, Arrays.UBound(singleDimArray, 1));

    // Multi-dimension array
    assertEquals(FIRST_DIMENSION, Arrays.UBound(singleDimArray, 1));
    assertEquals(FIRST_DIMENSION, Arrays.UBound(multiDimArray, 1));
    assertEquals(SECOND_DIMENSION, Arrays.UBound(multiDimArray, 2));
  }

  private <T> void assertContentsAnyOrder(List<T> list, T... contents) {
    assertEquals(list.size(), contents.length);

    for (T element : contents) {
      assertTrue(list.contains(element));
    }
  }

  private <T> void assertContentsInOrder(List<T> list, T... contents) {
    assertEquals(list.size(), contents.length);

    for (int i = 0; i < contents.length; i++) {
      assertEquals(list.get(i), contents[i]);
    }
  }
}
