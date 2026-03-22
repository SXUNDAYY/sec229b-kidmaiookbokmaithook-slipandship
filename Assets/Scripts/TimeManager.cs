using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GlobalTimer : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI timerText;

    [Header("Settings")]
    public float initialTime = 60f; 
    public float warningThreshold = 10f; // เริ่มเตือนที่ 10 วินาที
    
    [Header("Colors")]
    public Color normalColor = Color.green; // สีปกติ
    public Color warningColor = Color.red;   // สีเตือน

    public static float timeRemaining; 
    public static bool isTimerRunning = false;
    private static bool initialized = false;

    void Awake()
    {
        // ถ้าเริ่มเกมครั้งแรก ให้ตั้งค่าเวลา
        if (!initialized)
        {
            timeRemaining = initialTime;
            isTimerRunning = true;
            initialized = true;
        }
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                UpdateTextColor();
            }
            else
            {
                // เวลาหมด = แพ้ (Reset ระบบ)
                timeRemaining = 0;
                isTimerRunning = false;
                initialized = false; 
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        // แสดงผล นาที:วินาที
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void UpdateTextColor()
    {
        if (timerText == null) return;

        // ถ้าเวลาเหลือน้อยกว่าค่าที่กำหนด (10 วิ) ให้เปลี่ยนเป็นสีแดง
        if (timeRemaining <= warningThreshold)
        {
            timerText.color = warningColor;
        }
        else
        {
            timerText.color = normalColor;
        }
    }

    public static void StopTimer()
    {
        isTimerRunning = false;
    }
}