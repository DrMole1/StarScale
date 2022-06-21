using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LevelButton : MonoBehaviour
{
    // ===================== VARIABLES =====================

    [SerializeField] private int id;
    [SerializeField] private GameObject[] infos;
    [SerializeField] private GameObject[] texts;
    [SerializeField] private SoundManager soundManager;

    private bool canDoSound = true;

    // =====================================================


    void OnMouseOver()
    {
        if (soundManager != null && canDoSound) { soundManager.playAudioClip(1); canDoSound = false; }

        texts[id - 1].SetActive(true);

        for (int i = 0; i < id; i++)
        {
            infos[i].SetActive(true);
        }
    }

    void OnMouseExit()
    {
        canDoSound = true;

        for(int i = 0; i < infos.Length; i++)
        {
            infos[i].SetActive(false);
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].SetActive(false);
        }
    }
}
