using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseManager
{
    public static bool IsPaused { get; private set;}

    public static void TogglePause(bool pauseMusic=true)
    {
        if (IsPaused)
        {
            UnPause();
        }
        else
        {
            Pause(pauseMusic);
        }
    }

    public static void Pause(bool pauseMusic = true)
    {
        if (IsPaused)
        {
            return;
        }
        else
        {
            IsPaused = true;
            Time.timeScale = 0;
            AudioListener.pause = pauseMusic;
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
