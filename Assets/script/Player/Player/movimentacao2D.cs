using UnityEngine;

public class movimentacao2D : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;
    private PlayerHealth playerHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (playerHealth == null) return;

        rb.MovePosition(rb.position + movement * playerHealth.moveSpeed * Time.fixedDeltaTime);
    }
}