using UnityEngine;
using UnityEngine.SceneManagement; // Sahneleri yönetmek (yeniden baþlatmak) için kütüphane

public class PlayerCollision : MonoBehaviour
{
    // Objemiz bir "Trigger" alanýna girdiðinde bu fonksiyon otomatik çalýþýr
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarptýðýmýz objenin etiketi "Obstacle" ise
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // Þimdilik test için konsola yazdýrýyoruz
        Debug.Log("Engele Çarptýn! Oyun Yeniden Baþlýyor...");

        // Oyunu Yeniden Baþlat (Aktif olan sahneyi tekrar yükler)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}