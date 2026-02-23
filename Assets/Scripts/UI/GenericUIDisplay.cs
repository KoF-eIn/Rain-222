using UnityEngine;
using UnityEngine.UI;

public abstract class GenericUIDisplay<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] protected Spawner<T> _spawner;
    [SerializeField] protected Text _spawnedText;
    [SerializeField] protected Text _createdText;
    [SerializeField] protected Text _activeText;

    protected virtual void Update()
    {
        if (_spawner == null) return;

        _spawnedText.text = $"Spawned: {_spawner.SpawnCount}";
        _createdText.text = $"Created: {_spawner.TotalCreated}";
        _activeText.text = $"Active: {_spawner.ActiveCount}";
    }
}