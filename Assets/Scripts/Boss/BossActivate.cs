using UnityEngine;

public class BossActivate : MonoBehaviour
{
    public BossController bossController; 
    void Start()
    {
        bossController = FindObjectOfType<BossController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossController.Transform();
            Destroy(gameObject);
        }
    }
}
