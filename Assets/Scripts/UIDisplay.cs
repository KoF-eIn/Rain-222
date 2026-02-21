using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    public Text cubeSpawnedText;
    public Text cubeCreatedText;
    public Text cubeActiveText;
    public Text bombSpawnedText;
    public Text bombCreatedText;
    public Text bombActiveText;

    private void Update()
    {
        if (SpawnerLocator.CubeSpawner != null)
        {
            cubeSpawnedText.text = $"Cube Spawned: {SpawnerLocator.CubeSpawner.SpawnCount}";
            cubeCreatedText.text = $"Cube Created: {SpawnerLocator.CubeSpawner.TotalCreated}";
            cubeActiveText.text = $"Cube Active: {SpawnerLocator.CubeSpawner.ActiveCount}";
        }

        if (SpawnerLocator.BombSpawner != null)
        {
            bombSpawnedText.text = $"Bomb Spawned: {SpawnerLocator.BombSpawner.SpawnCount}";
            bombCreatedText.text = $"Bomb Created: {SpawnerLocator.BombSpawner.TotalCreated}";
            bombActiveText.text = $"Bomb Active: {SpawnerLocator.BombSpawner.ActiveCount}";
        }
    }
}