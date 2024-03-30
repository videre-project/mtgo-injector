﻿/** @file
  Copyright (c) 2021, Xappy.
  Copyright (c) 2024, Cory Bennett. All rights reserved.
  SPDX-License-Identifier: Apache-2.0 and MIT
**/

using System;
using System.Runtime.InteropServices;


namespace MTGOSDK.Core.Remoting.Interop.Extensions;

public static class IntPtrExt
{
  public static IntPtr GetMethodTable(this IntPtr o)
  {
    try
    {
      IntPtr methodTable = Marshal.ReadIntPtr(o);
      return methodTable;
    }
    catch (Exception e)
    {
      throw new AccessViolationException("Failed to read MethodTable at the object's address.", e);
    }
  }
}
