using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Takip Ayarlarý")]
    public Transform target; // Takip edilecek top
    public float yOffset = 3f; // Kameranýn topa göre Y eksenindeki ofseti

    void LateUpdate()
    {
        if (target != null)
        {
            // Kameranýn X ve Z pozisyonu sabit kalýr, sadece Y ekseninde topu takip eder
            Vector3 newPosition = new Vector3(transform.position.x, target.position.y + yOffset, transform.position.z);
            transform.position = newPosition;
        }
    }
}