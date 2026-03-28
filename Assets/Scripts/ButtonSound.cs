using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Audio Sources")]
    public AudioSource sfxSource;   // ตัวเล่นเสียง

    [Header("Audio Clips")]
    public AudioClip hoverSound;    // เสียงตอนเอาเมาส์ชี้
    public AudioClip clickSound;    // เสียงตอนกด

    // เมาส์ชี้
    public void OnPointerEnter(PointerEventData eventData)
    {
        // เช็คว่ามีไฟล์เสียง และ มีลำโพงใส่ไว้แล้ว ค่อยสั่งเล่น
        if (hoverSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(hoverSound);
        }
    }

    // กดปุ่ม
    public void OnPointerClick(PointerEventData eventData)
    {
        // เช็คว่ามีไฟล์เสียง และ มีลำโพงใส่ไว้แล้ว ค่อยสั่งเล่น
        if (clickSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clickSound);
        }
    }
}