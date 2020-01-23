using System.Linq;
using UnityEngine;

namespace BSGame.ScriptableObject
{
    public class SingletonScriptableObject<T> : UnityEngine.ScriptableObject where T : UnityEngine.ScriptableObject
    {
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance == null) _instance = (T) Resources.LoadAll("", typeof(T)).FirstOrDefault();
                return _instance;
            }
        }
    }
}
