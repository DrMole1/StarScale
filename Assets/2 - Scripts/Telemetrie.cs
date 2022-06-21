using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Telemetrie : MonoBehaviour
{
    private const float DISTANCE_REAL = 384400f;
    private const float FACTOR = 591.3846f;

    // ======================= VARIABLES =======================

    [Header("MiniGame")]
    [SerializeField] private Image btnLaunch;
    [SerializeField] private Image btnStop;
    [SerializeField] private Transform laser01;
    [SerializeField] private Transform laser02;

    [Header("Results")]
    [SerializeField] private GameObject result;
    [SerializeField] private TextMeshProUGUI distancePlayer;
    [SerializeField] private TextMeshProUGUI error;
    [SerializeField] private Image blackCurtain;

    private bool canShot = true;
    private bool canStop = false;

    private Color colorDesactivate = new Color(1f, 1f, 1f, 0.4f);
    private Color colorActivate = new Color(1f, 1f, 1f, 1f);

    private Coroutine coroutine;

    private float count = 0;

    private bool canNext = true;

    private SoundManager soundManager;

    // =========================================================

    private void Awake()
    {
        if (GameObject.Find("SoundManager") != null) { soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>(); }
    }

    public void launchLaser()
    {
        if(!canShot) { return; }

        if (soundManager != null) { soundManager.playAudioClip(0); }

        btnLaunch.color = colorDesactivate;
        btnStop.color = colorActivate;

        canShot = false;
        canStop = true;

        coroutine = StartCoroutine(IAnimateLaser());
    }

    public void stopLaser()
    {
        if (!canStop) { return; }

        if (soundManager != null) { soundManager.playAudioClip(0); }

        btnStop.color = colorDesactivate;

        canStop = false;

        StopCoroutine(coroutine);

        showInfos();
    }

    private IEnumerator IAnimateLaser()
    {
        yield return new WaitForSeconds(0.2f);

        while(true)
        {
            yield return null;

            if (count < 325)
            {
                laser01.localScale = new Vector2(laser01.localScale.x, laser01.localScale.y + 0.01f);

                count++;
            }
            else
            {
                laser02.localScale = new Vector2(laser02.localScale.x, laser02.localScale.y + 0.01f);

                count++;
            }
        }
    }

    private void showInfos()
    {
        result.SetActive(true);

        float distance = Mathf.Ceil(count * FACTOR);
        string distanceTextTemp = distance.ToString();
        string distanceText = "";
        int countLetter = 0;

        if (distanceTextTemp.Length % 3 == 0) { distanceText = distanceTextTemp.Substring(0, 3); countLetter = 3; }
        else if (distanceTextTemp.Length % 3 == 1) { distanceText = distanceTextTemp.Substring(0, 1); countLetter = 1; }
        else if (distanceTextTemp.Length % 3 == 2) { distanceText = distanceTextTemp.Substring(0, 2); countLetter = 2; }

        while(countLetter + 3 <= distanceTextTemp.Length)
        {
            distanceText += " ";

            distanceText += distanceTextTemp.Substring(countLetter, 3);

            countLetter += 3;
        }

        distanceText += " km";

        distancePlayer.text = distanceText;

        float marginError = Mathf.Abs(distance / DISTANCE_REAL);
        marginError = (Mathf.Abs(((Mathf.Round(marginError * 100)) / 100) - 1f) * 100);
        error.text = marginError + " %";

        GameObject.Find("GameManager").GetComponent<GameManager>().setErrorMargin(marginError);
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

        if (GameObject.Find("GameManager").GetComponent<GameManager>().getMaxLevel() == 1)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            SceneManager.LoadScene("2Game");
        }
    }
}
