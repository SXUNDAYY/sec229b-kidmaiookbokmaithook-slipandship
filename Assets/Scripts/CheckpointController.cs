using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private bool isReached = false;
    private Renderer rd; // ตัวแปรสำหรับเก็บพื้นผิวของวัตถุ

    void Start()
    {
        // 1. ดึงข้อมูลพื้นผิว (Material) ของเส้นชัยมาเก็บไว้ตอนเริ่มเกม
        rd = GetComponent<Renderer>();

        // 2. ตั้งค่าเริ่มต้นให้เส้นชัยเป็น "สีแดง"
        if (rd != null)
        {
            rd.material.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // เช็คว่าคนที่มาชนคือ Player และยังไม่เคยเข้าเส้นชัยมาก่อน
        if (other.CompareTag("Player") && !isReached)
        {
            isReached = true;
            Debug.Log("เข้าเส้นชัยแล้ว!");

            // 3. เปลี่ยนสีเส้นชัยเป็น "สีเขียว" ทันทีที่รถมาเหยียบ
            if (rd != null)
            {
                rd.material.color = Color.green;
            }

            // ค้นหา WinManager และสั่งให้เปิดหน้าต่างชนะ
            WinManager win = FindObjectOfType<WinManager>();
            if (win != null)
            {
                win.ShowWinWindow();
            }
            else
            {
                Debug.LogError("หา WinManager ไม่เจอ!");
            }
        }
    }
}