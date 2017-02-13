package com.google.devtools.simple.runtime.components;

import com.google.devtools.simple.runtime.annotations.SimpleComponent;
import com.google.devtools.simple.runtime.annotations.SimpleFunction;
import com.google.devtools.simple.runtime.annotations.SimpleObject;
import com.google.devtools.simple.runtime.annotations.SimpleProperty;
import com.google.devtools.simple.runtime.annotations.UsesPermissions;

@SimpleComponent
@SimpleObject
@UsesPermissions(permissionNames = "android.permission.READ_PHONE_STATE")
public interface VB4AProp extends Component {

    @SimpleFunction
    String GetIMEI();

	@SimpleFunction
    String GetModel();

	@SimpleFunction
	String GetSoftwareVersion();

	@SimpleFunction
	String GetPhoneNum();

	@SimpleFunction
	String GetSimSerial();

	@SimpleFunction
	String GetID();

	//@SimpleFunction
	//String GetABI();

}
