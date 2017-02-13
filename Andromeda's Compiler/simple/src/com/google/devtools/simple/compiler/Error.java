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
        "Array dimension expected");
    errArrayOrCollectionNeededInForEach = Message.localize("errArrayOrCollectionNeededInForEach",
        "Array or Collection needed to iterate over in For-Each statement");
    errAssignmentOrCallExprExpected = Message.localize("errAssignmentOrCallExprExpected",
        "Assignment or Call expression expected");
    errCannotConvertType = Message.localize("errCannotConvertType",
        "Cannot convert from %1 type to %2 type");
    errCannotInvokeEvent = Message.localize("errCannotInvokeEvent",
        "Cannot invoke events directly - use RaiseEvent statement");
    errCaseElseNotLast = Message.localize("errCaseElseNotLast",
        "Case Else statement is not last Case statement");
    errConstantPoolOverflow = Message.localize("errConstantPoolOverflow",
        "Constant pool overflow generating class file for %1");
    errConstantValueExpected = Message.localize("errConstantValueExpected",
        "Constant value expected");
    errCorruptedSourceFileProperties = Message.localize("errCorruptedSourceFileProperties",
        "Property section of source file corrupted");
    errDataMemberExpected = Message.localize("errDataMemberExpected",
        "%1 is not a data member, but should be");
    errDimensionInDynamicArray = Message.localize("errDimensionInDynamicArray",
        "Dimensions not allowed for dynamically sized arrays");
    errExpected = Message.localize("errExpected",
        "Expected '%1' but '%2' found");
    errForNextIdentifierMismatch = Message.localize("errForNextIdentifierMismatch",
        "Identifier %1 in Next statement does not match identifier %2 in For Statement");
    errFunctionOrArrayTypeNeeded = Message.localize("errFunctionOrArrayTypeNeeded",
        "Function or array type expected");
    errFunctionTooBig = Message.localize("errFunctionTooBig",
        "Function %2 in class %1 too big");
    errIdentifierDoesNotResolveToSymbol = Message.localize("errIdentifierDoesNotResolveToSymbol",
        "Qualified identifier %1 does not resolve to a symbol|Qualified identifier %1 does not resolve to a symbol");
    errIdentifierNotFound = Message.localize("errIdentifierNotFound",
        "Identifier %1 not found");
    errIllegalCharacter = Message.localize("errIllegalCharacter",
        "Illegal source character");
    errInstanceMemberWithoutMe = Message.localize("errInstanceMemberWithoutMe",
      "use of data member '%1' outside of instance member function");
    errMalformedDecimalConstant = Message.localize("errMalformedecimalConstant",
        "Malformed decimal constant");
    errMalformedFloatConstant = Message.localize("errMalformedFloatConstant",
        "Malformed floating point constant");
    errMalformedHexConstant = Message.localize("errMalformedHexConstant",
        "Malformed hexadecimal constant");
    errMalformedUnicodeEscapeSequence = Message.localize("errMalformedUnicodeEscapeSequence",
        "Malformed unicode escape sequence");
    errMultipleOnErrorStatements = Message.localize("errMultipleOnErrorStatements",
        "On-Error statement redefinition");  // TODO: report location of other statement
    errNoComponentImplementation = Message.localize("errNoComponentImplementation",
        "no component implementation found for %1");
    errNoMatchForExit = Message.localize("errNoMatchForExit",
        "Cannot find a match for Exit %1");
    errNoPackage = Message.localize("errNoPackage",
        "Simple source files need to be part of a package");
    errObjectTypeNeeded = Message.localize("errObjectTypeNeeded",
        "Object type expected, but %1 found");
    errObjectOrArrayTypeNeeded = Message.localize("errObjectOrArrayTypeNeeded",
        "Object or array type expected");
    errOperandNotAssignable = Message.localize("errOperandNotAssignable",
        "Left operand needs to be assignable");
    errPropertyWithoutGetterOrSetter = Message.localize("errPropertyWithoutGetterOrSetter",
        "Property %1 needs to define either a getter or a setter or both");
    errRaiseEventFromObjectFunction = Message.localize("errRaiseEventFromObjectFunction",
        "RaiseEvent must be used in instance functions only");
    errReadError = Message.localize("errReadError",
        "I/O error reading file %1");
    errRuntimeErrorTypeNeeded = Message.localize("errRuntimeErrorTypeNeeded",
        "Runtime error type expected");
    errScalarIntegerTypeNeededForOperand = Message.localize("errScalarIntegerTypeNeededForOperand",
        "Operand neeeds to have Boolean or integer type (Byte, Short, Integer or Long)");
    errScalarTypeNeededForOperand = Message.localize("errScalarTypeNeededForOperand",
        "Operand neeeds to have scalar type");
    errScalarTypeNeededInForNext = Message.localize("errScalarTypeNeededInForNext",
        "Scalar type needed for loop variable in For-Next statement");
    errScalarTypeNeededInSelectCase = Message.localize("errScalarTypeNeededInSelectCase",
        "Scalar type needed in Case expression");
    errSymbolRedefinition = Message.localize("errSymbolRedefinition",
        "Redefinition of %1");  // TODO: also report location of original definition
    errTooFewArguments = Message.localize("errTooFewArguments",
        "Too few arguments in call");
    errTooFewIndices = Message.localize("errTooFewIndices",
        "Too few indices in call");
    errTooManyArguments = Message.localize("errTooManyArguments",
        "Too many arguments in call");
    errTooManyIndices = Message.localize("errTooManyIndices",
        "Too many indices in call");
    errUndefinedSymbol = Message.localize("errUndefinedSymbol",
        "Cannot resolve identifier %1");
    errUnexpected = Message.localize("errUnexpected",
        "Unexpected '%1' found");
    errUnterminatedString = Message.localize("errUnterminatedString",
        "Unterminated string constant");
    errValueExpected = Message.localize("errValueExpected",
        "Value expected");
    errWriteError = Message.localize("errWriteError",
        "I/O error while writing file %1");
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
