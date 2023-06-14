using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=UlEE6wjWuCY&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=9
public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    [SerializeField] float speed = 1.0f;
    private int waypointIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length > 0)
        {
            // Check if reached next waypoint
            if (Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < 0.01)
            {
                // increment waypoint index
                waypointIndex++;
                // reset index to 0 if it has gone through all waypoints
                if (waypointIndex >= waypoints.Length) waypointIndex = 0;
            }
            // move towards waypoint
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, Time.deltaTime * speed);
        }      
    }
}
