using UnityEngine;

public class OnSystemStaticEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerObject;


    [Header("Floats")]
    [SerializeField] private float shootFreq = 5f;
    private float shootFreqOG;
    private void Start()
    {
        shootFreqOG = shootFreq;
    }

        void Update()
    {
        transform.LookAt(new Vector3(playerObject.position.x,transform.position.y, playerObject.position.z));
        Shooting();
    }

    private void Shooting()
    {
        shootFreq -= Time.deltaTime;
        if (shootFreq <= 0)
        {
            if (Random.value <= 0.5)
            {
                Debug.Log("Enemy Shooting");
            }
            else
            {
                Debug.Log("Shooting unsuccessful");
            }
            shootFreq = shootFreqOG;
        }
    }
}
