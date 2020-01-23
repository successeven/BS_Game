using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BSGame.Tools
{
	public static class UnityExtensionMethods
	{

		public static void Activate(this GameObject go) => go.SetActive(true);

		public static void Deactivate(this GameObject go) => go.SetActive(false);

		public static T Activate<T>(this T component) where T : Component
		{
			component.gameObject.SetActive(true);
			return component;
		}

		public static bool IsActivate<T>(this T component) where T : Component
		{
			return component.gameObject.activeInHierarchy;
		}

		public static T Deactivate<T>(this T component) where T : Component
		{
			component.gameObject.SetActive(false);
			return component;
		}

		public static T SetActive<T>(this T component, bool active) where T : Component
		{
			component.gameObject.SetActive(active);
			return component;
		}

		public static T Copy<T>(this T source,
		                        Transform parent = null,
		                        string name = "",
		                        bool activate = true) where T : Component
		{
			if (source == null)
			{
				Debug.LogError("Null prefab");
				return null;
			}

#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				if (PrefabUtility.InstantiatePrefab(source, parent) is T copy)
				{
					copy.SetActive(activate);
					return copy;
				}
			}
#endif

			return source.gameObject.Copy(parent, name, activate).GetComponent<T>();
		}

		public static GameObject Copy(this GameObject source,
		                              Transform parent = null,
		                              string name = "",
		                              bool activate = true)
		{
			if (source == null)
			{
				Debug.LogError("Null prefab");
				return null;
			}

			var obj = Object.Instantiate(source, parent != null ? parent : source.transform.parent, false);
			obj.SetActive(activate);

			if (!string.IsNullOrEmpty(name))
			{
				obj.name = name;
			}
#if UNITY_EDITOR
			else
			{
				obj.name = obj.name.Replace("(Clone)", "");
			}
#endif

			return obj;
		}

		public static T SetParent<T>(this T behaviour, Transform parent, bool worldPositionStays = true) where T : BaseBehaviour
		{
			behaviour.transform.SetParent(parent, worldPositionStays);
			return behaviour;
		}

		public static void Destroy(this GameObject go, float delay = 0f)
		{
			if (go == null)
			{
				Debug.LogError("Can't destroy null obj");
				return;
			}

			if (!Application.isPlaying)
			{
				Object.DestroyImmediate(go, true);
				return;
			}

			if (delay > 0f)
			{
				Object.Destroy(go, delay);
			}
			else
			{
				Object.Destroy(go);
			}
		}

		public static void Destroy<T>(this GameObject go, float delay = 0f) where T : Component
		{
			if (go == null)
			{
				Debug.LogError("Can't destroy null obj");
				return;
			}

			go.GetComponent<T>().DestroyComponent();
		}

		public static void DestroyComponent(this Component component, float delay = 0f)
		{
			if (component == null)
			{
				Debug.LogError("Can't destroy null obj");
				return;
			}

			if (!Application.isPlaying)
			{
				Object.DestroyImmediate(component);
				return;
			}

			if (delay > 0f)
			{
				Object.Destroy(component, delay);
			}
			else
			{
				Object.Destroy(component);
			}
		}

		public static void DestroyGO<T>(this T component, float delay = 0f) where T : Component
		{
			if (component == null)
			{
				Debug.LogError("Can't destroy null component");
				return;
			}
            component.gameObject.Destroy(delay);
		}

		public static List<GameObject> GetChilds(this GameObject gameObject)
		{
			var result = new List<GameObject>();
			foreach (Transform child in gameObject.transform)
			{
				result.Add(child.gameObject);
			}
			return result;
		}

		public static float Absolute(this float value)
		{
			return value < 0 ? -value : value;
		}

		public static bool EqualsTo(this float valueA, float valueB, float precission = 0.00001f)
		{
			return (valueA - valueB).Absolute() < precission;
		}

	}
}