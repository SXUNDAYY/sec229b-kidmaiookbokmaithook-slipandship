using UnityEngine;
using TMPro;

public class GlobalTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float initialTime = 60f;

    [Header("Text Colors")]
    public Color normalColor = Color.white;
    public Color warningColor = Color.red;

    [Header("Sound")] // 🛑 [เพิ่ม]
    public AudioSource warningAudio;   // 🛑 [เพิ่ม] เสียงเตือน 10 วิ
    public AudioClip warningClip;      // 🛑 [เพิ่ม]

    public AudioSource deathAudio;     // 🛑 [เพิ่ม] เสียงตอนแพ้
    public AudioClip deathClip;        // 🛑 [เพิ่ม]

    private bool hasPlayedWarning = false; // 🛑 [เพิ่ม] กันเสียงเตือนเล่นซ้ำ
    private bool hasPlayedDeath = false;   // 🛑 [เพิ่ม] กันเสียงตายเล่นซ้ำ

    public static float timeRemaining;
    public static bool isTimerRunning = false;
    public static bool isInitialized = false;

    void Start()
    {
        if (!isInitialized)
        {
            timeRemaining = initialTime;
            isInitialized = true;
        }
        isTimerRunning = true;

        hasPlayedWarning = false; // 🛑 [เพิ่ม]
        hasPlayedDeath = false;   // 🛑 [เพิ่ม]

        if (timerText != null)
        {
            timerText.color = (timeRemaining <= 10f) ? warningColor : normalColor;

        }
        //timerText.color = (timeRemaining <= 10f) ? warningColor : normalColor;
    }

    void Update()
    {
        if (isTimerRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            // ▶️ 🛑 [เพิ่ม] เสียงเตือนตอนเหลือ 10 วิ (เล่นครั้งเดียว)
            if (timeRemaining <= 10f && !hasPlayedWarning)
            {
                if (warningAudio != null && warningClip != null)
                {
                    warningAudio.PlayOneShot(warningClip);
                }
                hasPlayedWarning = true;
            }

            if (timerText != null)
            {
                int minutes = Mathf.FloorToInt(timeRemaining / 60);
                int seconds = Mathf.FloorToInt(timeRemaining % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                if (timeRemaining <= 10f) timerText.color = warningColor;
                else timerText.color = normalColor;
            }
        }
        // --- ส่วนที่เพิ่มเข้ามา: เช็คว่าเวลาหมดหรือยัง ---
        else if (isTimerRunning && timeRemaining <= 0)
        {
            timeRemaining = 0;
            isTimerRunning = false; // สั่งหยุดเวลา
            if (timerText != null) timerText.text = "00:00";

            // ▶️ 🛑 [เพิ่ม] เล่นเสียงตอนตาย (ครั้งเดียว)
            if (!hasPlayedDeath)
            {
                if (deathAudio != null && deathClip != null)
                {
                    deathAudio.PlayOneShot(deathClip);
                }
                hasPlayedDeath = true;
            }

            // ▶️ 🛑 [เพิ่ม] หยุดเสียงเตือน (กันเสียงค้าง)
            if (warningAudio != null)
            {
                warningAudio.Stop();
            }

            // เรียกสคริปต์ LoseManager เพื่อโชว์หน้าต่างแพ้
            LoseManager loseMgr = FindObjectOfType<LoseManager>();
            if (loseMgr != null)
            {
                loseMgr.ShowLoseWindow();
            }
        }
    }

    public static void StopTimer() { isTimerRunning = false; }
    public static void ResetForNextLevel() { isInitialized = false; isTimerRunning = true; }
}