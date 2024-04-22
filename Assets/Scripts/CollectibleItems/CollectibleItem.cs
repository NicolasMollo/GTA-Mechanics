using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class CollectibleItem : MonoBehaviour {
    #region Attributes
    private Collider myCollider = null;

    [SerializeField]
    private GameObject father = null;
    #endregion



    private void Awake() {
        TakeTheReferences();
    }
    #region Awake methods
    private void TakeTheReferences() {
        myCollider = GetComponent<Collider>();
    }
    #endregion



    private void Start() {
        myCollider.isTrigger = true;
    }



    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            MessageManager.CallOnTakeTheCoin();
            TurnMeOff();
        }
    }
    #region OnTriggerEnter methods
    private void TurnMeOff() {
        father.SetActive(false);
    } 
    #endregion
}
