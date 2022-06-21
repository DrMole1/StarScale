using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    // ========================== VARIABLES ==========================

    [Header("Properties")]
    [SerializeField] private float speedAxisX;
    [SerializeField] private float speedAxisY;
    [SerializeField] private float speedAxisZ;

    private bool canTranslate = true;

    // ===============================================================


    private void Update()
    {
        if(canTranslate)
        {
            transform.Translate(speedAxisX * Time.deltaTime, speedAxisY * Time.deltaTime, speedAxisZ * Time.deltaTime);
        }
    }
}
