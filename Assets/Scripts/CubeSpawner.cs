using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [Header("Spawn Settings")]
    [SerializeField] private float _spawnInterval = 1f;
    [SerializeField] private float _spawnHeight = 10f;
 
    [SerializeField] private Vector3 _spawnAreaCenter = Vector3.zero;
    [SerializeField] private Vector3 _spawnAreaSize = new Vector3(20f, 0f, 20f);


    protected override void Awake()
    {
        base.Awake();
        SpawnerLocator.CubeSpawner = this;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnRandomCube), 0f, _spawnInterval);
    }

    private void SpawnRandomCube()
    {
        float randomX = Random.Range(-_spawnAreaSize.x / 2, _spawnAreaSize.x / 2);
        float randomZ = Random.Range(-_spawnAreaSize.z / 2, _spawnAreaSize.z / 2);

        Vector3 spawnPos = new Vector3(
            _spawnAreaCenter.x + randomX,
            _spawnHeight,
            _spawnAreaCenter.z + randomZ
        );

        Spawn(spawnPos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(_spawnAreaCenter.x, _spawnHeight, _spawnAreaCenter.z),
                            new Vector3(_spawnAreaSize.x, 0.1f, _spawnAreaSize.z));
    }
}