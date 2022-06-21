using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

public class Cepheides : MonoBehaviour
{
    // ======================= VARIABLES =======================

    [SerializeField] private Slider slider;
    [SerializeField] private Light2D lightStar;
    [SerializeField] private Light2D lightStarPlayer;

    [Header("Results")]
    [SerializeField] private GameObject result;
    [SerializeField] private TextMeshProUGUI distancePlayerText;
    [SerializeField] private TextMeshProUGUI distanceStarText;
    [SerializeField] private TextMeshProUGUI error;
    [SerializeField] private TextMeshProUGUI totalMarginErrorText;
    [SerializeField] private Image blackCurtain;

    private float realIntensity = 0f;
    private float playerIntensity = 0f;

    private int distanceStar = 0;

    private bool canNext = true;
    private bool canValidate = true;

    private SoundManager soundManager;

    // =========================================================


    private void Awake()
    {
        if (GameObject.Find("SoundManager") != null) { soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>(); }
    }

    private void Start()
    {
        realIntensity = Random.Range(1f, 50f);
        lightStar.intensity = realIntensity;

        distanceStar = Random.Range(100000, 1000000000);
    }

    public void changeIntensity()
    {
        playerIntensity = slider.value * 50f;
        lightStarPlayer.intensity = playerIntensity;
    }

    public void validateDistance()
    {
        if (!canValidate) { return; }

        if (soundManager != null) { soundManager.playAudioClip(0); }

        canValidate = false;
        result.SetActive(true);

        distanceStarText.text = distanceStar.ToString("n0") + " a.l.";

        float distanceRatio = Mathf.Ceil(Mathf.Abs(playerIntensity / realIntensity) * distanceStar);

        distancePlayerText.text = distanceRatio.ToString("n0") + " a.l.";

        float marginError = Mathf.Abs(distanceRatio / distanceStar);
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

        if (GameObject.Find("GameManager").GetComponent<GameManager>().getMaxLevel() == 3)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            SceneManager.LoadScene("4Game");
        }
    }
}
