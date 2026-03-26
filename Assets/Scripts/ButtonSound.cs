using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Audio Sources")]
    public AudioSource sfxSource;   // ตัวเล่นเสียง

    [Header("Audio Clips")]
    public AudioClip hoverSound;    // เสียงตอนชี้เมาส์
    public AudioClip clickSound;    // เสียงตอนกด

    //เมาส์ชี้ปุ่ม
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            sfxSource.PlayOneShot(hoverSound);
        }
    }

    //กดปุ่ม
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            sfxSource.PlayOneShot(clickSound);
        }
    }
}