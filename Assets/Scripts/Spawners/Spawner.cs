using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected Transform ParentForObjects;

    protected ObjectPool<T> _pool;

    public int TotalCreated { get; private set; }
    public int SpawnCount { get; private set; }
    public int ActiveCount { get; private set; }

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(Prefab, ParentForObjects != null ? ParentForObjects : transform);
        _pool.ObjectCreated += OnObjectCreated;
    }

    private void OnObjectCreated(T obj)
    {
        TotalCreated++;
    }

    public virtual T Spawn(Vector3 position, Quaternion rotation)
    {
        T obj = _pool.Get(position, rotation);
        SpawnCount++;
        ActiveCount++;

        return obj;
    }

    public virtual void Despawn(T obj)
    {
        _pool.Return(obj);
        ActiveCount--;
    }

    protected virtual void OnDestroy()
    {
        if (_pool != null)
            _pool.ObjectCreated -= OnObjectCreated;
    }
}