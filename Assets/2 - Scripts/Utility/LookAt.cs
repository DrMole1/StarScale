using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    // =============== VARIABLES ===============

    [Header("To Drag from Scene")]
    public Transform player;

    private float yRotRod;
    private Vector3 currentRotRod;
    private Quaternion currentQuaternionRotRod;

    // =========================================


    public void Update()
    {
        LookAtPlayer();
    }

    /// <summary>
    /// The sunflower looks at the player every frames
    /// </summary>
    public void LookAtPlayer()
    {
        transform.LookAt(player);

        // We block X axis to 0
        yRotRod = transform.localEulerAngles.y;
        currentRotRod = new Vector3(0, yRotRod, 0);
        currentQuaternionRotRod.eulerAngles = currentRotRod;
        transform.localRotation = currentQuaternionRotRod;
    }
}
