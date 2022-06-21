using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplicationManager : MonoBehaviour
{
    // ======================= VARIABLES =======================

    [SerializeField] private Image blackCurtain;
    [SerializeField] private GameObject[] objectToDesactive;
    
    private SoundManager soundManager;

    // =========================================================



    private void Start()
    {
        if (GameObject.Find("SoundManager") != null) { soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>(); }

        StartCoroutine(IFadeBackground());
    }

    private IEnumerator IFadeBackground()
    {
        float a = 1f;
        Color color = new Color(0f, 0f, 0f, 1);

        while (a > 0f)
        {
            color = new Color(0f, 0f, 0f, a);
            blackCurtain.color = color;
            a -= 0.02f;

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void exitExplication()
    {
        if (soundManager != null) { soundManager.playAudioClip(0); }


        for (int i = 0; i < objectToDesactive.Length; i++)
        {
            objectToDesactive[i].SetActive(false);
        }
    }
}
