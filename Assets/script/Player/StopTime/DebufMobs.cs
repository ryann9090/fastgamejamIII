using UnityEngine;
using System.Collections;

public class DebufMobs : MonoBehaviour
{
    public float debuffDuration = 5f;
    public float debuffPercent = 0.75f;
    private static int activeStacks = 0;

    public ParticleSystem particleSystemPrefab; 

    void Start()
    {
        if (particleSystemPrefab != null)
        {
            ParticleSystem ps = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity, transform);
            var main = ps.main;
            main.startColor = Color.blue; 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            StartCoroutine(ApplyDebuff());
            Destroy(gameObject);
        }
    }

    IEnumerator ApplyDebuff()
    {
        activeStacks++;

        MobMovement[] mobs = Object.FindObjectsByType<MobMovement>(FindObjectsSortMode.None);
        foreach (MobMovement mob in mobs)
        {
            mob.speed *= (1f - debuffPercent);
        }

        yield return new WaitForSeconds(debuffDuration);

        activeStacks--;

        if (activeStacks <= 0)
        {
            foreach (MobMovement mob in mobs)
            {
                mob.speed /= (1f - debuffPercent);
            }
            activeStacks = 0;
        }
    }
}