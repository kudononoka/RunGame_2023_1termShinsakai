using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if(!instance)
                {
                    Debug.Log("Scene����" + typeof(T).Name + "���A�^�b�`���Ă���GameObject������܂���");
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
