using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PATH
{
    Path, Path2, Path3, Path4, Path5
}
public class PathFinding : MonoBehaviour
{
    public static PathFinding Instance;
    [Header("PATH SETTING :")]
    public PATH pathType;
    private NavMeshAgent agent;
    [SerializeField] private GameObject PATH;
    [SerializeField] private Transform[] pathPoint;
    public int maxPath;

    [Header("PATH DISTANCE UPDATE :")]
    public float minDistance = 10;

    [Header("CAR SETTING :")]
    public float speed = 10;

    [SerializeField] private int index;


    
    private void Awake()
    {
        Instance = this;
        PATH = GameObject.Find(pathType.ToString());
    }
    void Start()
    {
        //pengambilan component GameObject
        agent = GetComponent<NavMeshAgent>();

        //membuat array sesuai jumlah child GameObject
        pathPoint = new Transform[PATH.transform.childCount];

        //proses looping untuk memasukan child GameObject kedalam Array
        for(int i = 0; i < pathPoint.Length; i++)
        {
            pathPoint[i] = PATH.transform.GetChild(i);
        }

        //agent.transform.position = pathPoint[index - 1].position;
    }

    void Update()
    {
        roam();
    }

    private void roam()
    {
        //Mengecek posisi mobil apakah sudah berada dalam posisi GameObject "PATH"
        if(Vector3.Distance(transform.position, pathPoint[index].position) < minDistance)
        {
            //Mengecek apakah jumlah index tidak sama dengan jumlah total array "pathPoint"
            if(index + 1 != pathPoint.Length)
            {
                index += 1;
            } else if (pathPoint.Length - 1 == index)
            {
                index = 0;
            }
        }

        //menggerakan mobil ke path selanjutnya
        agent.SetDestination(pathPoint[index].position);
    }

    public void StopVehicle()
    {
        agent.speed = 0;
        agent.isStopped = true;
    }

    public void StartVehicle()
    {
        agent.speed = speed;
        agent.isStopped = false;
    }
}
