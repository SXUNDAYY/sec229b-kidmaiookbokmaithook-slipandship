using UnityEngine;

public class CarSound : MonoBehaviour
{
    [Header("Engine")]
    public AudioSource engineSound;
    public float minPitch = 0.8f;
    public float maxPitch = 1.5f;

    [Header("Brake")]
    public AudioSource brakeSource;
    public AudioClip brakeClip;
    public float brakeThreshold = 2f; // ความแรงในการเบรก

    [Header("Collision")]
    public AudioSource crashSource;
    public AudioClip crashClip;
    public float crashThreshold = 5f; // ความแรงชนขั้นต่ำ

    Rigidbody rb;
    float lastSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        engineSound.Play();
    }

    void Update()
    {
        // ถ้าหมดเวลา → หยุดเสียงทั้งหมด
        if (!GlobalTimer.isTimerRunning)
        {
            StopAllSounds();
            return; // ออกจาก Update ทันที
        }

        float speed = rb.linearVelocity.magnitude;

        // ปรับเสียงเครื่องตามความเร็ว
        engineSound.pitch = Mathf.Lerp(minPitch, maxPitch, speed / 10f);

        // ตรวจจับการ "เบรก"
        float speedDiff = lastSpeed - speed;

        if (speedDiff > brakeThreshold && speed > 1f)
        {
            if (!brakeSource.isPlaying)
            {
                brakeSource.PlayOneShot(brakeClip);
            }
        }

        lastSpeed = speed;

    }

    //ตรวจจับการชน
    void OnCollisionEnter(Collision collision)
    {
        //ถ้าหมดเวลา → ไม่ให้เล่นเสียงชน
        if (!GlobalTimer.isTimerRunning) return;

        float impact = collision.relativeVelocity.magnitude;

        if (impact > crashThreshold)
        {
            crashSource.PlayOneShot(crashClip);
        }
    }

    // 🛑ฟังก์ชันหยุดเสียงทั้งหมด
    void StopAllSounds()
    {
        if (engineSound != null && engineSound.isPlaying)
            engineSound.Stop();

        if (brakeSource != null && brakeSource.isPlaying)
            brakeSource.Stop();

        if (crashSource != null && crashSource.isPlaying)
            crashSource.Stop();
    }
}