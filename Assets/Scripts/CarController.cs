using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    [Header("Car Settings")]
    public float motorForce = 1500f;
    public float turboMultiplier = 2.5f; // กด Shift แล้วแรงขึ้น 2.5 เท่า
    public float brakeForce = 5000f;
    public float maxSteerAngle = 35f;

    [Header("Physics Setup")]
    public Vector3 centerOfMassOffset = new Vector3(0, -0.9f, 0.5f); // ช่วยเรื่องการเลี้ยว

    [Header("Brake Sound 🔊")]
    public AudioSource brakeSource;
    public AudioClip brakeClip;

    private float moveInput;
    private float steerInput;
    private bool isBraking;
    private bool isTurbo;

    void Start()
    {
        // แก้ปัญหา "เลี้ยวแล้วรถไถลตรง" โดยการกดจุดศูนย์ถ่วงให้ต่ำและค่อนไปข้างหน้า
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.centerOfMass = centerOfMassOffset;
        }
    }

    void FixedUpdate()
    {
        GetInputs();
        HandleMotor();
        HandleSteering();
    }

    private void GetInputs()
    {
        // เลี้ยว A, D
        steerInput = Input.GetAxisRaw("Horizontal");

        // W เดินหน้า / S ถอยหลัง
        if (Input.GetKey(KeyCode.W)) moveInput = 1f;
        else if (Input.GetKey(KeyCode.S)) moveInput = -1f;
        else moveInput = 0f;

        // Shift = Turbo
        isTurbo = Input.GetKey(KeyCode.LeftShift);

        // Space = Brake
        isBraking = Input.GetKey(KeyCode.Space);

        //เล่นเสียงเบรก (S หรือ Space)
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Space)))
        {
            if (brakeSource != null && brakeClip != null)
            {
                brakeSource.PlayOneShot(brakeClip);
            }
        }
    }

    private void HandleMotor()
    {
        // คำนวณความแรง (ปกติ vs เทอร์โบ)
        float currentMotorForce = isTurbo ? motorForce * turboMultiplier : motorForce;

        // สั่งวิ่ง (ขับเคลื่อนล้อหน้า)
        frontLeftWheel.motorTorque = moveInput * currentMotorForce;
        frontRightWheel.motorTorque = moveInput * currentMotorForce;

        // ระบบเบรก
        float currentBrake = isBraking ? brakeForce : 0f;
        ApplyBraking(currentBrake);
    }

    private void ApplyBraking(float force)
    {
        frontLeftWheel.brakeTorque = force;
        frontRightWheel.brakeTorque = force;
        rearLeftWheel.brakeTorque = force;
        rearRightWheel.brakeTorque = force;
    }

    private void HandleSteering()
    {
        // การเลี้ยวที่ล้อหน้า
        float steering = steerInput * maxSteerAngle;
        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;
    }
}