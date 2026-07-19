using UnityEngine;
using UnityEngine.UI;

public class OnSystemWeapons : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OnSystemInputHandler playerInputHandler;
    [SerializeField] private Image weaponImage;
    [SerializeField] private Sprite idleSprite, shootingSprite;

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



    
    //General Gun Variables
    private int selectedGun;
    private int loadedBullets;
    private int magazineCap;
    private int savedBullets;
    private bool isAutomatic;
    //Pistol
    [Header("GunBullets")]
    [SerializeField] private int pistolLoadedBullets = 7;
    [SerializeField] private int pistolMagazineCap = 7;
    [SerializeField] private int pistolSavedBullets = 14;
    [SerializeField] private int pistolDamage = 5;
    [SerializeField] private float pistolShootRange = 50f;
    [SerializeField] private float pistolShootFreq = 0.5f;

    //Shotgun
    [SerializeField] private int shotgunLoadedBullets = 6;
    [SerializeField] private int shotgunMagazineCap = 6;
    [SerializeField] private int shotgunSavedBullets = 12;
    [SerializeField] private int shotgunDamage = 15;
    [SerializeField] private float shotgunShootRange = 10f;
    [SerializeField] private float shotgunShootFreq = 2;

    //Ak-47
    [SerializeField] private int ak47LoadedBullets = 30;
    [SerializeField] private int ak47MagazineCap = 30;
    [SerializeField] private int ak47SavedBullets = 90;
    [SerializeField] private int ak47Damage = 5;
    [SerializeField] private float ak47ShootRange = 75f;
    [SerializeField] private float ak47ShootFreq = 0.5f;

    void Start()
    {
        spriteTimeOnScreenOG = spriteTimeOnScreen;
        selectedGun = 1;
        loadedBullets = pistolLoadedBullets;
        savedBullets = pistolSavedBullets;
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

            if (shootFreq <= 0)
            {
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
                    pistolSavedBullets = savedBullets;
                  
                    break;
                case 2:
                    shotgunLoadedBullets = loadedBullets;
                    shotgunSavedBullets = savedBullets;
          
                    break;
                case 3:
                    ak47LoadedBullets = loadedBullets;
                    ak47SavedBullets = savedBullets;
    
                    break;
            }
            if (playerInputHandler.gunSelectionInput.y > 0)
            {
                selectedGun += 1;
            }
            else
            {
                selectedGun -= 1;
            }
            if (selectedGun > 3)
            {
                selectedGun = 1;
            }

            if (selectedGun < 1)
            {
                selectedGun = 3;
            }


            switch (selectedGun)
            {
                case 1:
                    gunDamage = pistolDamage;
                    shootFreqOG = pistolShootFreq;
                    magazineCap = pistolMagazineCap;
                    loadedBullets = pistolLoadedBullets;
                    savedBullets = pistolSavedBullets;
                    shootRange = pistolShootRange;
                    idleSprite = pistolSprite;
                    shootingSprite = pistolShootingSprite;
                    isAutomatic = false;
                    break;
                case 2:
                    gunDamage = shotgunDamage;
                    shootFreqOG = shotgunShootFreq;
                    magazineCap = shotgunMagazineCap;
                    loadedBullets = shotgunLoadedBullets;
                    savedBullets = shotgunSavedBullets;
                    shootRange = shotgunShootRange;
                    idleSprite = shotgunSprite;
                    shootingSprite = shotgunShootingSprite;
                    isAutomatic = false;
                    break;
                case 3:
                    gunDamage = ak47Damage;
                    shootFreqOG = ak47ShootFreq;
                    magazineCap = ak47MagazineCap;
                    loadedBullets = ak47LoadedBullets;
                    savedBullets = ak47SavedBullets;
                    shootRange = ak47ShootRange;
                    idleSprite = ak47Sprite;
                    shootingSprite = ak47ShootingSprite;
                    isAutomatic = true;
                    break;
            }
        }
    }
}
