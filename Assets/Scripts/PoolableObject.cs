using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    public virtual void OnSpawn() { }
    public virtual void OnDespawn() { }
}