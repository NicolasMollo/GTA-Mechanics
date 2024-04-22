using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {
    protected Rigidbody myRigidbody = null;



    [SerializeField]
    protected CharacterType myCharacterType/* = CharacterType.Chad*/;
    public CharacterType MyCharacterType {
        get { return myCharacterType; }
    }



    protected virtual void Awake() {
        TakeTheReferences();
    }
    #region Awake methods
    protected virtual void TakeTheReferences() {
        myRigidbody = GetComponent<Rigidbody>();
    } 
    #endregion



    public virtual void Update() {
        
    }



    public virtual void FixedUpdate() { 
    
    }
}
