using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TutorialManager : MonoBehaviour
{
    public Hand leftHand;
    public Hand rightHand;
    public SteamVR_Action_Vector2 WalkingAction;

    public VRWalking VrWalking;

    private bool playerWalked = false;
    private bool walkingTutorialFinished = false;

    private void Update()
    {
        if(!walkingTutorialFinished)
            HandleWalkingHint(leftHand);
    }

    private void HandleWalkingHint(Hand hand)
    {
        if (!playerWalked)
        {
            ControllerButtonHints.ShowTextHint(hand, WalkingAction, "Raak de trackpad aan om te lopen", true);
            hand.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithController, 0.1f);
        }
        else
        {
            ControllerButtonHints.HideAllTextHints(hand);
            hand.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithoutController, 0.1f);
            walkingTutorialFinished = true;
        }

        if (VrWalking.walkState == walkingState.Walking)
            playerWalked = true;
    }

    private void HandleRotationHint(Hand hand)
    {

    }
}
