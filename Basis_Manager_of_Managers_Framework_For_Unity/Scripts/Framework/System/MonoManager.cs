using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoManager : PersistentSingleton<MonoManager>
{
    public event UnityAction updataEvent;

    private void Update()
    {
        updataEvent?.Invoke();
    }

    public void AddUpdateListener(UnityAction fun)
    {
        updataEvent += fun;
    }

    public void RemoveUpdateListener(UnityAction fun)
    {
        updataEvent -= fun;
    }
}
