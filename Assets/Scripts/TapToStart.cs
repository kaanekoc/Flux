using UnityEngine;
using UnityEngine.SceneManagement;

public class TapToStart : MonoBehaviour
{
    [Header("Sahne Ayarlarý")]
    public string gameSceneName = "SampleScene"; // Asýl oyun sahnenin adýný buraya yaz (Örn: SampleScene)

    void Update()
    {
        // Farenin sol tuþuna basýldýðýnda VEYA telefonda ekrana dokunulduðunda
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        Debug.Log("Oyun Yükleniyor...");
        // Belirtilen sahneyi yükle
        SceneManager.LoadScene(gameSceneName);
    }
}