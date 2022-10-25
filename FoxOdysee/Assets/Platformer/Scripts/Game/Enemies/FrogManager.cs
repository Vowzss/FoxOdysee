using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FrogManager : MonoBehaviour
{
    [Header("Enemy Attributes")]
    private KillFrog killFrog;

    [Header("Enemy Movement")]
    public float enemySpeed;
    private bool isFlip = false;

    public Transform getWaypointsList;
    private Transform[] waypointList;
    private Transform startPoint;
    private int endPoint = 0;
    private void Awake()
    {
        killFrog = GameObject.FindObjectOfType<KillFrog>();
        waypointList = getWaypointsList.GetComponentsInChildren<Transform>();
        startPoint = waypointList[0];
    }

    void Update()
    {
        if (!killFrog.isDead)
        {
            Vector3 dir = startPoint.transform.position - transform.position;
            transform.Translate(dir.normalized * enemySpeed * Time.deltaTime * Time.timeScale, Space.World);

            if (Vector3.Distance(transform.position, startPoint.transform.position) < 0.3f)
            {
                endPoint = (endPoint + 1) % waypointList.Length;
                startPoint = waypointList[endPoint];
            }

            if (startPoint.position == waypointList[1].position && isFlip)
                Flip();
            else if (startPoint.position == waypointList[0].position && !isFlip)
                Flip();
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFlip = !isFlip;
    }
}
