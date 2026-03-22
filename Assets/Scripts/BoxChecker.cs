using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxChecker : MonoBehaviour
{
    [Header("Settings")]
    public string playerTag = "Player"; // ชื่อ Tag ของรถ
    public float restartDelay = 1.5f;   // เวลาหน่วงก่อนเริ่มใหม่
    public float fallThreshold = -5f;  // ถ้าตกเหว (Y ต่ำกว่า -5) ก็ให้แพ้ด้วย

    private bool hasLost = false;

    void Update()
    {
        // เช็คกันเหนียว: ถ้ากล่องตกเหวโดยไม่ชนอะไรเลย
        if (transform.position.y < fallThreshold && !hasLost)
        {
            GameOver("Box fell off the world!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasLost) return;

        // ถ้ากล่องชนกับวัตถุที่ "ไม่ใช่รถ" (Player)
        if (!collision.gameObject.CompareTag(playerTag))
        {
            GameOver("Box touched: " + collision.gameObject.name);
        }
    }

    void GameOver(string reason)
    {
        hasLost = true;
        Debug.Log("Game Over: " + reason);

        // โหลดฉากปัจจุบันใหม่
        Invoke("RestartLevel", restartDelay);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}