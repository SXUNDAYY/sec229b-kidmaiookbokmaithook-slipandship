using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ComicController : MonoBehaviour
{
    public Image comicImage;// ช่องแสดงภาพ
    public Sprite[] comics;// รูปทั้งหมด

    public AudioSource music; //เสียง
    public string Credit;

    int currentIndex = 0;

    void OnEnable() //จะทำงานตอนเปิดหน้า comic
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
            Debug.Log("จบแล้ว!");
            SceneManager.LoadScene(Credit);
        }
    }

    void ShowComic()
    {
        comicImage.sprite = comics[currentIndex];
    }
    void OnDisable()
    {
        if (music != null)
        {
            music.Stop();
        }
    }
}