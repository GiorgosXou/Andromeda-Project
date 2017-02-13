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
import com.google.devtools.simple.runtime.errors.FileAlreadyExistsError;
import com.google.devtools.simple.runtime.errors.FileIOError;
import com.google.devtools.simple.runtime.errors.NoSuchFileError;
import com.google.devtools.simple.runtime.errors.UnknownFileHandleError;
import com.google.devtools.simple.runtime.variants.StringVariant;
import com.google.devtools.simple.runtime.variants.Variant;
import com.google.devtools.simple.runtime.android.ApplicationImpl;

import android.os.Environment;
import android.database.Cursor;  
import android.database.sqlite.SQLiteDatabase;  
import android.content.Context;  
import android.content.res.AssetManager;  
import com.google.devtools.simple.runtime.annotations.UsesPermissions;

import java.io.File;
import java.io.FilenameFilter;
import java.io.FileInputStream;
import java.io.InputStreamReader;  
import java.io.BufferedReader;
import java.io.OutputStreamWriter;
import java.io.FileOutputStream;

import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.io.FileOutputStream;   
import java.io.InputStream;   

import java.util.HashMap;
import java.util.Map;



/**
 * Implementation of various file related runtime functions.
 * 
 * @author Herbert Czymontek
 */
@SimpleObject
@UsesPermissions(permissionNames = "android.permission.WRITE_EXTERNAL_STORAGE,"
								  +"android.permission.MOUNT_UNMOUNT_FILESYSTEMS")
public final class Files {

  /**
   * Provides access to the root directory of the application.
   */
  public static interface RootProvider {
    File getApplicationRootDirectory();
  }
  
  /*
   * Helper class for handling file I/O.
   */
  private static class FileDescriptor {
    private final File file;
    private RandomAccessFile raf;

    private void raiseError(String message) {
      throw new FileIOError(message + ": " + file.getName());
    }

    FileDescriptor(File file) {
      this.file = file;
      try {
        raf = new RandomAccessFile(file, "rw");
      } catch (FileNotFoundException e) {
        raiseError("Opening file");
      }
    }

    void close() {
        try {
          raf.close();
        } catch (IOException e) {
          raiseError("Closing file");
        }
    }
  }

  /*
   * Simple is a single threaded language. Therefore no further
   * synchronization is needed here.
   */
  private static int fileHandleCounter;
  private static Map<Integer, FileDescriptor> fileHandleMap =
      new HashMap<Integer, FileDescriptor>();

  private static File rootDirectory;
  private static File assetDirectory;

  private Files() {  // COV_NF_LINE
  }                  // COV_NF_LINE

  /**
   * Sets a root directory for file access by the application.
   *
   * @param rootDir  root directory for file access by the application
   */
  public static void initialize(File rootDir) {
    rootDirectory = rootDir;
  }
  
  /*
   * Gets a File object for a given file name and raises a runtime error if the
   * file doesn't exist.
   */
  private static File getExistingFile(String name) {
    File file = new File(rootDirectory, name);
    if (!file.exists()) {
      throw new NoSuchFileError(name);
    }
    return file;
  }

  private static File getExistingFile2(String name) {
    File file = new File(name);
    if (!file.exists()) {
      throw new NoSuchFileError(name);
    }
    return file;
  }

  /*
   * Gets a File object for a given file name and raises a runtime error if the
   * file already exists.
   */
  private static File getNonExistingFile(String name) {
    File file = new File(rootDirectory, name);
    if (file.exists()) {
      throw new FileAlreadyExistsError(name);
    }
    return file;
  }

  private static File getNonExistingFile2(String name) {
    File file = new File(name); //rootDirectory, name);
    if (file.exists()) {
      throw new FileAlreadyExistsError(name);
    }
    return file;
  }

  /*
   * Gets the file descriptor associated with the file handle. Raises a runtime
   * error if no file descriptor can be found.
   */
  private static FileDescriptor getFileDescriptor(int handle) {
    FileDescriptor descriptor = fileHandleMap.get(handle);
    if (descriptor == null) {
      throw new UnknownFileHandleError();
    }
    return descriptor;
  }

  /*
   * Raises a file write runtime error.
   */
  private static void writeError(String name) {
    throw new FileIOError("Write error: " + name);
  }

  /*
   * Raises a file read runtime error.
   */
  private static void readError(String name) {
    throw new FileIOError("Read error: " + name);
  }

  /*
   * Raises a file read runtime error.
   */
  private static void seekError(String name) {
    throw new FileIOError("Seek error: " + name);
  }

  /*
   * Raises a file access runtime error.
   */
  private static void accessError(String name) {
    throw new FileIOError("Access error: " + name);
  }
  
  /**
   * Renames a file. Causes a runtime error if the file doesn't exist. 
   * 
   * @param oldname  file name before renaming
   * @param newname  file name after renaming
   */

@SimpleFunction
  public static void Rename(String oldname, String newname) {

    File oldfile = getExistingFile(oldname);
    File newfile = new File(rootDirectory, newname);

    if (!oldfile.equals(newfile)) {
      if (newfile.exists()) {
        throw new FileAlreadyExistsError(newname);
      }
  
      if (!oldfile.renameTo(newfile)) {
        accessError(oldname);
      }
    }
  }

  /*
  public static boolean SavePicture(String imagePath){
          try {  
            File file = new File(Environment.getExternalStorageDirectory(),  
                    System.currentTimeMillis() + ".png");  
            FileOutputStream stream = new FileOutputStream(file);  
            baseBitmap.compress(CompressFormat.PNG, 100, stream);  
            Toast.makeText(MainActivity.this, "保存图片成功", 0).show();  
              
        } catch (Exception e) {  
            Toast.makeText(MainActivity.this, "保存图片失败", 0).show();  
            e.printStackTrace();  
        }  
  }
  */
  
  @SimpleFunction
  public static void Rename2(String oldname, String newname) {

    File oldfile = getExistingFile(oldname);
    File newfile = new File(newname); //new File(rootDirectory, newname);

    if (!oldfile.equals(newfile)) {
      if (newfile.exists()) {
        throw new FileAlreadyExistsError(newname);
      }
  
      if (!oldfile.renameTo(newfile)) {
        accessError(oldname);
      }
    }
  }

  /**
   * Deletes a file.
   * 
   * @param name  name of file to delete
   */
  @SimpleFunction
  public static void Delete(String name) {
    File file = getExistingFile(name);
    if (file.isDirectory() || !file.delete()) {
      accessError(name);
    }
  }

  @SimpleFunction
  public static void Delete2(String name) {
    File file = getExistingFile2(name);
    if (file.isDirectory() || !file.delete()) {
      accessError(name);
    }
  }
  
  /**
   * Creates a new directory.
   * 
   * @param name  name of new directory
   */
  @SimpleFunction
  public static void Mkdir(String name) {
    if (!getNonExistingFile(name).mkdir()) {
      accessError(name);
    }
  }

  @SimpleFunction
  public static void Mkdir2(String name) {
    if (!getNonExistingFile2(name).mkdir()) {
      accessError(name);
    }
  }

   public static FilenameFilter getFileExtensionFilter(String extension) {   
    final String _extension = extension;   
    return new FilenameFilter() {   
        public boolean accept(File file, String name) {   
            boolean ret = name.endsWith(_extension);    
            return ret;   
        }   
    };   
   }   
  @SimpleFunction
  public static String[] ListAll(String dname) {
    String[] tnames;
	File[] tfiles;
	tfiles=getExistingFile(dname).listFiles();
	tnames=new String[tfiles.length];
	for(int i = 0; i < tfiles.length; i++){
      tnames[i]=tfiles[i].getName();
    }
	return tnames;
  }
  
  @SimpleFunction
  public static String[] ListAll2(String dname) {
    String[] tnames;
	File[] tfiles;
	tfiles=getExistingFile2(dname).listFiles();
	tnames=new String[tfiles.length];
	for(int i = 0; i < tfiles.length; i++){
      tnames[i]=tfiles[i].getName();
    }
	return tnames;
  }
  @SimpleFunction
  public static String[] ListFiles(String dname,String filter) {
    String[] tnames;
	File[] tfiles;
	tfiles=getExistingFile(dname).listFiles(getFileExtensionFilter(filter));
	tnames=new String[tfiles.length];
	for(int i = 0; i < tfiles.length; i++){
      tnames[i]=tfiles[i].getName();
    }
	return tnames;
  }
  
  @SimpleFunction
  public static String[] ListFiles2(String dname,String filter) {
    String[] tnames;
	File[] tfiles;
	tfiles=getExistingFile2(dname).listFiles(getFileExtensionFilter(filter));
	tnames=new String[tfiles.length];
	for(int i = 0; i < tfiles.length; i++){
      tnames[i]=tfiles[i].getName();
    }
	return tnames;
  }
  /**
   * Deletes a directory.
   * 
   * @param name  name of directory to delete
   */

  @SimpleFunction
  public static void Rmdir(String name) {
    File directory = getExistingFile(name);
    if (!directory.isDirectory() || !directory.delete()) {
      accessError(name);
    }
  }
  @SimpleFunction
  public static void Rmdir2(String name) {
    File directory = getExistingFile2(name);
    if (!directory.isDirectory() || !directory.delete()) {
      accessError(name);
    }
  }

  /**
   * Checks whether the given name is the name of an existing directory.
   * Causes a runtime error if the directory doesn't exist.
   * 
   * @param name  name to check
   * @return  {@code true} if the name belongs to an existing directory,
   *          {@code false} otherwise
   */

  @SimpleFunction
  public static boolean IsDirectory(String name) {
    return getExistingFile(name).isDirectory();
  }

  @SimpleFunction
  public static boolean IsDirectory2(String name) {
    return getExistingFile2(name).isDirectory();
  }

  /**
   * Checks whether a file or directory exists.
   * 
   * @param name  file to check
   * @return  {@code true} if the file or directory exists, {@code false}
   *          otherwise
   */
  @SimpleFunction
  public static boolean Exists(String name) {
    return new File(rootDirectory, name).exists(); //rootDirectory, 
  }

  @SimpleFunction
  public static boolean Exists2(String name) {
    return new File(name).exists(); //rootDirectory, 
  }

  /**
   * Opens an existing file or creates a new file for reading or writing. 
   * 
   * @param name  name of file to open or create
   * @return  file handle
   */
  @SimpleFunction
  public static int Open(String name) {
    File file = new File(rootDirectory, name);
	if (!file.exists()) {
      try {
        file.createNewFile();
      } catch (IOException e) {
        Log.Error(Log.MODULE_NAME_RTL, e.getMessage());
        e.printStackTrace();

        throw new FileIOError("Creating file: " + name);
      }
    } else if (IsDirectory(name)) {
      throw new FileIOError("Cannot open directory: " + name);
    }
    FileDescriptor descriptor = new FileDescriptor(file);
    int handle = ++fileHandleCounter;
    fileHandleMap.put(handle, descriptor);
    return handle;
  }

  @SimpleFunction
  public static void Unpack(String fName, String Opath) {
	File jhPath=new File(Opath);  
    try {   
		
		if (jhPath.exists()){
			jhPath.delete();
		}
		
		AssetManager am= ApplicationImpl.MyContext().getAssets();   
		InputStream is=am.open(fName);    
        FileOutputStream fos=new FileOutputStream(jhPath);   
        byte[] buffer=new byte[1024];   
        int count = 0;   

        while((count = is.read(buffer))>0){   
			fos.write(buffer,0,count);   
        }   
        
        fos.flush();   
        fos.close();   
        is.close();   
    } catch (IOException e) {   
        e.printStackTrace();   
    }   
  }

  @SimpleFunction
  public static void UnpackDB(String fName, String Opath) {
	File jhPath=new File(Opath);  
    try {   
    
		AssetManager am= ApplicationImpl.MyContext().getAssets();   
		InputStream is=am.open(fName);    
        FileOutputStream fos=new FileOutputStream(jhPath);   
        byte[] buffer=new byte[1024];   
        int count = 0;   

        while((count = is.read(buffer))>0){   
			fos.write(buffer,0,count);   
        }   
        
        fos.flush();   
        fos.close();   
        is.close();   
    } catch (IOException e) {   
        e.printStackTrace();   
    }   
  }
  
  @SimpleFunction
  public static int Open2(String name) {
    File file = new File(name); //(rootDirectory, name);
	

	if (!file.exists()) {
      try {
        file.createNewFile();
      } catch (IOException e) {
        Log.Error(Log.MODULE_NAME_RTL, e.getMessage());
        e.printStackTrace();

        throw new FileIOError("Creating file: " + name);
      }
    } else if (IsDirectory(name)) {
      throw new FileIOError("Cannot open directory: " + name);
    }     

    FileDescriptor descriptor = new FileDescriptor(file);
    int handle = ++fileHandleCounter;

    fileHandleMap.put(handle, descriptor);

    return handle;
  }

  /**
   * Closes a file previously opened.
   * 
   * @param handle  handle of file to close
   */
  @SimpleFunction
  public static void Close(int handle) {
    getFileDescriptor(handle).close();
    fileHandleMap.remove(handle);
  }

  /**
   * Checks whether the current file position is at the end of the file.
   *
   * @param handle  handle of file to check
   * @return  {@code true} if the end of the file was reaches, {@code false}
   *          otherwise
   */
  @SimpleFunction
  public static boolean Eof(int handle) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      return descriptor.raf.getFilePointer() == descriptor.file.length();
    } catch (IOException e) {
      readError(descriptor.file.getName());
      return false;  // COV_NF_LINE
    }
  }

  /**
   * Positions the file pointer to an absolute position.
   * 
   * @param handle  handle of file
   * @param offset  absolute position within file
   * @return  new position within file
   */
  @SimpleFunction
  public static long Seek(int handle, long offset) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      if (offset < 0 || offset > descriptor.file.length()) {
        seekError(descriptor.file.getName());
      }
      descriptor.raf.seek(offset);
      long newOffset = descriptor.raf.getFilePointer();
      if (newOffset != offset) {
        seekError(descriptor.file.getName());
      }
      return newOffset;
    } catch (IOException e) {
      seekError(descriptor.file.getName());
      return 0;  // COV_NF_LINE
    }
  }

  /**
   * Returns the size of a file.
   * 
   * @param handle  handle of file
   * @return  file size
   */
  @SimpleFunction
  public static long Size(int handle) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    return descriptor.file.length();
  }

  /**
   * Writes a String to a file. 
   * 
   * @param handle  handle of file
   * @param value  value to write
   */
  @SimpleFunction
  public static void WriteString(int handle, String value) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      descriptor.raf.writeUTF(value);
    } catch (IOException e) {
      writeError(descriptor.file.getName());
    }
  }

  /**
   * Reads a String value from a file.
   * 
   * @param handle  handle of file
   * @return  value read
   */
  @SimpleFunction
  public static String ReadString(int handle) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      return descriptor.raf.readUTF();
    } catch (IOException e) {
      readError(descriptor.file.getName());
      return "";  // COV_NF_LINE
    }
  }

  /**
   * Writes a Boolean value to a file.
   * 
   * @param handle  handle of file
   * @param value  value to write
   */
  @SimpleFunction
  public static void WriteBoolean(int handle, boolean value) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      descriptor.raf.writeBoolean(value);
    } catch (IOException e) {
      writeError(descriptor.file.getName());
    }
  }

  /**
   * Reads a Boolean value from a file.
   * 
   * @param handle  handle of file
   * @return  value read
   */
  @SimpleFunction
  public static boolean ReadBoolean(int handle) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      return descriptor.raf.readBoolean();
    } catch (IOException e) {
      readError(descriptor.file.getName());
      return false;  // COV_NF_LINE
    }
  }


  /**
   * Writes a Byte value to a file.
   * 
   * @param handle  handle of file
   * @param value  value to write
   */
  @SimpleFunction
  public static void WriteByte(int handle, byte value) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      descriptor.raf.writeByte(value);
    } catch (IOException e) {
      writeError(descriptor.file.getName());
    }
  }

  /**
   * Reads a Byte value from a file.
   * 
   * @param handle  handle of file
   * @return  value read
   */
  @SimpleFunction
  public static byte ReadByte(int handle) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      return descriptor.raf.readByte();
    } catch (IOException e) {
      readError(descriptor.file.getName());
      return 0;  // COV_NF_LINE
    }
  }

  /**
   * Writes a Short value to a file.
   * 
   * @param handle  handle of file
   * @param value  value to write
   */
  @SimpleFunction
  public static void WriteShort(int handle, short value) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      descriptor.raf.writeShort(value);
    } catch (IOException e) {
      writeError(descriptor.file.getName());
    }
  }

  /**
   * Reads a Short value from a file.
   * 
   * @param handle  handle of file
   * @return  value read
   */
  @SimpleFunction
  public static short ReadShort(int handle) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      return descriptor.raf.readShort();
    } catch (IOException e) {
      readError(descriptor.file.getName());
      return 0;  // COV_NF_LINE
    }
  }

  /**
   * Writes an Integer boolean value to a file.
   * 
   * @param handle  handle of file
   * @param value  value to write
   */
  @SimpleFunction
  public static void WriteInteger(int handle, int value) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      descriptor.raf.writeInt(value);
    } catch (IOException e) {
      writeError(descriptor.file.getName());
    }
  }

  /**
   * Reads an Integer value from a file.
   * 
   * @param handle  handle of file
   * @return  value read
   */
  @SimpleFunction
  public static int ReadInteger(int handle) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      return descriptor.raf.readInt();
    } catch (IOException e) {
      readError(descriptor.file.getName());
      return 0;  // COV_NF_LINE
    }
  }

  /**
   * Writes a Long value to a file.
   * 
   * @param handle  handle of file
   * @param value  value to write
   */
  @SimpleFunction
  public static void WriteLong(int handle, long value) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      descriptor.raf.writeLong(value);
    } catch (IOException e) {
      writeError(descriptor.file.getName());
    }
  }

  /**
   * Reads a Long value from a file.
   * 
   * @param handle  handle of file
   * @return  value read
   */
  @SimpleFunction
  public static long ReadLong(int handle) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      return descriptor.raf.readLong();
    } catch (IOException e) {
      readError(descriptor.file.getName());
      return 0;  // COV_NF_LINE
    }
  }

  /**
   * Writes a Single value to a file.
   * 
   * @param handle  handle of file
   * @param value  value to write
   */
  @SimpleFunction
  public static void WriteSingle(int handle, float value) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      descriptor.raf.writeFloat(value);
    } catch (IOException e) {
      writeError(descriptor.file.getName());
    }
  }

  /**
   * Reads a Single value from a file.
   * 
   * @param handle  handle of file
   * @return  value read
   */
  @SimpleFunction
  public static float ReadSingle(int handle) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      return descriptor.raf.readFloat();
    } catch (IOException e) {
      readError(descriptor.file.getName());
      return 0;  // COV_NF_LINE
    }
  }



  /**
   * Writes a Double value to a file.
   * 
   * @param handle  handle of file
   * @param value  value to write
   */
  @SimpleFunction
  public static void WriteDouble(int handle, double value) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      descriptor.raf.writeDouble(value);
    } catch (IOException e) {
      writeError(descriptor.file.getName());
    }
  }

  /**
   * Reads a Double value from a file.
   * 
   * @param handle  handle of file
   * @return  value read
   */
  @SimpleFunction
  public static double ReadDouble(int handle) {
    FileDescriptor descriptor = getFileDescriptor(handle);
    try {
      return descriptor.raf.readDouble();
    } catch (IOException e) {
      readError(descriptor.file.getName());
      return 0;  // COV_NF_LINE
    }
  }

  @SimpleFunction
  public static String GetSDPath() {
	return Environment.getExternalStorageDirectory()+"";
  }

  @SimpleFunction
  public static String GetAppPath() {
	return rootDirectory+"";
  }
  /*
  @SimpleFunction
  public static String GetPKGPath() {
	
  }
  */

  @SimpleFunction
  public static String ReadTxt(String strFilePath) {
	String path = strFilePath;
	String content = ""; //文件内容字符串
	//打开文件
	File file = new File(path);
	//如果path是传递过来的参数，可以做一个非目录的判断
	if (file.isDirectory())
            {
                //Log.d("TestFile", "The File doesn't not exist.");
            }
	else
            {
		try {
                    InputStream instream = new FileInputStream(file); 
                    if (instream != null) 
                    {
                        InputStreamReader inputreader = new InputStreamReader(instream);
                        BufferedReader buffreader = new BufferedReader(inputreader);
                        String line;
                        //分行读取
                        while (( line = buffreader.readLine()) != null) {
                            content += line + "\n";
                        }                
                        instream.close();
                    }
                }
                catch (java.io.FileNotFoundException e) 
                {
                    //Log.d("TestFile", "The File doesn't not exist.");
                } 
                catch (IOException e) 
                {
                     //Log.d("TestFile", e.getMessage());
                }
            }
            return content;
    }

  @SimpleFunction
  public static void WriteTxt(String filePath, String content) {
              //如果filePath是传递过来的参数，可以做一个后缀名称判断；没有指定的文件名没有后缀，则自动保存为.txt格式
//            if(!filePath.endsWith(".txt") && !filePath.endsWith(".log")) 
//            filePath += ".txt";
            //保存文件
            File file = new File(filePath);
            try {
            FileOutputStream outstream = new FileOutputStream(file);
            OutputStreamWriter out = new OutputStreamWriter(outstream);
            out.write(content);
            out.close();
            } catch (java.io.IOException e) {
            e.printStackTrace();
			}

  }






}
