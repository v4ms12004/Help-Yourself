using UnityEngine;
using UnityEngine.UI;

public class RandomTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float minTime = 1f;
    public float maxTime = 10f;
    public bool isRunning = false;
    
    [Header("UI References")]
    public Text timerText;
    public Text patientCountText;
    public InputField minTimeInput;
    public InputField maxTimeInput;
    public InputField patientCountInput;
    
    private float currentTime;
    private int patientCount = 1;
    private float targetTime;

    void Start()
    {
        // Initialize UI fields
        minTimeInput.text = minTime.ToString();
        maxTimeInput.text = maxTime.ToString();
        patientCountInput.text = patientCount.ToString();
        
        // Set up listeners for input changes
        minTimeInput.onEndEdit.AddListener(UpdateMinTime);
        maxTimeInput.onEndEdit.AddListener(UpdateMaxTime);
        patientCountInput.onEndEdit.AddListener(UpdatePatientCount);
        
        ResetTimer();
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
            
            if (currentTime <= 0f)
            {
                TimerComplete();
            }
        }
    }

    public void StartTimer()
    {
        if (!isRunning)
        {
            isRunning = true;
            SetRandomTargetTime();
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        currentTime = 0f;
        UpdateTimerDisplay();
    }

    private void SetRandomTargetTime()
    {
        targetTime = Random.Range(minTime, maxTime);
        currentTime = targetTime;
    }

    private void TimerComplete()
    {
        Debug.Log("Timer completed!");
        SetRandomTargetTime();
        
        // Optional: Add logic for when timer completes
        // Could trigger events for patient management here
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = currentTime.ToString("F1");
        }
    }

    private void UpdateMinTime(string value)
    {
        if (float.TryParse(value, out float newMin))
        {
            minTime = Mathf.Clamp(newMin, 0.1f, maxTime - 0.1f);
        }
    }

    private void UpdateMaxTime(string value)
    {
        if (float.TryParse(value, out float newMax))
        {
            maxTime = Mathf.Max(newMax, minTime + 0.1f);
        }
    }

    private void UpdatePatientCount(string value)
    {
        if (int.TryParse(value, out int newCount))
        {
            patientCount = Mathf.Max(1, newCount);
            if (patientCountText != null)
            {
                patientCountText.text = "Patients: " + patientCount;
            }
        }
    }

    public void RandomizeTimerRange()
    {
        // Randomize within reasonable bounds
        minTime = Random.Range(1f, 5f);
        maxTime = Random.Range(minTime + 1f, 10f);
        
        // Update UI fields
        minTimeInput.text = minTime.ToString();
        maxTimeInput.text = maxTime.ToString();
    }
}
