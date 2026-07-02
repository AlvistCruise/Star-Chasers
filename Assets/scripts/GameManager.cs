using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Wajib untuk fungsi Restart dan Main Menu

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Data")]
    public int score = 0;
    public int maxScore = 10;
    private bool isGameOver = false; // Mencegah pause saat game sudah selesai

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject pausePanel;

    [Header("Audio Settings")]
    public AudioSource bgmSource;    // AudioSource yang memutar lagu level saat ini
    public AudioClip winMusic;       // Lagu saat menang
    public AudioClip loseMusic;      // Lagu saat kalah

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Pastikan waktu berjalan normal (1) saat game dimulai
        // Ini SANGAT PENTING agar game tidak tetap freeze setelah di-restart
        Time.timeScale = 1f; 
        
        // Pastikan semua panel mati di awal game
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        pausePanel.SetActive(false);

        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return; // Jangan tambah skor kalau game sudah selesai

        score += amount;
        UpdateScoreUI();

        // Cek kondisi menang
        if (score >= maxScore)
        {
            WinGame();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Stars Collected: " + score + " / " + maxScore;
    }

    // --- KONDISI MENANG DAN KALAH ---

    public void WinGame()
    {
        isGameOver = true;
        winPanel.SetActive(true);    // Tampilkan Win Panel
        ChangeBGM(winMusic);         // Ganti lagu
        Time.timeScale = 0f;         // Freeze game
    }

    public void LoseGame()
    {
        isGameOver = true;
        losePanel.SetActive(true);   // Tampilkan Lose Panel
        ChangeBGM(loseMusic);        // Ganti lagu
        Time.timeScale = 0f;         // Freeze game
    }

    private void ChangeBGM(AudioClip newClip)
    {
        if (bgmSource != null && newClip != null)
        {
            bgmSource.Stop();        // Hentikan lagu yang sedang main
            bgmSource.clip = newClip; // Masukkan lagu baru
            bgmSource.Play();        // Putar lagu baru
        }
    }

    // --- FUNGSI UNTUK TOMBOL (BUTTONS) ---

    public void PauseGame()
    {
        if (isGameOver) return; // Tidak bisa pause kalau sudah mati/menang

        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Freeze game
    }

    public void ResumeGame() // Untuk tombol ReturnBTN
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Unfreeze game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Wajib unfreeze sebelum pindah/restart scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Memuat ulang scene yang sedang dimainkan
    }

    public void GoToTitle()
    {
        Time.timeScale = 1f; // Wajib unfreeze sebelum pindah scene
        SceneManager.LoadScene("MainMenu"); // Pastikan nama scenenya benar
    }
}