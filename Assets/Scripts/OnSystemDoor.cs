using System;
using UnityEngine;

public class OnSystemDoor : MonoBehaviour
{
    [SerializeField] private KeyType reqKey;

    [SerializeField] private bool isLocked = true;
    [SerializeField] private OnSystemKeys keyManager;
    [SerializeField] private GameObject doorObject;
    [SerializeField] private OnSystemInputHandler playerInputHandler;

    [SerializeField] private float doorOpenTime = 5f;
    [SerializeField] private float doorOpenTimeOG;
    [SerializeField] private bool doorIsOpen = false;
    [SerializeField] private bool playerInside = false;

    private void Start()
    {
        doorOpenTimeOG = doorOpenTime;
    }

    private void Update()
    {
        if (doorIsOpen)
        {
            if (isLocked)
            {
                isLocked = false;
                Debug.Log("Door unlocked!");
            }
            if (doorObject.transform.position.y < 7.5 && doorIsOpen == true)
           {
                doorObject.transform.position = Vector3.MoveTowards(doorObject.transform.position, new Vector3(doorObject.transform.position.x, 7.5f, doorObject.transform.position.z), 5f * Time.deltaTime);
           }
          
        }
        if (doorObject.transform.position.y == 7.5)
        {
            doorOpenTime -= Time.deltaTime;
            doorIsOpen = false;
        }
        if (doorOpenTime < 0 && playerInside == false && doorObject.transform.position.y > 2.5f)
        {
            doorObject.transform.position = Vector3.MoveTowards(doorObject.transform.position, new Vector3(doorObject.transform.position.x, 2.5f, doorObject.transform.position.z), 5f * Time.deltaTime);
           
        }
       if(doorObject.transform.position.y == 2.5)
        {
            doorOpenTime = doorOpenTimeOG;
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
                if ((keyManager.hasSimpleKey == true && reqKey == KeyType.Simple) || (keyManager.hasBaseKey == true && reqKey == KeyType.Base) || (keyManager.hasMasterKey == true && reqKey == KeyType.Master))  
                {
                    if(doorIsOpen == false && doorObject.transform.position.y == 2.5f)
                    {
                        doorIsOpen = true;
                    }
                   
                }
                else
                {
                    Debug.Log("You don't have the required key to open this door.");
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

