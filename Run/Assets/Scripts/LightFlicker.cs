using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Francis Collin, 1738286
/// Lorsqu'il y a suffisamment de basse fréquences dans la musique, les lumières qui possède se scripte vont flasher et changer de couleur aléatoirement.
/// </summary>
public class LightFlicker : MonoBehaviour
{
    public bool colorChange = false; //Activer le changement de couleur
    public bool multipleLightSource = false; //À activer si on à plusieur lumières à faire marcher enssemble
    public Transform[] lights;

    private float timeDelay;
    private Color32 couleur;

    // Update is called once per frame
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
            StartCoroutine(FlickeringLight());
        }

    }

    /// <summary>
    /// Méthode qui fait flasher la lumière
    /// </summary>
    /// <returns> Un temps d'attente </returns>
    IEnumerator FlickeringLight()
    {
        if (!multipleLightSource)
        {
            //Éteint la lumière
            GetComponent<Light>().enabled = false;
            timeDelay = Random.Range(0.01f, 0.2f);
            yield return new WaitForSeconds(timeDelay);


            if (colorChange)
            {
                //Donne une couleur aléatoire à la lumière
                couleur = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f)
                    );
                GetComponent<Light>().color = couleur;
            }

            //Allume la lumière
            GetComponent<Light>().enabled = true;
            timeDelay = Random.Range(0.01f, 0.2f);
            yield return new WaitForSeconds(timeDelay);
        }

        //S'il y a plusieur lumières
        else
        {
            if (colorChange)
            {
                //Donne une couleur aléatoire à la lumière
                couleur = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f)
                    );

                //Pour chaque lumière
                for (int i = 0; i < lights.Length; i++)
                {
                    //Éteint la lumière
                    lights[i].GetComponent<Light>().enabled = false;
                    timeDelay = Random.Range(0.01f, 0.2f);
                    yield return new WaitForSeconds(timeDelay);

                    //Change la couleur de la lumière
                    lights[i].GetComponent<Light>().color = couleur;

                    //Allume la lumière
                    lights[i].GetComponent<Light>().enabled = true;
                    timeDelay = Random.Range(0.01f, 0.2f);
                    yield return new WaitForSeconds(timeDelay);
                }
            }

            
        }
    }
}
