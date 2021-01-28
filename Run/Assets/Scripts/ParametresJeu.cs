using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametresJeu : MonoBehaviour
{
    public float vitesse = 1f;
    public GameObject joueur;
    private Animator animJoueur;
    // Start is called before the first frame update
    void Start()
    {
        animJoueur = joueur.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animJoueur.SetFloat("vitesse", vitesse);
    }
}
