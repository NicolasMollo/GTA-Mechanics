using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class CarMovement : Movement {
    #region Attributes
    [SerializeField]
    private DatabaseCar databaseCar = null;

    [SerializeField]
    private WheelConfiguration[] wheels = null;

    private float rotate = 0f;
    private float move = 0f;
    private bool brake = false;

    [SerializeField]
    private Transform steeringWheel = null;
    private Quaternion handsRotation;
    #endregion



    private void Start() {
        VariableAssignment();   
    }
    #region Start methods
    private void VariableAssignment() {
        myRigidbody.mass = databaseCar.Mass;
        myRigidbody.drag = databaseCar.Drag;
        myRigidbody.angularDrag = databaseCar.AngularDrag;
        handsRotation = transform.localRotation;
    } 
    #endregion



    public override void Update() {
        InitAxes();
    }
    #region Update methods
    private void InitAxes() {
        rotate = Input.GetAxis("Horizontal");
        move = Input.GetAxis("Vertical");
        brake = Input.GetKey(databaseCar.BrakeButton);
    } 
    #endregion



    public override void FixedUpdate() {
        Movement();
        SteeringWheelMovement();
    }
    #region FixedUpdate methods
    private void Movement() {
        for (int i = 0; i < wheels.Length; i++) {
            wheels[i].UpdateWheel(move * databaseCar.MotorTorque,
                                  brake ? databaseCar.BrakeTorque : 0,
                                  rotate * databaseCar.SteeringAngle);
        }
    }
    private void SteeringWheelMovement() {
        steeringWheel.localRotation = handsRotation;
        steeringWheel.Rotate(steeringWheel.forward, -rotate * 60, Space.World);
    }
    #endregion
}
