using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] backgroundMusics;
    private AudioClip backgroundMusicsFase;

    private static AudioController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        UpdateMusicForScene(SceneManager.GetActiveScene().name);
        audioSource.clip = backgroundMusicsFase;
        audioSource.Play();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateMusicForScene(scene.name);
        audioSource.clip = backgroundMusicsFase;
        if (!audioSource.isPlaying) 
        {
            audioSource.Play();
        }
    }

    private void UpdateMusicForScene(string sceneName)
    {
        if (sceneName == "Menu")
        {
            backgroundMusicsFase = backgroundMusics[0];
        }   
        else if (sceneName == "Fase1")
        {
            backgroundMusicsFase = backgroundMusics[1];
        } else if (sceneName == "Fase2")
        {
            backgroundMusicsFase = backgroundMusics[2];
        } else if (sceneName == "Fase3")
        {
            backgroundMusicsFase = backgroundMusics[3];
        }
    }
}