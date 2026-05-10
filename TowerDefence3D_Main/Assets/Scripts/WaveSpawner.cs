using UnityEngine;
using System.Collections;
using TMPro;

public class WaveSpwaner : MonoBehaviour
{
    [HideInInspector] public LevelData currentLevelData;

    public static int EnemiesAlive = 0;

    private Wave[] waves;

    public Transform Enemyprefab;

    public Transform SpawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public TextMeshProUGUI wavecount_txt;
    private bool allWavesSpawned = false;

    private int waveIndex = 0;

    public void InitializeLevel(LevelData levelToLoad)
    {
        GameManager.instance.EnemyScrore = 0;
        currentLevelData = levelToLoad;
        waves = currentLevelData.waves;
        waveIndex = 0;
        EnemiesAlive = 0;
        countdown = 2f;
        allWavesSpawned = false;

        this.enabled = true;
    }

    private void Update()
    {
        if (waves == null || waves.Length == 0) return;

        if (allWavesSpawned && EnemiesAlive <= 0)
        {
            WinLevel();
            return;
        }

        if (EnemiesAlive > 0)
        { 
            return;
        }

        if (!allWavesSpawned && countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        if (!allWavesSpawned)
        {
            countdown -= Time.deltaTime;
            float currentCountdown = Mathf.Max(0f, countdown);
            wavecount_txt.text = "Next Wave in\n" + FormatTime(currentCountdown);
        }
        else
        {
            wavecount_txt.text = "Clear Remaining Enemies!";
        }
    }

    IEnumerator SpawnWave()
    {
        GameManager.instance.WaveRoundnumber++;
        Wave wave = waves[waveIndex];

        for (int i= 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.Enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;

        if (waveIndex >= waves.Length)
        {
            allWavesSpawned = true;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy,SpawnPoint.position,SpawnPoint.rotation);
        EnemiesAlive++;
    }

    private void WinLevel()
    {
        GameManager.instance.OnLevelWin();
        wavecount_txt.text = "VICTORY!";
        this.enabled = false;
    }

    string FormatTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        
        return string.Format("{0}:{1:00}", minutes, seconds);
    }
}
