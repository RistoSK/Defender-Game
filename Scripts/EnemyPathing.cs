using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    Wave wave;
    List<Transform> waypoints;
    int targetWaypoint = 0;

    // Use this for initialization
    void Start ()
    {
        waypoints = wave.GetWaypoints();
        transform.position = waypoints[targetWaypoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWave(Wave wave)
    {
        this.wave = wave;
    }

    private void Move()
    {
        if (targetWaypoint < waypoints.Count)
        {
            var targetPosition = waypoints[targetWaypoint].transform.position;
            float step = wave.GetEnemySpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

            if (transform.position == targetPosition)
            {
                targetWaypoint++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
