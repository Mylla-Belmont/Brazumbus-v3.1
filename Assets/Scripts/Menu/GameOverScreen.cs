using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject canvas;
    public void Setup()
    {
        PlayerPrefs.SetInt("PlayerHeart", 5);
        PlayerPrefs.SetInt("PlayerCollectible", 0);
        PlayerPrefs.Save();
        canvas.SetActive(false);
        gameObject.SetActive(true);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Fase1");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
