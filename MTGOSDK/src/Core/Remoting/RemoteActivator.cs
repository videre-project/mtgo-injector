﻿/** @file
  Copyright (c) 2021, Xappy.
  Copyright (c) 2024, Cory Bennett. All rights reserved.
  SPDX-License-Identifier: Apache-2.0 and MIT
**/

using System;
using System.Linq;

using ScubaDiver.API;
using ScubaDiver.API.Interactions;

using RemoteNET.Internal.Reflection;


namespace RemoteNET;

public class RemoteActivator(DiverCommunicator communicator, RemoteApp app)
{
  public RemoteObject CreateInstance(Type t) =>
    CreateInstance(t, new object[0]);

  public RemoteObject CreateInstance(Type t,
                                      params object[] parameters) =>
    CreateInstance(t.Assembly.FullName, t.FullName, parameters);

  public RemoteObject CreateInstance(string typeFullName,
                                      params object[] parameters) =>
    CreateInstance(null, typeFullName, parameters);

  public RemoteObject CreateInstance(string assembly,
                                      string typeFullName,
                                      params object[] parameters)
  {
    object[] paramsNoEnums = parameters.ToArray();
    for (int i = 0; i < paramsNoEnums.Length; i++)
    {
      var val = paramsNoEnums[i];
      if (val.GetType().IsEnum)
      {
        var enumClass = app.GetRemoteEnum(val.GetType().FullName);
        // TODO: This will break on the first enum value which represents 2 or more flags
        object enumVal = enumClass.GetValue(val.ToString());
        // NOTE: Object stays in place in the remote app as long as we have it's reference
        // in the paramsNoEnums array (so until end of this method)
        paramsNoEnums[i] = enumVal;
      }
    }

    ObjectOrRemoteAddress[] remoteParams = paramsNoEnums.Select(
        RemoteFunctionsInvokeHelper.CreateRemoteParameter).ToArray();

    // Create object + pin
    InvocationResults invoRes = communicator
      .CreateObject(typeFullName, remoteParams);

    // Get proxy object
    var remoteObject = app.GetRemoteObject(
        invoRes.ReturnedObjectOrAddress.RemoteAddress,
        invoRes.ReturnedObjectOrAddress.Type);

    return remoteObject;
  }

  public RemoteObject CreateInstance<T>() => CreateInstance(typeof(T));
}
