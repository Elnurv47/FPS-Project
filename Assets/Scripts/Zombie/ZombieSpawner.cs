using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    private List<Zombie> spawnedZombies;
    public List<Zombie> SpawnedZombies { get => spawnedZombies; }

    [SerializeField] private float spawnInterval;
    [SerializeField] private Zombie zombiePrefab;
    [SerializeField] private Transform[] spawnLocations;

    public Action OnNewZombieSpawned;

    private void Awake()
    {
        spawnedZombies = new List<Zombie>();
        StartCoroutine(SpawnZombieCoroutine());
    }

    private IEnumerator SpawnZombieCoroutine()
    {
        while (true)
        {
            SpawnZombie();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnZombie()
    {
        Vector3 randomSpawnLocation = GetRandomLocation();
        Zombie spawnedZombie = Instantiate(zombiePrefab, randomSpawnLocation, Quaternion.identity);
        spawnedZombies.Add(spawnedZombie);
        OnNewZombieSpawned?.Invoke();
    }

    private Vector3 GetRandomLocation()
    {
        int randomIndex = Random.Range(0, spawnLocations.Length);
        Vector3 randomLocation = spawnLocations[randomIndex].position;
        return randomLocation;
    }
}
