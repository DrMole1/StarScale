using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    // ========================== VARIABLES ==========================

    [Header("Properties")]
    [SerializeField] private float delay;

    // ===============================================================

    void Start()
    {
        Destroy(gameObject, delay);
    }
}