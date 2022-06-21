using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Supernova : MonoBehaviour
{
    // ======================= VARIABLES =======================

    [SerializeField] private GameObject btnStart;
    [SerializeField] private WhiteStar whiteStar;
    [SerializeField] private Transform star;
    [SerializeField] private GameObject energyPref;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Slider slider;

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
    private bool canSpawn = false;

    private float rot = 0;
    private Vector3 currentRot;
    private Quaternion currentQuaternionRot;

    private int timer = 30;

    private SoundManager soundManager;

    // =========================================================

    private void Awake()
    {
        if (GameObject.Find("SoundManager") != null) { soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>(); }
    }

    public void startGame()
    {

        if (soundManager != null) { soundManager.playAudioClip(0); }

        distanceStar = Random.Range(100000000f, 10000000000f);

        btnStart.SetActive(false);
        whiteStar.canMove = true;
        canSpawn = true;

        StartCoroutine(ISpawn());
        StartCoroutine(ITimer());
    }

    private IEnumerator ISpawn()
    {
        while(canSpawn)
        {
            yield return new WaitForSeconds(0.1f);

            rot = Random.Range(0f, 360f);
            currentRot = new Vector3(0, 0, rot);
            currentQuaternionRot.eulerAngles = currentRot;

            GameObject energy;
            energy = Instantiate(energyPref, star.position, currentQuaternionRot);
            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            energy.GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Impulse);
        }
    }

    private IEnumerator ITimer()
    {
        while(timer > 0)
        {
            yield return new WaitForSeconds(1f);

            timer--;

            timerText.text = timer.ToString();
        }

        canSpawn = false;
        whiteStar.canCatch = false;
        whiteStar.canMove = false;

        validateDistance();
    }

    public void validateDistance()
    {
        if (!canValidate) { return; }

        if (soundManager != null) { soundManager.playAudioClip(0); }

        canValidate = false;
        result.SetActive(true);

        distanceStarText.text = distanceStar.ToString("n0") + " a.l.";

        float distanceRatio = Mathf.Ceil(Mathf.Abs(slider.value / 0.725f) * distanceStar);

        float marginError = 0f;

        if (slider.value >= 0.65f && slider.value <= 0.8f)
        {
            distancePlayerText.text = distanceRatio.ToString("n0") + " a.l.";

            marginError = Mathf.Abs(distanceRatio / distanceStar);
            marginError = (Mathf.Abs(((Mathf.Round(marginError * 100)) / 100) - 1f) * 100);
            error.text = marginError + " %";
        }
        else if (slider.value < 0.65f)
        {
            distancePlayerText.text = "La naine blanche n'a pas absorbé assez d'énergie.";

            marginError = 100f;
            error.text = marginError + " %";
        }
        else if (slider.value > 0.8f)
        {
            distancePlayerText.text = "La naine blanche a absorbé trop d'énergie.";

            marginError = 100f;
            error.text = marginError + " %";
        }

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

        if (GameObject.Find("GameManager").GetComponent<GameManager>().getMaxLevel() == 4)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            SceneManager.LoadScene("5Game");
        }
    }
}
