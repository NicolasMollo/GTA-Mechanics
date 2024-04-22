using UnityEngine;

[DisallowMultipleComponent]
public class WheelConfiguration : MonoBehaviour {
    #region Attributes
    [SerializeField]
    private DatabaseWheel databaseWheel = null;
    [SerializeField]
    private WheelCollider myWheelCollider = null;
    [SerializeField]
    private Transform graphicsTransform = null;

    private Vector3 pos;
    private Quaternion rot;
    #endregion



    #region Public methods
    public void UpdateWheel(float motorTorque, float brakeTorque, float steering) {
        myWheelCollider.motorTorque = motorTorque * databaseWheel.MotorRatio;
        myWheelCollider.brakeTorque = brakeTorque * databaseWheel.BrakeRatio;
        myWheelCollider.steerAngle = steering * databaseWheel.SteeringRatio;
        myWheelCollider.GetWorldPose(out pos, out rot);
        graphicsTransform.SetPositionAndRotation(pos, rot);
    } 
    #endregion
}
