using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
    #region Private enum
    private enum AudioType : byte {
        Environment,
        Car,
        Last
    }
    #endregion
    #region Attributes and properties
    private AudioSource myAudioSource = null;

    [SerializeField]
    private AudioClip[] audioClips = new AudioClip[(int)AudioType.Last];

    public static AudioManager Instance {
        get;
        private set;
    } = null;

    #endregion


    private void Awake() {
        TakeTheReferences();
        Instance = this;
    }
    #region Awake methods
    private void TakeTheReferences() {
        myAudioSource = GetComponent<AudioSource>();
    }
    #endregion



    private void Start() {
        SetVolume();
    }
    private void SetVolume() {
        if (myAudioSource.clip ==
            audioClips[(int)AudioType.Environment]) {
            myAudioSource.volume = 1f;
        }
        else if (myAudioSource.clip ==
                 audioClips[(int)AudioType.Car]) {
            myAudioSource.volume = 0.5f;
        }
    }



    //private void Update() {
    //    PlayAudioEnvironment();
    //}
    //private void Update() {
    //    switch (Controller.CharacterType) {
    //        case CharacterType.Chad:
    //        case CharacterType.Leonard:
    //            myAudioSource.Stop();
    //            myAudioSource.clip = audioClips[(int)AudioType.Environment];
    //            myAudioSource.Play();
    //            break;
    //        case CharacterType.Hatchback:
    //        case CharacterType.SportsCar:
    //            myAudioSource.Stop();
    //            myAudioSource.clip = audioClips[(int)AudioType.Car];
    //            myAudioSource.Play();
    //            break;
    //    }
    //}

    public void PlayAudioEnvironment() {
        myAudioSource.clip = audioClips[(int)AudioType.Environment];
        myAudioSource.Play();
    }
    public void PlayAudioCar() {
        myAudioSource.clip = audioClips[(int)AudioType.Car];
        myAudioSource.Play();
    }
    public void StopAudioCar() {
        myAudioSource.Stop();
    }
}
