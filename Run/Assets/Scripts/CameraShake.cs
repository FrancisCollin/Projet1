using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Francis Collin, 1738286
/// Lorsqu'il y a suffisamment de basse fréquences dans la musique la caméra bouge aléatoirement pour créer un effet de "Glitch".
/// </summary>
public class CameraShake : MonoBehaviour
{
    void Update()
    {
        //Détection de la basse
        float[] spectrum = new float[256];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        float highestBass = 0;
        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            if (spectrum[i] > highestBass)
            {
                highestBass = spectrum[i];
            }
        }

        //Quand que la basse à une valeur de 30%
        if (highestBass >= 0.3f)
        {
            StartCoroutine(Shake(.001f, .1f));
        }

    }

    /// <summary>
    /// Méthode qui fait shaker la caméra aléatoirement
    /// </summary>
    /// <param name="duration"> la durée de l'effet </param>
    /// <param name="magnitude"> La force de l'effet </param>
    /// <returns> Un temps d'attente </returns>
    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.y);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
