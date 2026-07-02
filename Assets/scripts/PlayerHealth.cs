using UnityEngine;
using UnityEngine.UI; // WAJIB DITAMBAHKAN KARENA KITA MENGAKSES UI

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    public int currentHealth;

    [Header("UI Settings")]
    public Image[] hearts; // Array untuk menyimpan semua gambar HP/Hati dari Canvas

    void Start()
    {
        // Set HP ke penuh saat game dimulai
        currentHealth = maxHealth;
        
        // Pastikan UI menampilkan HP penuh saat mulai
        UpdateHealthUI(); 
    }

    // Fungsi ini akan dipanggil oleh Void atau Musuh
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Player terkena damage! HP sisa: " + currentHealth);

        // Update tampilan UI setiap kali terkena damage
        UpdateHealthUI();

        if (currentHealth <= 1)
        {
            Die();
        }
    }

    // --- FUNGSI BARU UNTUK MENGATUR UI ---
    void UpdateHealthUI()
    {
        // Looping untuk mengecek setiap gambar hati di dalam array
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                // Jika urutan (i) masih di bawah jumlah HP saat ini, tampilkan gambarnya
                hearts[i].enabled = true;
            }
            else
            {
                // Jika urutan (i) lebih besar/sama dengan HP saat ini, sembunyikan gambarnya
                hearts[i].enabled = false;
            }
        }
    }

    void Die()
    {
        Debug.Log("Player Mati! Game Over.");
        
        // Panggil fungsi LoseGame dari GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.LoseGame();
        }
    }
}