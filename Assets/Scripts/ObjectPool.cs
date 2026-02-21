using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : PoolableObject
{
    private readonly T _prefab;

    private readonly Transform _parent;

    private readonly Stack<T> _freeObjects = new Stack<T>();

    public int TotalCreated { get; private set; }
    public int ActiveCount { get; private set; }
    public int SpawnCount { get; private set; }

    public ObjectPool(T prefab, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
    }

    public T Get(Vector3 position, Quaternion rotation)
    {
        T obj;

        if (_freeObjects.Count > 0)
        {
            obj = _freeObjects.Pop();
            obj.transform.SetPositionAndRotation(position, rotation);
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = Object.Instantiate(_prefab, position, rotation, _parent);
            TotalCreated++;
        }

        ActiveCount++;
        SpawnCount++;
        obj.OnSpawn();

        return obj;
    }

    public void Return(T obj)
    {
        obj.OnDespawn();
        obj.gameObject.SetActive(false);
        _freeObjects.Push(obj);
        ActiveCount--;
    }
}