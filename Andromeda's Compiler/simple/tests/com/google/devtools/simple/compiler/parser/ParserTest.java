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

package com.google.devtools.simple.compiler.parser;

import com.google.devtools.simple.compiler.Compiler;
import com.google.devtools.simple.compiler.Compiler.Platform;
import com.google.devtools.simple.compiler.scanner.Scanner;

import junit.framework.TestCase;

/**
 * Tests for {@link Parser}.
 *
 * @author Igor Karp
 */
public class ParserTest extends TestCase {
  private Compiler compiler;

  public ParserTest(String testName) {
    super(testName);
  }

  @Override
  protected void setUp() throws Exception {
    super.setUp();

    compiler = new Compiler(Platform.None, System.out, System.err);
  }

  /**
   * Tests whether the Simple parser correctly (without exception) handles the
   * absence of () in event handler declaration.
   */
  public void testErrorInEventHandlerDeclaration() {
    // TODO: parser should complain about Int, should be Integer
    try {
      Scanner scanner = new Scanner(compiler, "Dim A As Int\n"
          + "\n"
          + "Event Form1.Initialize\n" // should be Event Form1.Initialize()
          + "  A = 1\n"
          + "End Event\n"
          + "\n"
          + "$Properties\n"
          + "  $Source $Object\n"
          + "$End $Properties\n");
  
      Parser parser = new Parser(compiler, scanner, "does.not.matter");
      parser.parse();

    } catch (Exception e) {
      e.printStackTrace();
      fail();
    }

    assertEquals(1, compiler.getErrorCount());
  }

  /**
   * Tests whether the Simple parser correctly (without exception) handles the
   * presence of () after the New operator for objects (instead of arrays).
   */
  public void testErrorInNewOperator() {
    try {
      Scanner scanner = new Scanner(compiler,
          "Sub Bar()\n"
          + "  Dim n As Object\n"
          + "  n = New Object()\n"
          + "End Sub\n"
          + "\n"
          + "$Properties\n"
          + "  $Source $Object\n"
          + "$End $Properties\n");
  
      Parser parser = new Parser(compiler, scanner, "does.not.matter");
      parser.parse();
      compiler.resolve();

    } catch (Exception e) {
      e.printStackTrace();
      fail();
    }

    assertEquals(1, compiler.getErrorCount());
  }

  /**
   * Tests whether the Simple parser prohibits sequences of identity operators.
   */
  public void testErrorInIdentityOperator() {
    try {
      Scanner scanner = new Scanner(compiler,
          "Sub Bar()\n"
          + "  Dim n As Integer\n"
          + "  n = -+n\n"
          + "End Sub\n"
          + "\n"
          + "$Properties\n"
          + "  $Source $Object\n"
          + "$End $Properties\n");
  
      Parser parser = new Parser(compiler, scanner, "does.not.matter");
      parser.parse();

    } catch (Exception e) {
      e.printStackTrace();
      fail();
    }

    assertEquals(1, compiler.getErrorCount());
  }


  /**
   * Tests whether the Simple parser detects function wrong Exit statements.
   */
  public void testErrorInExitStatement() {
    try {
      Scanner scanner = new Scanner(compiler,
          "Sub Bar()\n"
          + "  Exit Function\n"
          + "End Sub\n"
          + "\n"
          + "$Properties\n"
          + "  $Source $Object\n"
          + "$End $Properties\n");
  
      Parser parser = new Parser(compiler, scanner, "does.not.matter");
      parser.parse();
      compiler.resolve();

    } catch (Exception e) {
      e.printStackTrace();
      fail();
    }

    assertEquals(1, compiler.getErrorCount());
  }
}
