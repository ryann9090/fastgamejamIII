using UnityEngine;

public class Companheiro : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int damage = 10;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;

    private float lastAttackTime = 0f;
    private Transform target;

    void Update()
    {
        // procura inimigo mais próximo
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Mobs");
        float menorDistancia = Mathf.Infinity;
        target = null;

        foreach (GameObject inimigo in inimigos)
        {
            float dist = Vector2.Distance(transform.position, inimigo.transform.position);
            if (dist < menorDistancia)
            {
                menorDistancia = dist;
                target = inimigo.transform;
            }
        }

        
        if (target != null)
        {
            float dist = Vector2.Distance(transform.position, target.position);

            if (dist > attackRange)
            {
                // mover em direção ao inimigo
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    target.position,
                    moveSpeed * Time.deltaTime
                );
            }
            else
            {
                // atacar se cooldown passou
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    MobHealth mob = target.GetComponent<MobHealth>();
                    if (mob != null)
                    {
                        mob.TakeDamage(damage, gameObject);
                        Debug.Log("Companheiro atacou " + target.name + " causando " + damage + " de dano!");
                    }
                    lastAttackTime = Time.time;
                }
            }
        }
    }
}