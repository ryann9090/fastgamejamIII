using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float speed = 15f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        
        if (move != 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
    }
}