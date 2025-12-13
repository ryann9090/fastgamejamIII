using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 12f;
    private int damage;

    public void SetBuffedDamage(int dmg)
    {
        damage = dmg;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        MobHealth mob = other.GetComponent<MobHealth>();
        if (mob != null)
        {
            mob.TakeDamage(damage, gameObject);
            Destroy(gameObject);
        }
    }
}