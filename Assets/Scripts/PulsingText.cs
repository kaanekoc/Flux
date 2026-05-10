using UnityEngine;
using TMPro; // TextMeshPro kullanacaðýmýz için

public class PulsingText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    [Header("Büyüme/Küçülme Ayarlarý")]
    public float scaleSpeed = 3f; // Ne kadar hýzlý büyüyecek?
    public float scaleAmount = 0.15f; // Ne kadar büyüyecek? (0.15 = %15 büyüme)
    private Vector3 startScale;

    [Header("Görünürlük (Fade) Ayarlarý")]
    public float fadeSpeed = 3f; // Ne kadar hýzlý yanýp sönecek?
    public float minAlpha = 0.3f; // En fazla ne kadar kaybolacak? (0 tamamen görünmez, 1 tam görünür)
    private Color startColor;

    void Start()
    {
        // Metin bileþenini ve ilk deðerleri hafýzaya alýyoruz
        textMesh = GetComponent<TextMeshProUGUI>();
        startScale = transform.localScale;
        startColor = textMesh.color;
    }

    void Update()
    {
        // 1. BÜYÜYÜP KÜÇÜLME: Sinüs dalgasý kullanarak -1 ile 1 arasýnda yumuþak bir ritim oluþturuyoruz
        float scaleMultiplier = 1f + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = startScale * scaleMultiplier;

        // 2. KAYBOLUP GELME (Alpha): Sinüs deðerini 0 ile 1 arasýna sýkýþtýrýp Alpha (Saydamlýk) deðerine uyguluyoruz
        float alphaSine = (Mathf.Sin(Time.time * fadeSpeed) + 1f) / 2f;

        Color newColor = startColor;
        // Saydamlýðý minAlpha ile baþlangýç deðeri arasýnda gidip gelecek þekilde ayarlýyoruz
        newColor.a = Mathf.Lerp(minAlpha, startColor.a, alphaSine);
        textMesh.color = newColor;
    }
}