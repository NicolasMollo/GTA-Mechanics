using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

using S = System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EnvironmentUtilities
{
	[DisallowMultipleComponent]
	[AddComponentMenu("Environment Utilities/Activate on Start")]
	public sealed class ActivateOnStart : MonoBehaviour
	{
		#region Private variables
		[SerializeField]
		private GameObject[] objectsToActivate = new GameObject[0];
		#endregion
		#region Lifecycle
		void Start()
		{
			foreach(GameObject obj in objectsToActivate)
				if(
					obj != null &&
					!obj.activeSelf
				)
					obj.SetActive(true);
		}
		#endregion
	}
}
