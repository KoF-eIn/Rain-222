using System;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    protected override void Awake()
    {
        base.Awake();

        SpawnerLocator.CubeSpawner = this;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnRandomCube), 0f, 1f);
    }

    private void SpawnRandomCube()
    {
        Vector3 randomPos = new Vector3(UnityEngine.Random.Range(-10f, 10f), 10f, UnityEngine.Random.Range(-10f, 10f));
        Spawn(randomPos, Quaternion.identity);
    }
}