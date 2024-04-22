using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
public class SfxManager : MonoBehaviour {
    #region Private enum
    private enum SfxType : byte {
        TakeTheCoin,
        CrashIntoCollider,
        Last
    } 
    #endregion
    #region Attributes
    [SerializeField]
    private AudioSource[] myAudioSources = new AudioSource[(int)SfxType.Last];

    [SerializeField]
    private AudioClip[] myAudioClips = new AudioClip[(int)SfxType.Last]; 
    #endregion



    private void Start() {
        AddListener();
    }
    #region Start methods
    private void AddListener() {
        MessageManager.OnTakeTheCoin += PlayTakeTheCoin;
        MessageManager.OnCrashIntoCollider += PlayCrashIntoTheCollider;
    }
    #endregion



    #region Methods for events
    private void PlayTakeTheCoin() {
        myAudioSources[(int)SfxType.TakeTheCoin].PlayOneShot(myAudioClips[(int)SfxType.TakeTheCoin]);
    }
    private void PlayCrashIntoTheCollider() {
        myAudioSources[(int)SfxType.CrashIntoCollider].PlayOneShot(myAudioClips[(int)SfxType.CrashIntoCollider]);
    } 
    #endregion



    private void OnDestroy() {
        RemoveListener();
    }
    #region OnDestroy methods
    private void RemoveListener() {
        MessageManager.OnTakeTheCoin -= PlayTakeTheCoin;
        MessageManager.OnCrashIntoCollider -= PlayCrashIntoTheCollider;
    } 
    #endregion
}
