using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    float offset = 0.0f;
    public void Awake()
    {
        offset = Random.Range(0.0f, 1000.0f);
    }

    public void Begin(int damage)
    {
        Begin(0.1f + (0.05f * damage), 16.0f, 0.1f, 0.1f + (0.05f * damage));
    }

    public void Begin(float amplitude, float frequency, float duration1, float duration2)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine(amplitude, frequency, duration1, duration2));
    }

    public IEnumerator ShakeCoroutine(float amplitude, float frequency, float duration1, float duration2)
    {
        for (float currentTime = 0.0f; currentTime <= duration1; currentTime += Time.deltaTime)
        {
            float currentAmplitude = amplitude * (currentTime / duration1);
            DoShake(currentAmplitude, (Time.time * frequency) + offset);

            yield return null;
        }
        for (float currentTime = 0.0f; currentTime <= duration2; currentTime += Time.deltaTime)
        {
            float currentAmplitude = amplitude * Mathf.Pow(1 - (currentTime / duration2), 2);
            DoShake(currentAmplitude, (Time.time * frequency) + offset);

            yield return null;
        }
    }

    void DoShake(float scale, float time)
    {
        float xRandom = Mathf.PerlinNoise(0.0f, time);
        float yRandom = Mathf.PerlinNoise(1.0f, time);
        float zRandom = Mathf.PerlinNoise(2.0f, time);
        transform.localPosition = new Vector3(xRandom, yRandom, zRandom) * scale;
    }
}
