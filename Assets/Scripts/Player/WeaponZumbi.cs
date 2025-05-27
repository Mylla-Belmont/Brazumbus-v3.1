using System.Collections;
using UnityEngine;

public class WeaponZumbi : MonoBehaviour
{
    public bool isShooting;
    public Transform firePointZumbi;
    public GameObject bulletPrefabZumbi;
    private Transform positionPlayer;
    public GameObject armZumbi;
    private Animator animaArmZumbi;

    void Start()
    {
        animaArmZumbi = armZumbi.GetComponent<Animator>(); 
        positionPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Shoot()
    {
        animaArmZumbi.SetBool("Shoot", true);

        Vector3 playerPositionAdjusted = new Vector3(positionPlayer.position.x, positionPlayer.position.y + 1f, positionPlayer.position.z);
        Vector2 direction = (playerPositionAdjusted - firePointZumbi.position).normalized;
        float angle = direction.x > 0 ? 0f : 180f;

        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        Instantiate(bulletPrefabZumbi, firePointZumbi.position, rotation);
        StartCoroutine(DisableShootAnimation());
    }

    IEnumerator DisableShootAnimation()
    {
        yield return new WaitForSeconds(1f);  
        animaArmZumbi.SetBool("Shoot", false);  
    }
}