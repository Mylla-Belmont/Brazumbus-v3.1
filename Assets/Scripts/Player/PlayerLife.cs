using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public int health;
    private Animator anima;

    public int heart;
    public int maxHeart;
    public int maxHealth;
    private Player player;
    public GameOverScreen gameOverScreen;

    void Start()
    {
        anima = GetComponent<Animator>();
        player = FindAnyObjectByType<Player>();
        heart = PlayerPrefs.GetInt("PlayerHeart", heart);
    }

    public void SaveHeart()
    {
        PlayerPrefs.SetInt("PlayerHeart", heart);
        PlayerPrefs.Save();
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(DelayedDamage(damage));
    }

    IEnumerator DelayedDamage(int damage)
    {
        health -= damage;
        anima.SetBool("Damage", true);
        yield return new WaitForSeconds(0.5f);
        anima.SetBool("Damage", false);

        if (health <= 0)
        {  
            Die();
        }
    }

    void Die()
    {
        anima.SetBool("DeadGun", true);
        anima.SetBool("Run", false);
        anima.SetBool("Jump", false);
        anima.SetBool("Shoot", false);
        anima.SetBool("RunWithGun", false);

        GetComponent<Player>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        if (heart > 1)
        {
            SubtractHeart();
            Invoke(nameof(LoadScene), 1.5f);
        }
        else
        {
            anima.SetBool("DeadGun", true);
            player.collectible = 0;
            player.SaveCollectible();
            SubtractHeart();
            GameOver(); 
        }
    }

    public void SubtractHeart()
    {
        heart -= 1;
        SaveHeart(); 
    }

    void LoadScene()
    {
        if (SceneManager.GetActiveScene().name == "Fase1")
        {
            SceneManager.LoadScene("Fase1");          
        } else 
        if (SceneManager.GetActiveScene().name == "Fase2")
        {
            SceneManager.LoadScene("Fase2");
        } else 
        if (SceneManager.GetActiveScene().name == "Fase3")
        {
            SceneManager.LoadScene("Fase3");
        }
    }

    void GameOver() 
    {
        gameOverScreen.Setup();
    }
}
