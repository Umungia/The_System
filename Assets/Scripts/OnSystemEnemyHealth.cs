using UnityEngine;
using TMPro;
public class OnSystemEnemyHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text healthText;

    [Header("Floats")]
    [SerializeField] private float health = 100f;
    public bool isDead = false;

    private void Start()
    {
        healthText.text = "Health: " + health;
    }

    public void HurtEnemy(float damage)
    {
        health -= damage;
        healthText.text = "Health: " + health;
        if (health <= 0)
        {
            isDead = true;
        }
    }
}
