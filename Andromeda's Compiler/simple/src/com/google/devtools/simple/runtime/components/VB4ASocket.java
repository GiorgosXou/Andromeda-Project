/*
	2014 04 14 Lu Chengwei VB4A
	With Great Thanks to http://blog.csdn.net/zz7zz7zz/article/details/9341635
 */

package com.google.devtools.simple.runtime.components;

import com.google.devtools.simple.runtime.annotations.SimpleComponent;
import com.google.devtools.simple.runtime.annotations.SimpleFunction;
import com.google.devtools.simple.runtime.annotations.SimpleEvent;
import com.google.devtools.simple.runtime.annotations.SimpleObject;
import com.google.devtools.simple.runtime.annotations.SimpleProperty;
import com.google.devtools.simple.runtime.annotations.UsesPermissions;

@SimpleComponent
@SimpleObject
@UsesPermissions(permissionNames = "android.permission.INTERNET") 
public interface VB4ASocket extends Component {

@SimpleEvent
void SocketResponse(String rec);

@SimpleFunction
void OpenSoc(String ip, int port);

@SimpleFunction
void CloseSoc();

@SimpleFunction
void Reconnect();

@SimpleFunction
void Send(String txt);

}
