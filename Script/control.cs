using UnityEngine;

public class Control : MonoBehaviour
{
    public float berat;
    public float tinggiLoncat;
    public GameObject bird;

    private Rigidbody2D birdRigidbody;

    // Digunakan untuk inisialisasi
    void Start()
    {
        if (bird != null)
        {
            birdRigidbody = bird.GetComponent<Rigidbody2D>();
            if (birdRigidbody == null)
            {
                Debug.LogError("Rigidbody2D tidak ditemukan pada objek bird!");
            }
        }
        else
        {
            Debug.LogError("GameObject bird belum diassign!");
        }
    }

    void OnMouseDown()
    {
        if (birdRigidbody != null)
        {
            birdRigidbody.gravityScale = berat;
            birdRigidbody.linearVelocity = new Vector2(birdRigidbody.linearVelocity.x, tinggiLoncat);
        }
    }

    void Update()
    {
        // Anda dapat menambahkan logika di sini jika diperlukan
    }
}
