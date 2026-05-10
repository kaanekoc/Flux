using UnityEngine;
using TMPro; // TextMeshPro'yu kullanmak için gerekli kütüphane

public class ScoreManager : MonoBehaviour
{
    [Header("Referanslar")]
    public Transform player; // Topumuzun pozisyonunu takip edeceðiz
    public TextMeshProUGUI scoreText; // Ekrana yazdýracaðýmýz metin

    private float maxScore = 0f;
    private float startingY;

    void Start()
    {
        // Topun oyuna baþladýðý ilk Y pozisyonunu kaydediyoruz
        if (player != null)
        {
            startingY = player.position.y;
        }
    }

    void Update()
    {
        // Top sahnede var olduðu sürece hesaplama yap
        if (player != null)
        {
            // O anki skor = Topun þu anki Y pozisyonu - Baþlangýç Y pozisyonu
            float currentScore = player.position.y - startingY;

            // Eðer top eski rekorundan daha yükseðe çýktýysa skoru güncelle
            // (Bu kontrol, top geriye düþerse skorun azalmasýný engeller)
            if (currentScore > maxScore)
            {
                maxScore = currentScore;

                // Skoru Mathf.FloorToInt ile yuvarlayarak tam sayý olarak ekrana yazdýrýyoruz
                scoreText.text = Mathf.FloorToInt(maxScore).ToString();
            }
        }
    }
}