﻿using UnityEngine;

namespace CarGame.Extensions
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject gmo = new GameObject();
                    gmo.name = typeof(T).Name;
                    gmo.AddComponent<T>();
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance)
            {
                DestroyImmediate(gameObject);
                return;
            }
            instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }

    }
}
