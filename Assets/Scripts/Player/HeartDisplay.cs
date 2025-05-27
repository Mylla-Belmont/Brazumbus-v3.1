using UnityEngine;
using UnityEngine.UI;

public class HeartDisplay : MonoBehaviour
{
    public int heart;
    public int maxHeart;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public PlayerLife playerLife;

    void Update()
    {
        heart = playerLife.heart;
        maxHeart = playerLife.maxHeart;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < heart)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < maxHeart)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
