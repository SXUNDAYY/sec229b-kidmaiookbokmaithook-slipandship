using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [Header("Physics Settings")]
    public float targetAcceleration = 50f; // ความเร่งที่ต้องการ (a)

    private void OnTriggerEnter(Collider other)
    {
        // เช็คว่ารถมาเหยียบแผ่นนี้หรือเปล่า
        if (other.CompareTag("Player"))
        {
            Rigidbody carRb = other.GetComponent<Rigidbody>();

            if (carRb != null)
            {
                // 1. ดึงค่ามวล (m) ของรถจาก Rigidbody
                float m = carRb.mass;

                // 2. คำนวณหาแรง (F) ตามกฎข้อที่ 2 ของนิวตัน: F = ma
                float forceMagnitude = m * targetAcceleration;

                // 3. กำหนดทิศทางของแรง (ให้พุ่งไปข้างหน้าตามทิศของรถ)
                Vector3 forceDirection = other.transform.forward * forceMagnitude;

                // 4. นำผลลัพธ์ Force ที่ได้ไปใช้ใน AddForce() เพื่อแสดงผลฟิสิกส์
                carRb.AddForce(forceDirection, ForceMode.Impulse);

                Debug.Log("ใช้กฎ F = ma | รถหนัก: " + m + " | ใส่แรงพุ่ง: " + forceMagnitude);
            }
        }
    }
}