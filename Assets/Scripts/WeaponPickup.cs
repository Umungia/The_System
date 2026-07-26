using UnityEngine;



public enum WeaponType
{
    Shotgun,
    Ak47
}
public class OnSystemWeaponPickup : MonoBehaviour
{
    [SerializeField] private Transform playerObject;
    [SerializeField] private OnSystemWeapons weaponManager;
    [SerializeField] private WeaponType weaponType;



    void Update()
    {

        transform.LookAt(new Vector3(playerObject.position.x, transform.position.y, playerObject.position.z));
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            weaponManager.GetGun(weaponType);
        }
    }
}
