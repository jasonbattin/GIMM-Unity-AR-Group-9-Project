using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    private float timeRestoreRate;
    private bool restoreTime;

    private void Update()
    {
        if (restoreTime)
        {
            if (Time.timeScale < 1f)
            {
                Time.timeScale += timeRestoreRate * Time.unscaledDeltaTime;
            }
            else
            {
                Time.timeScale = 1f;
                restoreTime = false;
            }
        }
    }

    public void StopTime(float newTimeScale, float restoreSpeed, float delay)
    {
        timeRestoreRate = restoreSpeed;
        Time.timeScale = Mathf.Clamp(newTimeScale, 0f, 1f); // Ensures newTimeScale is within valid range

        if (delay > 0)
        {
            StopCoroutine(StartTimeAgain(delay));
            StartCoroutine(StartTimeAgain(delay));
        }
        else
        {
            restoreTime = true;
        }
    }

    IEnumerator StartTimeAgain(float amt)
    {
        yield return new WaitForSecondsRealtime(amt);
        restoreTime = true;
    }
}

