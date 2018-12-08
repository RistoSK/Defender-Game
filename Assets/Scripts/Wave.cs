using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Configuration")]
public class Wave : ScriptableObject
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject path;
    [SerializeField] int currentEnemyAmount = 5;
    [SerializeField] float currentEnemySpeed = 2f;
    [SerializeField] float spawnDelay = 0.7f;
    [SerializeField] int enemyAmountIncremention = 1;
    [SerializeField] float enemySpeedIncremention = 0.2f;

    int originalEnemyAmount = 5;
    float originalEnemySpeed = 2f;

    public void ResetWaveSettings()
    {
        currentEnemyAmount = originalEnemyAmount;
        currentEnemySpeed = originalEnemySpeed;
    }

    public GameObject GetEnemy()
    {
        return enemy;
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
    
        foreach(Transform waypoint in path.transform)
        {
            waypoints.Add(waypoint);
        }
        return waypoints;
    }

    public int GetEnemyAmount()
    {
        return currentEnemyAmount;
    }

    public void IncrementEnemyAmount()
    {
        this.currentEnemyAmount += enemyAmountIncremention;
    }

    public float GetEnemySpeed()
    {
        return currentEnemySpeed;
    }

    public void IncrementEnemySpeed()
    {
        this.currentEnemySpeed += enemySpeedIncremention;
    }

    public float GetSpawnDelay()
    {
        return spawnDelay;
    }
}
