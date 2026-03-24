using UnityEngine;

public class SlipperyZone : MonoBehaviour
{
    //ค่าปกติของล้อ (ตอนออกจากโซน)
    public float normalSidewaysExtremum = 1f;      // แรงเกาะสูงสุด (ยิ่งมาก = เกาะดี)
    public float normalSidewaysAsymptote = 0.75f;  // แรงเกาะตอนเริ่มลื่น

    //ค่าลื่น (ตอนอยู่ในโซน)
    public float slipperyExtremum = 0.3f;   // ยิ่งน้อย = ยิ่งลื่น
    public float slipperyAsymptote = 0.2f;  // ยิ่งน้อย = ลื่นมากขึ้น

    //เมื่อมี object เข้ามาใน Trigger
    private void OnTriggerEnter(Collider other)
    {
        // เช็คว่าเป็น Player ไหม
        if (other.CompareTag("Player"))
        {
            Debug.Log("เข้าโซนลื่น!");

            // ดึง WheelCollider ทั้งหมดของรถ
            WheelCollider[] wheels = other.GetComponentsInChildren<WheelCollider>();

            // วนลูปทุกล้อ
            foreach (WheelCollider wheel in wheels)
            {
                // ดึงค่า friction ปัจจุบัน
                WheelFrictionCurve f = wheel.sidewaysFriction;

                // เปลี่ยนเป็นค่าลื่น
                f.extremumValue = slipperyExtremum;
                f.asymptoteValue = slipperyAsymptote;

                // เอาค่าที่แก้แล้วกลับไปใส่
                wheel.sidewaysFriction = f;
            }
        }
    }
    //เมื่อออกจากโซน
    private void OnTriggerExit(Collider other)
    {
        // เช็คว่าเป็น Player ไหม
        if (other.CompareTag("Player"))
        {
            Debug.Log("ออกจากโซนลื่น!");

            // ดึง WheelCollider ทั้งหมดของรถ
            WheelCollider[] wheels = other.GetComponentsInChildren<WheelCollider>();

            // วนลูปทุกล้อ
            foreach (WheelCollider wheel in wheels)
            {
                // ดึงค่า friction ปัจจุบัน
                WheelFrictionCurve f = wheel.sidewaysFriction;

                // รีเซ็ตกลับค่าปกติ
                f.extremumValue = normalSidewaysExtremum;
                f.asymptoteValue = normalSidewaysAsymptote;

                // เอาค่ากลับไปใส่
                wheel.sidewaysFriction = f;
            }
        }
    }
}