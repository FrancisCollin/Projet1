using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Francis Collin, 1738286
/// Cette classe génére les terrains et les obstacles du parcourt aléatoirement.
/// </summary>
public class GenParcourt : MonoBehaviour
{
    public Transform[] terrainObj;
    public Transform[] obstaclesObj;

    public List<GameObject> terrains;
    public List<GameObject> obstacles;

    public bool difficile = true;

    
    private Vector3 nextTerrainPos;
    private Vector3 lastTerrainPos;
    private Vector3 nextObstaclePos;

    private int firstTerrainId = 0;
    private int firstObstacleId = 0;

    private int[] rows = new int[] { -1, 0, 1 };

    private ParametresJeu parametres;

    // Start is called before the first frame update
    void Start()
    {
        parametres = GameObject.Find("GameManager").GetComponent<ParametresJeu>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculeNextPos();

        //Si le terrain est derrière le joueur, le supprime et augmente le score de 1
        var firstTerrain = terrains[firstTerrainId];
        if (firstTerrain.transform.position.z <= -3.76f)
        {
            Destroy(firstTerrain);
            firstTerrainId++;
            parametres.score++;
        }

        //Si l'obstacle est derrière le joueur, le supprime
        var firstObstacle = obstacles[firstObstacleId];
        if (firstObstacle.transform.position.z <= -3.76)
        {
            Destroy(firstObstacle);
            firstObstacleId++;
            
        }
    }

    /// <summary>
    /// Fait apparaitre un terrain aléatoire
    /// </summary>
    void SpawnTerrain()
    {
        var terrain = terrainObj[Random.Range(0, terrainObj.Length)];
        var go = Instantiate(terrain, nextTerrainPos, terrain.rotation);
        terrains.Add(go.gameObject);
    }

    /// <summary>
    /// Fait apparaitre un obstacle aléatoire
    /// </summary>
    void SpawnObstacle()
    {
        var obstacle = obstaclesObj[Random.Range(0, obstaclesObj.Length)];
        var go = Instantiate(obstacle, new Vector3(nextObstaclePos.x,obstacle.position.y, nextObstaclePos.z), Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f))));
        obstacles.Add(go.gameObject);
    }

    /// <summary>
    /// Détermine la position du prochain terrain et du/des prochains obstacles
    /// </summary>
    void CalculeNextPos()
    {
        var lastTerrain = terrains[terrains.Count - 1];

        lastTerrainPos.z = lastTerrain.transform.position.z;
        nextTerrainPos.z = lastTerrainPos.z + 3.72f;

        nextObstaclePos.z = lastTerrainPos.z + Random.Range(-0.2f, 1.86f);
        nextObstaclePos.x = rows[Random.Range(0, 3)];

        //Détermine si la disposition des obstacles sera difficile ou facile
        if (Random.Range(0, 2) == 1)
        {
            difficile = true;
        }
        else
        {
            difficile = false;
        }

        if (lastTerrainPos.z < 50f) //15.04f
        {
            SpawnTerrain();
            SpawnObstacle();

            //Si difficle, fait apparaître un deuxième obstacle
            if (difficile)
            {
                var rd = Random.Range(0, 2);

                //Si le premier obstacle est au CENTRE
                if (nextObstaclePos.x == 0)
                {
                    
                    if (rd == 0)
                    {
                        nextObstaclePos.x = -1;
                    }
                    else
                    {
                        nextObstaclePos.x = 1;
                    }
                }

                //Si le premier obstacle est à GAUCHE
                else if (nextObstaclePos.x == -1)
                {
                    if (rd == 0)
                    {
                        nextObstaclePos.x = 0;
                    }
                    else
                    {
                        nextObstaclePos.x = 1;
                    }
                }

                //Si le premier obstacle est à DROITE
                else
                {
                    if (rd == 0)
                    {
                        nextObstaclePos.x = 0;
                    }
                    else
                    {
                        nextObstaclePos.x = -1;
                    }
                }

                SpawnObstacle();
            }
        }
    }
}
