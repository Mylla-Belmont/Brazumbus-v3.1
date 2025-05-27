using UnityEngine;
using UnityEngine.UI;

public class CollectibleDisplay : MonoBehaviour
{
    public int collectible;
    public int maxCollectible;
    public Image[] collectibleIcons;
    public Sprite fullCollectible;
    public Sprite emptyCollectible;
    public Player player;

    void Update()
    {
        collectible = player.collectible;
        maxCollectible = player.maxCollectible;
        for (int i = 0; i < collectibleIcons.Length; i++)
        {
            if (i < collectible)
            {
                collectibleIcons[i].sprite = fullCollectible;
            }
            else
            {
                collectibleIcons[i].sprite = emptyCollectible;
            }

            if (i < maxCollectible)
            {
                collectibleIcons[i].enabled = true;
            }
            else
            {
                collectibleIcons[i].enabled = false;
            }
        }
    }
}
