using UnityEngine;
using UnityEngine.SceneManagement;

public class FireObstacle : MonoBehaviour
{
    private Animator anima;
    public GameOverScreen gameOverScreen;
    private PlayerLife playerLife;
    public Player player;
    void Start()
    {
        anima = GetComponent<Animator>();
        playerLife = GetComponent<PlayerLife>();
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        playerLife = collision.gameObject.GetComponent<PlayerLife>();
        if (playerLife != null)
        {
            Vector3 adjustedPosition = collision.gameObject.transform.position;
            adjustedPosition.y += 1.5f; 
            collision.gameObject.transform.position = adjustedPosition;

            Animator playerAnimator = collision.gameObject.GetComponent<Animator>();
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            BoxCollider2D playerCollider = collision.gameObject.GetComponent<BoxCollider2D>();
            Player playerScript = collision.gameObject.GetComponent<Player>();

            playerAnimator.SetTrigger("DeadFire");
            playerRigidbody.velocity = Vector2.zero;
            playerCollider.enabled = false;
            playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
            playerScript.enabled = false;
            playerAnimator.SetBool("Jump", false);
            playerLife.SubtractHeart();
            Invoke(nameof(LoadScene), 1.5f);

            if (playerLife.heart <= 1)
            {
                playerLife.SubtractHeart();
                player.collectible = 0;
                player.SaveCollectible();
                GameOver();
            }
        }
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
