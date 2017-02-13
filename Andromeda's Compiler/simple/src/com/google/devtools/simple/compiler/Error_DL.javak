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

/**
 * This class supplies error messages for reporting by the compiler.
 * 
 * @author Herbert Czymontek
 */
public final class Error extends Message {

  /**
   * Error message templates.
   */
  public static final String errArrayDimensionExpected;
  public static final String errArrayOrCollectionNeededInForEach;
  public static final String errAssignmentOrCallExprExpected;
  public static final String errCannotConvertType;
  public static final String errCannotInvokeEvent;
  public static final String errCaseElseNotLast;
  public static final String errConstantPoolOverflow;
  public static final String errConstantValueExpected;
  public static final String errCorruptedSourceFileProperties;
  public static final String errDataMemberExpected;
  public static final String errDimensionInDynamicArray;
  public static final String errExpected;
  public static final String errForNextIdentifierMismatch;
  public static final String errFunctionOrArrayTypeNeeded;
  public static final String errFunctionTooBig;
  public static final String errIdentifierDoesNotResolveToSymbol;
  public static final String errIdentifierNotFound;
  public static final String errIllegalCharacter;
  public static final String errInstanceMemberWithoutMe;
  public static final String errMalformedDecimalConstant;
  public static final String errMalformedFloatConstant;
  public static final String errMalformedHexConstant;
  public static final String errMalformedUnicodeEscapeSequence;
  public static final String errMultipleOnErrorStatements;
  public static final String errNoComponentImplementation;
  public static final String errNoMatchForExit;
  public static final String errNoPackage;
  public static final String errObjectOrArrayTypeNeeded;
  public static final String errObjectTypeNeeded;
  public static final String errOperandNotAssignable;
  public static final String errPropertyWithoutGetterOrSetter;
  public static final String errRaiseEventFromObjectFunction;
  public static final String errReadError;
  public static final String errRuntimeErrorTypeNeeded;
  public static final String errScalarIntegerTypeNeededForOperand;
  public static final String errScalarTypeNeededForOperand;
  public static final String errScalarTypeNeededInForNext;
  public static final String errScalarTypeNeededInSelectCase;
  public static final String errSymbolRedefinition;
  public static final String errTooFewArguments;
  public static final String errTooFewIndices;
  public static final String errTooManyArguments;
  public static final String errTooManyIndices;
  public static final String errUndefinedSymbol;
  public static final String errUnexpected;
  public static final String errUnterminatedString;
  public static final String errValueExpected;
  public static final String errWriteError;
  public static final String UseChinese = System.getenv("VB4AUSECH");
  

  static {
    errArrayDimensionExpected = Message.localize("errArrayDimensionExpected",
        "数组维度未定义|Array dimension expected");
    errArrayOrCollectionNeededInForEach = Message.localize("errArrayOrCollectionNeededInForEach",
        "For-Each语句需要使用数组(Array)或集合(Collection)类型|Array or Collection needed to iterate over in For-Each statement");
    errAssignmentOrCallExprExpected = Message.localize("errAssignmentOrCallExprExpected",
        "Assignment or Call expression expected|Assignment or Call expression expected");
    errCannotConvertType = Message.localize("errCannotConvertType",
        "无法执行从 %1 到 %2 的类型转换|Cannot convert from %1 type to %2 type");
    errCannotInvokeEvent = Message.localize("errCannotInvokeEvent",
        "无法直接调用事件，使用RaiseEvent语句|Cannot invoke events directly - use RaiseEvent statement");
    errCaseElseNotLast = Message.localize("errCaseElseNotLast",
        "Case Else语句应该为Case的最后一个|Case Else statement is not last Case statement");
    errConstantPoolOverflow = Message.localize("errConstantPoolOverflow",
        "生成class文件 %1 时常数池溢出|Constant pool overflow generating class file for %1");
    errConstantValueExpected = Message.localize("errConstantValueExpected",
        "需要常数值|Constant value expected");
    errCorruptedSourceFileProperties = Message.localize("errCorruptedSourceFileProperties",
        "文件Property部分损坏|Property section of source file corrupted");
    errDataMemberExpected = Message.localize("errDataMemberExpected",
        "%1 不是数据成员, 但它应该是|%1 is not a data member, but should be");
    errDimensionInDynamicArray = Message.localize("errDimensionInDynamicArray",
        "动态数组不允许定义维度|Dimensions not allowed for dynamically sized arrays");
    errExpected = Message.localize("errExpected",
        "需要 '%1' 但是发现 '%2'|Expected '%1' but '%2' found");
    errForNextIdentifierMismatch = Message.localize("errForNextIdentifierMismatch",
        "Next语句中的标识符 %1 和For语句中的标识符 %2 不匹配|Identifier %1 in Next statement does not match identifier %2 in For Statement");
    errFunctionOrArrayTypeNeeded = Message.localize("errFunctionOrArrayTypeNeeded",
        "需要定义函数或数组类型|Function or array type expected");
    errFunctionTooBig = Message.localize("errFunctionTooBig",
        "Class %1 中的函数 %2 过大|Function %2 in class %1 too big");
    errIdentifierDoesNotResolveToSymbol = Message.localize("errIdentifierDoesNotResolveToSymbol",
        "Qualified identifier %1 does not resolve to a symbol|Qualified identifier %1 does not resolve to a symbol");
    errIdentifierNotFound = Message.localize("errIdentifierNotFound",
        "未找到标识符 %1 |Identifier %1 not found");
    errIllegalCharacter = Message.localize("errIllegalCharacter",
        "非法字符|Illegal source character");
    errInstanceMemberWithoutMe = Message.localize("errInstanceMemberWithoutMe",
      "use of data member '%1' outside of instance member function|use of data member '%1' outside of instance member function");
    errMalformedDecimalConstant = Message.localize("errMalformedecimalConstant",
        "畸形小数常数|Malformed decimal constant");
    errMalformedFloatConstant = Message.localize("errMalformedFloatConstant",
        "畸形浮点常数|Malformed floating point constant");
    errMalformedHexConstant = Message.localize("errMalformedHexConstant",
        "畸形Hex常数|Malformed hexadecimal constant");
    errMalformedUnicodeEscapeSequence = Message.localize("errMalformedUnicodeEscapeSequence",
        "畸形的unicode转义序列|Malformed unicode escape sequence");
    errMultipleOnErrorStatements = Message.localize("errMultipleOnErrorStatements",
        "On-Error语句重复定义|On-Error statement redefinition");  // TODO: report location of other statement
    errNoComponentImplementation = Message.localize("errNoComponentImplementation",
        "未找到 %1 的组件定义|no component implementation found for %1");
    errNoMatchForExit = Message.localize("errNoMatchForExit",
        "找不到适用于Exit %1 的语句|Cannot find a match for Exit %1");
    errNoPackage = Message.localize("errNoPackage",
        "Simple source files need to be part of a package|Simple source files need to be part of a package");
    errObjectTypeNeeded = Message.localize("errObjectTypeNeeded",
        "期望Object类型, 但发现 %1 |Object type expected, but %1 found");
    errObjectOrArrayTypeNeeded = Message.localize("errObjectOrArrayTypeNeeded",
        "期望Object或数组类型|Object or array type expected");
    errOperandNotAssignable = Message.localize("errOperandNotAssignable",
        "左操作数应该为可分配的|Left operand needs to be assignable");
    errPropertyWithoutGetterOrSetter = Message.localize("errPropertyWithoutGetterOrSetter",
        "属性 %1 需要进行设置或读取|Property %1 needs to define either a getter or a setter or both");
    errRaiseEventFromObjectFunction = Message.localize("errRaiseEventFromObjectFunction",
        "RaiseEvent must be used in instance functions only|RaiseEvent must be used in instance functions only");
    errReadError = Message.localize("errReadError",
        "读取文件 %1 时发生输入输出错误|I/O error reading file %1");
    errRuntimeErrorTypeNeeded = Message.localize("errRuntimeErrorTypeNeeded",
        "需要设置RuntimeError类型|Runtime error type expected");
    errScalarIntegerTypeNeededForOperand = Message.localize("errScalarIntegerTypeNeededForOperand",
        "操作数应该为布尔型或者整形(Byte, Short, Integer或者Long)|Operand neeeds to have Boolean or integer type (Byte, Short, Integer or Long)");
    errScalarTypeNeededForOperand = Message.localize("errScalarTypeNeededForOperand",
        "操作数应该为标量类型|Operand neeeds to have scalar type");
    errScalarTypeNeededInForNext = Message.localize("errScalarTypeNeededInForNext",
        "For-Next语句的变量应为标量类型|Scalar type needed for loop variable in For-Next statement");
    errScalarTypeNeededInSelectCase = Message.localize("errScalarTypeNeededInSelectCase",
        "Case语句需要数值类类型|Scalar type needed in Case expression");
    errSymbolRedefinition = Message.localize("errSymbolRedefinition",
        "发现 %1 重复定义|Redefinition of %1");  // TODO: also report location of original definition
    errTooFewArguments = Message.localize("errTooFewArguments",
        "调用参数过少|Too few arguments in call");
    errTooFewIndices = Message.localize("errTooFewIndices",
        "调用索引过少|Too few indices in call");
    errTooManyArguments = Message.localize("errTooManyArguments",
        "调用参数过多|Too many arguments in call");
    errTooManyIndices = Message.localize("errTooManyIndices",
        "调用索引过多|Too many indices in call");
    errUndefinedSymbol = Message.localize("errUndefinedSymbol",
        "无法解析标识符 %1|Cannot resolve identifier %1");
    errUnexpected = Message.localize("errUnexpected",
        "发现多余的 '%1' |Unexpected '%1' found");
    errUnterminatedString = Message.localize("errUnterminatedString",
        "不闭合的String常数|Unterminated string constant");
    errValueExpected = Message.localize("errValueExpected",
        "期望数值|Value expected");
    errWriteError = Message.localize("errWriteError",
        "写文件 %1 时发生I/O错误|I/O error while writing file %1");
  }


  

  /**
   * Creates a new error message.
   * 
   * @param position  source position
   * @param message  error message template
   * @param params  parameters for error message
   */
  public Error(long position, String message, String... params) {
    super(position, message, params);
  }

  @Override
  protected String messageKind() {
    return "error";
  }
}
