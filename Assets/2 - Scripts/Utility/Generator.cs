using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private const float TIME_DESTROY_OBJECT = 40f;

    // ========================== VARIABLES ==========================

    [SerializeField] private float delay = 15f;

    [Header("Object To Generate")]
    [SerializeField] private GameObject objectPref;

    [Header("Optional")]
    [SerializeField] private Sprite[] sprites;

    // ===============================================================


    private void Start()
    {
        StartCoroutine(ISpawnObject());
    }

    private IEnumerator ISpawnObject()
    {
        GameObject obj;
        obj = Instantiate(objectPref, transform.position, Quaternion.identity, gameObject.transform);

        if(sprites.Length != 0) { getSpriteRenderer(obj); }

        Destroy(obj, TIME_DESTROY_OBJECT);

        yield return new WaitForSeconds(delay);

        StartCoroutine(ISpawnObject());
    }

    private void getSpriteRenderer(GameObject _obj)
    {
        SpriteRenderer sprRd;

        if (_obj.GetComponent<SpriteRenderer>() != null)
        {
            sprRd = _obj.GetComponent<SpriteRenderer>();

            changeSprite(sprRd);
        }
        else if (_obj.transform.GetChild(0).GetComponent<SpriteRenderer>() != null)
        {
            sprRd = _obj.transform.GetChild(0).GetComponent<SpriteRenderer>();

            changeSprite(sprRd);
        }
        else
        {
            print("No Sprite Renderer attached to the object or first child of the object.");
        }
    }

    private void changeSprite(SpriteRenderer _sprRd)
    {
        int choice = Random.Range(0, sprites.Length);
        Sprite choosenSprite = sprites[choice];

        _sprRd.sprite = choosenSprite;
    }
}
