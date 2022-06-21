using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTitle : MonoBehaviour
{
    private const float DELAY_NEXT_ANIM = 4f;
    private const float DELAY_NEXT_LETTER = 0.2f;
    private const float DELAY_GROW = 0.005f;
    private float MINSCALE = 1f;
    private float MAXSCALE = 1.4f;

    // ===================== VARIABLES =====================

    [SerializeField] private Transform[] letters;

    // =====================================================


    private void Start()
    {
        StartCoroutine(IAnimateLetters());
    }

    private IEnumerator IAnimateLetters()
    {
        int cpt = 0;

        while (cpt < letters.Length)
        {
            StartCoroutine(IGrowLetter(letters[cpt]));

            yield return new WaitForSeconds(DELAY_NEXT_LETTER);

            cpt++;
        }

        yield return new WaitForSeconds(DELAY_NEXT_ANIM);

        StartCoroutine(IAnimateLetters());
    }

    private IEnumerator IGrowLetter(Transform _letter)
    {
        while (_letter.localScale.x < MAXSCALE)
        {
            _letter.localScale = new Vector2(_letter.localScale.x + 0.02f, _letter.localScale.y + 0.02f);

            yield return new WaitForSeconds(DELAY_GROW);
        }

        while (_letter.localScale.x > MINSCALE)
        {
            _letter.localScale = new Vector2(_letter.localScale.x - 0.015f, _letter.localScale.y - 0.015f);

            yield return new WaitForSeconds(DELAY_GROW);
        }
    }
}
