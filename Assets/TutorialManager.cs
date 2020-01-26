using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TutorialManager : MonoBehaviour
{
    public Hand leftHand;
    public Hand rightHand;

    public SteamVR_Action_Vector2 WalkingAction;
    public SteamVR_Action_Boolean RotateRight;
    public SteamVR_Action_Boolean RotateLeft;
    public SteamVR_Action_Boolean GrabGrip;

    public bool playerWalked = false;
    public bool playerTurnedRight = false;
    public bool playerTurnedLeft = false;
    public bool playerGrabbedGun = false;


    enum TutorialState
    {
        Walking,
        Turning,
        Grabbing,
        Holstering
    }

    private TutorialState tutorialState = TutorialState.Walking;

    private void Update()
    {
        if(tutorialState == TutorialState.Walking)
            HandleWalkingHint();

        if (tutorialState == TutorialState.Turning)
            HandleSnapTurnHint();

        if (tutorialState == TutorialState.Grabbing)
            HandleGrabHint();
    }

    private void HandleWalkingHint()
    {
        if (WalkingAction.changed)
            playerWalked = true;

        if (!playerWalked)
        {
            ControllerButtonHints.ShowTextHint(leftHand, WalkingAction, "Raak de trackpad aan om te lopen", true);
            leftHand.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithController, 0.1f);
        }
        else
        {
            ControllerButtonHints.HideAllTextHints(leftHand);
            leftHand.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithoutController, 0.1f);
            tutorialState = TutorialState.Turning;
        }
    }

    private void HandleSnapTurnHint()
    {
        if (RotateRight.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            playerTurnedRight = true;
            ControllerButtonHints.HideTextHint(rightHand, RotateRight);
        }
        if (RotateLeft.GetStateDown(SteamVR_Input_Sources.RightHand))
            playerTurnedLeft = true;

        if (!playerTurnedRight)
        {
            ControllerButtonHints.ShowTextHint(rightHand, RotateRight, "Druk aan de rechterkant van de trackpad om snel naar rechts te draaien", true);
            rightHand.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithController, 0.1f);
        }
        else if (!playerTurnedLeft)
        {
            ControllerButtonHints.ShowTextHint(rightHand, RotateLeft, "Druk aan de linkerkant van de trackpad om snel naar links te draaien", true);
            rightHand.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithController, 0.1f);
        }
        else
        {
            ControllerButtonHints.HideAllTextHints(rightHand);
            rightHand
.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithoutController, 0.1f);
            tutorialState = TutorialState.Grabbing;
        }
    }

    private void HandleGrabHint()
    {
        if (!playerGrabbedGun)
        {
            ControllerButtonHints.ShowTextHint(leftHand, GrabGrip, "Druk op de grip button om je wapen op te pakken", true);
            ControllerButtonHints.ShowTextHint(rightHand, GrabGrip, "Druk op de grip button om je wapen op te pakken", true);
            rightHand.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithController, 0.1f);
            leftHand.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithController, 0.1f);
        }
    }
}
