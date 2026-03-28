using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ComicController : MonoBehaviour
{
    public Image comicImage; // ช่องแสดงภาพ
    public Sprite[] comics;  // รูปทั้งหมด

    public AudioSource music; // เสียง
    public string Credit;

    int currentIndex = 0;

    void OnEnable() // จะทำงานตอนเปิดหน้า comic (ซึ่ง WinManager จะเป็นคนสั่งเปิดให้)
    {
        if (music != null)
        {
            music.Play(); // เล่นเพลง
        }

        currentIndex = 0;
        ShowComic();
    }

    public void NextComic()
    {
        currentIndex++;

        if (currentIndex < comics.Length)
        {
            ShowComic();
        }
        else
        {
            Debug.Log("จบแล้ว! กำลังไปหน้า Credit");
            
            // --- สิ่งที่เพิ่มเข้ามา: ต้องคืนค่าเวลาเป็น 1 ก่อนโหลดฉากใหม่ เผื่อหน้า Credit มีอนิเมชัน ---
            Time.timeScale = 1f; 
            
            SceneManager.LoadScene(Credit);
        }
    }

    void ShowComic()
    {
        // แอบเพิ่มการเช็คกันเหนียวให้ครับ เผื่อลืมลากรูปใส่ช่อง จะได้ไม่ Error
        if (comicImage != null && comics.Length > 0 && currentIndex < comics.Length)
        {
            comicImage.sprite = comics[currentIndex];
        }
    }

    void OnDisable()
    {
        if (music != null)
        {
            music.Stop();
        }
    }
}