<<<<<<< Updated upstream
=======
using System;
>>>>>>> Stashed changes
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
<<<<<<< Updated upstream
=======
using UnityEngine.SceneManagement;
>>>>>>> Stashed changes

public class LevelOpening : MonoBehaviour
{
    public GameObject opaquePanel;
    public TextMeshProUGUI countdownText;
    public int countdownSeconds = 3;
    public int goSecond = 1;
<<<<<<< Updated upstream
=======
    public TextMeshProUGUI timerText;
    public float timerCount = 0f;
    Coroutine timerCoroutine;
    public bool ending = false;
>>>>>>> Stashed changes

    void Start()
    {
        if (opaquePanel == null || countdownText == null)
        {
            return;
        }

        opaquePanel.SetActive(true);
        Time.timeScale = 0f; 
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        for (int i = countdownSeconds; i >= 1; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        countdownText.text = "Go!";
        yield return new WaitForSecondsRealtime(goSecond);

        countdownText.text = string.Empty;

        opaquePanel.SetActive(false);
        Time.timeScale = 1f;

<<<<<<< Updated upstream
      
    }

    
=======
        StartLevelTimer();
    }

    void StartLevelTimer()
    {
        if (timerText == null) return;
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        timerCount = Time.time;
        timerCoroutine = StartCoroutine(LevelTimerCoroutine());
    }

    IEnumerator LevelTimerCoroutine()
    {
        while (true)
        {
            // defensive null-check: the Text component may be destroyed or unassigned at runtime.
            if (timerText == null)
            {
                timerCoroutine = null;
                yield break;
            }

            float elapsed = Time.time - timerCount;
            int minutes = (int)(elapsed / 60f);
            int seconds = (int)(elapsed % 60f);
            int milliseconds = (int)((elapsed - Mathf.Floor(elapsed)) * 1000f);
            timerText.text = string.Format("Time: {0:00}:{1:00}:{1:00}", minutes, seconds, milliseconds);
            yield return null;
        }
    }

    public void WinState()
    {
        if (PacStudentController.pelletCounter == 0)
        {
            if (timerCoroutine != null) { StopCoroutine(timerCoroutine); timerCoroutine = null; }
            if (opaquePanel != null) opaquePanel.SetActive(true);
            if (countdownText != null) countdownText.text = "Game Over";
            Time.timeScale = 0f;
            ending = true;
            StartCoroutine(EndAndReturnToStart());
        }
    }

    public void LoseState()
    {
        if (PacStudentController.lives == 0)
        {
            if (timerCoroutine != null) { StopCoroutine(timerCoroutine); timerCoroutine = null; }
            if (opaquePanel != null) opaquePanel.SetActive(true);
            if (countdownText != null) countdownText.text = "Game Over";
            Time.timeScale = 0f;
            ending = true;
            StartCoroutine(EndAndReturnToStart());
        }
    }

    IEnumerator EndAndReturnToStart()
    {
        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1f;

        SceneManager.LoadScene("StartScene");
    }
>>>>>>> Stashed changes
}
