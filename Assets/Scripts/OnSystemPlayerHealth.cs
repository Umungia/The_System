using UnityEngine;
using TMPro;

public class OnSystemPlayerHealth : MonoBehaviour
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
            playerIsDead = true;
        }
    }
    public void HealPlayer()
    {
        health += 20f;
        if (health > 100f)
        {
            health = 100f;
        }
        healthText.text = "Health: " + health;
    }
}
