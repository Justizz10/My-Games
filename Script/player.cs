using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
  private SpriteRenderer spriteRenderer;
  public Sprite[] sprites;
  private int spriteIndex;

  private Vector3 direction;
  public float gravity = -9.8f;
  public float strength = 5f;

  // Variabel Audio
  private AudioSource audioSource;
  public AudioClip flapSound;       // Suara untuk tombol ditekan
  public AudioClip gameOverSound;  // Suara untuk Game Over
  public AudioClip scoreSound;     // Suara untuk menambahkan skor

  private void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    audioSource = GetComponent<AudioSource>();
  }

  private void Start()
  {
    InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
  }

  private void OnEnable()
  {
    Vector3 position = transform.position;
    position.y = 0f;
    transform.position = position;
    direction = Vector3.zero;
  }

  private void Update()
  {
    // Input untuk keyboard
    if (Keyboard.current.spaceKey.wasPressedThisFrame)
    {
      direction = Vector3.up * strength;
      PlayFlapSound();
    }

    // Input untuk mouse
    if (Mouse.current.leftButton.wasPressedThisFrame)
    {
      direction = Vector3.up * strength;
      PlayFlapSound();
    }

    // Input untuk sentuhan di perangkat Android (tekan sekali)
    if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
    {
      direction = Vector3.up * strength;
      PlayFlapSound();
    }

    direction.y += gravity * Time.deltaTime;
    transform.position += direction * Time.deltaTime;
  }

  private void AnimateSprite()
  {
    spriteIndex++;

    if (spriteIndex >= sprites.Length)
    {
      spriteIndex = 0;
    }
    spriteRenderer.sprite = sprites[spriteIndex];
  }

  [System.Obsolete]
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Obstacle")
    {
      PlayGameOverSound();
      FindObjectOfType<GameManager>().GameOver();
    }
    else if (other.gameObject.tag == "Scoring")
    {
      PlayScoreSound();
      FindObjectOfType<GameManager>().IncreaseScore();
    }
  }

  private void PlayFlapSound()
  {
    if (flapSound != null && audioSource != null)
    {
      audioSource.PlayOneShot(flapSound);
    }
  }

  private void PlayGameOverSound()
  {
    if (gameOverSound != null && audioSource != null)
    {
      audioSource.PlayOneShot(gameOverSound);
    }
  }

  private void PlayScoreSound()
  {
    if (scoreSound != null && audioSource != null)
    {
      audioSource.PlayOneShot(scoreSound);
    }
  }
}
