using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin perlinNoise;

    public float duration = 0.5f;
    public float amplitude = 5f;
    public float frequency = 5f;

    private bool isShaking = false;

    void Awake()
    {
        // Get the Perlin noise component
        perlinNoise = GetComponent<CinemachineBasicMultiChannelPerlin>();

        if (perlinNoise == null)
        {
            Debug.LogError("CinemachineBasicMultiChannelPerlin not found on " + gameObject.name);
        }
    }

    void Start()
    {
        // Initialize shake to 0 so it doesn't shake by default
        if (perlinNoise != null)
        {
            perlinNoise.AmplitudeGain = 0f;
            perlinNoise.FrequencyGain = 0f;
        }
    }

    private IEnumerator ShakeCoroutine(float duration, float amplitude, float frequency)
    {
        if (perlinNoise != null)
        {
            isShaking = true;

            // Set the shake intensity
            perlinNoise.AmplitudeGain = amplitude;
            perlinNoise.FrequencyGain = frequency;

            // Wait for the duration
            yield return new WaitForSeconds(duration);

            // Reset shake
            perlinNoise.AmplitudeGain = 0f;
            perlinNoise.FrequencyGain = 0f;

            isShaking = false;
        }
    }

    public void StartShaking()
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeCoroutine(duration, amplitude, frequency));
            AudioManager.Instance.PlayExplosion();
        }
    }

    public void StartShaking(float customDuration, float customAmplitude, float customFrequency)
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeCoroutine(customDuration, customAmplitude, customFrequency));
        }
    }
}