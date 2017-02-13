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

package com.google.devtools.simple.compiler;

import java.io.IOException;
import java.io.File;
import com.google.devtools.simple.compiler.Compiler.Platform;

/**
 * Main entry point for the command line version of the Simple compiler.
 *
 * @author Herbert Czymontek
 */
public final class Main {

  // COV_NF_START
  private static final String CHECKDIR = System.getenv("VB4A_HOME");
  private Main() {
  }

  /**
   * Main entry point.
   *
   * @param args  command line arguments
   */
  public static void main(String[] args) {

/*  if (new File(CHECKDIR+"vbdroid.exe").exists()==false)
    {
		System.err.println("Filed to start compiler, please check the vb4a installation.");
		System.exit(0);
    }
*/
	if (args.length != 1) {
      System.err.println("Î´Öª´íÎó£¡");
    } else {
      try {
        Compiler.compile(Platform.Android, new Project(args[0], null), System.out, System.err);
      } catch (IOException ioe) {
        System.err.println("Cannot read project file '" + args[0] + "'");
      }
    }
  }

  // COV_NF_END
}
