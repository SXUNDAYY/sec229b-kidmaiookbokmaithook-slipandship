using UnityEngine;
using UnityEngine.UI;

public class ComicController : MonoBehaviour
{
    public Image comicImage;      // ช่องแสดงภาพ
    public Sprite[] comics;       // รูปทั้งหมด

    int currentIndex = 0;

    void Start()
    {
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
            Debug.Log("จบแล้ว!");
            gameObject.SetActive(false); // ปิด panel
        }
    }

    void ShowComic()
    {
        comicImage.sprite = comics[currentIndex];
    }
}