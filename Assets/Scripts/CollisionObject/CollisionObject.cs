using UnityEngine;

public enum CollisionObjectType : byte {
    ParkAccess,
    StreetAccess,
    Last
}
[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider))]
public class CollisionObject : MonoBehaviour {
    #region Attributes
    [SerializeField]
    private CollisionObjectType collisionObjectType = CollisionObjectType.ParkAccess;
    #endregion



    private void OnCollisionEnter(Collision collision) {
        switch (collisionObjectType) {
            case CollisionObjectType.ParkAccess:
                UiManager.Instance.SetAreaText(UiManager.Instance.Phrases[(int)CollisionObjectType.StreetAccess]);
                UiManager.Instance.TextAreaSwitch(true);
                MessageManager.CallOnCrashIntoCollider();
                break;
            case CollisionObjectType.StreetAccess:
                UiManager.Instance.SetAreaText(UiManager.Instance.Phrases[(int)CollisionObjectType.ParkAccess]);
                UiManager.Instance.TextAreaSwitch(true);
                MessageManager.CallOnCrashIntoCollider();
                break;
        }
    }
}
