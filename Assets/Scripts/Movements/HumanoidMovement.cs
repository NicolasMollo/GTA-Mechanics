using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CapsuleCollider),
                  typeof(Animator))]
public class HumanoidMovement : Movement {
    #region Attributes
    [SerializeField]
    private DatabasePlayer playerData = null;

    private Animator myAnimator = null;

    private float horizontal = 0f;
    private float vertical = 0f;

    private bool jump = false;

    private bool notRotation = false;


    private float counter = 0f;
    [SerializeField]
    private float maxTime = 8f;

    [SerializeField]
    [Tooltip("'0' for left button/'1' for right button")]
    private int mouseButton = 0;
    #endregion
    #region Static property
    public static CharacterType Car {
        get;
        private set;
    } = CharacterType.Hatchback;
    #endregion



    #region Awake methods
    protected override void TakeTheReferences() {
        base.TakeTheReferences();
        myAnimator = GetComponent<Animator>();
    }
    #endregion

   

    private void Start() {
        ResetCounter(0);
    }

    

    public override void Update() {
        TakeTheInputs();
        SetAnimatorParameters();    
        SuperIdle();
        OneWayWalk();
    }
    #region Update methods
    private void TakeTheInputs() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jump = Input.GetKeyDown(playerData.JumpButton) ? true : false;
    }
    private void SetAnimatorParameters() {
        playerData.animatorSpeed = myAnimator.GetFloat("ForwardSpeed");
        playerData.animatorRun = myAnimator.GetBool("Run");
        playerData.animatorAngle = myAnimator.GetFloat("Angle");
        Vector2 inputDirection = FlattenVector(transform.forward * vertical + transform.right * horizontal);
        myAnimator.SetFloat("ForwardSpeed", Mathf.LerpUnclamped(playerData.animatorSpeed, inputDirection.magnitude, 1));
        myAnimator.SetBool("Run", Input.GetKey(playerData.RunButton));
        myAnimator.SetFloat("Angle", Mathf.Lerp(playerData.animatorAngle, Vector2.SignedAngle(inputDirection, FlattenVector(transform.forward)), 1));
        myAnimator.SetBool("Jump", jump);
    }
    private void SuperIdle() {
        if (myRigidbody.velocity == Vector3.zero) {
            counter += Time.deltaTime;
            myAnimator.SetFloat("Counter", counter);
            if (counter >= maxTime) {
                ResetCounter(0);
            }
        }
        else {
            ResetCounter(0);
            myAnimator.SetFloat("Counter", counter);
        }
    }
    private void OneWayWalk() {
        if (Input.GetMouseButton(mouseButton)) {
            myRigidbody.rotation = Quaternion.Euler(Vector3.zero);
            notRotation = true;
            myAnimator.SetBool("NotRotation", notRotation);
            Vector2 inputDirection = FlattenVector(transform.forward * vertical + transform.right * horizontal);
            myAnimator.SetFloat("Angle", Vector2.SignedAngle(inputDirection, FlattenVector(transform.forward)));
        }
        else {
            notRotation = false;
            myAnimator.SetBool("NotRotation", notRotation);
        }
    }
    #endregion



    public override void FixedUpdate() {
        Movement();
    }
    #region FixedUpdate methods
    private void Movement() {
        Vector3 velocity = new Vector3(horizontal, 0, vertical);


        myRigidbody.velocity = (velocity.normalized * playerData.Speed) +
                               playerData.Gravity(myRigidbody.velocity.y);

        myRigidbody.rotation = velocity != Vector3.zero && !notRotation ?
                               Quaternion.AngleAxis(playerData.animatorAngle, Vector3.up) :
                               myRigidbody.rotation;
    } 
    #endregion



    #region Reusable methods
    private Vector2 FlattenVector(Vector3 toFlatten) {
        Vector2 flatten = new Vector2(toFlatten.x, toFlatten.z);
        flatten.Normalize();
        flatten *= toFlatten.magnitude;
        return flatten;
    }
    private void ResetCounter(float time) {
        counter = time;
    }
    private void StopVelocity() {
        myRigidbody.velocity = Vector3.zero;
    }
    #endregion



    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Hatchback") {
            SetPlayerType(myCharacterType);
            StopTheTime();
            MessageManager.CallOnCollisionWithTheCar();
            Car = CharacterType.Hatchback;
        }
        else if(collision.gameObject.name == "SportsCar") {
            SetPlayerType(myCharacterType);
            StopTheTime();
            MessageManager.CallOnCollisionWithTheCar();
            Car = CharacterType.SportsCar;
        }
    }
    #region OnCollisionEnter methods
    private void SetPlayerType(CharacterType _value) {
        Controller.LastTypeCharacter = _value;
    }
    private void StopTheTime() {
        Time.timeScale = 0;
    } 
    #endregion
}
