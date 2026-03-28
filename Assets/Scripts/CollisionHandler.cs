using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Header("Settings")]
    public string groundTag = "Ground"; // ตั้ง Tag พื้นว่า Ground จะได้ไม่แพ้ตอนเริ่ม
    public float restartDelay = 2f;    // รอ 2 วินาทีก่อนเริ่มใหม่

    private bool hasCrashed = false;

    private void OnCollisionEnter(Collision collision)
    {
        // ถ้าชนสิ่งที่ไม่ใช่พื้น และยังไม่เคยชนมาก่อน
        if (collision.gameObject.tag != groundTag && !hasCrashed)
        {
            hasCrashed = true;
            Debug.Log("Crashed into: " + collision.gameObject.name);
            
            // สั่งหยุดรถ (ปิดการทำงานของ CarController)
            GetComponent<CarController>().enabled = false;
            
            // เริ่มเกมใหม่
            Invoke("RestartLevel", restartDelay);
        }
    }

    void RestartLevel()
    {
        // โหลดฉากปัจจุบันใหม่
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}