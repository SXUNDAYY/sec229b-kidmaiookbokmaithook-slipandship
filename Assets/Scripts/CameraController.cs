using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // ลากตัวรถมาใส่ช่องนี้

    [Header("Settings")]
    public Vector3 offset = new Vector3(0, 3, -6); // ระยะห่างจากรถ (สูง 3, ถอยหลัง 6)
    public float followSpeed = 10f; // ความเร็วในการเลื่อนตาม
    public float lookSpeed = 10f;   // ความเร็วในการหันตาม

    void FixedUpdate()
    {
        if (target == null) return;

        // 1. คำนวณตำแหน่งที่กล้องควรจะไปอยู่ (อ้างอิงจากตำแหน่งและทิศทางของรถ)
        Vector3 targetPosition = target.TransformPoint(offset);

        // 2. ทำให้กล้องค่อยๆ เลื่อนไปหาตำแหน่งนั้น (Smooth)
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // 3. คำนวณการหันหน้ากล้องให้มองไปที่รถตลอดเวลา
        Vector3 lookDirection = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        // 4. ทำให้กล้องค่อยๆ หันไป (Smooth)
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);
    }
}