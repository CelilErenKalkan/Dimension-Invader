using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;
using System.Collections;
using Cinemachine.PostFX;

public class BloomController : MonoBehaviour
{
    // Kontrol edeceğimiz Cinemachine uzantısı
    public CinemachinePostProcessing postProcessingExtension;

    private Bloom targetBloom;

    [Header("Eşik Değerleri")]
    [Tooltip("Sadece sparksların parladığı yüksek değer")]
    public float highThreshold = 1.2f;

    [Tooltip("Skybox'ın da parlamaya başladığı düşük değer")]
    public float lowThreshold = 0.9f;

    [Header("Zamanlama Ayarları")]
    public float pulseDuration = 1.5f; // Parlamanın toplam süresi
    public float waitDuration = 5f;    // Parlamalar arası bekleme süresi

    void Start()
    {
        // Referansların doğru atandığından emin ol
        if (postProcessingExtension == null || postProcessingExtension.m_Profile == null)
        {
            Debug.LogError("Cinemachine Post Processing veya Profili atanmamış!", this);
            return;
        }

        // Profilin içindeki Bloom'u bul
        postProcessingExtension.m_Profile.TryGetSettings(out targetBloom);

        if (targetBloom != null)
        {
            StartCoroutine(PulseThreshold());
        }
        else
        {
            Debug.LogError("Atanan profilde Bloom efekti bulunamadı!", this);
        }
    }

    private IEnumerator PulseThreshold()
    {
        while (true)
        {
            // BEKLEME AŞAMASI
            targetBloom.threshold.value = highThreshold; // Threshold'u yüksek tut (sadece sparkslar parlar)
            yield return new WaitForSeconds(waitDuration);

            // PARLAMA AŞAMASI
            float halfPulse = pulseDuration / 2f;
            // Threshold'u yavaşça düşür (Skybox parlamaya başlar)
            yield return StartCoroutine(FadeThreshold(highThreshold, lowThreshold, halfPulse));

            // Threshold'u yavaşça geri yükselt (Skybox'ın parlaması durur)
            yield return StartCoroutine(FadeThreshold(lowThreshold, highThreshold, halfPulse));
        }
    }

    private IEnumerator FadeThreshold(float start, float end, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            targetBloom.threshold.value = Mathf.Lerp(start, end, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        targetBloom.threshold.value = end;
    }
}