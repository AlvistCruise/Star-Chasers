using UnityEngine;

public class CollectibleStar : MonoBehaviour
{
    public int scoreValue = 1; // Nilai skor untuk satu bintang

    [Header("Audio Settings")]
    public AudioClip collectSound; // Tempat untuk menaruh file SFX kamu

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah objek yang menabrak memiliki Tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Mainkan efek suara (SFX) sesaat sebelum bintang dihancurkan
            if (collectSound != null)
            {
                // PlayClipAtPoint memastikan suara tetap selesai dimainkan meskipun objek mati
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // Panggil fungsi AddScore dari GameManager
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(scoreValue);
            }

            // Hancurkan objek bintang ini agar hilang dari scene
            Destroy(gameObject);
        }
    }
}