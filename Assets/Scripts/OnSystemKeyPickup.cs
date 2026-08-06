using UnityEngine;

public enum KeyType
{
    Simple,
    Base,
    Master
}
public class OnSystemKeyPickup : MonoBehaviour
{
    [SerializeField] private Transform playerObject;
    [SerializeField] private OnSystemKeys keyManager;
    [SerializeField] private KeyType keyType;
    void Start()
    {
        transform.LookAt(new Vector3(playerObject.position.x, transform.position.y, playerObject.position.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            keyManager.GetKey(keyType);
        }
    }
}
