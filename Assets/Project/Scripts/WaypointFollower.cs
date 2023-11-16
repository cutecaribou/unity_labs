using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int curPointIdx = 0;
    [SerializeField] float speed = 3f;

    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[curPointIdx].transform.position) < .1f)
        {
            curPointIdx++;

            if (curPointIdx >= waypoints.Length)
            {
                curPointIdx = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[curPointIdx].transform.position, speed * Time.deltaTime); 
    }
}
