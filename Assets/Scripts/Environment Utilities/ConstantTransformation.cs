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
	[AddComponentMenu("Environment Utilities/Constant Transformation")]
	public sealed class ConstantTransformation : MonoBehaviour
	{
		#region Public variables
		[Header("Time")]
		public float timeMultiplier = 1.0f;
		[Header("Switches")]
		public bool autoApplyOnUpdate = true;
		public bool applyConstantTranslation = false;
		public bool applyConstantRotation = false;
		public bool applyConstantScale = false;
		[Header("Settings")]
		public Vector3 constantTranslation = Vector3.zero;
		public Vector3 constantRotation = Vector3.zero;
		public Vector3 constantScale = Vector3.zero;
		#endregion
		#region Lifecycle
		void Update()
		{
			if(autoApplyOnUpdate)
				Step(Time.deltaTime * timeMultiplier);
		}
		#endregion
		#region Public methods
		public void Step(float deltaTime)
		{
			if(applyConstantTranslation)
				transform.position += constantTranslation * deltaTime;
			if(applyConstantRotation)
				transform.eulerAngles += constantRotation * deltaTime;
			if(applyConstantScale)
				transform.localScale += constantScale * deltaTime;
		}
		#endregion
	}
}
