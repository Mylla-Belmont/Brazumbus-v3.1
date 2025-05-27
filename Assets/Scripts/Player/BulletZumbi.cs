using UnityEngine;
using System.Collections;

public class BulletZumbi : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage;

    void Start()
    {
        rb.velocity = transform.right * speed;
        StartCoroutine(DestroyAfterTime());
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerLife playerLife = hitInfo.GetComponent<PlayerLife>();
        if (playerLife != null)
        {
            playerLife.TakeDamage(damage); 
        }
        Destroy(gameObject);
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}