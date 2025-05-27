using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeScript : MonoBehaviour
{
    void Start() {
        if (SceneManager.GetActiveScene().name == "Fase1")
        {
            if (PlayerPrefs.GetInt("HeartCollected") == 1)
            {
                gameObject.SetActive(false);
            }
        }
        if (SceneManager.GetActiveScene().name == "Fase2")
        {
            if (PlayerPrefs.GetInt("HeartCollected") == 2)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
