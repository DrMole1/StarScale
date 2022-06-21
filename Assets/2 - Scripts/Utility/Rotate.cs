using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // ========================== VARIABLES ==========================

    [Header("Properties")]
    public float xAngle;
    public float yAngle;
    public float zAngle;

    private bool canRotate = true;

    // ===============================================================

    public void Update()
    {
        if (canRotate) { RotateObject(); }
    }

    private void RotateObject()
    {
        transform.Rotate(xAngle * Time.deltaTime, yAngle * Time.deltaTime, zAngle * Time.deltaTime, Space.Self);
    }
}
