using UnityEngine;

public class CarDoor : MonoBehaviour {
    #region Attributes
    [SerializeField]
    private ConfigurableJoint myConfigurableJoint = null;
    [SerializeField]
    private ConfigurableJoint myConfigurableJoint_2 = null;
    [SerializeField]
    [Tooltip("Value beyond which the opening of the doors is simulated")]
    private float breakingValue = 15f; 
    #endregion



    private void Start() {
        TurnOffJoint();
    }
    #region Start methods
    private void TurnOffJoint() {
        myConfigurableJoint.gameObject.SetActive(false);
        myConfigurableJoint_2.gameObject.SetActive(false);
    } 
    #endregion



    private void OnCollisionEnter(Collision collision) {
        if (collision.relativeVelocity.magnitude > breakingValue) {
            myConfigurableJoint.gameObject.SetActive(true);
            myConfigurableJoint_2.gameObject.SetActive(true);
        }
    }
}
