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

import com.google.devtools.simple.runtime.errors.FileAlreadyExistsError;
import com.google.devtools.simple.runtime.errors.FileIOError;
import com.google.devtools.simple.runtime.errors.NoSuchFileError;
import com.google.devtools.simple.runtime.errors.UnknownFileHandleError;

import junit.framework.TestCase;

import java.io.File;
import java.io.IOException;

/**
 * Tests for {@link Files}.
 *
 * @author Herbert Czymontek
 */
public class FilesTest extends TestCase {

  private File tmpDir; 

  public FilesTest(String testName) {
    super(testName);

    Files.initialize(new File("/"));
  }

  @Override
  protected void setUp() throws Exception {
    super.setUp();

    tmpDir = createTempDir();
  }

  @Override
  protected void tearDown() throws Exception {
    super.tearDown();

    deleteDirectoryContents(tmpDir);
    tmpDir.delete();
  }
  
  /**
   * Tests {@link Files#Rename(String, String)}.
   */
  public void testRename() throws IOException {

    File source = new File(tmpDir, "testRenameSource");
    source.createNewFile();

    File target = new File(tmpDir, "testRenameTarget");
    
    // Source is null
    try {
      Files.Rename(null, target.getAbsolutePath());
      fail();
    } catch (NullPointerException expected) {
    }

    // Target is null
    try {
      Files.Rename(source.getAbsolutePath(), null);
      fail();
    } catch (NullPointerException expected) {
    }

    // Source does not exist
    try {
      Files.Rename("whatever", target.getAbsolutePath());
      fail();
    } catch (NoSuchFileError expected) {
    }

    // Target exists
    File existingTarget = new File(tmpDir, "testRenameExistingTarget");
    existingTarget.createNewFile();

    try {
      Files.Rename(source.getAbsolutePath(), existingTarget.getAbsolutePath());
      fail();
    } catch (FileAlreadyExistsError expected) {
    }

    // Rename with both names the same
    Files.Rename(source.getAbsolutePath(), source.getAbsolutePath());

    // Rename
    Files.Rename(source.getAbsolutePath(), target.getAbsolutePath());
  }

  /**
   * Tests {@link Files#Delete(String)}.
   */
  public void testDelete() throws IOException {
    // Name is null
    try {
      Files.Delete(null);
      fail();
    } catch (NullPointerException expected) {
    }
    
    // File does not exist
    try {
      Files.Delete("whatever");
      fail();
    } catch (NoSuchFileError expected) {
    }

    // File is directory
    File directory = new File(tmpDir, "testDeleteDirectory");
    directory.mkdir();

    try {
      Files.Delete(directory.getAbsolutePath());
      fail();
    } catch (FileIOError expected) {
    }

    // Delete
    File file = new File(tmpDir, "testDeleteFile");
    file.createNewFile();

    Files.Delete(file.getAbsolutePath());
  }

  /**
   * Tests {@link Files#Mkdir(String)}.
   */
  public void testMkdir() throws IOException {
    // Name is null
    try {
      Files.Mkdir(null);
      fail();
    } catch (NullPointerException expected) {
    }
    
    // Directory already exists
    File existingDirectory = new File(tmpDir, "testMkdirExistingDirectory");
    existingDirectory.mkdir();

    try {
      Files.Mkdir(existingDirectory.getAbsolutePath());
      fail();
    } catch (FileAlreadyExistsError expected) {
    }

    // File with same name exists
    File existingFile = new File(tmpDir, "testMkdirExistingFile");
    existingFile.createNewFile();

    try {
      Files.Mkdir(existingFile.getAbsolutePath());
      fail();
    } catch (FileAlreadyExistsError expected) {
    }

    // Mkdir
    File directory = new File(tmpDir, "testMkdirDirectory");
    Files.Mkdir(directory.getAbsolutePath());
  }

  /**
   * Tests {@link Files#Rmdir(String)}.
   */
  public void testRmdir() throws IOException {
    // Name is null
    try {
      Files.Rmdir(null);
      fail();
    } catch (NullPointerException expected) {
    }

    // Directory does not exist
    try {
      Files.Rmdir("whatever");
      fail();
    } catch (NoSuchFileError expected) {
    }

    // File with same name exists
    File existingFile = new File(tmpDir, "testRmdirExistingFile");
    existingFile.createNewFile();
    try {
      Files.Rmdir(existingFile.getAbsolutePath());
      fail();
    } catch (FileIOError expected) {
    }
    
    // Rmdir
    File existingDirectory = new File(tmpDir, "testRmdirExistingDirectory");
    existingDirectory.mkdir();

    Files.Rmdir(existingDirectory.getAbsolutePath());
  }

  /**
   * Tests {@link Files#Exists(String)}.
   */
  public void testExists() throws IOException {
    // Name is null
    try {
      Files.Exists(null);
      fail();
    } catch (NullPointerException expected) {
    }

    // File/directory does not exist
    assertFalse(Files.Exists("whatever"));

    // File exists
    File existingFile = new File(tmpDir, "testExistsExistingFile");
    existingFile.createNewFile();

    assertTrue(Files.Exists(existingFile.getAbsolutePath()));
    
    // Directory exists
    File existingDirectory = new File(tmpDir, "testExistsExistingDirectory");
    existingDirectory.mkdir();

    assertTrue(Files.Exists(existingDirectory.getAbsolutePath()));
  }

  /**
   * Tests {@link Files#IsDirectory(String)}.
   */
  public void testIsDirectory() throws IOException {
    // Name is null
    try {
      Files.IsDirectory(null);
      fail();
    } catch (NullPointerException expected) {
    }

    // File/directory does not exist
    try {
      Files.IsDirectory("whatever");
      fail();
    } catch (NoSuchFileError expected) {
    }

    // Is directory
    File existingDirectory = new File(tmpDir, "testIsDirectoryExistingDirectory");
    existingDirectory.mkdir();

    assertTrue(Files.IsDirectory(existingDirectory.getAbsolutePath()));
    
    // Is file
    File existingFile = new File(tmpDir, "testIsDirectoryExistingFile");
    existingFile.createNewFile();

    assertFalse(Files.IsDirectory(existingFile.getAbsolutePath()));
  }

  /**
   * Tests {@link Files#Open(String)}.
   */
  public void testOpen() throws IOException {
    // Name is null
    try {
      Files.Open(null);
      fail();
    } catch (NullPointerException expected) {
    }

    // Is directory
    File existingDirectory = new File(tmpDir, "testIsDirectoryExistingDirectory");
    existingDirectory.mkdir();

    try {
      Files.Open(existingDirectory.getAbsolutePath());
      fail();
    } catch (FileIOError expected) {
    }

    // Does not exist
    File file = new File(tmpDir, "testOpenFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.Close(handle);
    
    assertTrue(file.exists());

    // Exists
    File existingFile = new File(tmpDir, "testOpenExistingFile");
    existingFile.createNewFile();

    handle = Files.Open(existingFile.getAbsolutePath());
    Files.Close(handle);
  }

  /**
   * Tests {@link Files#Close(int)}.
   */
  public void testClose() {
    // Unknown handle and Close
    File file = new File(tmpDir, "testCloseFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.Close(handle);

    try {
      Files.Close(handle);
      fail();
    } catch (UnknownFileHandleError expected) {
    }
  }

  /**
   * Tests {@link Files#Eof(int)}.
   */
  public void testEof() {
    // Unknown handle
    File file = new File(tmpDir, "testEofFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.WriteLong(handle, 0x123456789ABCDEFL);
    Files.Close(handle);

    try {
      Files.Eof(handle);
      fail();
    } catch (UnknownFileHandleError expected) {
    }

    // Is not eof
    handle = Files.Open(file.getAbsolutePath());
    assertFalse(Files.Eof(handle));
    
    // Is eof
    Files.ReadLong(handle);
    assertTrue(Files.Eof(handle));

    // Write some more
    Files.WriteLong(handle, 0x123456789ABCDEFL);
    assertTrue(Files.Eof(handle));

    // Seek back
    Files.Seek(handle, 4);
    assertFalse(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#Seek(int, long)}.
   */
  public void testSeek() {
    // Unknown handle
    File file = new File(tmpDir, "testSeekFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.WriteLong(handle, 0x123456789ABCDEFL);
    Files.WriteLong(handle, 0xFEDCBA987654321L);
    Files.Close(handle);

    try {
      Files.Seek(handle, 0);
      fail();
    } catch (UnknownFileHandleError expected) {
    }
    
    // Seek position before start of file (negative offset)
    handle = Files.Open(file.getAbsolutePath());

    try {
      Files.Seek(handle, -1);
      fail();
    } catch (FileIOError expected) {
    }

    // Seek position in the middle of file
    assertEquals(8, Files.Seek(handle, 8));
    assertEquals(0xFEDCBA987654321L, Files.ReadLong(handle));

    // Seek position start of file
    assertEquals(0, Files.Seek(handle, 0));
    assertEquals(0x123456789ABCDEFL, Files.ReadLong(handle));

    // Seek position end of file
    assertEquals(16, Files.Seek(handle, 16));
    assertTrue(Files.Eof(handle));

    // Seek position beyond end of file
    try {
      Files.Seek(handle, Long.MAX_VALUE);
      fail();
    } catch (FileIOError expected) {
    }

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#Size(int)}.
   */
  public void testSize() {
    // Unknown handle
    File file = new File(tmpDir, "testSizeFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.Close(handle);

    try {
      Files.Size(handle);
      fail();
    } catch (UnknownFileHandleError expected) {
    }

    // Empty file size
    handle = Files.Open(file.getAbsolutePath());

    assertEquals(0, Files.Size(handle));

    // File size
    Files.WriteLong(handle, 0x123456789ABCDEFL);

    assertEquals(8, Files.Size(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#WriteString(int, String)}.
   */
  public void testWriteString() {
    // Unknown handle
    File file = new File(tmpDir, "testWriteStringFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.Close(handle);

    try {
      Files.WriteString(handle, "foo");
      fail();
    } catch (UnknownFileHandleError expected) {
    }
    
    // Write null
    handle = Files.Open(file.getAbsolutePath());

    try {
      Files.WriteString(handle, null);
      fail();
    } catch (NullPointerException expected) {
    }
    
    // Write empty string
    Files.WriteString(handle, "");
    
    // Write
    Files.WriteString(handle, "foo");

    Files.Seek(handle, 0);
    assertEquals("", Files.ReadString(handle));
    assertEquals("foo", Files.ReadString(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#ReadString(int)}.
   */
  public void testReadString() {
    // Unknown handle
    File file = new File(tmpDir, "testReadStringFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.WriteString(handle, "");
    Files.WriteString(handle, "foo");
    Files.Close(handle);

    try {
      Files.ReadString(handle);
      fail();
    } catch (UnknownFileHandleError expected) {
    }

    // Read
    handle = Files.Open(file.getAbsolutePath());

    assertEquals("", Files.ReadString(handle));
    assertEquals("foo", Files.ReadString(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#WriteBoolean(int, boolean)}.
   */
  public void testWriteBoolean() {
    // Unknown handle
    File file = new File(tmpDir, "testWriteBooleanFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.Close(handle);

    try {
      Files.WriteBoolean(handle, true);
      fail();
    } catch (UnknownFileHandleError expected) {
    }
    
    // Write
    handle = Files.Open(file.getAbsolutePath());
    Files.WriteBoolean(handle, true);
    Files.WriteBoolean(handle, false);
    
    Files.Seek(handle, 0);
    assertTrue(Files.ReadBoolean(handle));
    assertFalse(Files.ReadBoolean(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#ReadBoolean(int)}.
   */
  public void testReadBoolean() {
    // Unknown handle
    File file = new File(tmpDir, "testReadBooleanFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.WriteBoolean(handle, true);
    Files.WriteBoolean(handle, false);
    Files.Close(handle);

    try {
      Files.ReadString(handle);
      fail();
    } catch (UnknownFileHandleError expected) {
    }

    // Read
    handle = Files.Open(file.getAbsolutePath());

    assertTrue(Files.ReadBoolean(handle));
    assertFalse(Files.ReadBoolean(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#WriteByte(int, byte)}.
   */
  public void testWriteByte() {
    // Unknown handle
    File file = new File(tmpDir, "testWriteByteFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.Close(handle);

    try {
      Files.WriteByte(handle, Byte.MAX_VALUE);
      fail();
    } catch (UnknownFileHandleError expected) {
    }
    
    // Write
    handle = Files.Open(file.getAbsolutePath());
    Files.WriteByte(handle, Byte.MAX_VALUE);
    Files.WriteByte(handle, Byte.MIN_VALUE);
    
    Files.Seek(handle, 0);
    assertEquals(Byte.MAX_VALUE, Files.ReadByte(handle));
    assertEquals(Byte.MIN_VALUE, Files.ReadByte(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#ReadByte(int)}.
   */
  public void testReadByte() {
    // Unknown handle
    File file = new File(tmpDir, "testReadByteFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.WriteByte(handle, Byte.MAX_VALUE);
    Files.WriteByte(handle, Byte.MIN_VALUE);
    Files.Close(handle);

    try {
      Files.ReadByte(handle);
      fail();
    } catch (UnknownFileHandleError expected) {
    }

    // Read
    handle = Files.Open(file.getAbsolutePath());

    assertEquals(Byte.MAX_VALUE, Files.ReadByte(handle));
    assertEquals(Byte.MIN_VALUE, Files.ReadByte(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#WriteShort(int, short)}.
   */
  public void testWriteShort() {
    // Unknown handle
    File file = new File(tmpDir, "testWriteShortFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.Close(handle);

    try {
      Files.WriteShort(handle, Short.MAX_VALUE);
      fail();
    } catch (UnknownFileHandleError expected) {
    }
    
    // Write
    handle = Files.Open(file.getAbsolutePath());
    Files.WriteShort(handle, Short.MAX_VALUE);
    Files.WriteShort(handle, Short.MIN_VALUE);
    
    Files.Seek(handle, 0);
    assertEquals(Short.MAX_VALUE, Files.ReadShort(handle));
    assertEquals(Short.MIN_VALUE, Files.ReadShort(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#ReadShort(int)}.
   */
  public void testReadShort() {
    // Unknown handle
    File file = new File(tmpDir, "testReadShortFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.WriteShort(handle, Short.MAX_VALUE);
    Files.WriteShort(handle, Short.MIN_VALUE);
    Files.Close(handle);

    try {
      Files.ReadShort(handle);
      fail();
    } catch (UnknownFileHandleError expected) {
    }

    // Read
    handle = Files.Open(file.getAbsolutePath());

    assertEquals(Short.MAX_VALUE, Files.ReadShort(handle));
    assertEquals(Short.MIN_VALUE, Files.ReadShort(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#WriteInteger(int, int)}.
   */
  public void testWriteInteger() {
    // Unknown handle
    File file = new File(tmpDir, "testWriteIntegerFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.Close(handle);

    try {
      Files.WriteInteger(handle, Integer.MAX_VALUE);
      fail();
    } catch (UnknownFileHandleError expected) {
    }
    
    // Write
    handle = Files.Open(file.getAbsolutePath());
    Files.WriteInteger(handle, Integer.MAX_VALUE);
    Files.WriteInteger(handle, Integer.MIN_VALUE);
    
    Files.Seek(handle, 0);
    assertEquals(Integer.MAX_VALUE, Files.ReadInteger(handle));
    assertEquals(Integer.MIN_VALUE, Files.ReadInteger(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#ReadInteger(int)}.
   */
  public void testReadInteger() {
    // Unknown handle
    File file = new File(tmpDir, "testReadIntegerFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.WriteInteger(handle, Integer.MAX_VALUE);
    Files.WriteInteger(handle, Integer.MIN_VALUE);
    Files.Close(handle);

    try {
      Files.ReadInteger(handle);
      fail();
    } catch (UnknownFileHandleError expected) {
    }

    // Read
    handle = Files.Open(file.getAbsolutePath());

    assertEquals(Integer.MAX_VALUE, Files.ReadInteger(handle));
    assertEquals(Integer.MIN_VALUE, Files.ReadInteger(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#WriteLong(int, long)}.
   */
  public void testWriteLong() {
    // Unknown handle
    File file = new File(tmpDir, "testWriteLongFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.Close(handle);

    try {
      Files.WriteLong(handle, Long.MAX_VALUE);
      fail();
    } catch (UnknownFileHandleError expected) {
    }
    
    // Write
    handle = Files.Open(file.getAbsolutePath());
    Files.WriteLong(handle, Long.MAX_VALUE);
    Files.WriteLong(handle, Long.MIN_VALUE);
    
    Files.Seek(handle, 0);
    assertEquals(Long.MAX_VALUE, Files.ReadLong(handle));
    assertEquals(Long.MIN_VALUE, Files.ReadLong(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#ReadLong(int)}.
   */
  public void testReadLong() {
    // Unknown handle
    File file = new File(tmpDir, "testReadLongFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.WriteLong(handle, Long.MAX_VALUE);
    Files.WriteLong(handle, Long.MIN_VALUE);
    Files.Close(handle);

    try {
      Files.ReadLong(handle);
      fail();
    } catch (UnknownFileHandleError expected) {
    }

    // Read
    handle = Files.Open(file.getAbsolutePath());

    assertEquals(Long.MAX_VALUE, Files.ReadLong(handle));
    assertEquals(Long.MIN_VALUE, Files.ReadLong(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#WriteSingle(int, float)}.
   */
  public void testWriteSingle() {
    // Unknown handle
    File file = new File(tmpDir, "testWriteSingleFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.Close(handle);

    try {
      Files.WriteSingle(handle, Float.MAX_VALUE);
      fail();
    } catch (UnknownFileHandleError expected) {
    }
    
    // Write
    handle = Files.Open(file.getAbsolutePath());
    Files.WriteSingle(handle, Float.MAX_VALUE);
    Files.WriteSingle(handle, Float.MIN_VALUE);
    
    Files.Seek(handle, 0);
    assertEquals(Float.MAX_VALUE, Files.ReadSingle(handle));
    assertEquals(Float.MIN_VALUE, Files.ReadSingle(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#ReadSingle(int)}.
   */
  public void testReadSingle() {
    // Unknown handle
    File file = new File(tmpDir, "testReadSingleFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.WriteSingle(handle, Float.MAX_VALUE);
    Files.WriteSingle(handle, Float.MIN_VALUE);
    Files.Close(handle);

    try {
      Files.ReadSingle(handle);
      fail();
    } catch (UnknownFileHandleError expected) {
    }

    // Read
    handle = Files.Open(file.getAbsolutePath());

    assertEquals(Float.MAX_VALUE, Files.ReadSingle(handle));
    assertEquals(Float.MIN_VALUE, Files.ReadSingle(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#WriteDouble(int, double)}.
   */
  public void testWriteDouble() {
    // Unknown handle
    File file = new File(tmpDir, "testWriteDoubleFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.Close(handle);

    try {
      Files.WriteDouble(handle, Double.MAX_VALUE);
      fail();
    } catch (UnknownFileHandleError expected) {
    }
    
    // Write
    handle = Files.Open(file.getAbsolutePath());
    Files.WriteDouble(handle, Double.MAX_VALUE);
    Files.WriteDouble(handle, Double.MIN_VALUE);
    
    Files.Seek(handle, 0);
    assertEquals(Double.MAX_VALUE, Files.ReadDouble(handle));
    assertEquals(Double.MIN_VALUE, Files.ReadDouble(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  /**
   * Tests {@link Files#ReadDouble(int)}.
   */
  public void testReadDouble() {
    // Unknown handle
    File file = new File(tmpDir, "testReadDoubleFile");

    int handle = Files.Open(file.getAbsolutePath());
    Files.WriteDouble(handle, Double.MAX_VALUE);
    Files.WriteDouble(handle, Double.MIN_VALUE);
    Files.Close(handle);

    try {
      Files.ReadDouble(handle);
      fail();
    } catch (UnknownFileHandleError expected) {
    }

    // Read
    handle = Files.Open(file.getAbsolutePath());

    assertEquals(Double.MAX_VALUE, Files.ReadDouble(handle));
    assertEquals(Double.MIN_VALUE, Files.ReadDouble(handle));
    assertTrue(Files.Eof(handle));

    Files.Close(handle);
  }

  private static File createTempDir() {
    String baseName = System.getProperty("java.io.tmpdir") + File.separator + System.currentTimeMillis() + '-';
    for (int attempts = 0; attempts < 1000; attempts++) {      File tempDir = new File(baseName + attempts);      if (tempDir.mkdir()) {        return tempDir;      }    }

    throw new IllegalStateException("Cannot create temp directory with base name " + baseName);
  }

  private static void deleteDirectoryContents(File dir) throws IOException {
    if (!dir.isDirectory()) {
      throw new IllegalArgumentException("directory expected");
    }

    File[] files = dir.listFiles();    if (files == null) {      throw new IOException("cannot get directory listings");    }    for (File file : files) {      if (file.isDirectory()) {
        deleteDirectoryContents(file);
      }
      file.delete();    }
  }
}
