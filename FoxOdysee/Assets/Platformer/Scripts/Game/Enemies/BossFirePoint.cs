using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFirePoint : MonoBehaviour
{
    BossManager bossManager;
    public Transform firePoint;

    private void Awake()
    {
        bossManager = GameObject.FindObjectOfType<BossManager>();
    }

    private void Update()
    {
        transform.position = firePoint.transform.position;
    }
}
