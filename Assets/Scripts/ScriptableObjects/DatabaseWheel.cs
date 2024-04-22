using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/DatabaseWheel")]
public class DatabaseWheel : ScriptableObject {
    #region Attributes and properties
    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Value between 0 and 1 which tells us how the wheel is affected by the motor")]
    private float motorRatio = 0f;
    public float MotorRatio {
        get { return motorRatio; }
    }


    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Value between 0 and 1 which tells us how the wheel is affected by the steering")]
    private float steeringRatio = 0f;
    public float SteeringRatio {
        get { return steeringRatio; }
    }


    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Value between 0 and 1 which tells us how the wheel is affected by the brake")]
    private float brakeRatio = 0f;
    public float BrakeRatio {
        get { return brakeRatio; }
    } 
    #endregion
}
