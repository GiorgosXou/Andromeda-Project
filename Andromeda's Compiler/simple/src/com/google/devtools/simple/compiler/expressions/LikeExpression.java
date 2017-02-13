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

package com.google.devtools.simple.compiler.expressions;

import com.google.devtools.simple.classfiles.Method;
import com.google.devtools.simple.compiler.Compiler;
import com.google.devtools.simple.compiler.symbols.FunctionSymbol;
import com.google.devtools.simple.compiler.types.BooleanType;
import com.google.devtools.simple.compiler.types.StringType;

/**
 * This class represents the comparison of a string against a regular
 * expression.
 *
 * @author Herbert Czymontek
 */
public final class LikeExpression extends ComparisonExpression {

  /**
   * Creates a new regular expression comparison.
   * 
   * @param position  source code start position of expression
   * @param leftOperand  operand to compare
   * @param rightOperand  regular expression
   */
  public LikeExpression(long position, Expression leftOperand, Expression rightOperand) {
    super(position, leftOperand, rightOperand);
  }

  @Override
  public Expression resolve(Compiler compiler, FunctionSymbol currentFunction) {
    leftOperand = leftOperand.resolve(compiler, currentFunction).checkType(compiler,
        StringType.stringType);
    rightOperand = rightOperand.resolve(compiler, currentFunction).checkType(compiler,
        StringType.stringType);

    type = BooleanType.booleanType;

    return fold(compiler, currentFunction);
  }

  @Override
  public void generate(Method m) {
    super.generate(m);
    leftOperand.type.generateLike(m);
  }

  @Override
  public void generateBranchOnFalse(Method m, Method.Label falseLabel) {
    super.generate(m);
    leftOperand.type.generateLike(m);
    type.generateBranchIfCmpNotEqual(m, falseLabel);
  }

  @Override
  public void generateBranchOnTrue(Method m, Method.Label trueLabel) {
    super.generate(m);
    leftOperand.type.generateLike(m);
    type.generateBranchIfCmpEqual(m, trueLabel);
  }

  @Override
  public String toString() {
    return leftOperand.toString() + " Like " + rightOperand.toString();  // COV_NF_LINE
  }
}
