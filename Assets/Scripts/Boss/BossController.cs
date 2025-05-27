using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Camera mainCamera; 
    private Animator animaBoss;
    public Transform bossTransform;
    public Transform playerTransform; 
    public float cameraFocusDuration; 
    private bool isCameraFocusedOnBoss = false;

    private bool isWalking = false;
    public int health;
    public float moveSpeed;
    public float rightLimit;
    public float leftLimit;

    public float punchInterval;
    public float walkDuration;

    private void Start()
    {
        animaBoss = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isWalking)
        {
            WalkBoss();
        }
    }

    public void Transform()
    {
        animaBoss.SetBool("Transformation", true);
        FindObjectOfType<CamaraFollow>().LockCamera();  
        StartCoroutine(FocusCameraOnBoss());
    }

    IEnumerator FocusCameraOnBoss()
    {
        isCameraFocusedOnBoss = true;

        float cameraYOffset = 10f; 
        Vector3 originalCameraPosition = mainCamera.transform.position;
        Vector3 bossCameraPosition = new Vector3(
            bossTransform.position.x, 
            bossTransform.position.y + cameraYOffset, 
            originalCameraPosition.z
        );

        float elapsedTime = 0f;
        while (elapsedTime < cameraFocusDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(originalCameraPosition, bossCameraPosition, elapsedTime / cameraFocusDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        mainCamera.transform.position = bossCameraPosition;
        yield return new WaitForSeconds(cameraFocusDuration);

        elapsedTime = 0f;
        Vector3 playerCameraPosition = new Vector3(
            playerTransform.position.x, 
            playerTransform.position.y, 
            originalCameraPosition.z
        );

        while (elapsedTime < cameraFocusDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(bossCameraPosition, playerCameraPosition, elapsedTime / cameraFocusDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = playerCameraPosition;
        FindObjectOfType<CamaraFollow>().UnlockCamera(); 
        isCameraFocusedOnBoss = false;
        StartCoroutine(WaitForTransformationToEnd());
    }

    IEnumerator WaitForTransformationToEnd()
    {
        yield return new WaitForSeconds(2f);

        animaBoss.SetBool("Transformation", false);
        animaBoss.SetBool("Walk", true);

        isWalking = true; 
        StartCoroutine(WalkAndPunchRoutine());
    }

    private void WalkBoss()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        if (transform.position.x >= rightLimit)
        {
            moveSpeed *= -1; 
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (transform.position.x <= leftLimit)
        {
            moveSpeed *= -1; 
            transform.eulerAngles = Vector3.zero;
        }
    }

    IEnumerator WalkAndPunchRoutine()
    {
        while (true)
        {
            animaBoss.SetBool("Punch", true); 
            yield return new WaitForSeconds(1f);
            animaBoss.SetBool("Punch", false); 

            yield return new WaitForSeconds(punchInterval);
        }
    }

    public void TakeDemage (int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.GetComponent<Animator>().SetBool("Die", true);
        gameObject.GetComponent<Animator>().SetBool("Walk", false);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        gameObject.GetComponent<BossController>().enabled = false;
        Destroy(gameObject, 2.5f);
    }
}
