using UnityEngine;

public class OnSystemStaticEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerObject;


    void Update()
    {
        transform.LookAt(new Vector3(playerObject.position.x,transform.position.y, playerObject.position.z));

    }
}
