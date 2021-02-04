using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Francis Collin, 1738286
/// Classe qui permet de contrôler le jeu et ses paramètres.
/// </summary>
public class ParametresJeu : MonoBehaviour
{
    public float vitesse = 0f;
    public float vitesseDepart = 3f;
    public float score = 0;

    public GameObject joueur;

    public Text scoreText;
    public Text scoreFinalText;

    public AudioClip[] musiques; //Les musiques à jouer pendant la partie
    public AudioClip musiqueMenu; //musique du menu

    public GameObject menuPrincipale;
    public GameObject menuScore;
    public GameObject menuGameOver;

    private Animator animJoueur;
    private AudioSource musique;
    private static bool restart = false;
   
    // Start is called before the first frame update
    void Start()
    {
        animJoueur = joueur.GetComponent<Animator>();
        musique = GetComponent<AudioSource>();

        //Si le joueur recommence n'affiche pas le menu principale
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
            //Augmente la vitesse selon le score
            vitesse = vitesseDepart + ((score - vitesseDepart) / 10f);
        }

        //Si le joueur meurt
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

    /// <summary>
    /// Commence la partie
    /// </summary>
    public void Commencer()
    {
        score = 0;
        vitesse = vitesseDepart;
        musique.clip = musiques[Random.Range(0, musiques.Length)];
        musique.Play();
    }

    /// <summary>
    /// Recommence une partie
    /// </summary>
    public void Recommencer()
    {
        restart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Quitte le jeu
    /// </summary>
    public void Quitter()
    {
        Application.Quit();
    }
}
