﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace ScubaDiver.API.Utils
{
  public static class JsonConvert
  {
    public static T DeserializeObject<T>(
      string body,
      object _withErrors = null) where T : class
    {
      Type convert = NewtonsoftProxy.JsonConvert;

      List<object> args = new List<object>();
      args.Add(body);
      if(_withErrors != null)
        args.Add(_withErrors);

      var DeserializeObject = convert.GetMethods((BindingFlags)0xffff)
        .Where(mi => mi.Name=="DeserializeObject")
        .Where(mi => mi.IsGenericMethod)
        .Where(mi => mi.GetParameters().Length == args.Count)
        .Where(mi => !mi.GetParameters().Last().ParameterType.IsArray)
        .Single();

      try
      {
        var x = DeserializeObject
          .MakeGenericMethod(typeof(T))
          .Invoke(null, args.ToArray());
        return (T)x;
      }
      catch(TargetInvocationException ex)
      {
        throw ex.InnerException;
      }
    }

    public static string SerializeObject(object o)
    {
      Type convert = NewtonsoftProxy.JsonConvert;

      List<object> args = new List<object>() {o};

      var SerializeObject = convert.GetMethods((BindingFlags)0xffff)
        .Where(mi => mi.Name == "SerializeObject")
        .Where(mi => mi.GetParameters().Length == 1)
        .Single();

      try
      {
        var x = SerializeObject.Invoke(null, args.ToArray());
        return (string)x;
      }
      catch (TargetInvocationException ex)
      {
        throw ex.InnerException;
      }
    }
  }

  public static class NewtonsoftProxy
  {
    public static object JsonSerializerSettingsWithErrors
    {
      get
      {
        Type MissingMemberHandlingEnum = typeof(Newtonsoft.Json.MissingMemberHandling);
        var MissingMemberHandling_Error = Enum
          .GetValues(MissingMemberHandlingEnum)
          .Cast<object>()
          .Single(val => val.ToString() == "Error");

        Type t = typeof(Newtonsoft.Json.JsonSerializerSettings);
        var inst = Activator.CreateInstance(t);
        t.GetProperty("MissingMemberHandling")
          .SetValue(inst, MissingMemberHandling_Error);

        return inst;
      }
    }

    public static Type JsonConvert => typeof(Newtonsoft.Json.JsonConvert);
  }
}
