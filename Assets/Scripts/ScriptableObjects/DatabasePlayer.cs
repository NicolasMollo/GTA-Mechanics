using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/DatabasePlayer")]
public class DatabasePlayer : ScriptableObject {
    #region Attributes and properties
    [SerializeField]
    [Range(50f, 300f)]
    [Tooltip("Scalar which will be multiplied to the velocity vector of the rigidbody")]
    private float speed = 50f;
    public float Speed {
        get {
            return animatorRun ? speed * runMultiplier * Time.fixedDeltaTime
            : speed * Time.fixedDeltaTime;
        }
    }


    [SerializeField]
    [Range(1f, 5f)]
    [Tooltip("Multiplier that will be multiplied at speed when the player is in a running state")]
    private float runMultiplier = 1f;


    public Vector3 Gravity(float gravity) {
        return gravity * Vector3.up;
    }


    [SerializeField]
    [Tooltip("Button for running (for the keyboard)")]
    private KeyCode runButton = KeyCode.LeftShift;
    public KeyCode RunButton {
        get { return runButton; }
    }


    [SerializeField]
    [Tooltip("Jump button (for the keyboard)")]
    private KeyCode jumpButton = KeyCode.Space;
    public KeyCode JumpButton {
        get { return jumpButton; }
    }
    #endregion
    #region Animator attributes
    [HideInInspector]
    public float animatorAngle = 0f;
    [HideInInspector]
    public float animatorSpeed = 0f;
    [HideInInspector]
    public bool animatorRun = false;
    #endregion
}
