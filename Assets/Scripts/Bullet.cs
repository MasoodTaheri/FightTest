using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private float _lifetime;
    [SerializeField] private float _damage;
    private bool _isHit = false;

    public void Initialize(Vector3 startPosition, Vector3 force)
    {
        transform.position = startPosition;
        m_rigidbody.AddForce(force, ForceMode.Impulse);
        Destroy(gameObject, _lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isHit)
            return;
        var character = other.gameObject.GetComponent<IHealth>();
        if (character!=null)
        {
            character.TakeDamage(_damage);
        }

        gameObject.SetActive(false);
        Destroy(gameObject, 0.1f);
        _isHit = true;
    }
}