using UnityEngine;

public class Drop_XP : MonoBehaviour
{
    public static void Drop(int amount, Vector3 position, GameObject xpPrefab, int orbValue)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            GameObject orb = Object.Instantiate(xpPrefab, position + offset, Quaternion.identity);

            XPORB xpOrb = orb.GetComponent<XPORB>();
            if (xpOrb != null)
            {
                xpOrb.xpValue = orbValue; 
            }
        }
    }
}