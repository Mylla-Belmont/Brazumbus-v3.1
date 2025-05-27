using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator anima;
    public bool isShooting;
    public Transform firePoint;
    public GameObject bulletPrefab;
    void Start()
    {
        anima = GetComponent<Animator>();
    } 

    IEnumerator ActivatePowerUpAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if(isShooting) {
            anima.SetBool("Shoot", false); 
            isShooting = false; 
        }
    }
    public void Shoot()
    {
        isShooting = true;
        anima.SetBool("Shoot", true);  
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine(ActivatePowerUpAfterDelay());
    }
}
