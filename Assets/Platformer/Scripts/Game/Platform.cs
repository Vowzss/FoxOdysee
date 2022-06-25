using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float platformSpeed;
    public Transform WaypointsList;

    [SerializeField] private Transform[] waypoint;
    private Transform startPoint;
    private int endPoint = 1;

    void Start()
    {
        waypoint = WaypointsList.GetComponentsInChildren<Transform>();
        startPoint = waypoint[1];
    }

    void Update()
    {
        Vector3 dir = startPoint.transform.position - transform.position;
        transform.Translate(dir.normalized * platformSpeed * Time.deltaTime * Time.timeScale, Space.World);

        if (Vector3.Distance(transform.position, startPoint.transform.position) < 0.3f)
        {
            endPoint = (endPoint + 1) % waypoint.Length;
            startPoint = waypoint[endPoint];
        }
    }
}
