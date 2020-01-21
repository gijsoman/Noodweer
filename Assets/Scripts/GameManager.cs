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

    public bool introPlaying = false;
    public bool playIntro = true;

    public StudioEventEmitter PoliceIntroEventEmitter;
    private bool FadedIn = false;

    private void Start()
    {
        if (playIntro)
        {
            SteamVR_Fade.View(Color.black, 0);
            introPlaying = true;
        }
    }

    private void Update()
    {        
        if(!FadedIn && PoliceIntroPlaybackPosition() >= 40000)
        {
            FadedIn = true;            
            FadeToClear();
            introPlaying = false;
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

    
}
