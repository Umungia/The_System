using UnityEngine;
using UnityEngine.UI;

public class OnSystemKeys : MonoBehaviour
{
    public bool hasSimpleKey = false;
    public bool hasBaseKey = false;
    public bool hasMasterKey = false;

    [SerializeField] private Image simpleKeyUI;
    [SerializeField] private Image baseKeyUI;
    [SerializeField] private Image masterKeyUI;


    void Start()
    {
        simpleKeyUI.gameObject.SetActive(false);
        baseKeyUI.gameObject.SetActive(false);
        masterKeyUI.gameObject.SetActive(false);
    }

   
    void Update()
    {
        
    }

    public void GetKey(KeyType keyType)
    {
        if(keyType == KeyType.Simple)
        {
            hasSimpleKey = true;
            simpleKeyUI.gameObject.SetActive(true);
        }
        if(keyType == KeyType.Base)
        {
            hasBaseKey = true;
            baseKeyUI.gameObject.SetActive(true);       
        }
        if(keyType == KeyType.Master)
        {
            hasMasterKey = true;
            masterKeyUI.gameObject.SetActive(true);
        }
    }
}
