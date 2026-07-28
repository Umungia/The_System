using UnityEngine;

public class OnSystemHealthPickup : MonoBehaviour
{
    [SerializeField] private Transform playerObject;
    [SerializeField] private OnSystemPlayerHealth healthManager;
    void Start()
    {

    }


    void Update()
    {

        transform.LookAt(new Vector3(playerObject.position.x, transform.position.y, playerObject.position.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            healthManager.HealPlayer();
        }
    }
}
