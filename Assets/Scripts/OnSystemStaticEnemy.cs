using UnityEngine;

public class OnSystemStaticEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerObject;
    [SerializeField] private OnSystemPlayerHealth playerHealth;
    [SerializeField] private OnSystemEnemyHealth enemyHealth;

    [Header("Floats")]
    [SerializeField] private float shootFreq = 5f;
    private float shootFreqOG;
    [SerializeField] private float visionRange = 5f;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite IdleSprite, AlertSprite, deathSprite;

    //Player Detection Raycast
    private Vector3 playerPos;
    private Vector3 playerDirection;

    private void Start()
    {
        shootFreqOG = shootFreq;
    }

        void Update()
    {
        transform.LookAt(new Vector3(playerObject.position.x,transform.position.y, playerObject.position.z));
        PlayerDeath();
        if (!playerHealth.playerIsDead && !enemyHealth.isDead)
        {
            PlayerDetection();
        }
        Death();
    }

    private void PlayerDetection()
    {
        playerPos = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z);
        playerDirection = (playerPos - transform.position).normalized;
        if (Physics.Raycast(transform.position, playerDirection, out RaycastHit hit, visionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                spriteRenderer.sprite = AlertSprite;
                Shooting();
            }

        }
        else
        {
            spriteRenderer.sprite = IdleSprite;
        }
    }
    private void Shooting()
    {
        shootFreq -= Time.deltaTime;
        if (shootFreq <= 0)
        {
            if (Random.value <= 0.5)
            {
                Debug.Log("Enemy Shooting");
                playerHealth.TakeDamage(Random.Range(5,15));
            }
            else
            {
                Debug.Log("Shot Missed");
            }
             shootFreq = shootFreqOG;
        }
    }
    private void PlayerDeath()
    {
        if (playerHealth.playerIsDead)
        {
            
            if (spriteRenderer.sprite != IdleSprite)
            {
                spriteRenderer.sprite = IdleSprite;
            }
            visionRange = 0f;

        }
    }

    private void Death()
    {
        if (enemyHealth.isDead)
        {
            spriteRenderer.sprite = deathSprite;
        }
    }
}
