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
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public GameObject VRplayer;

    public bool IntroPlaying = false;
    public bool PlayIntro = true;
    public StudioEventEmitter PoliceIntroEventEmitter;

    public EnemyScript Enemy;
    public float SlowDownFactor = 0.05f;
    public float SlowDownLength = 2f;

    public bool enemyDied = false;
    public bool playerDied = false;
    public int enemyDiedFadeLength = 5;
    public int playerDiedFadeLength = 5;

    public GameObject SceneSwitcher;

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

        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneSwitcher.SetActive(true);
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
        SteamVR_Fade.View(Color.clear, enemyDiedFadeLength);
        StartCoroutine(WaitForFade(enemyDiedFadeLength));
    }

    private void PlayerKilledSequence()
    {
        playerDied = true;
        Time.timeScale = SlowDownFactor;
        SteamVR_Fade.View(Color.red, 0);
        SteamVR_Fade.View(Color.clear, playerDiedFadeLength);
        StartCoroutine(WaitForFade(playerDiedFadeLength));
    }

    IEnumerator WaitForFade(int _fadeToWaitFor)
    {
        yield return new WaitForSeconds(_fadeToWaitFor);        
        SceneSwitcher.SetActive(true);
    }

    private void OnLevelWasLoaded(int level)
    {
        //reset the vrplayer to the center.
        if (level == 1)
        {
            Time.timeScale = 1;
            VRplayer.GetComponent<CharacterController>().enabled = false;
            VRplayer.GetComponent<VRWalking>().enabled = false;
            VRplayer.transform.position = new Vector3(0, VRplayer.transform.position.y, 0);
            VRplayer.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void OnDestroy()
    {
        Enemy.IDied -= EnemyKilledSequence;
        Enemy.IStabbed -= PlayerKilledSequence;
    }

}
