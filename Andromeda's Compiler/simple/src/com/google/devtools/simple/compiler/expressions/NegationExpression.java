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

/**
 * This class represents a negation expression.
 *
 * @author Herbert Czymontek
 */
public final class NegationExpression extends UnaryExpression {

  /**
   * Creates a negation expression.
   *
   * @param position  source code start position of expression
   * @param operand  operand to negate
   */
  public NegationExpression(long position, Expression operand) {
    super(position, operand);
  }

  @Override
  protected Expression fold(Compiler compiler, FunctionSymbol currentFunction) {
    if (operand instanceof ConstantNumberExpression) {
      return new ConstantNumberExpression(getPosition(),
          ((ConstantNumberExpression) operand).value.negate()).resolve(compiler, currentFunction);
    }

    return this;
  }

  @Override
  public Expression resolve(Compiler compiler, FunctionSymbol currentFunction) {
    super.resolve(compiler, currentFunction);

    type = operand.type;
    if (!type.isScalarType()) {
      return reportScalarTypeNeededError(compiler, operand);
    }

    return fold(compiler, currentFunction);
  }

  @Override
  public void generate(Method m) {
    super.generate(m);
    type.generateNegation(m);
  }

  @Override
  public String toString() {
    return '-' + operand.toString();  // COV_NF_LINE
  }
}
