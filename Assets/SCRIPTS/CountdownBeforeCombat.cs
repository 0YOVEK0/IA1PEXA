using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownBeforeCombat : MonoBehaviour
{
    public Text countdownText;   // The UI text that will display the countdown
    public GameObject boss;      // The Boss object that will be activated to start the combat
    public float countdownTime = 5f; // Countdown time in seconds
    private bool combatStarted = false;

    void Start()
    {
        if (countdownText != null)
        {
            countdownText.text = countdownTime.ToString("F0");  // Show the initial time
        }

        StartCoroutine(StartCombatCountdown());
    }

    IEnumerator StartCombatCountdown()
    {
        while (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;  // Reduce the countdown time
            countdownText.text = Mathf.Ceil(countdownTime).ToString("F0"); // Update the text

            yield return null;  // Wait for the next frame
        }

        // When the countdown reaches zero, the combat starts
        countdownText.text = "FIGHT!";
        combatStarted = true;

        // Start the combat (e.g., activate the Boss)
        if (boss != null)
        {
            boss.SetActive(true);  // Make sure the boss is activated when the combat begins
        }

        // Wait for 1 second before hiding the countdown text
        yield return new WaitForSeconds(1f);

        // Hide the countdown text and proceed with the combat
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        // Here you can add additional code to start the combat (enable player control, etc.)
    }
}
