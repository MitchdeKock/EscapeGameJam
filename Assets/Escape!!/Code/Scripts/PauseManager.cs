using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseManager
{
    public static bool IsPaused { get; private set;}

    public static void TogglePause()
    {
        if (IsPaused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }

    public static void Pause()
    {
        if (IsPaused)
        {
            return;
        }
        else
        {
            IsPaused = true;
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
    }

    public static void UnPause()
    {
        if (!IsPaused)
        {
            return; 
        }
        else
        {
            IsPaused = false;
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }
}
