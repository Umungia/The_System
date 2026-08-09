using UnityEngine;
using TMPro;
using UnityEngine.Animations;

public class OnSystemDoor : MonoBehaviour
{
    [SerializeField] private KeyType reqKey;

    [SerializeField] private bool isLocked = true;
    [SerializeField] private OnSystemKeys keyManager;
    [SerializeField] private GameObject doorObject;
    [SerializeField] private OnSystemInputHandler playerInputHandler;
    [SerializeField] private TMP_Text doorStateText;

    [SerializeField] private float doorOpenTime = 5f;
    [SerializeField] private float doorOpenTimeOG;
    [SerializeField] private bool doorIsOpen = false;
    [SerializeField] private bool playerInside = false;
    private BoxCollider boxCollider;
    private bool noKey = false;
    private bool doorHasOpened = false;
    [SerializeField] private float textTimeOnScreen = 1f;
    private float textTimeOnScreenOG;

    private void Start()
    {
        doorOpenTimeOG = doorOpenTime;
        textTimeOnScreenOG = textTimeOnScreen;
        doorStateText.gameObject.SetActive(false);
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (doorIsOpen)
        {
            
            if (isLocked)
            {
                isLocked = false;
                doorHasOpened = true;
            }
            if (doorObject.transform.position.y < 5)
            {
                doorObject.transform.position = Vector3.MoveTowards(doorObject.transform.position, new Vector3(doorObject.transform.position.x, 5f, doorObject.transform.position.z), 5f * Time.deltaTime);
            }
          
        }
        if (doorObject.transform.position.y == 5)
        {
            doorOpenTime -= Time.deltaTime;
            doorIsOpen = false;
        }
        if (doorOpenTime < 0 && doorObject.transform.position.y > 0f)
        {
            boxCollider.enabled = true;
            if (playerInside)
            {

                doorObject.transform.position = Vector3.MoveTowards(doorObject.transform.position, new Vector3(doorObject.transform.position.x, 0f, doorObject.transform.position.z), 5f * Time.deltaTime);


            }
        }
     
       if(doorObject.transform.position.y == 0)
        {
            doorOpenTime = doorOpenTimeOG;
        }
        if(noKey)
        {
            doorStateText.text = ("You don't have the required key to open this door.");
            doorStateText.gameObject.SetActive(true);     
            textTimeOnScreen -= Time.deltaTime;
            if(textTimeOnScreen <= 0)
            {
                doorStateText.gameObject.SetActive(false);
                noKey = false;
                textTimeOnScreen = textTimeOnScreenOG;
            }
            
        }
        if (doorHasOpened)
        {
            doorStateText.text = ("Door Unlocked!");
            doorStateText.gameObject.SetActive(true);
            textTimeOnScreen -= Time.deltaTime;
            if (textTimeOnScreen <= 0)
            {
                doorStateText.gameObject.SetActive(false);
                doorHasOpened = false;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(playerInputHandler.interactionTriggered)
            {
                if ((keyManager.hasSimpleKey == true && reqKey == KeyType.Simple) || (keyManager.hasBaseKey == true && reqKey == KeyType.Base) || (keyManager.hasMasterKey == true && reqKey == KeyType.Master) || (reqKey == KeyType.None))  
                {
                    if(doorIsOpen == false && doorObject.transform.position.y == 0f)
                    {
                        boxCollider.enabled = false;
                        doorIsOpen = true;
                    }
                   
                }
                else
                {
                    noKey = true;
                }
            }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
    
        }
    }
}

