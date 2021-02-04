using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Francis Collin, 1738286
/// Déplace les terrains et les obstacles vers le joueur
/// </summary>
public class MouvementParcourt : MonoBehaviour
{
    private ParametresJeu parametres;

    /// <summary>
    /// Appelé quand l'objet est instantié
    /// </summary>
    private void Awake()
    {
        parametres = GameObject.Find("GameManager").GetComponent<ParametresJeu>();
    }

    // Update is called once per frame
    void Update()
    {
        //Bouge dans la direction -z
        transform.Translate(Vector3.back * Time.deltaTime * parametres.vitesse, Space.World);
    }
}
