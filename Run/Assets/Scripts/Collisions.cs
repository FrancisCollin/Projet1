using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Francis Collin, 1738286
/// Gère les collisions du joueur avec les obstacles
/// </summary>
public class Collisions : MonoBehaviour
{
    private ParametresJeu parametres;
    private MouvementJoueur joueur;
    private Rigidbody rb;
    public bool mort = false;

    public AudioClip hurt;
    private AudioSource sound;

    // Start is called before the first frame update
    private void Start()
    {
        parametres = GameObject.Find("GameManager").GetComponent<ParametresJeu>();
        joueur = GetComponent<MouvementJoueur>();
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Quand le joueur entre en collision avec un Collider
    /// </summary>
    /// <param name="collision"> Information sur la collision </param>
    private void OnCollisionEnter(Collision collision)
    {
        //Si le joueur entre en collsion avec un obstacle, il meurt
        if (collision.collider.tag == "Obstacle")
        {
            sound.clip = hurt;
            sound.Play();

            joueur.enabled = false;
            mort = true;

            StartCoroutine("Die");

            
        }
    }

    /// <summary>
    /// Arrête le joueur en place et le fait mourrir
    /// </summary>
    /// <returns> Un temps d'attente </returns>
    IEnumerator Die()
    {
        rb.constraints = RigidbodyConstraints.None;
        yield return new WaitForSeconds(0.01f);
        parametres.vitesse = 0;
    }
}
