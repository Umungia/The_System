using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text healthText;

    [Header("Floats")]
    [SerializeField] private float health = 100f;
    public bool playerIsDead = false;


    public void TakeDamage(float damage)
    {
        health -= damage;
        healthText.text = "Health: " + health;
        if (health <= 0)
        {
            Debug.Log("Player is dead");
            playerIsDead = true;
        }
    }
}
