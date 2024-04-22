using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class FeetIK : MonoBehaviour {
    #region private enum
    private enum FootSide : byte {
        Left,
        Right
    }
    #endregion
    #region Attributes
    private Animator myAnimator = null;

    [SerializeField]
    private LayerMask layerMask = ~0x0;

    [SerializeField]
    private float rayCastDistance = 0.5f;

    public string leftFootContactCurveName = "Left Foot Contact";
    public string rightFootContactCurveName = "Right Foot Contact"; 
    #endregion

    

    private void Awake() {
        TakeTheReferences();
    }
    #region Awake methods
    private void TakeTheReferences() {
        myAnimator = GetComponent<Animator>();
    } 
    #endregion



    private void OnAnimatorIK(int layerIndex) {
        SetFootIK(FootSide.Left);
        SetFootIK(FootSide.Right);      
    }
    #region OnAnimatorIK methods
    private void SetFootIK(FootSide footSide) {
        #region Variables assignment
        HumanBodyBones footBone = footSide == FootSide.Left ? HumanBodyBones.LeftFoot : HumanBodyBones.RightFoot;
        AvatarIKGoal footGoal = footSide == FootSide.Left ? AvatarIKGoal.LeftFoot : AvatarIKGoal.RightFoot;
        string curveName = footSide == FootSide.Left ? leftFootContactCurveName : rightFootContactCurveName;
        Transform foot = myAnimator.GetBoneTransform(footBone);
        #endregion

        Ray ray = new Ray(foot.position + Vector3.up * rayCastDistance * 0.5f, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayCastDistance, layerMask)) {
            #region Variables assignment
            Vector3 goalPosition = hit.point + Vector3.up * (footSide == FootSide.Left ?
                                       myAnimator.leftFeetBottomHeight : myAnimator.rightFeetBottomHeight);

            Quaternion goalRotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation;
            #endregion

            myAnimator.SetIKPosition(footGoal, goalPosition);
            myAnimator.SetIKRotation(footGoal, goalRotation);

            myAnimator.SetIKPositionWeight(footGoal, myAnimator.GetFloat(curveName));
            myAnimator.SetIKRotationWeight(footGoal, myAnimator.GetFloat(curveName));
        }
    } 
    #endregion
}
