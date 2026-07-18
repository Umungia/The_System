using UnityEngine;

public class OnSystemStaticEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerObject;
    [SerializeField] private float visionRange = 5f;

    [Header("Floats")]
    [SerializeField] private float shootFreq = 5f;
    private float shootFreqOG;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite IdleSprite, AlertSprite;

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
        PlayerDetection();
    }

    private void PlayerDetection()
    {
        playerPos = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z);
        playerDirection = (playerPos - transform.position).normalized;
        Debug.DrawRay(transform.position, playerDirection * visionRange, Color.green, 2f);
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
        
                Debug.Log("Enemy Shooting");
            shootFreq = shootFreqOG;
        }
    }
}
