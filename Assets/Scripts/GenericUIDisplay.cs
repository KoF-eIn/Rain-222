using UnityEngine;
using UnityEngine.UI;

public class GenericUIDisplay<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private Text _spawnedText;
    [SerializeField] private Text _createdText;
    [SerializeField] private Text _activeText;

    private void Update()
    {
        if (_spawner == null) return;

        _spawnedText.text = $"Spawned: {_spawner.SpawnCount}";
        _createdText.text = $"Created: {_spawner.TotalCreated}";
        _activeText.text = $"Active: {_spawner.ActiveCount}";
    }
}