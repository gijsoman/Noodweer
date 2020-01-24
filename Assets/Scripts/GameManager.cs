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

    public EnemyScript Enemy;
    public float SlowDownFactor = 0.05f;
    public float SlowDownLength = 2f;

    public bool enemyDied = false;
    public bool playerDied = false;
    private bool FadedIn = false;

    private void Start()
    {
        if (PlayIntro)
        {
            SteamVR_Fade.View(Color.black, 0);
            IntroPlaying = true;
        }
        else
        {
            IntroPlaying = false;
        }

        if (Enemy != null)
        {
            Enemy.IDied += EnemyKilledSequence;
            Enemy.IStabbed += PlayerKilledSequence;
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

    private void EnemyKilledSequence()
    {
        enemyDied = true;
        Time.timeScale = SlowDownFactor;
        SteamVR_Fade.View(Color.white, 0);
        SteamVR_Fade.View(Color.clear, 5);
    }

    private void PlayerKilledSequence()
    {
        playerDied = true;
        Time.timeScale = SlowDownFactor;
        SteamVR_Fade.View(Color.red, 0);
        SteamVR_Fade.View(Color.clear, 10);        
    }

    private void OnDestroy()
    {
        Enemy.IDied -= EnemyKilledSequence;
        Enemy.IStabbed -= PlayerKilledSequence;
    }

}
