using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Üretim Ayarlarý")]
    public GameObject obstaclePrefab;
    public float spawnInterval = 1.5f; // Kaç saniyede bir engel çýkacak?
    public float spawnWidth = 2.2f; // X ekseninde engellerin çýkabileceði maksimum geniþlik

    private float timer;

    void Update()
    {
        if (!MagnetController.isGameStarted) return;
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }

    }

    void SpawnObstacle()
    {
        // X ekseninde, sol ve sað sýnýrlarýmýz içinde rastgele bir nokta belirliyoruz
        float randomX = Random.Range(-spawnWidth, spawnWidth);

        // Z eksenini 0f olarak sabitliyoruz ki kameranýn (Z: -10) önünde, tam sahnede (Z: 0) çýksýn.
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0f);

        // Engeli sahnede oluþtur
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}