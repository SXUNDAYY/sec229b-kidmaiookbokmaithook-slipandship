using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject losePanel;        // ลากหน้าต่าง LosePanel มาใส่
    public Button retryButton;          // ลากปุ่ม RE มาใส่
    public Button mainMenuButton;       // ลากปุ่ม Main Menu มาใส่

    void Start()
    {
        // เริ่มเกมมาให้ซ่อนหน้าต่างแพ้ไว้ก่อน
        if (losePanel != null) losePanel.SetActive(false);

        // เชื่อมปุ่ม (ปุ่ม RE ให้โหลดฉากเดิม, ปุ่ม Menu ให้กลับหน้าแรก)
        if (retryButton != null) retryButton.onClick.AddListener(RetryLevel);
        if (mainMenuButton != null) mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    // ฟังก์ชันนี้จะถูกเรียกจาก GlobalTimer ตอนเวลาหมด
    public void ShowLoseWindow()
    {
        if (losePanel != null) losePanel.SetActive(true);

        // โชว์เมาส์
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // สั่งหยุดเกม (แช่แข็ง)
        Time.timeScale = 0f; 
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f; // คืนค่าเวลา
        GlobalTimer.ResetForNextLevel(); // รีเซ็ตเวลาให้กลับมา 60 วิ
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // โหลดด่านนี้ใหม่
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // คืนค่าเวลา
        GlobalTimer.ResetForNextLevel();
        SceneManager.LoadScene(0); 
    }
}