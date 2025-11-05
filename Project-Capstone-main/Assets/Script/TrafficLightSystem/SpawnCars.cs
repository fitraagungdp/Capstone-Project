using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCars : MonoBehaviour
{
    public int carSpawn;
    public GameObject[] car;
    [SerializeField] private List<SpawnCars> spawnCars;
    void Start()
    {
        spawnCars = new List<SpawnCars>();

        for (int i = 0; i < carSpawn; i++)
        {
            foreach (GameObject go in car)
            {
                spawnCars.Add(Instantiate(go, transform.position, Quaternion.identity).GetComponent<SpawnCars>());
            }

        }
    }
}