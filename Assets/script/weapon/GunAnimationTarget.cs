using UnityEngine;
using UnityEditor.Animations;
using UnityEditor;
public class GunAnimationTarget : MonoBehaviour
{
    public AnimationClip animationClip;
    public Transform rightArmBone;
    public Transform leftArmBone;
    public Transform cameraTransform;

    private void Start()
    {
        if (animationClip == null || rightArmBone == null || leftArmBone == null || cameraTransform == null)
        {
            Debug.LogError("Veuillez assigner les références nécessaires dans l'inspecteur Unity.");
            return;
        }

        AnimationClip newAnimationClip = Instantiate(animationClip);

        // Récupérer les courbes de rotation du bras droit et gauche
        AnimationCurve rightArmCurve = AnimationUtility.GetEditorCurve(newAnimationClip, GetCurveBinding(rightArmBone, "localEulerAnglesBaked"));
        AnimationCurve leftArmCurve = AnimationUtility.GetEditorCurve(newAnimationClip, GetCurveBinding(leftArmBone, "localEulerAnglesBaked"));

        // Modifier les keyframes des courbes en fonction de l'orientation de la caméra
        for (int i = 0; i < rightArmCurve.keys.Length; i++)
        {
            float time = rightArmCurve.keys[i].time;
            Quaternion rightArmRotation = GetModifiedArmRotation(rightArmBone, cameraTransform);
            Quaternion leftArmRotation = GetModifiedArmRotation(leftArmBone, cameraTransform);

            rightArmCurve.MoveKey(i, new Keyframe(time, rightArmRotation.eulerAngles.y));
            leftArmCurve.MoveKey(i, new Keyframe(time, leftArmRotation.eulerAngles.y));
        }

        // Appliquer les nouvelles courbes modifiées à l'animationClip
        AnimationUtility.SetEditorCurve(newAnimationClip, GetCurveBinding(rightArmBone, "localEulerAnglesBaked"), rightArmCurve);
        AnimationUtility.SetEditorCurve(newAnimationClip, GetCurveBinding(leftArmBone, "localEulerAnglesBaked"), leftArmCurve);

        // Créer un nouvel AnimatorController avec la nouvelle animation modifiée
        AnimatorController animatorController = new AnimatorController();
        animatorController.AddMotion(newAnimationClip);
        Animator animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animatorController;
    }

    private EditorCurveBinding GetCurveBinding(Transform boneTransform, string propertyName)
    {
        EditorCurveBinding curveBinding = new EditorCurveBinding();
        curveBinding.type = typeof(Transform);
        curveBinding.path = AnimationUtility.CalculateTransformPath(boneTransform, transform);
        curveBinding.propertyName = propertyName;
        return curveBinding;
    }

    private Quaternion GetModifiedArmRotation(Transform armBone, Transform cameraTransform)
    {
        Vector3 targetDirection = cameraTransform.forward;
        Vector3 armDirection = armBone.position - cameraTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion armRotation = Quaternion.LookRotation(armDirection, Vector3.up);
        Quaternion modifiedRotation = Quaternion.Inverse(targetRotation) * armRotation;
        return modifiedRotation;
    }
}