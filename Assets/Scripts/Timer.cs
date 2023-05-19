using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Timer : MonoBehaviour
{
    public float gameTimeInSeconds = 120f; // Total game time in seconds
    public Text timerText; // Reference to the UI text displaying the timer

    private float currentTime; // Current time remaining

    private void Start()
    {
        currentTime = gameTimeInSeconds;
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while (currentTime > 0)
        {
            // Update the timer text with the current time
            timerText.text = FormatTime(currentTime);

            // Decrease the current time
            currentTime -= 1f;

            yield return new WaitForSeconds(1f);
        }

        // Perform game over logic here, such as loading a game over scene
        SceneManager.LoadScene("gameOver"); // Change "GameOverScene" to your desired game over scene name
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}