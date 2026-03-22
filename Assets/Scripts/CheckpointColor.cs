using UnityEngine;

public class CheckpointColor : MonoBehaviour
{
    [Header("Color Settings")]
    public Color inactiveColor = Color.red;   // สีตอนยังไม่เข้า
    public Color activeColor = Color.green;   // สีตอนเข้าแล้ว
    
    private Renderer checkpointRenderer;
    private bool isReached = false;

    void Start()
    {
        // ดึง Renderer มาเพื่อเปลี่ยนสี Material
        checkpointRenderer = GetComponent<Renderer>();
        
        // ตั้งค่าสีเริ่มต้นเป็นสีแดง
        if (checkpointRenderer != null)
        {
            checkpointRenderer.material.color = inactiveColor;
        }
    }

    // --- มี OnTriggerEnter แค่อันเดียวในไฟล์นี้ ---
    private void OnTriggerEnter(Collider other)
    {
        // เช็คว่าสิ่งที่มาชนมี Tag ว่า Player และเรายังไม่เคยผ่านจุดนี้มาก่อน
        if (other.CompareTag("Player") && !isReached)
        {
            isReached = true;
            Reached();
        }
    }

    void Reached()
    {
        // 1. เปลี่ยนสีวัตถุเป็นสีเขียว
        if (checkpointRenderer != null)
        {
            checkpointRenderer.material.color = activeColor;
        }

        // 2. สั่งหยุดเวลา (เรียกใช้ฟังก์ชันจากสคริปต์ GlobalTimer)
        GlobalTimer.StopTimer();

        Debug.Log("Checkpoint Reached: Timer Stopped & Color Changed to Green!");
    }
}