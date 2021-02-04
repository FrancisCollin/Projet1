using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public bool colorChange = false;
    public bool multipleLightSource = false;
    public Transform[] lights;

    private float timeDelay;
    private Color32 couleur;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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


        if (highestBass >= 0.3f)
        {
            StartCoroutine(FlickeringLight());
        }

    }

    IEnumerator FlickeringLight()
    {
        if (!multipleLightSource)
        {
            GetComponent<Light>().enabled = false;
            timeDelay = Random.Range(0.01f, 0.2f);
            yield return new WaitForSeconds(timeDelay);

            if (colorChange)
            {
                couleur = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f)
                    );
                GetComponent<Light>().color = couleur;
            }

            GetComponent<Light>().enabled = true;
            timeDelay = Random.Range(0.01f, 0.2f);
            yield return new WaitForSeconds(timeDelay);
        }
        else
        {
            if (colorChange)
            {
                couleur = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f)
                    );

                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].GetComponent<Light>().enabled = false;
                    timeDelay = Random.Range(0.01f, 0.2f);
                    yield return new WaitForSeconds(timeDelay);

                    lights[i].GetComponent<Light>().color = couleur;

                    lights[i].GetComponent<Light>().enabled = true;
                    timeDelay = Random.Range(0.01f, 0.2f);
                    yield return new WaitForSeconds(timeDelay);
                }
            }

            
        }
    }
}
