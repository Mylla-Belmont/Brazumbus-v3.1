using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Image[] healthIcons;
    public Sprite fullHealth;
    public Sprite emptyHealth;
    public PlayerLife playerLife;

    void Update()
    {
        health = playerLife.health;
        maxHealth = playerLife.maxHealth;
        for (int i = 0; i < healthIcons.Length; i++)
        {
            if (i < health)
            {
                healthIcons[i].sprite = fullHealth;
            }
            else
            {
                healthIcons[i].sprite = emptyHealth;
            }

            if (i < maxHealth)
            {
                healthIcons[i].enabled = true;
            }
            else
            {
                healthIcons[i].enabled = false;
            }
        }
    }
}
