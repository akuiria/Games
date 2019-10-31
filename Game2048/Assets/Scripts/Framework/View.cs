using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public abstract string Name { get; }

    [HideInInspector]
    public List<string> AttentionList = new List<string>();

    public virtual void RegisterAttentionEvent() { }

    public abstract void HandleEvent(string name, object data);

    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
