using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/DatabaseCar")]
public class DatabaseCar : ScriptableObject {
    #region Attributes and properties
    [SerializeField]
    [Range(800f, 8000f)]
    [Tooltip("Mass of the rigidbody")]
    private float mass = 800f;
    public float Mass {
        get { return mass; }
    }


    [SerializeField]
    [Range(0f, 2f)]
    [Tooltip("Drag of the rigidbody")]
    private float drag = 0f;
    public float Drag {
        get { return drag; }
    }


    [SerializeField]
    [Range(0.05f, 2f)]
    [Tooltip("Angular Drag of the rigidbody")]
    private float angularDrag = 0.05f;
    public float AngularDrag {
        get { return angularDrag; }
    }


    [SerializeField]
    [Range(500f, 10000f)]
    [Tooltip("Accelerating force")]
    private float motorTorque = 500f;
    public float MotorTorque {
        get { return motorTorque; }
    }


    [SerializeField]
    [Range(750f, 20000f)]
    [Tooltip("Braking force")]
    private float brakeTorque = 750f;
    public float BrakeTorque {
        get { return brakeTorque; }
    }


    [SerializeField]
    [Range(10f, 80f)]
    [Tooltip("Steering angle")]
    private float steeringAngle = 10f;
    public float SteeringAngle {
        get { return steeringAngle; }
    }


    [SerializeField]
    [Tooltip("Button to brake")]
    private KeyCode brakeButton = KeyCode.LeftShift;
    public KeyCode BrakeButton {
        get { return brakeButton; }
    }
    #endregion
}
