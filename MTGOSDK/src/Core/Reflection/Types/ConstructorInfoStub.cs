/** @file
  Copyright (c) 2024, Cory Bennett. All rights reserved.
  SPDX-License-Identifier: Apache-2.0
**/

using System.Globalization;
using System.Reflection;


namespace MTGOSDK.Core.Reflection.Types;

public class ConstructorInfoStub : ConstructorInfo
{
  public override string Name => throw new NotImplementedException();
  public override Type DeclaringType => throw new NotImplementedException();
  public override Type ReflectedType => throw new NotImplementedException();

  public override MethodAttributes Attributes =>
    throw new NotImplementedException();

  public override RuntimeMethodHandle MethodHandle =>
    throw new NotImplementedException();
  public override object[] GetCustomAttributes(bool inherit)
  {
    throw new NotImplementedException();
  }

  public override object[] GetCustomAttributes(Type attributeType, bool inherit)
  {
    throw new NotImplementedException();
  }

  public override MethodImplAttributes GetMethodImplementationFlags()
  {
    throw new NotImplementedException();
  }

  public override ParameterInfo[] GetParameters() =>
    throw new NotImplementedException();

  public override object Invoke(
    BindingFlags invokeAttr,
    Binder binder,
    object[] parameters,
    CultureInfo culture)
  {
    throw new NotImplementedException();
  }

  public override object Invoke(
    object obj,
    BindingFlags invokeAttr,
    Binder binder,
    object[] parameters,
    CultureInfo culture)
  {
    throw new NotImplementedException();
  }

  public override bool IsDefined(Type attributeType, bool inherit)
  {
    throw new NotImplementedException();
  }

  public override string ToString()
  {
    throw new NotImplementedException();
  }
}
