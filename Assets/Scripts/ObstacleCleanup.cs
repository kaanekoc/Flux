using UnityEngine;

public class ObstacleCleanup : MonoBehaviour
{
    private Transform mainCamera;
    public float destroyDistance = 12f; // Kameranýn ne kadar aþaðýsýnda silinecek?

    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        // Eðer engel, kameranýn belirli bir mesafe altýnda kaldýysa kendini yok et
        if (transform.position.y < mainCamera.position.y - destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}