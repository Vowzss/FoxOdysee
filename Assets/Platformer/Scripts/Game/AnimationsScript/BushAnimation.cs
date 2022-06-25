using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushAnimation : MonoBehaviour
{
    [Header("Bush UI/UX")]
    [SerializeField] private float offset;
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("offset", Random.Range(0.1f, 1.0f));
        offset = animator.GetFloat("offset");
    }
}
