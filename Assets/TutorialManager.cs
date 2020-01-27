using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class TutorialManager : MonoBehaviour
{
    private Player player;

    public Hand lefthand;
    public Hand righthand;

    public SteamVR_Action_Vector2 walk;

    private Coroutine hintCoroutine = null;

    private void Start()
    {
        player = Player.instance;
       ShowHint(lefthand, walk, "Raak de touchpad aan om te lopen");
    }

    private void Update()
    {
        if (walk.changed)
        {
            CancelHint(walk);
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

    private void StopAllHints(ISteamVR_Action_In_Source action)
    {
        StopAllCoroutines();
        bool isShowingHintRight = !string.IsNullOrEmpty(ControllerButtonHints.GetActiveHintText(righthand, action));
        bool isShowingHintLeft = !string.IsNullOrEmpty(ControllerButtonHints.GetActiveHintText(lefthand, action));

        if (isShowingHintLeft)
        {
            Debug.Log("SettingRangeOfmotion");
            lefthand.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithoutController, 0.1f);
            ControllerButtonHints.HideAllTextHints(lefthand);
        }
        if (isShowingHintRight)
        {
            righthand.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithoutController, 0.1f);
            ControllerButtonHints.HideAllTextHints(righthand);
        }
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

                    hand.TriggerHapticPulse(500);
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
