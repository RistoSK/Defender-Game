using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Wave> waves;
    [SerializeField] List<Wave> bossWaves;
    [SerializeField] int targetWave = 0;
    [SerializeField] bool shouldLoop = true;

    GameObject enemy;
    int allWavesCompletedIndex;
    int counter;

    void Start()
    {
        allWavesCompletedIndex = 0;
        StartCoroutine(IterateThroughWaves());
    }

    // Use this for initialization
    IEnumerator IterateThroughWaves()
    {
        do
        {
            if ((allWavesCompletedIndex % 4) == 0 && allWavesCompletedIndex != 0)
            {
                yield return StartCoroutine(SpawnAllWaves(bossWaves));
                // Wait until the boss is killed
                yield return new WaitUntil(() => enemy == null);
            }
            else
            {
                yield return StartCoroutine(SpawnAllWaves(waves));
            }
        }
        while (shouldLoop);
    }

    private IEnumerator SpawnAllWaves(List<Wave> wavesToSpawn)
    {
        // Randomize the order that the waves appear in the game
        System.Random random = new System.Random();
        for (int i = targetWave; i < wavesToSpawn.Count; i++)
        {
            int randomNumber = i + random.Next(wavesToSpawn.Count - i);
            Wave wave = wavesToSpawn[randomNumber];
            wavesToSpawn[randomNumber] = wavesToSpawn[i];
            wavesToSpawn[i] = wave;
        }

        for (int i = targetWave; i < wavesToSpawn.Count; i++)
        {
            Wave currentWave = wavesToSpawn[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));

            // make wave harder next time it appears
            wavesToSpawn[i].IncrementEnemyAmount();
            wavesToSpawn[i].IncrementEnemySpeed();
            allWavesCompletedIndex++;
        }
    }

	private IEnumerator SpawnAllEnemiesInWave(Wave wave)
    {
        for (int i = 0; i < wave.GetEnemyAmount(); i++)
        {
            enemy = Instantiate(wave.GetEnemy(), wave.GetWaypoints()[0].transform.position, Quaternion.identity);
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
