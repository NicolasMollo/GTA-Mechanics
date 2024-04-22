using UnityEngine;


public enum CharacterType : byte {
    Chad,
    Leonard,
    Hatchback,
    SportsCar,
    Last
}
[DisallowMultipleComponent]
public class Controller : MonoBehaviour {
    #region Movement objects array
    [SerializeField]
    private Movement[] movingObjects = new Movement[(int)CharacterType.Last];
    #endregion
    #region Static properties
    public static CharacterType CharacterType {
        get;
        set;
    } = CharacterType.Chad;

    public static CharacterType LastTypeCharacter {
        get;
        set;
    } = CharacterType.Chad;
    #endregion
    #region Buttons
    [SerializeField]
    private KeyCode getOutOfTheCarButton = KeyCode.P;

    [SerializeField]
    private KeyCode changeCharacterButton = KeyCode.Tab; 
    #endregion
    #region Static players attributes
    [SerializeField]
    private GameObject sportsCarStaticChad = null;
    [SerializeField]
    private GameObject sportsCarstaticLeonard = null;
    [SerializeField]
    private GameObject hatchBackStaticChad = null;
    [SerializeField]
    private GameObject hatchBackStaticLeonard = null;
    #endregion
    #region CarDoors attributes
    [SerializeField]
    private Transform sportsCarDoorLogic = null;

    [SerializeField]
    private Transform hatchBackDoorLogic = null;
    #endregion
    #region Distance to check
    [SerializeField]
    [Tooltip("Distance within which you are within range of the second player")]
    private float minDistance = 2f;

    [SerializeField]
    private LayerMask layerMask = 0;
    #endregion



    private void Start() {
        DisableMovingObjects();
        SetStaticPlayer(sportsCarStaticChad, false);
        SetStaticPlayer(sportsCarstaticLeonard, false);
        SetStaticPlayer(hatchBackStaticChad, false);
        SetStaticPlayer(hatchBackStaticLeonard, false);
    }
    #region Start methods
    private void DisableMovingObjects() {
        for (int i = 0; i < movingObjects.Length; i++) {
            if (movingObjects[i].MyCharacterType != CharacterType) {
                movingObjects[i].enabled = false;
            }
        }
    } 
    #endregion



    private void Update() {
        Debug.DrawRay(movingObjects[(int)CharacterType.SportsCar].transform.position, -movingObjects[(int)CharacterType.SportsCar].transform.up, Color.white, 4);
        switch (CharacterType) {
            case CharacterType.Chad:
                movingObjects[(int)CharacterType.Chad].Update();
                ChangePlayer(CharacterType.Leonard, CharacterType.Chad);
                break;
            case CharacterType.Leonard:
                movingObjects[(int)CharacterType.Leonard].Update();
                ChangePlayer(CharacterType.Chad, CharacterType.Leonard);
                break;
            case CharacterType.Hatchback:
                movingObjects[(int)CharacterType.Hatchback].Update();
                movingObjects[(int)LastTypeCharacter].gameObject.SetActive(false);
                SetStaticPlayer(LastTypeCharacter == CharacterType.Leonard ? hatchBackStaticLeonard : hatchBackStaticChad, true);
                if (Input.GetKeyDown(getOutOfTheCarButton)) {
                    GetOutOfTheCar(hatchBackDoorLogic);
                }
                break;
            case CharacterType.SportsCar:
                movingObjects[(int)CharacterType.SportsCar].Update();
                movingObjects[(int)LastTypeCharacter].gameObject.SetActive(false);
                SetStaticPlayer(LastTypeCharacter == CharacterType.Leonard ? sportsCarstaticLeonard : sportsCarStaticChad, true);
                if (Input.GetKeyDown(getOutOfTheCarButton)) {
                    GetOutOfTheCar(sportsCarDoorLogic);
                }
                break;
        }
    }
    #region Update methods
    private void ChangePlayer(CharacterType _typeToSet, CharacterType _typeToDisable) {
        if (DistanceToSecondPlayer()) {
            MessageManager.CallOnPlayerSelectionArea();
            if (Input.GetKeyDown(changeCharacterButton)) {
                SetPlayerType(_typeToSet);
                movingObjects[(int)_typeToDisable].enabled = false;
            }
        }
        else {
            MessageManager.CallOnGameArea();
        }
    }
    private bool DistanceToSecondPlayer() {
        #region Variable Assignment
        Vector3 chadPosition = movingObjects[(int)CharacterType.Chad].transform.position;
        Vector3 leonardPosition = movingObjects[(int)CharacterType.Leonard].transform.position;
        #endregion

        return (chadPosition - leonardPosition).magnitude < minDistance;
    }
    private void GetOutOfTheCar(Transform _carDoor) {
        CharacterType = LastTypeCharacter;
        movingObjects[(int)LastTypeCharacter].transform.SetPositionAndRotation(
                      _carDoor.position, Quaternion.Euler(_carDoor.transform.rotation * _carDoor.transform.right)
                      );
        SetStaticPlayer(sportsCarStaticChad, false);
        SetStaticPlayer(sportsCarstaticLeonard, false);
        SetStaticPlayer(hatchBackStaticChad, false);
        SetStaticPlayer(hatchBackStaticLeonard, false);
        movingObjects[(int)LastTypeCharacter].gameObject.SetActive(true);
    }
    #endregion



    private void FixedUpdate() {
        switch (CharacterType) {
            case CharacterType.Chad:
                movingObjects[(int)CharacterType.Chad].FixedUpdate();
                break;
            case CharacterType.Leonard:
                movingObjects[(int)CharacterType.Leonard].FixedUpdate();
                break;
            case CharacterType.Hatchback:
                movingObjects[(int)CharacterType.Hatchback].FixedUpdate();
                break;
            case CharacterType.SportsCar:
                movingObjects[(int)CharacterType.SportsCar].FixedUpdate();
                break;
        }
    }



    #region Reusable methods
    private void SetStaticPlayer(GameObject _staticPlayer, bool _value) {
        _staticPlayer.SetActive(_value);
    }
    private void SetPlayerType(CharacterType _type) {
        CharacterType = _type;
    }
    #endregion
}
