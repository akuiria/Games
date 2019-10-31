using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MVC
{
    public static Dictionary<string, Model> Models = new Dictionary<string, Model>();

    public static Dictionary<string, View> Views = new Dictionary<string, View>();

    public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();

    public static void RegisterView(View view)
    {
        if (Views.ContainsKey(view.Name))
        {
            Views.Remove(view.Name);
        }

        //注册事件
        view.RegisterAttentionEvent();

        Views[view.Name] = view;
    }

    public static void RegisterModel(Model model)
    {
        Models[model.Name] = model;
    }

    public static void RegisterController(string eventName, Type controller)
    {
        CommandMap[eventName] = controller;
    }

    public static T GetModel<T>() where T : Model
    {
        foreach (var model in Models.Values)
        {
            if (model is T variable) return variable;
        }

        return null;
    }

    public static T GetView<T>() where T : View
    {
        foreach (var model in Views.Values)
        {
            if (model is T variable) return variable;
        }

        return null;
    }
    
    public static void SendEvent(string eventName, object data = null)
    {
        if (CommandMap.ContainsKey(eventName))
        {
            Type t = CommandMap[eventName];

            Controller c = Activator.CreateInstance(t) as Controller;

            c.Execute(data);
        }

        foreach (var v in Views.Values)
        {
            if (v.AttentionList.Contains(eventName))
            {
                v.HandleEvent(eventName, data);
            }
        }
    }
}
