using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Parallaxe : MonoBehaviour
{
    // ======================= VARIABLES =======================

    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private Transform telescope01;
    [SerializeField] private Transform telescope02;
    [SerializeField] private Slider slider01;
    [SerializeField] private Slider slider02;
    [SerializeField] private Transform star;

    [Header("Results")]
    [SerializeField] private GameObject result;
    [SerializeField] private TextMeshProUGUI distancePlayerText;
    [SerializeField] private TextMeshProUGUI distanceStarText;
    [SerializeField] private TextMeshProUGUI error;
    [SerializeField] private TextMeshProUGUI totalMarginErrorText;
    [SerializeField] private Image blackCurtain;

    private int distanceReal = 0;
    private int distanceStar = 0;
    private float distancePlayer = 0f;

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
        distanceReal = Random.Range(1500, 9999);
        distanceText.text = distanceReal.ToString() + " km";

        distanceStar = Random.Range(1, 9999);
    }

    public void changeTelescopeDistance01()
    {
        Transform tr = telescope01;
        Slider slider = slider01;

        float value = (slider.value - 0.5f) * 100;
        tr.position = new Vector3(tr.position.x, tr.position.y, value);

        tr.LookAt(star);
    }

    public void changeTelescopeDistance02()
    {
        Transform tr = telescope02;
        Slider slider = slider02;

        float value = (slider.value - 0.5f) * 50;
        tr.position = new Vector3(tr.position.x, tr.position.y, value);

        tr.LookAt(star);
    }

    public void validateDistance()
    {
        if(!canValidate) { return; }

        if (soundManager != null) { soundManager.playAudioClip(0); }

        canValidate = false;
        result.SetActive(true);

        distanceStarText.text = distanceStar.ToString("n0") + " a.l.";
        distancePlayer = Mathf.Abs(slider01.value - slider02.value) * 10000f;

        float distanceRatio = Mathf.Ceil(Mathf.Abs(distancePlayer / distanceReal) * distanceStar);

        distancePlayerText.text = distanceRatio.ToString("n0") + " a.l.";

        float marginError = Mathf.Abs(distanceRatio / distanceStar);
        marginError = (Mathf.Abs(((Mathf.Round(marginError * 100)) / 100) - 1f) * 100);
        error.text = marginError + " %";

        float totalMarginError = GameObject.Find("GameManager").GetComponent<GameManager>().getErrorMargin() + marginError;
        totalMarginErrorText.text = totalMarginError + " %";

        GameObject.Find("GameManager").GetComponent<GameManager>().setErrorMargin(totalMarginError);
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

        if (GameObject.Find("GameManager").GetComponent<GameManager>().getMaxLevel() == 2)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            SceneManager.LoadScene("3Game");
        }
    }
}
