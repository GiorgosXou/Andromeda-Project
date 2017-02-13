/*
  VB4A ComplexNumber Lu Chengwei 2014 03 16
*/

package com.google.devtools.simple.runtime.environment;

import com.google.devtools.simple.runtime.annotations.SimpleFunction;
import com.google.devtools.simple.runtime.annotations.SimpleObject;
import com.google.devtools.simple.runtime.annotations.SimpleProperty;
import com.google.devtools.simple.runtime.errors.IndexOutOfBoundsError;
import com.google.devtools.simple.runtime.variants.Variant;
import com.google.devtools.simple.runtime.Files;
import java.io.UnsupportedEncodingException;
import org.apache.http.util.EncodingUtils; 

@SimpleObject
public class Survey {
 private String lineSep="\n";
 private String itemSep=",";
 private String codec="UTF-8";
 private boolean haveHead=true;
 private String[][] tMa;
@SimpleProperty
 public String LineSep() {
	return this.lineSep;
 }

@SimpleFunction
 public String[][] LoadCSV(String CSVFname) throws UnsupportedEncodingException {
	String CSVContent=Files.ReadTxt(CSVFname);
    CSVContent = EncodingUtils.getString(CSVContent.getBytes(codec),"UTF-8");
	String[] lines=CSVContent.split("\\Q" + lineSep + "\\E");
	String[] cols;
	int dx=lines.length;
	String[] tc=lines[0].split("\\Q" + itemSep + "\\E");
	int dy=tc.length;
	tMa=new String[dx][dy];
	for(int i=0;i<lines.length;i++){
		cols=lines[i].split("\\Q" + itemSep + "\\E");
		for(int j=0;j<cols.length;j++){
			tMa[i][j]=cols[j];
		}
	}
	return tMa;
 }

}