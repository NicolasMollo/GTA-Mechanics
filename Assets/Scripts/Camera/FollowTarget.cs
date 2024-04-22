using UnityEngine;

[DisallowMultipleComponent]
public class FollowTarget : MonoBehaviour {
    #region Humanoid camera attributes
    [SerializeField]
    private Transform chadTransform = null;

    [SerializeField]
    private Transform leonardTransform = null;

    [SerializeField]
    private float zOffset = 0f;

    private Vector3 startPosition = Vector3.zero;

    private Quaternion startRotation = new Quaternion(0, 0, 0, 0);

    private Transform targetTransform = null;
    #endregion
    #region Car camera attributes
    [SerializeField]
    private Transform hatchbackTransform = null;
    [SerializeField]
    private Transform sportsCarTransform = null;
    [SerializeField]
    private float upDist = 5f;
    [SerializeField]
    private float backDist = 5f;
    [SerializeField]
    private float movementSpeed = 3f;
    [SerializeField]
    private float rotationSpeed = 10f;
    private Vector3 targetCar = Vector3.zero;
    private Quaternion quaternionTo = new Quaternion(0, 0, 0, 0); 
    #endregion



    private void Start() {
        SaveInitialValues();
    }
    #region Start methods
    private void SaveInitialValues() {
        startPosition = transform.position;
        startRotation = transform.rotation;
    } 
    #endregion



    private void FixedUpdate() {
        switch (Controller.CharacterType) {
            case CharacterType.Chad:
                targetTransform = chadTransform;
                FollowHumanoidTarget(zOffset);
                break;
            case CharacterType.Leonard:
                targetTransform = leonardTransform;
                FollowHumanoidTarget(zOffset);
                break;
            case CharacterType.Hatchback:
                targetTransform = hatchbackTransform;
                FollowCarTarget(targetTransform);
                break;
            case CharacterType.SportsCar:
                targetTransform = sportsCarTransform;
                FollowCarTarget(targetTransform);
                break;
        }
    }
    #region FixedUpdate methods
    private void FollowHumanoidTarget(float _zOffset) {
        Vector3 savePosition = targetTransform.position;
        savePosition.y = startPosition.y;
        savePosition.z = targetTransform.position.z - _zOffset;
        transform.position = savePosition;
        transform.rotation = startRotation;
    }
    private void FollowCarTarget(Transform _carTransform) {
        #region VariableAssignment
        targetCar = _carTransform.position - _carTransform.forward * backDist + _carTransform.up * upDist;
        quaternionTo = Quaternion.LookRotation(_carTransform.position - transform.position, _carTransform.up);
        #endregion

        transform.position = Vector3.Slerp(transform.position, targetCar, movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, quaternionTo, rotationSpeed * Time.deltaTime);
    }
    #endregion
}
