using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Jump & Ground Settings")]
    public int maxJumps = 2;           // Jumlah maksimal lompatan (2 = Double Jump)
    public string groundTag = "Floor"; // Tag untuk lantai/ground

    private Rigidbody2D rb;
    private Animator anim;
    
    private float moveInput;
    private bool isGrounded;
    private bool isFacingRight = true;
    private int jumpCount;             // Sisa kesempatan lompatan saat ini

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Mengecek input horizontal (A/D atau panah Kiri/Kanan)
        moveInput = Input.GetAxisRaw("Horizontal");

        // Logika untuk melompat (Tombol Spasi)
        // Cek apakah masih ada sisa jumpCount
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            // Reset velocity Y agar double jump konsisten dan tidak menjumlahkan gaya lompat sebelumnya
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            
            jumpCount--; // Kurangi kesempatan lompat
        }

        // Membalikkan arah hadap karakter (Flip)
        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }

        // Update state animasi
        UpdateAnimationUpdate();
    }

    void FixedUpdate()
    {
        // Mengaplikasikan pergerakan
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void UpdateAnimationUpdate()
    {
        // Animasi Move/Idle
        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        // Animasi Jump: Aktif jika jumpCount tidak maksimal ATAU tidak menyentuh tanah
        anim.SetBool("IsJumping", !isGrounded);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    // --- SISTEM DETEKSI GROUND MENGGUNAKAN TAG ---

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ketika collider player mendeteksi objek dengan tag yang sesuai
        if (collision.gameObject.CompareTag(groundTag))
        {
            isGrounded = true;
            jumpCount = maxJumps; // Reset kesempatan lompat (Player siap double jump lagi)
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Ketika collider player berhenti menyentuh objek dengan tag tersebut (misal saat sedang melayang)
        if (collision.gameObject.CompareTag(groundTag))
        {
            isGrounded = false;
        }
    }
}