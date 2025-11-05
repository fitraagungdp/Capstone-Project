using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    public float distance;
    public PathFinding pathFinding;
    public bool isHit;
    public string[] tags;

    void Start()
    {
        pathFinding = GetComponentInParent<PathFinding>();
    }
    void Update()
    {
        HitTarget();
    }

    public void HitTarget()
    {
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, distance);
        
        if (hitInfo.transform)
        {
            for(int i = 0; i < tags.Length; i++)
            {
                if(hitInfo.transform.tag == tags[i])
                {
                    isHit = true;
                    pathFinding.StopVehicle();
                }
            }
        }
        else
        {
            isHit = false;
            pathFinding.StartVehicle();
        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.red);
    }
}
