using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CallNewScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay(25f));
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Fase1");
    }
}
