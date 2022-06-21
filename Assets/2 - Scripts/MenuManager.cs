using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    // ======================= VARIABLES =======================

    [SerializeField] private GameObject btnPlay;
    [SerializeField] private GameObject levelsPanel;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private GameManager gameManager;

    [Header("Introduction")]
    [SerializeField] GameObject introduction;
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private Image[] stars;
    private int state = 0;
    private Coroutine coroutine;

    // =========================================================


    private void Awake()
    {
        if (GameObject.Find("SoundManager") != null) { soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>(); }
    }

    private void Update()
    {
        if((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
        {
            if(coroutine != null && state < 4)
            {
                StopCoroutine(coroutine);
                Color color = new Color(1f, 1f, 1f, 1f);
                texts[state].color = color;
                state++;
                coroutine = StartCoroutine(IShowText());
            }
            else if (state == 4)
            {
                string nameLevel = "1Game";
                SceneManager.LoadScene(nameLevel);
            }
        }
    }

    public void play()
    {
        if(soundManager != null) { soundManager.playAudioClip(0); }

        btnPlay.SetActive(false);
        levelsPanel.SetActive(true);
    }

    public void goToLevel(int _id)
    {
        if (soundManager != null) { soundManager.playAudioClip(0); }

        gameManager.setMaxLevel(_id);

        introduction.SetActive(true);
        levelsPanel.SetActive(false);

        StartCoroutine(IFadeBackground());
    }

    private IEnumerator IFadeBackground()
    {
        float a = 0f;
        Color color = new Color(0f, 0f, 0f, 0);

        while (a < 1f)
        {
            color = new Color(0f, 0f, 0f, a);
            background.color = color;
            a += 0.02f;

            yield return new WaitForSeconds(0.01f);
        }

        coroutine = StartCoroutine(IShowText());
    }

    private IEnumerator IShowText()
    {
        byte a = 0;
        Color32 color = new Color32(255, 255, 255, 0);
        bool check = false;

        while (!check)
        {
            color = new Color32(255, 255, 255, a);
            texts[state].color = color;
            a += 4;
            if(a > 249) { check = true; }

            yield return new WaitForSeconds(0.01f);
        }

        if (state == 4)
        {
            float alpha = 0f;
            Color color2 = new Color(1f, 1f, 1f, 0);

            while (alpha < 1f)
            {
                color2 = new Color(1f, 1f, 1f, alpha);
                stars[0].color = color2;
                stars[1].color = color2;
                stars[2].color = color2;
                alpha += 0.02f;

                yield return new WaitForSeconds(0.01f);
            }
        }

        yield return new WaitForSeconds(8f);

        if(state != 4)
        {
            state++;
            coroutine = StartCoroutine(IShowText());
        }
    }
}
