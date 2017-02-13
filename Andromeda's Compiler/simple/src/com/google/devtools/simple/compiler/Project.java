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

import com.google.devtools.simple.util.Preconditions;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

/**
 * This class gives access to Simple project files.
 *
 * <p>A Simple project file is essentially a Java properties file.
 *
 * @author Herbert Czymontek
 */
public final class Project {

  /**
   * Representation of a source file containing its name and file location.
   */
  public static class SourceDescriptor {

    // Qualified name of the class defined by the source file
    private final String qualifiedName;

    // File descriptor for the source
    private final File file;

    private SourceDescriptor(String qualifiedName, File file) {
      this.qualifiedName = qualifiedName;
      this.file = file;
    }

    /**
     * Returns the qualified name of the class defined by the source file.
     *
     * @return  class name of source file
     */
    public String getQualifiedName() {
      return qualifiedName;
    }

    /**
     * Returns a file descriptor for the source file
     *
     * @return  file descriptor
     */
    public File getFile() {
      return file;
    }
  }

  /*
   * Property tags defined in the project file:
   *
   *    main - qualified name of main form class
   *    name - application name
   *    source - comma separated list of source root directories
   *    assets - assets directory (for image and data files bundled with the application)
   *    build - output directory for the compiler
   *    key - location of key store for signing Android applications
   */
  private static final String MAINTAG = "main";
  private static final String NAMETAG = "name";
  private static final String SOURCETAG = "source";
  private static final String ASSETSTAG = "assets";
  private static final String BUILDTAG = "build";
  private static final String LIBSTAG = "libs";
  private static final String KEYLOCATIONTAG = "key.location";
  private static final String KEYALIASTAG = "key.alias";
  private static final String APPNAMETAG = "appname";
  // Simple source file extension
  public static final String SOURCEFILE_EXTENSION = ".vac";

  // Table containing project properties
  private Properties properties;

  // Project root directory
  private String projectRoot;

  // Build output directory override, or null.
  private String buildDirOverride;

  // List of source files
  private List<SourceDescriptor> sources;

  /**
   * Creates a new Simple project descriptor.
   *
   * @param projectFile  path to project file
   * @throws IOException  if the project file cannot be read
   */
  public Project(String projectFile) throws IOException {
    this(new File(projectFile));
  }

  /**
   * Creates a new Simple project descriptor.
   *
   * @param projectFile  path to project file
   * @param buildDirOverride  build output directory override, or null
   * @throws IOException  if the project file cannot be read
   */
  public Project(String projectFile, String buildDirOverride) throws IOException {
    this(new File(projectFile));
    this.buildDirOverride = buildDirOverride;
  }

  /**
   * Creates a new Simple project descriptor.
   *
   * @param file  project file
   * @throws IOException  if the project file cannot be read
   */
  public Project(File file) throws IOException {
    File parentFile = Preconditions.checkNotNull(file.getParentFile());
    projectRoot = parentFile.getAbsolutePath();

    // Load project file
    properties = new Properties();
    FileInputStream in = new FileInputStream(file);
    try {
      properties.load(in);
    } finally {
      in.close();
    }
  }

  /**
   * Returns the name of the main form class
   *
   * @return  main form class name
   */
  public String getMainForm() {
    return properties.getProperty(MAINTAG);
  }

  /**
   * Sets the name of the main form class.
   *
   * @param main  main form class name
   */
  public void setMainForm(String main) {
    properties.setProperty(MAINTAG, main);
  }

  /**
   * Returns the name of the project (application).
   *
   * @return  project name
   */
  public String getProjectName() {
    return properties.getProperty(NAMETAG);
  }

  public String getAppName() {
    return properties.getProperty(APPNAMETAG);
  }
  
  public String getAppTheme() {
    return properties.getProperty("apptheme");
  }
  
  public String getAppMinSDK() {
    return properties.getProperty("appminsdk");
  }
  
  public String getAppIcon() {
    return properties.getProperty("appicon");
  }
  
  /**
   * Sets the name of the project (application)
   *
   * @param name  project name
   */
  public void setProjectName(String name) {
    properties.setProperty(NAMETAG, name);
  }

  /**
   * Returns the root path of the project.
   *
   * @return  project root path
   */
  public String getProjectRoot() {
    return projectRoot;
  }

  /**
   * Returns the location of the assets directory.
   *
   * @return  assets directory
   */
  public File getAssetsDirectory() {
    return new File(projectRoot, properties.getProperty(ASSETSTAG));
  }
  
  public File getLibsDirectory() {
    return new File(projectRoot, properties.getProperty(LIBSTAG));
  }

  /**
   * Returns the location of the build output directory.
   *
   * @return  build output directory
   */
  public File getBuildDirectory() {
    if (buildDirOverride != null) {
      return new File(buildDirOverride);
    }
    return new File(projectRoot, properties.getProperty(BUILDTAG));
  }

  /**
   * Returns the location of the keystore for signing Android applications.
   *
   * @return  location of keystore
   */
  public String getKeystoreLocation() {
    return properties.getProperty(KEYLOCATIONTAG);
  }

  /**
   * Sets the location of the keystore for signing Android applications.
   *
   * @param location  location of keystore
   */
  public void setKeystoreLocation(String location) {
    properties.setProperty(KEYLOCATIONTAG, location);
  }

  /**
   * Returns the alias name of the keystore for signing Android applications.
   *
   * @return  alias name of keystore
   */
  public String getKeystoreAlias() {
    return properties.getProperty(KEYALIASTAG);
  }

  /**
   * Sets the alias name of the keystore for signing Android applications.
   *
   * @param alias  alias name of keystore
   */
  public void setKeystoreAlias(String alias) {
    properties.setProperty(KEYALIASTAG, alias);
  }

  /*
   * Recursively visits source directories and adds found Simple source files to the list of source
   * files.
   */
  private void visitSourceDirectories(String root, File file) {
    if (file.isDirectory()) {
      // Recursively visit nested directories.
      for (String child : file.list()) {
        visitSourceDirectories(root, new File(file, child));
      }
    } else {
      // Add Simple source files to the source file list
      if (file.getName().endsWith(SOURCEFILE_EXTENSION)) {
        String absName = file.getAbsolutePath();
        String name = absName.substring(root.length() + 1, absName.length() -
            SOURCEFILE_EXTENSION.length());
        sources.add(new SourceDescriptor(name.replace(File.separatorChar, '.'), file));
      }
    }
  }

  /**
   * Returns a list of Simple source files in the project.
   *
   * @return  list of source files
   */
  public List<SourceDescriptor> getSources() {
    // Lazily discover source files
    if (sources == null) {
      sources = new ArrayList<SourceDescriptor>();
      String sourceTag = properties.getProperty(SOURCETAG);
      for (String sourceDir : sourceTag.split(",")) {
        File dir = new File(projectRoot + File.separatorChar + sourceDir);
        visitSourceDirectories(dir.getAbsolutePath(), dir);
      }
    }
    return sources;
  }
}
