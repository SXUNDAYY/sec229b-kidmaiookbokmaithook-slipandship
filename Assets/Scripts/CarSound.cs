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

    // 💥 ตรวจจับการชน
    void OnCollisionEnter(Collision collision)
    {
        float impact = collision.relativeVelocity.magnitude;

        if (impact > crashThreshold)
        {
            crashSource.PlayOneShot(crashClip);
        }
    }
}