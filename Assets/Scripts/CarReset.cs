using UnityEngine;
using UnityEngine.SceneManagement;

public class CarReset : MonoBehaviour
{
    [Header("Settings")]
    public KeyCode resetKey = KeyCode.R; // ปุ่มสำหรับกดรีเซ็ต
    
    void Update()
    {
        // เมื่อกดปุ่ม R
        if (Input.GetKeyDown(resetKey))
        {
            ResetPlayer();
        }
    }

    void ResetPlayer()
    {
        Debug.Log("Manual Reset! Time is still running...");
        
        // โหลด Scene ปัจจุบันใหม่
        // เนื่องจากเราใช้ 'static' ใน GlobalTimer ค่าเวลาจะยังคงนับต่อจากเดิมเป๊ะๆ
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}