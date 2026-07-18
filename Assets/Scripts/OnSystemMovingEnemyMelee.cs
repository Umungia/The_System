using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class OnSystemMovingEnemyMelee : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerObject;
 

    [Header("Floats")]
    [SerializeField] private float shootFreq = 5f;
    [SerializeField] private float shootFreqOG;
    [SerializeField] private float stayTime = 5f;
    [SerializeField] private float stayTimeOG;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float maxDist = 5f;
    [SerializeField] private float minDist = -5f;
    [SerializeField] private float visionRange = 5f;
    private Vector3 randomPos, candidatePos;
    

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite IdleSprite, AlertSprite;


    //Wall Detection Raycast
    private Vector3 direction;
    private float distance;

    //Player Detection Raycast
    private Vector3 playerPos;
    private Vector3 playerDirection;

    //Player Chase
    private Vector3 playerChasePos;


    private void Start()
    {
        shootFreqOG = shootFreq;
        stayTimeOG = stayTime;
        randomPos = transform.position;
      
    }
    void Update()
    {
        transform.LookAt(new Vector3(playerObject.position.x,transform.position.y, playerObject.position.z));
   
        PlayerDetection();
    }

    private void Patroll()
    {
     
        stayTime -= Time.deltaTime;
        if (stayTime <= 0 && Vector3.Distance(transform.position, randomPos) < 0.1f)
        {
            candidatePos = new Vector3(transform.position.x + Random.Range(minDist, maxDist), transform.position.y, transform.position.z + Random.Range(minDist, maxDist));
            direction = (candidatePos - transform.position).normalized;
            distance = Vector3.Distance(transform.position, candidatePos);
            Debug.DrawRay(transform.position, direction * distance, Color.red, 2f);
            if (!Physics.Raycast(transform.position, direction, distance))
            {

                stayTime = stayTimeOG;
                randomPos = candidatePos;
            }
          
        }
        transform.position = Vector3.MoveTowards(transform.position, randomPos, speed * Time.deltaTime);

    }

    private void Chase()
    {
        playerChasePos = new Vector3(playerObject.position.x, transform.position.y, playerObject.position.z);
        transform.position = Vector3.MoveTowards(transform.position, playerChasePos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, playerChasePos) < 0.2f)
        {
            
            shootFreq -= Time.deltaTime;
            if (shootFreq <= 0)
            {
                Debug.Log("Player Bitten");
                shootFreq = shootFreqOG;
            }
            
        }
        
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
                Chase();
            }
         
        }
        else
        {
            spriteRenderer.sprite = IdleSprite;
            Patroll();
        }
    }

}
