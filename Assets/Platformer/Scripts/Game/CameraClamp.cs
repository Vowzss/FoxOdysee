using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    [SerializeField] public GameObject obj;
    public float offset;
    public Vector3 posOffset;
    private Vector3 velocity;

    private void Awake()
    {
        obj = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, obj.transform.position + posOffset, ref velocity, offset);
    }
}
