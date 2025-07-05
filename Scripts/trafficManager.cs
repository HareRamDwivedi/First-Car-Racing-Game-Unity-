using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class trafficManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform[] lanes;
    [SerializeField] GameObject[] cars;
    [SerializeField] CarController carController;
    [SerializeField] float minSpawnTime = 30f;
    [SerializeField] float maxSpawnTime = 60f;
    void Start()
    {
        StartCoroutine(TrafficSpawner());
    }

    IEnumerator TrafficSpawner()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            float dynamicTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime) /carController.CarSpeed();

            if (carController.CarSpeed() > 20f)
            {
                SpawnTraffic();
            }
            yield return new WaitForSeconds(dynamicTime);
        }
    }

    void SpawnTraffic()
    {
        int laneIndex = UnityEngine.Random.Range(0, lanes.Length);
        int carIndex = UnityEngine.Random.Range(0, cars.Length);
        Instantiate(cars[carIndex], lanes[laneIndex].position, Quaternion.identity);
    }
}
