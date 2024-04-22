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
using UnityEngine.Audio;

namespace EnvironmentUtilities
{
	[AddComponentMenu("Environment Utilities/Ambient SFX Scatterer")]
	public sealed class AmbientSFXScatterer : MonoBehaviour
	{
		#region Public variables
		public AudioClip[] soundEffects = new AudioClip[0];
		#endregion
		#region Private variables
		[SerializeField]
		public AudioMixerGroup targetMixerGroup = null;
		[SerializeField]
		private float minInterval = 0.5f;
		[SerializeField]
		private float maxInterval = 10.0f;
		[SerializeField]
		private float scatterRadius = 5.0f;
		[SerializeField]
		[Range(0.0f, 1.0f)]
		[Tooltip("A random value between -volume random and volume random is added to the standard volume")]
		private float volumeRandom = 0.25f;
		[SerializeField]
		[Range(0.0f, 0.8f)]
		[Tooltip("A random value between -pitch random and pitch random is added to the standard pitch")]
		private float pitchRandom = 0.1f;
		private AudioSource scatteredAudioSource = null;
		#endregion
		#region Lifecycle
		void Awake()
		{
			GameObject sfxAudio = new GameObject("Scattered Audio");
			sfxAudio.SetActive(false);
			sfxAudio.transform.SetParent(transform);
			scatteredAudioSource = sfxAudio.AddComponent<AudioSource>();
			scatteredAudioSource.playOnAwake = false;
			scatteredAudioSource.spatialBlend = 1.0f;
			scatteredAudioSource.loop = false;
			scatteredAudioSource.outputAudioMixerGroup = targetMixerGroup;
		}
		void OnEnable()
		{
			StartCoroutine(ScatterSounds());
		}
		#endregion
		#region Private methods
		private IEnumerator ScatterSounds()
		{
			AudioClip currentClip = null;
			while(true)
			{
				yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
				currentClip = soundEffects[Random.Range(0, soundEffects.Length - 1)];
				if(currentClip != null)
				{
					scatteredAudioSource.gameObject.SetActive(true);
					scatteredAudioSource.transform.position = Random.onUnitSphere * Random.Range(0.0f, scatterRadius);
					scatteredAudioSource.pitch = 1.0f + Random.Range(-pitchRandom, pitchRandom);
					scatteredAudioSource.PlayOneShot(
						currentClip,
						1.0f + Random.Range(-volumeRandom, volumeRandom)
					);
					yield return new WaitForSeconds(currentClip.length);
					scatteredAudioSource.gameObject.SetActive(false);
				}
			}
		}
		#endregion
	}
}
