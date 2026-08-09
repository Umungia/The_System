using UnityEngine;

public enum KeyType
{
    Simple,
    Base,
    Master,
    None
}
public class OnSystemKeyPickup : MonoBehaviour
{
    [SerializeField] private Transform playerObject;
    [SerializeField] private OnSystemKeys keyManager;
    [SerializeField] private KeyType keyType;
    void Update()
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
