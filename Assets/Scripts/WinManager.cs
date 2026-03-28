using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject winPanel;
    public TextMeshProUGUI resultTimeText;
    public Button nextLevelButton;
    public Button mainMenuButton;

    [Header("Final Level Settings (ด่านสุดท้าย)")]
    public bool isFinalLevel = false;      // ติ๊กถูกถ้าด่านนี้คือด่านจบ
    public GameObject comicCanvas;         // ลาก Canvas_Commic มาใส่ช่องนี้

    [Header("Settings")]
    public float levelTotalTime = 60f;

    void Start()
    {
        Time.timeScale = 1f; 

        if (winPanel != null) winPanel.SetActive(false);
        if (comicCanvas != null) comicCanvas.SetActive(false); // ซ่อนคอมมิคไว้ก่อนเริ่มเกม

        // ผูกปุ่มเข้ากับฟังก์ชัน
        if (nextLevelButton != null) nextLevelButton.onClick.AddListener(OnNextButtonClicked);
        if (mainMenuButton != null) mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    public void ShowWinWindow()
    {
        GlobalTimer.StopTimer();

        if (winPanel != null) winPanel.SetActive(true);

        // --- ถ้าเป็นด่านสุดท้าย ให้ซ่อนปุ่ม Main Menu ทิ้งไปเลย ---
        if (isFinalLevel && mainMenuButton != null)
        {
            mainMenuButton.gameObject.SetActive(false);
        }

        float timeUsed = levelTotalTime - GlobalTimer.timeRemaining;
        int mins = Mathf.FloorToInt(Mathf.Max(0, timeUsed) / 60);
        int secs = Mathf.FloorToInt(Mathf.Max(0, timeUsed) % 60);
        
        if (resultTimeText != null)
            resultTimeText.text = string.Format("Time Used: {0:00}:{1:00}", mins, secs);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f; 
    }

    public void OnNextButtonClicked()
    {
        if (isFinalLevel)
        {
            // --- ถ้าเป็นด่านสุดท้าย: ปิดหน้าต่างชนะ แล้วเปิดหน้าคอมมิคแทน ---
            if (winPanel != null) winPanel.SetActive(false);
            if (comicCanvas != null) comicCanvas.SetActive(true);
        }
        else
        {
            // --- ถ้าไม่ใช่ด่านสุดท้าย: โหลดด่านต่อไปปกติ ---
            Time.timeScale = 1f; 
            GlobalTimer.ResetForNextLevel();
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextScene < SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene(nextScene);
            else SceneManager.LoadScene(0); 
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; 
        GlobalTimer.ResetForNextLevel();
        SceneManager.LoadScene(0); 
    }
}