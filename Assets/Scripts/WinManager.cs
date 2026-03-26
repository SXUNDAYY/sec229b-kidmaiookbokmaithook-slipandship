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

    [Header("Settings")]
    public float levelTotalTime = 60f;

    void Start()
    {
        // --- [สำคัญ] คืนค่าเวลาให้เดินปกติ (ป้องกันบั๊กเกมค้างจากด่านที่แล้ว) ---
        Time.timeScale = 1f; 

        if (winPanel != null) 
        {
            winPanel.SetActive(false);
        }

        if (nextLevelButton != null) nextLevelButton.onClick.AddListener(NextLevel);
        if (mainMenuButton != null) mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    public void ShowWinWindow()
    {
        GlobalTimer.StopTimer();

        if (winPanel != null) winPanel.SetActive(true);

        float timeUsed = levelTotalTime - GlobalTimer.timeRemaining;
        int mins = Mathf.FloorToInt(Mathf.Max(0, timeUsed) / 60);
        int secs = Mathf.FloorToInt(Mathf.Max(0, timeUsed) % 60);
        
        if (resultTimeText != null)
        {
            resultTimeText.text = string.Format("Time Used: {0:00}:{1:00}", mins, secs);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // --- สิ่งที่เพิ่มเข้ามา: สั่งหยุดการเคลื่อนไหวทุกอย่างในเกม (รถเบรกหัวทิ่มทันที) ---
        Time.timeScale = 0f; 
    }

    public void NextLevel()
    {
        // --- สิ่งที่เพิ่มเข้ามา: ต้องคืนค่าเวลาให้เป็น 1 ก่อนโหลดฉากใหม่ ---
        Time.timeScale = 1f; 
        
        GlobalTimer.ResetForNextLevel();
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextScene);
        else
            SceneManager.LoadScene(0); 
    }

    public void GoToMainMenu()
    {
        // --- สิ่งที่เพิ่มเข้ามา: ต้องคืนค่าเวลาให้เป็น 1 ก่อนโหลดฉากเมนู ---
        Time.timeScale = 1f; 
        
        GlobalTimer.ResetForNextLevel();
        SceneManager.LoadScene(0); 
    }
}