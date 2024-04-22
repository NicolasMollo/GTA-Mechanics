using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class HandsIK : MonoBehaviour {
    #region Attributes
    private Animator myAnimator = null;

    [SerializeField]
    private Transform leftHand = null;

    [SerializeField]
    private Transform rightHand = null;

    [SerializeField]
    [Range(0, 1)]
    private float IkWeight = 1f; 
    #endregion



    private void Awake() {
        TakeThereferences();
    }
    #region Awake methods
    private void TakeThereferences() {
        myAnimator = GetComponent<Animator>();
    } 
    #endregion



    private void OnAnimatorIK(int layerIndex) {
        SetHandsIK();
    }
    #region OnAnimatorIK methods
    private void SetHandsIK() {
        myAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, IkWeight);
        myAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, IkWeight);

        myAnimator.SetIKPosition(AvatarIKGoal.RightHand, leftHand.position);
        myAnimator.SetIKPosition(AvatarIKGoal.LeftHand, rightHand.position);
    } 
    #endregion
}
