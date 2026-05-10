using UnityEngine;
using System.Collections;

public class MagnetController : MonoBehaviour
{
    public static bool isGameStarted = false;

    [Header("Mıknatıs Referansları")]
    public Transform leftMagnet;
    public Transform rightMagnet;

    private Vector3 leftMagnetStartPos;
    private Vector3 rightMagnetStartPos;

    [Header("Fizik Ayarları")]
    public float pullForce = 15f;
    public float upSpeed = 5f;

    [Header("Yapışma Ayarları")]
    public float stickTime = 2f;
    public float throwForce = 15f;
    private bool isStuck = false;

    [Header("Efekt Ayarları")]
    public float shakeIntensity = 0.08f;
    public AudioClip magnetSound;
    private AudioSource audioSource;

    private Rigidbody2D rb;

    // Kontrol durumlarını tutacağımız değişkenler
    private bool isPressing = false;
    private bool isPressingLeft = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        leftMagnetStartPos = leftMagnet.localPosition;
        rightMagnetStartPos = rightMagnet.localPosition;

        isGameStarted = false;
    }

    void Update()
    {
        if (isStuck) return;

        // Ekranın neresine basıldığını algıla
        CheckInput();

        // Ekrana ilk dokunuşta oyunu başlat (Sağ veya sol fark etmez)
        bool isTap = Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
        if (isTap)
        {
            if (!isGameStarted)
            {
                isGameStarted = true;
            }

            if (magnetSound != null)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(magnetSound);
            }
        }

        HandleMagnetShake();
    }

    // Ekranın sağına mı soluna mı basıldığını hesaplayan fonksiyon
    void CheckInput()
    {
        isPressing = false;
        isPressingLeft = false;

        // Bilgisayar faresi için kontrol
        if (Input.GetMouseButton(0))
        {
            isPressing = true;
            isPressingLeft = Input.mousePosition.x < Screen.width / 2f;
        }
        // Mobil dokunmatik için kontrol
        else if (Input.touchCount > 0)
        {
            isPressing = true;
            isPressingLeft = Input.GetTouch(0).position.x < Screen.width / 2f;
        }
    }

    void HandleMagnetShake()
    {
        if (!isGameStarted) return;

        if (isPressing)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-shakeIntensity, shakeIntensity), Random.Range(-shakeIntensity, shakeIntensity), 0f);

            if (isPressingLeft)
            {
                // Ekranın soluna basılıyorsa sol mıknatısı titret
                leftMagnet.localPosition = leftMagnetStartPos + randomOffset;
                rightMagnet.localPosition = rightMagnetStartPos;
            }
            else
            {
                // Ekranın sağına basılıyorsa sağ mıknatısı titret
                rightMagnet.localPosition = rightMagnetStartPos + randomOffset;
                leftMagnet.localPosition = leftMagnetStartPos;
            }
        }
        else
        {
            // Hiçbir yere basılmıyorsa iki mıknatıs da sakin dursun
            leftMagnet.localPosition = leftMagnetStartPos;
            rightMagnet.localPosition = rightMagnetStartPos;
        }
    }

    void FixedUpdate()
    {
        // === OYUN BAŞLAMADIYSA TAMAMEN DONUK BEKLE ===
        if (!isGameStarted)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, upSpeed);

        if (isStuck) return;

        Transform targetMagnet;

        if (isPressing)
        {
            // Ekranın basılan tarafına göre hedefi belirle
            targetMagnet = isPressingLeft ? leftMagnet : rightMagnet;
        }
        else
        {
            // Hiçbir yere basılmıyorsa en yakın mıknatısı bul
            float distToLeft = Vector2.Distance(transform.position, leftMagnet.position);
            float distToRight = Vector2.Distance(transform.position, rightMagnet.position);

            targetMagnet = (distToLeft < distToRight) ? leftMagnet : rightMagnet;
        }

        float distanceX = targetMagnet.position.x - transform.position.x;
        Vector2 pullDirection = new Vector2(Mathf.Sign(distanceX), 0);
        rb.AddForce(pullDirection * pullForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGameStarted) return;

        if (!isStuck && (collision.gameObject.name == "LeftMagnet" || collision.gameObject.name == "RightMagnet"))
        {
            StartCoroutine(StickAndThrowRoutine(collision.gameObject.name));
        }
    }

    IEnumerator StickAndThrowRoutine(string magnetName)
    {
        isStuck = true;

        // Yapıştığında Y hızını sıfırlamak yerine mevcut Y hızını koruyoruz ki kameradan aşağı düşmesin
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        yield return new WaitForSeconds(stickTime);

        isStuck = false;

        float throwDirection = (magnetName == "LeftMagnet") ? 1f : -1f;
        rb.AddForce(new Vector2(throwDirection * throwForce, 0f), ForceMode2D.Impulse);
    }
}