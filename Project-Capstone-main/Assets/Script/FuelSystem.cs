using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSystem : MonoBehaviour
{
    public GameObject fuelPath;
    public Transform[] pathPoint;
    [SerializeField] private int maxPath;
    void Start()
    {
        fuelPath = GameObject.Find("FuelPath");
        pathPoint = new Transform[fuelPath.transform.childCount];

        for(int i = 0; i < pathPoint.Length; i++)
        {
            pathPoint[i] = fuelPath.transform.GetChild(i);
        }
    }

    public void GetPosition()
    {
        int randomPosition = Random.Range(1, maxPath);

        transform.position = pathPoint[randomPosition].position;
    }
}
