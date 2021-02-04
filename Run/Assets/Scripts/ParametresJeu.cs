using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParametresJeu : MonoBehaviour
{
    public float vitesse = 0f;
    public float vitesseDepart = 3f;
    public float score = 0;
    public GameObject joueur;
    public Text scoreText;
    public Text scoreFinalText;
    public AudioClip[] musiques;
    public AudioClip musiqueMenu;
    public GameObject menuPrincipale;
    public GameObject menuScore;
    public GameObject menuGameOver;

    private Animator animJoueur;
    private AudioSource musique;
    static private bool restart = false;
   
    // Start is called before the first frame update
    void Start()
    {
        animJoueur = joueur.GetComponent<Animator>();
        musique = GetComponent<AudioSource>();
        if (restart)
        {
            menuPrincipale.SetActive(false);
            menuScore.SetActive(true);
            Commencer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        animJoueur.SetFloat("vitesse", vitesse);
        scoreText.text = score.ToString();
        if (score >= vitesseDepart && vitesse < 8.6f && !joueur.GetComponent<Collisions>().mort)
        {
            vitesse = vitesseDepart + ((score - vitesseDepart) / 10f);
        }

        if (joueur.GetComponent<Collisions>().mort)
        {
            musique.Stop();
            menuScore.SetActive(false);
            menuGameOver.SetActive(true);
            scoreFinalText.text = score.ToString();
        }

        if (Input.GetKey("escape"))
        {
            Quitter();
        }
    }

    public void Commencer()
    {
        score = 0;
        vitesse = vitesseDepart;
        musique.clip = musiques[Random.Range(0, musiques.Length)];
        musique.Play();
    }

    public void Recommencer()
    {
        restart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quitter()
    {
        Application.Quit();
    }
}
