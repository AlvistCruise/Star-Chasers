using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float length;
    private float startpos;
    private float startpos_Y;
    private GameObject cam;

    [Header("Parallax Settings")]
    [Tooltip("0 = Diam (Langit jauh), 1 = Ikut kamera sepenuhnya (Foreground)")]
    public float parallaxEffect; 
    public float parallaxEffect_Y; 

    void Start()
    {
        // Otomatis mencari Main Camera di scene
        cam = Camera.main.gameObject; 

        // Menyimpan posisi awal objek
        startpos = transform.position.x;
        startpos_Y = transform.position.y;

        // Otomatis mengambil lebar (width) dari Sprite Renderer
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        // Menghitung seberapa jauh kamera telah bergerak relatif terhadap efek parallax
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        
        // Jarak yang harus ditempuh oleh background ini
        float distance = (cam.transform.position.x * parallaxEffect);
        float distance_Y = (cam.transform.position.y * parallaxEffect_Y);

        // Menggerakkan background (hanya di sumbu X)
        transform.position = new Vector3(startpos + distance, startpos_Y + distance_Y, transform.position.z);

        // --- Logika Infinite Scrolling ---
        // Jika kamera telah melewati batas kanan sprite, geser titik awal ke kanan
        // if (temp > startpos + length)
        // {
        //     startpos += length;
        // }
        // // Jika kamera melewati batas kiri, geser titik awal ke kiri
        // else if (temp < startpos - length)
        // {
        //     startpos -= length;
        // }
    }
}