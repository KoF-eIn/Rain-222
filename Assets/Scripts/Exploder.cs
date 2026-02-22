using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _force = 10f;
    [SerializeField] private LayerMask _targetLayers = -1;

    public void Explode(Vector3 center)
    {
        Collider[] colliders = Physics.OverlapSphere(center, _radius, _targetLayers);

        foreach (var col in colliders)
        {
            if (col.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.AddExplosionForce(_force, center, _radius, 0f, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}