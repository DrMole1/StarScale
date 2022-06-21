using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteStar : MonoBehaviour
{
    // ======================= VARIABLES =======================

    public bool canMove = false;
    public bool canCatch = true;
    public LayerMask hitLayers;
    public Slider slider;

    private float speed = 0.3f;

    // =========================================================


    private void Update()
    {
        if(canMove) { move(); }
    }

    private void move()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newPos = new Vector3(worldPosition.x, worldPosition.y, 0f);
        Vector3 dir = newPos - transform.position;
        transform.Translate(dir * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Energy") && canCatch)
        {
            slider.value += 0.015f;
            Destroy(col.gameObject);
        }
    }
}
