using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




public class OnSystemWeapons : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OnSystemInputHandler playerInputHandler;
    [SerializeField] private Image weaponImage;
    [SerializeField] private Sprite idleSprite, shootingSprite;
    [SerializeField] private TMP_Text bulletText;

    [Header("Floats")]
    [SerializeField] private float shootRange = 100f;
    [SerializeField] private float gunDamage = 10f;
    [SerializeField] private float spriteTimeOnScreen = 0.5f;
    [SerializeField] private float spriteTimeOnScreenOG;
    [SerializeField] private float shootFreq = 0.5f;
    [SerializeField] private float shootFreqOG;

    [Header("Gun Sprites")]
    [SerializeField] private Sprite pistolSprite;
    [SerializeField] private Sprite pistolShootingSprite;
    [SerializeField] private Sprite shotgunSprite;
    [SerializeField] private Sprite shotgunShootingSprite;
    [SerializeField] private Sprite ak47Sprite;
    [SerializeField] private Sprite ak47ShootingSprite;

    [Header("Gun Enablers")]
    private bool hasShotgun = false;
    private bool hasAk47 = false;

   

    //General Gun Variables
    private int selectedGun;
    private int loadedBullets;
    private int magazineCap;
    private int scrollDirection;


    private bool isAutomatic;
    //Pistol
    [Header("GunBullets")]
    [SerializeField] private int pistolLoadedBullets = 7;
    [SerializeField] private int pistolMagazineCap = 7;
    [SerializeField] private int pistolDamage = 5;
    [SerializeField] private float pistolShootRange = 50f;
    [SerializeField] private float pistolShootFreq = 0.5f;

    //Shotgun
    [SerializeField] private int shotgunLoadedBullets = 6;
    [SerializeField] private int shotgunMagazineCap = 6;
    [SerializeField] private int shotgunDamage = 15;
    [SerializeField] private float shotgunShootRange = 10f;
    [SerializeField] private float shotgunShootFreq = 2;

    //Ak-47
    [SerializeField] private int ak47LoadedBullets = 30;
    [SerializeField] private int ak47MagazineCap = 30;
    [SerializeField] private int ak47Damage = 5;
    [SerializeField] private float ak47ShootRange = 75f;
    [SerializeField] private float ak47ShootFreq = 0.5f;

 
    void Start()
    {
        spriteTimeOnScreenOG = spriteTimeOnScreen;
        selectedGun = 1;
        loadedBullets = pistolLoadedBullets;
    }

    void Update()
    {
        ChangeGuns();
        Shooting();
    }

    private void Shooting()
    {
        shootFreq -= Time.deltaTime;
        if (playerInputHandler.attackTriggered)
        {
            if (isAutomatic == false)
            {
                playerInputHandler.ConsumeAttack();
            }

            if (shootFreq <= 0 && loadedBullets > 0)
            {
                loadedBullets -= 1;
                bulletText.text = "Ammo: " + loadedBullets;
                shootFreq = shootFreqOG;
                weaponImage.sprite = shootingSprite;
                spriteTimeOnScreen = spriteTimeOnScreenOG;


                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, shootRange))
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        hit.collider.gameObject.GetComponent<OnSystemEnemyHealth>().HurtEnemy(gunDamage);

                    }
                }

            }
        }
        else
        {
            if (spriteTimeOnScreen > 0)
            {
                spriteTimeOnScreen -= Time.deltaTime;
            }
            else
            {
                weaponImage.sprite = idleSprite;
            }
        }

    }

    private void ChangeGuns()
    {
        if (playerInputHandler.gunSelectionInput != Vector2.zero)
        {

            //Save old amount of bullets
            switch (selectedGun)
            {
                case 1:
                    pistolLoadedBullets = loadedBullets;

                    break;
                case 2:
                    shotgunLoadedBullets = loadedBullets;

                    break;
                case 3:
                    ak47LoadedBullets = loadedBullets;

                    break;
            }
            scrollDirection = (int)playerInputHandler.gunSelectionInput.y;

            selectedGun += scrollDirection;
            if (selectedGun > 3) selectedGun = 1;
            if (selectedGun < 1) selectedGun = 3;

            while ((selectedGun == 2 && hasShotgun == false) || (selectedGun == 3 && hasAk47 == false))
            {
                selectedGun += scrollDirection;
                if (selectedGun > 3)
                {
                    selectedGun = 1;
                }

                if (selectedGun < 1)
                {
                    selectedGun = 3;
                }
            }
            switch (selectedGun)
            {
                case 1:
                    gunDamage = pistolDamage;
                    shootFreqOG = pistolShootFreq;
                    magazineCap = pistolMagazineCap;
                    loadedBullets = pistolLoadedBullets;
                    shootRange = pistolShootRange;
                    idleSprite = pistolSprite;
                    shootingSprite = pistolShootingSprite;
                    isAutomatic = false;
                    break;
                case 2:
                    if (hasShotgun == true)
                    {
                        gunDamage = shotgunDamage;
                        shootFreqOG = shotgunShootFreq;
                        magazineCap = shotgunMagazineCap;
                        loadedBullets = shotgunLoadedBullets;
                        shootRange = shotgunShootRange;
                        idleSprite = shotgunSprite;
                        shootingSprite = shotgunShootingSprite;
                        isAutomatic = false;
                    }
                    else
                    {
                        selectedGun += 1;
                    }
                    break;
                case 3:
                    if (hasAk47 == true)
                    {
                        gunDamage = ak47Damage;
                        shootFreqOG = ak47ShootFreq;
                        magazineCap = ak47MagazineCap;
                        loadedBullets = ak47LoadedBullets;
                        shootRange = ak47ShootRange;
                        idleSprite = ak47Sprite;
                        shootingSprite = ak47ShootingSprite;
                        isAutomatic = true;
                    }
                    else
                    {
                        selectedGun += 1;
                    }
                    break;
            }
            if (selectedGun > 3)
            {
                selectedGun = 1;
            }

            if (selectedGun < 1)
            {
                selectedGun = 3;
            }
        }
        bulletText.text = "Ammo: " + loadedBullets;
    }

    public void GetGun(WeaponType gunType)
    {
        if (gunType == WeaponType.Shotgun)
        {
            hasShotgun = true;
        }
        if (gunType == WeaponType.Ak47)
        {
            hasAk47 = true;
        }
    }
    public void GetBullet(BulletType bulletType)
    {
        if (bulletType == BulletType.Pistol)
        {
            if (selectedGun == 1)
            {
                loadedBullets += 10;
            }
            else
            {
                pistolLoadedBullets += 10;
            }
            if (pistolLoadedBullets > pistolMagazineCap)
            {
                pistolLoadedBullets = pistolMagazineCap;
            }
        }
        if (bulletType == BulletType.Shotgun)
        {
            if (selectedGun == 2)
            {
                loadedBullets += 10;
            }
            else
            {
                shotgunLoadedBullets += 10;
            }
            if (shotgunLoadedBullets > shotgunMagazineCap)
            {
                shotgunLoadedBullets = shotgunMagazineCap;
            }
        }
        if (bulletType == BulletType.Ak47)
        {
            if (selectedGun == 3)
            {
                loadedBullets += 20;
            }
            else
            {
                ak47LoadedBullets += 20;
            }
            if (ak47LoadedBullets > ak47MagazineCap)
            {
                ak47LoadedBullets = ak47MagazineCap;
            }
        }
    }
}
