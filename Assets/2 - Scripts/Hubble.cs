using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Hubble : MonoBehaviour
{
    // ======================= VARIABLES =======================

    [SerializeField] private Transform star;
    [SerializeField] private GameObject colorPanel;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject btnStart;
    [SerializeField] private GameObject btnValidate;

    [Header("Results")]
    [SerializeField] private GameObject result;
    [SerializeField] private TextMeshProUGUI distancePlayerText;
    [SerializeField] private TextMeshProUGUI distanceStarText;
    [SerializeField] private TextMeshProUGUI error;
    [SerializeField] private TextMeshProUGUI totalMarginErrorText;
    [SerializeField] private Image blackCurtain;

    private float distanceReal = 0;
    private float distanceStar = 0;
    private float distancePlayer = 0f;

    private bool canNext = true;
    private bool canValidate = true;

    private float redIntensity = 0f;

    private SoundManager soundManager;

    // =========================================================

    private void Awake()
    {
        if (GameObject.Find("SoundManager") != null) { soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>(); }
    }

    public void startGame()
    {
        if (soundManager != null) { soundManager.playAudioClip(0); }

        distanceStar = Random.Range(100000000f, 100000000000f);
        redIntensity = Random.Range(0f, 0.75f);
        Color color = new Color(1f, redIntensity, redIntensity);
        star.GetComponent<SpriteRenderer>().color = color;

        btnStart.SetActive(false);

        StartCoroutine(ITranslateStar());
    }

    private IEnumerator ITranslateStar()
    {
        while(star.position.x < 12)
        {
            star.position = new Vector2(star.position.x + 0.1f, star.position.y);

            yield return new WaitForSeconds(0.01f);
        }

        btnValidate.SetActive(true);
        colorPanel.SetActive(true);
    }

    public void validateDistance()
    {
        if (!canValidate) { return; }

        if (soundManager != null) { soundManager.playAudioClip(0); }

        canValidate = false;
        result.SetActive(true);

        print(redIntensity + " / " + slider.value);

        distanceStarText.text = distanceStar.ToString("n0") + " a.l.";

        float spaceBetween = Mathf.Abs(redIntensity - slider.value);

        float distanceRatio = Mathf.Ceil((1 - spaceBetween) * distanceStar);

        float marginError = 0f;

        distancePlayerText.text = distanceRatio.ToString("n0") + " a.l.";

        marginError = Mathf.Abs(distanceRatio / distanceStar);
        marginError = (Mathf.Abs(((Mathf.Round(marginError * 100)) / 100) - 1f) * 100);
        error.text = marginError + " %";

        float totalMarginError = 0f;
        if (GameObject.Find("GameManager") != null) { totalMarginError = GameObject.Find("GameManager").GetComponent<GameManager>().getErrorMargin() + marginError; }
        totalMarginErrorText.text = totalMarginError + " %";

        if (GameObject.Find("GameManager") != null)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().setErrorMargin(totalMarginError);
        }
    }

    public void nextMiniGame()
    {
        if (!canNext) { return; }

        if (soundManager != null) { soundManager.playAudioClip(0); }

        canNext = false;
        StartCoroutine(IFadeCurtain());
    }

    private IEnumerator IFadeCurtain()
    {
        float a = 0f;
        Color color = new Color(0f, 0f, 0f, a);

        while (a < 1f)
        {
            color = new Color(0f, 0f, 0f, a);
            blackCurtain.color = color;
            a += 0.02f;

            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene("Menu");
    }
}
