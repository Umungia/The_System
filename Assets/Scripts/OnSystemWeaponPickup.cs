using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponType weaponType;
    public enum WeaponType
    {
        Shotgun,
        Ak47
    }

    void Start()
    {

    }


    void Update()
    {
        GunSelection();
    }

    private void GunSelection()
    {
        switch (weaponType)
        {
            case WeaponType.Shotgun:
                // desbloquear escopeta
                break;
            case WeaponType.Ak47:
                // desbloquear AK
                break;
        }
    }
}
