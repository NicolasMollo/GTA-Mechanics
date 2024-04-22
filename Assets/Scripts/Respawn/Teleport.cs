using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider))]
public class Teleport : MonoBehaviour {
    #region Attributes
    private BoxCollider myBoxCollider = null;
    [SerializeField]
    private Transform respawnPoint = null; 
    #endregion



    private void Awake() {
        TakeTheReferences();
    }
    #region Awake methods
    private void TakeTheReferences() {
        myBoxCollider = GetComponent<BoxCollider>();
    }
    #endregion



    private void Start() {
        myBoxCollider.isTrigger = true;
    }



    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") ||
            other.gameObject.CompareTag("Car")) {
            other.transform.position = respawnPoint.transform.position;
        }
    }
}
