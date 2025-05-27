using System.Collections;
using UnityEngine;

public class ZumbiBasic : MonoBehaviour
{
    public int health;
    private Transform positionPlayer;
    public float speedZumbi;
    private bool isDead = false;
    private bool isTakingDamage = false;
    public GameObject armZumbi;
    public WeaponZumbi weaponZumbi;
    private float shootCooldown = 2f;  
    private float nextShootTime = 3f; 

    void Start()
    {
        weaponZumbi = GetComponent<WeaponZumbi>();
        positionPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        armZumbi.SetActive(true);
    }

    void Update()
    {
        if (!isDead && !isTakingDamage)
        {
            FollowPlayer();
            
            if (Time.time >= nextShootTime)
            {
                Shoot();
                nextShootTime = Time.time + shootCooldown;  
            }
        }
    }

    public void TakeDemage(int damage)
    {
        StartCoroutine(DelayedDamage(damage)); 
    }

    IEnumerator DelayedDamage(int damage)
    {
        isTakingDamage = true;
        armZumbi.SetActive(false);
        health -= damage;
        gameObject.GetComponent<Animator>().SetBool("Damage", true);

        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<Animator>().SetBool("Damage", false);
        isTakingDamage = false;
        armZumbi.SetActive(true);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        gameObject.GetComponent<Animator>().SetBool("Died", true);
        gameObject.GetComponent<Animator>().SetBool("Walk", false);
        armZumbi.SetActive(false);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        gameObject.GetComponent<ZumbiBasic>().enabled = false;
        Destroy(gameObject, 2.5f);
    }

    private void FollowPlayer()
    {
        if (positionPlayer.gameObject != null)
        {
            armZumbi.SetActive(true);
            gameObject.GetComponent<Animator>().SetBool("Walk", true);
            transform.position = Vector2.MoveTowards(transform.position, positionPlayer.position, speedZumbi * Time.deltaTime);

            if (positionPlayer.position.x < transform.position.x)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
    }

    public void Shoot()
    {
        weaponZumbi.Shoot();
    }
}
