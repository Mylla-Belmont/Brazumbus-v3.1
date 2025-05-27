using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 50;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Destroi bala quando atinge inimigo
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        ZumbiBasic zumbiBasic = hitInfo.GetComponent<ZumbiBasic>();
        if (zumbiBasic != null)
        {   
            zumbiBasic.TakeDemage(damage);
        } else 
        {
            BossController boss = hitInfo.GetComponent<BossController>();
            if (boss != null)
            {
                boss.TakeDemage(damage);
            }
        }
        Destroy(gameObject);
    }
}
