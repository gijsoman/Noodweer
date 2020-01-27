using UnityEngine;

public class EndscreenManager : MonoBehaviour
{
    private GameObject endscreenEnemyDied;
    private GameObject endScreenPlayerDied;
    private GameObject baseCharacterWon;
    private GameObject baseCharacterDied;

    private void Start()
    {
        endscreenEnemyDied = GameObject.Find("EndScreenEnemyDied");
        baseCharacterDied = GameObject.Find("BaseCharacterDied");
        endScreenPlayerDied = GameObject.Find("EndScreenPlayerDied");
        baseCharacterWon = GameObject.Find("BaseCharacterWon");

        if (GameManager.Instance.playerDied)
        {
            endscreenEnemyDied.SetActive(false);
            baseCharacterDied.SetActive(false);
        }
        else if (GameManager.Instance.enemyDied)
        {
            endScreenPlayerDied.SetActive(false);
            baseCharacterWon.SetActive(false);
        }
    }
}
