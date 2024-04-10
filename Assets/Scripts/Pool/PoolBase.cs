using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolBase<T>
{ 
    private readonly Func<T> preloadFunc;
    private readonly Action<T> getAction;
    private readonly Action<T> returnAction;

    private Queue<T> pool = new Queue<T>();
    private List<T> active = new List<T>();

    public PoolBase(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
    {
        this.preloadFunc = preloadFunc;
        this.getAction = getAction;
        this.returnAction = returnAction;
        if (preloadFunc == null)
        {
            Debug.LogError("Preload function is null");
            return;
        }
        for (int i = 0; i < preloadCount; i++)
        {
            Return(preloadFunc());
        }
    }
    virtual public T Get()
    {
        T item = pool.Count > 0 ? pool.Dequeue() : preloadFunc();
        getAction(item);
        active.Add(item);
        return item;
    }
    public void Return(T item)
    {
        returnAction(item);
        pool.Enqueue(item);
        active.Remove(item);
    }
    public void ReturnAll()
    {
        foreach (T item in active)
            Return(item);
    }
}
