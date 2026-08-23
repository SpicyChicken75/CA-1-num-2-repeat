using UnityEngine;

public class Collectable : MonoBehaviour
{
    public static int collected = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collected++;
            Destroy(gameObject);
        }
    }
}