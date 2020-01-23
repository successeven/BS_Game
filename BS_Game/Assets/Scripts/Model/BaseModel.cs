using System;
using System.Collections.Generic;
using BSGame.Tools;
using UnityEngine;

namespace BSGame.Model
{
    public class BaseModel
    {
        public delegate void OnFlagChanged(Enum flag, bool active);
        public delegate void OnParamChanged(Enum flag, float newValue);

        public event OnParamChanged ParamChanged;

        private static bool _balanceCached;


        private readonly List<BaseComponent> _components = new List<BaseComponent>();
        private readonly Dictionary<Enum, float> _params = new Dictionary<Enum, float>();
        private readonly List<Enum> _flags = new List<Enum>();

        public float this[Enum param]
        {
            get
            {
                if (_params.ContainsKey(param)) return _params[param];
                CreateNewParam(param, 0f, true);
                return 0f;
            }

            set
            {
                if (_params.ContainsKey(param))
                {
                    _params[param] = value;
                    ParamChanged?.Invoke(param, value);
                    return;
                }
                CreateNewParam(param, value, true);
            }
        }
        
        public bool ContainsParam(Enum param)
        {
            return _params.ContainsKey(param);
        }

        public void ClearParams()
        {
            _params.Clear();
        }

        public bool CompareParams( Dictionary<Enum, float> otherParams)
        {
            foreach (var item in _params)
            {
                if (otherParams.ContainsKey(item.Key))
                    if (item.Value.EqualsTo(otherParams[item.Key]))
                        continue;
                
                return false;
            }
            return true;
        }

        public virtual BaseModel Start()
        {
            foreach (var component in _components)
                component.Start();
            
            return this;
        }
        
        public virtual void FixedUpdate(float deltaTime)
        {
            foreach (var item in _components)
                item.FixedUpdate(deltaTime);
        }

        public virtual void Update(float deltaTime)
        {
            foreach (var item in _components)
                item.Update(deltaTime);
        }

        public virtual void Disable()
        {
            foreach (var item in _components)
                item.Disable();
        }

        public T AddComponent<T>(T component) where T : BaseComponent
        {
            _components.Add(component);
            return component;
        }

        public bool RemoveComponent<T>(T component) where T : BaseComponent
        {
             return _components.Remove(component);
        }

        public T AddComponent<T>() where T : BaseComponent
        {
            var component = Activator.CreateInstance(typeof(T), this) as T;
            return AddComponent(component);
        }

        public T GetComponent<T>() where T : BaseComponent
        {
            return _components.Find(c => c is T) as T;
        }
        
        public void CreateNewParam(Enum param, float value, bool logWarning = false)
        {
            if (_params.ContainsKey(param))
            {
                _params[param] = value;
                ParamChanged?.Invoke(param, value);
                if (logWarning)  Debug.LogWarning($"{this} already has {param} param");
                return;
            }

            _params.Add(param, value);
            ParamChanged?.Invoke(param, value);

            if (logWarning)  Debug.LogWarning($"{this} has no {param} param (created now)");
        }

        public void CreateNewParams(params (Enum, float)[] newParams)
        {
            foreach (var param in newParams) CreateNewParam(param.Item1, param.Item2);
        }

    }
}
