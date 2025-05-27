using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManage : MonoBehaviour
{
    public string levelName;
    public void Play()
    {
        SceneManager.LoadScene(levelName);
    }

    public void Exit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
