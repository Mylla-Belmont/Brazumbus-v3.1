using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Weapon weapon;
    private Animator anima;
    private Rigidbody2D Rig;

    public GameObject canvas;

    public float Speed;
    public bool isJumping;
    public float JumpForce;
    public bool doubleJump;
    public bool isTakingGun;
    private bool canMove = true;
    private int maxLife;

    public int collectible;
    public int maxCollectible;

    public GameOverScreen gameOverScreen;
    private PlayerLife playerLife;


    void Start()
    {
        weapon = FindObjectOfType<Weapon>();
        anima = GetComponent<Animator>();
        Rig = GetComponent<Rigidbody2D>();
        playerLife = FindObjectOfType<PlayerLife>(); 
        collectible = PlayerPrefs.GetInt("PlayerCollectible", collectible);
        maxLife = PlayerPrefs.GetInt("HeartCollected", maxLife);
    }

    void Update()
    {
        if (canMove && !weapon.isShooting)
        {
            Move();
            Jump();
        }

        if (isTakingGun && Input.GetKeyDown(KeyCode.J))
        {
            weapon.Shoot();
        }
    }

    public void Move()
    {
        Vector3 moviment = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += Speed * Time.deltaTime * moviment;

        if (canMove)
        {
            if (Input.GetAxis("Horizontal") > 0f)
            {
                anima.SetBool(isTakingGun ? "RunWithGun" : "Run", true);
                transform.eulerAngles = Vector3.zero;
            }
            else if (Input.GetAxis("Horizontal") < 0f)
            {
                anima.SetBool(isTakingGun ? "RunWithGun" : "Run", true);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            else
            {
                anima.SetBool(isTakingGun ? "RunWithGun" : "Run", false);
                anima.SetBool("IdleWithGun", isTakingGun ? true : false);
            }
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!isJumping)
            {
                Rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anima.SetBool("Jump", true);
            }
            else
            {
                if (doubleJump)
                {
                    Rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anima.SetBool("Jump", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            maxLife = 1;
            Destroy(collision.gameObject);
            if (playerLife.heart == 5)
            {
                playerLife.heart = 5;
            } else 
            {
                playerLife.heart += 1;
            }
            PlayerPrefs.SetInt("HeartCollected", maxLife);
            PlayerPrefs.Save();
        }
        if (collision.gameObject.layer == 9)
        {
            canMove = false;
            Destroy(collision.gameObject);
            anima.SetBool("Take", true);
            StartCoroutine(ActivatePowerUpAfterDelay());
        }
        if (collision.gameObject.layer == 10)
        {
            LoadSceneGame();
        }
        if (collision.gameObject.layer == 12)
        {
            if (SceneManager.GetActiveScene().name == "Fase1" && collectible == 0)
            {
                collectible = 1;
                Destroy(collision.gameObject);
                SaveCollectible();
            }
            else
            if (SceneManager.GetActiveScene().name == "Fase2" && collectible == 1)
            {
                collectible = 2;
                Destroy(collision.gameObject);
                SaveCollectible();
            }
            else
            if (SceneManager.GetActiveScene().name == "Fase3" && collectible == 2)
            {
                collectible = 3;
                Destroy(collision.gameObject);
                SaveCollectible();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

    IEnumerator ActivatePowerUpAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        canMove = true;
        isTakingGun = true;
    }

    IEnumerator ShowQuestReminderDelay()
    {
        yield return new WaitForSeconds(2f);
        canvas.SetActive(false);
    }

    public void SaveCollectible()
    {
        PlayerPrefs.SetInt("PlayerCollectible", collectible);
        PlayerPrefs.Save();
    }

    void LoadSceneGame()
    {
        if (SceneManager.GetActiveScene().name == "Fase1")
        {
            if (collectible == 1)
            {
                SceneManager.LoadScene("Fase2");
            } else {
                canvas.SetActive(true);
                StartCoroutine(ShowQuestReminderDelay());
            }
        } else 
        if (SceneManager.GetActiveScene().name == "Fase2")
        {
            if (collectible == 2)
            {
                SceneManager.LoadScene("Fase3");
            } else {
                canvas.SetActive(true);
                StartCoroutine(ShowQuestReminderDelay());
            }
        } else
        if (SceneManager.GetActiveScene().name == "Fase3")
        {
            if (collectible >= 3)
            {
                gameOverScreen.Setup();
            } else {
                canvas.SetActive(true);
                StartCoroutine(ShowQuestReminderDelay());
            }
        }
    }
}
