:: ------------------------------------------------------------------------------------------
:: Convenience script to invoke the Simple compiler
::
:: Before using this script, make sure the following two environment variables are defined:
::   ANDROID_HOME    root directory of Android SDK (must be version 1.5 or newer)
::   SIMPLE_HOME     root directory of Simple distribution (same as directory of this script)
:: ------------------------------------------------------------------------------------------

java -classpath %SIMPLE_HOME%\SimpleCompiler.jar com.google.devtools.simple.tools.ProjectCreator %*
