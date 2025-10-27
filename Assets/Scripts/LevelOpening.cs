using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelOpening : MonoBehaviour
{
    public GameObject opaquePanel;
    public TextMeshProUGUI countdownText;
    public int countdownSeconds = 3;
    public int goSecond = 1;

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

      
    }

    
}
