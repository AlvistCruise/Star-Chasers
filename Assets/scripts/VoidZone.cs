using UnityEngine;

public class VoidZone : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform spawnPoint; // Titik kembalinya player
    public int voidDamage = 1;   // Jumlah HP yang berkurang saat jatuh

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah yang menyentuh Void adalah Player
        if (collision.CompareTag("Player"))
        {
            // 1. Kurangi HP Player
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(voidDamage);
            }

            // 2. Teleport Player ke Spawn Point
            if (spawnPoint != null)
            {
                collision.transform.position = spawnPoint.position;
                
                // 3. Reset kecepatan jatuh (SANGAT PENTING)
                // Jika tidak di-reset, player akan terus meluncur ke bawah saat di-teleport
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero; 
                }
            }
            else
            {
                Debug.LogWarning("Spawn Point belum dimasukkan ke script VoidZone!");
            }
        }
    }
}