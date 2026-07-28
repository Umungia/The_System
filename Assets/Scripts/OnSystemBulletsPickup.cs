using UnityEngine;

public enum BulletType
{
    Pistol,
    Shotgun,
    Ak47
    
}
public class OnSystemBulletsPickup : MonoBehaviour
{
    [SerializeField] private Transform playerObject;
    [SerializeField] private OnSystemWeapons weaponManager;
    [SerializeField] private BulletType bulletType;
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
            weaponManager.GetBullet(bulletType);
        }
    }
}
