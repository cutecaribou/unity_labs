using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    
    [SerializeField] GameObject[] waypoints;
    int curPointIdx = 0;
    [SerializeField] float speed = 3f;
    private Animator anim;
    Vector3 rotationDest;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    int next_idx() {
        int vnext_idx = curPointIdx + 1;
        Debug.Log("Length: " + waypoints.Length.ToString()); 
        if (vnext_idx >= waypoints.Length)
        {
            vnext_idx = 0;
        }
        Debug.Log("Next: " + vnext_idx.ToString()); 
        return vnext_idx;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[curPointIdx].transform.position, speed * Time.deltaTime);  

        Vector3 lookingDirection = rotationDest - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookingDirection), 5f * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (Vector3.Distance(transform.position, waypoints[curPointIdx].transform.position) < 2f) {
            Debug.Log(Vector3.Distance(transform.position, col.transform.position));
            curPointIdx = next_idx();
            // Debug.Log("Changing points");   
        }
        rotationDest = waypoints[curPointIdx].transform.position;
    }

    void OnTriggerExit(Collider col) {

    }
}
