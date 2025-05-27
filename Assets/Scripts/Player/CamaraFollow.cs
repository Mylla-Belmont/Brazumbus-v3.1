using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform player;
    public float minX, maxX;
    public float minY, maxY;
    public float timeLerp;

    private bool isCameraLocked = false;

    private void FixedUpdate() 
    {
        if (!isCameraLocked)
        {
            Vector3 newPosition = player.position + new Vector3(0f, 0f, -10f);
            
            newPosition = Vector3.Lerp(transform.position, newPosition, timeLerp);

            transform.position = new Vector3(
                Math.Clamp(newPosition.x, minX, maxX), 
                Math.Clamp(newPosition.y, minY, maxY), 
                newPosition.z
            );
        }
    }

    public void LockCamera()
    {
        isCameraLocked = true;
    }

    public void UnlockCamera()
    {
        isCameraLocked = false;
    }
}
