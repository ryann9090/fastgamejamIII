using UnityEngine;

public class PlayerMovementAnimation : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);
    }
}