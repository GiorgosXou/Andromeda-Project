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

package com.google.devtools.simple;

import com.google.devtools.simple.compiler.CompilerSmokeTest;
import com.google.devtools.simple.compiler.parser.ParserTest;
import com.google.devtools.simple.compiler.scanner.ScannerTest;
import com.google.devtools.simple.runtime.ArraysTest;
import com.google.devtools.simple.runtime.ConversionsTest;
import com.google.devtools.simple.runtime.DatesTest;
import com.google.devtools.simple.runtime.FilesTest;
import com.google.devtools.simple.runtime.MathTest;
import com.google.devtools.simple.runtime.StringsTest;

import junit.framework.TestSuite;
import junit.framework.Test;

/**
 * Unit tests for com.google.devtools.simple
 *
 * <b>THIS TEST SUITE MUST BE RUN BY EVERY DEVELOPER BEFORE EVERY CHECKIN!</b>
 *
 * @author Herbert Czymontek
 */
public class AllTests {

  private static final Class<?>[] tests = {
    // com.google.devtools.simple.compiler
    CompilerSmokeTest.class,
    // com.google.devtools.simple.compiler.parser
    ParserTest.class,
    // com.google.devtools.simple.compiler.scanner
    ScannerTest.class,

    // com.google.devtools.simple.runtime
    ArraysTest.class,
    ConversionsTest.class,
    DatesTest.class,
    FilesTest.class,
    MathTest.class,
    StringsTest.class
  };

  public static Test suite() {
    TestSuite suite = new TestSuite("Simple Tests");
    for (Class<?> test : tests) {
      suite.addTestSuite(test);
    }
    return suite;
  }
}
