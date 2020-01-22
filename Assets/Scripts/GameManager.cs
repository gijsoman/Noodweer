using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using FMODUnity;

public class GameManager : MonoBehaviour
{
    #region Singleton!
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public bool IntroPlaying = false;
    public bool PlayIntro = true;
    public StudioEventEmitter PoliceIntroEventEmitter;
    public float SlowDownFactor = 0.05f;
    public float SlowDownLength = 2f;

    private bool FadedIn = false;

    private void Start()
    {
        if (PlayIntro)
        {
            SteamVR_Fade.View(Color.black, 0);
            IntroPlaying = true;
        }
    }

    private void Update()
    {        
        if(!FadedIn && PoliceIntroPlaybackPosition() >= 40000)
        {
            FadedIn = true;            
            FadeToClear();
            IntroPlaying = false;
        }
    }

    private int PoliceIntroPlaybackPosition()
    {
        int TimelineTime;
        PoliceIntroEventEmitter.EventInstance.getTimelinePosition(out TimelineTime);
        return TimelineTime;
    }

    public void FadeToClear()
    {
        SteamVR_Fade.View(Color.clear, 1);
    }

    private void DoSlowMotion()
    {
        Time.timeScale = SlowDownFactor;
    }

    
}
