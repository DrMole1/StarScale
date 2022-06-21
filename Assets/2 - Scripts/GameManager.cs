using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ============================ VARIABLES ============================

    public static int maxLevel = 0;
    public static float errorMargin = 0f;

    private static GameObject instance;

    // ===================================================================


    private void Awake()
    {
        if (instance)
        { // if bgAudio already references some object...
            Destroy(gameObject); // suicide
        }
        else
        { // if bgAudio is null, this is the first TronBG object:
            instance = gameObject; // assign this one
        }

        DontDestroyOnLoad(gameObject);
    }

    public void setMaxLevel(int _level)
    {
        maxLevel = _level;
    }

    public int getMaxLevel()
    {
        return maxLevel;
    }

    public void setErrorMargin(float _errorMargin)
    {
        errorMargin = _errorMargin;
    }

    public float getErrorMargin()
    {
        return errorMargin;
    }
}
