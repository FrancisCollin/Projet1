using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Francis Collin, 1738286
/// Permet au joueur de se déplacer sur les colonnes Gauche, Centre et Droite, puis lui permet aussi de sauter
/// </summary>
public class MouvementJoueur : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpVelocity = 5f;

    public GameObject RightHandLight;
    public GameObject LeftHandLight;
    public ParticleSystem particleJump;

    public AudioClip[] wooshs;
    public AudioClip jumpSound;



    private Vector3 gauche;
    private Vector3 centre;
    private Vector3 droite;
    private Vector3 destination;

    private Rigidbody rb;

    private bool jumping = false;
    private bool inAir = false;

    private AudioSource sound;
    


    // Start is called before the first frame update
    void Start()
    {
        gauche = GameObject.Find("Gauche").transform.position;
        centre = GameObject.Find("Centre").transform.position;
        droite = GameObject.Find("Droite").transform.position;

        rb = GetComponent<Rigidbody>();

        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        EffetJump();
        Mouvements();
    }

    /// <summary>
    /// Méthode qui gère les déplacements à effectuer
    /// </summary>
    private void Mouvements()
    {
        //Si le joueur à terminer son jump
        if (rb.velocity.y < 0)
        {
            jumping = false;
        }

        //Si le joueur n'est pas en train de sauter
        if (!jumping)
        {
            //S'il n'est pas à sa destination, se dirige vers celle-ci
            if (!CloseEnoughForMe(transform.position.x, destination.x, 0.1f))
            {
                rb.velocity = (destination - transform.position).normalized * moveSpeed;
            }
            else
            {
                rb.velocity = new Vector3(0, 0, 0);
            }

        }

        if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKey("d") && Input.GetKey("space")) || (Input.GetKey(KeyCode.RightArrow) && Input.GetKey("space")))
        {
            DeplacementDroite();
        }

        if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKey("a") && Input.GetKey("space")) || (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey("space")))
        {
            DeplacementGauche();
        }

        if ((Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) && inAir == false)
        {
            Jump();
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    /// <summary>
    /// Déplace le joueur vers la droite
    /// </summary>
    private void DeplacementDroite()
    {
        jumping = false;
        if (CloseEnoughForMe(transform.position.x, centre.x, 0.1f))
        {
            destination.x = droite.x;
            PlayWoosh();
        }
        else if (CloseEnoughForMe(transform.position.x, gauche.x, 0.1f))
        {
            PlayWoosh();
            destination.x = centre.x;
        }
    }

    /// <summary>
    /// Déplace le joueur vers la gauche
    /// </summary>
    private void DeplacementGauche()
    {
        jumping = false;
        if (CloseEnoughForMe(transform.position.x, centre.x, 0.1f))
        {
            destination.x = gauche.x;
            PlayWoosh();
        }
        else if (CloseEnoughForMe(transform.position.x, droite.x, 0.1f))
        {
            destination.x = centre.x;
            PlayWoosh();
        }
    }

    /// <summary>
    /// Fait sauter le joueur
    /// </summary>
    private void Jump()
    {
        jumping = true;
        rb.velocity = Vector3.up * jumpVelocity;

        sound.clip = jumpSound;
        sound.Play();
    }

    /// <summary>
    /// Gère les effets du saut du joueur
    /// </summary>
    private void EffetJump()
    {
        //Si le joueur est dans les airs
        if (transform.position.y > 0.05f)
        {
            inAir = true;
            RightHandLight.SetActive(true);
            LeftHandLight.SetActive(true);
            particleJump.Play();
        }
        //Sinon
        else
        {
            inAir = false;
            RightHandLight.SetActive(false);
            LeftHandLight.SetActive(false);
            particleJump.Stop();
        }
    }

    /// <summary>
    /// méthode qui permet de vérifier si la différence entre deux valeurs est acceptable pour nous
    /// </summary>
    /// <param name="value1"> Première Valeur </param>
    /// <param name="value2"> Deuxième valeur </param>
    /// <param name="acceptableDifference"> L'écart maximum entre les deux valeur </param>
    /// <returns> retoune true si l'écart est acceptable </returns>
    static bool CloseEnoughForMe(float value1, float value2, float acceptableDifference)
    {
        return Math.Abs(value1 - value2) <= acceptableDifference;
    }

    /// <summary>
    /// Joue un son de déplacement aléatoire
    /// </summary>
    void PlayWoosh()
    {
        sound.clip = wooshs[UnityEngine.Random.Range(0, wooshs.Length)];
        sound.Play();
    }
}
