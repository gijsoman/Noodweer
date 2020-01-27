using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class TutorialManager : MonoBehaviour
{
    private Player player;

    public Hand lefthand;
    public Hand righthand;
    public GameObject Handgun;
    public GameObject GunHighlighter;
    public GameObject SceneSwitcher;
    public HolsterSlot GunHolster;

    public SteamVR_Action_Vector2 walk;
    public SteamVR_Action_Boolean TurnRight;
    public SteamVR_Action_Boolean TurnLeft;
    public SteamVR_Action_Boolean Shoot;
    public SteamVR_Action_Boolean Grab;

    private Coroutine hintCoroutine = null;

    enum TutorialState
    {
        Walking,
        TurningRight,
        TurningLeft,
        Grabbing,
        Shooting,
        Holstering
    }
    private TutorialState tutState;

    private void Start()
    {
       player = Player.instance;
        Handgun.GetComponent<Wieldable>().enabled = false;
       ShowHint(lefthand, walk, "Raak de touchpad aan om te lopen");
    }

    private void Update()
    {
        if (walk.changed && tutState == TutorialState.Walking)
        {
            CancelHint(walk);
            tutState = TutorialState.TurningRight;
            ShowHint(righthand, TurnRight, "Druk de rechterkant van de touchpad in om naar rechts te draaien");
        }

        if (TurnRight.changed && tutState == TutorialState.TurningRight)
        {
            CancelHint(TurnRight);
            tutState = TutorialState.TurningLeft;
            ShowHint(righthand, TurnLeft, "Druk de linkerkant van de touchpad in om naar links te draaien");
        }

        if (TurnLeft.changed && tutState == TutorialState.TurningLeft)
        {
            CancelHint(TurnLeft);
            tutState = TutorialState.Grabbing;
            ShowHint (righthand, Grab, "Druk op de grip button om je wapen op te pakken (werkt ook met links)");
            GunHighlighter.SetActive(true);
            Handgun.GetComponent<Wieldable>().enabled = true;
        }

        if (Grab.changed && tutState == TutorialState.Grabbing )
        {            
            if (lefthand.currentAttachedObject == Handgun || righthand.currentAttachedObject == Handgun)
            {
                CancelHint(Grab);
                GunHighlighter.SetActive(false);
                tutState = TutorialState.Shooting;
                if (righthand.currentAttachedObject == Handgun)
                {
                    ShowHint(righthand, Shoot, "Druk op de trigger om te schieten");
                }
                else if (lefthand.currentAttachedObject == Handgun)
                {
                    ShowHint(lefthand, Shoot, "Druk op de trigger om te schieten");
                }
            }
        }

        if (Shoot.changed && tutState == TutorialState.Shooting)
        {
            CancelHint(Shoot);
            tutState = TutorialState.Holstering;
            if (righthand.currentAttachedObject == Handgun)
            {
                ShowHint(righthand, Grab, "Druk op de gripbutton in de buurt van je holster om je wapen op te bergen");
            }
            else if (lefthand.currentAttachedObject == Handgun)
            {
                ShowHint(lefthand, Grab, "Druk op de gripbutton in de buurt van je holster om je wapen op te bergen");
            }
        }

        if (Grab.changed && tutState == TutorialState.Holstering && GunHolster.HolsteredItem == Handgun)
        {
            CancelHint(Grab);
            SceneSwitcher.SetActive(true);
        }
       
    }

    public void ShowHint(Hand hand, ISteamVR_Action_In_Source _action, string hint)
    {
        CancelHint(_action);

        hintCoroutine = StartCoroutine(ShowHintCoroutine(hand, _action, hint));
    }

    public void CancelHint(ISteamVR_Action_In_Source _action)
    {
        if (hintCoroutine != null)
        {
            ControllerButtonHints.HideTextHint(player.leftHand, _action);
            ControllerButtonHints.HideTextHint(player.rightHand, _action);

            StopCoroutine(hintCoroutine);
            hintCoroutine = null;
        }

        CancelInvoke("ShowHintCoroutine");
    }

    private IEnumerator ShowHintCoroutine(Hand hand, ISteamVR_Action_In_Source _action, string hint)
    {
        float prevBreakTime = Time.time;
        float prevHapticPulseTime = Time.time;

        while (true)
        {
            bool pulsed = false;

            bool showHint = true;
            bool isShowingHint = !string.IsNullOrEmpty(ControllerButtonHints.GetActiveHintText(hand, _action));
            if (showHint)
            {
                if (!isShowingHint)
                {
                    ControllerButtonHints.ShowTextHint(hand, _action, hint);
                    prevBreakTime = Time.time;
                    prevHapticPulseTime = Time.time;
                }

                if (Time.time > prevHapticPulseTime + 0.05f)
                {
                    //Haptic pulse for a few seconds
                    pulsed = true;

                    hand.TriggerHapticPulse(1000);
                }
            }
            else if (!showHint && isShowingHint)
            {
                ControllerButtonHints.HideTextHint(hand, walk);
            }
            if (Time.time > prevBreakTime + 3.0f)
            {
                //Take a break for a few seconds
                yield return new WaitForSeconds(3.0f);
                prevBreakTime = Time.time;
            }

            if (pulsed)
            {
                prevHapticPulseTime = Time.time;
            }

            yield return null;
        }
    }
}
