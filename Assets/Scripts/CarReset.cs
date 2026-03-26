using UnityEngine;
using UnityEngine.SceneManagement; // ต้องมีบรรทัดนี้ด้วยสำหรับการโหลดฉาก

public class CarReset : MonoBehaviour
{
    void Update()
    {
        // เมื่อผู้เล่นกดปุ่ม R บนคีย์บอร์ด
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 1. [สำคัญมาก] คืนค่าเวลาให้เกมกลับมาเดินปกติก่อน (ป้องกันเกมค้าง)
            Time.timeScale = 1f; 

            // 2. โหลดฉากปัจจุบันใหม่ (เวลาจะไม่กลับไป 60 วิ เพราะเราดักไว้ใน GlobalTimer แล้ว)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}