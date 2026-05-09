using UnityEngine;
using System.Collections;
using TMPro;

public class WaveSpwaner : MonoBehaviour
{
    public Transform Enemyprefab;

    public Transform SpawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public TextMeshProUGUI wavecount_txt;

    private int waveIndex = 0;

    private void Update()
    {
        if (countdown <= 0f)
        { 
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;

        wavecount_txt.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i= 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(Enemyprefab,SpawnPoint.position,SpawnPoint.rotation);
    }
}
