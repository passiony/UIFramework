using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 继承Monobehaviour的单例
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get { return instance; }
    }
    protected virtual void Awake()
    {
        instance = this as T;
    }
    
}
