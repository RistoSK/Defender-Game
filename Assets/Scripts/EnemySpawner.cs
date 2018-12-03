using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Wave> waves;
    [SerializeField] int targetWave = 0;
    [SerializeField] bool shouldLoop = false;
    [SerializeField] int enemyAmountIncremention = 1;
    [SerializeField] float enemySpeedIncremention = 0.2f;

    // Use this for initialization
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (shouldLoop);
    }
	
    private IEnumerator SpawnAllWaves()
    {
        // Randomize the order that the waves appear in the game
        System.Random random = new System.Random();
        for (int i = targetWave; i < waves.Count; i++)
        {
            int randomNumber = i + random.Next(waves.Count - i);
            Wave wave = waves[randomNumber];
            waves[randomNumber] = waves[i];
            waves[i] = wave;
        }

        for (int i = targetWave; i < waves.Count; i++)
        {
            Wave currentWave = waves[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));

            // make wave harder next time it appears
            waves[i].SetEnemyAmount(enemyAmountIncremention);
            waves[i].SetEnemySpeed(enemySpeedIncremention);
        }
    }

	private IEnumerator SpawnAllEnemiesInWave(Wave wave)
    {
        for (int i = 0; i < wave.GetEnemyAmount(); i++)
        {
            var enemy = Instantiate(wave.GetEnemy(), wave.GetWaypoints()[0].transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyPathing>().SetWave(wave);  

            yield return new WaitForSeconds(wave.GetSpawnDelay());
        }
    }

    public void ResetAllWaveSettings()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            waves[i].ResetWaveSettings();
        }
    }
}
