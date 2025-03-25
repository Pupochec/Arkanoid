using UnityEngine;

public class Ball : MonoBehaviour
{
    public AudioClip Hit, Damage;

    [SerializeField] private float speed = 5f;
    [SerializeField] private GameManager gameManager;

    private float maxBounceAngle = 45f;
    private Vector2 direction;
    private float originSpeed;
    private AudioSource HitSound;

    private void Start()
    {
        direction = Vector2.up;
        originSpeed = speed;
        HitSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SlowBall(float SlowSpeed, float TimeBonus)
    {
        speed = SlowSpeed;

        Invoke(nameof(NormalSpeed), TimeBonus);
    }

    private void NormalSpeed()
    {
        speed = originSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player Platform"))
        {
            float hitPosition = (transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;

            float bounceAngle = hitPosition * maxBounceAngle * Mathf.Deg2Rad;

            direction = new Vector2(Mathf.Sin(bounceAngle), Mathf.Cos(bounceAngle)).normalized;

            HitSound.PlayOneShot(Hit);
        }
        else
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);

            HitSound.PlayOneShot(Hit);
        }

        if (collision.gameObject.tag == "Dead Zone")
        {
            gameManager.LoseLife();
            HitSound.PlayOneShot(Damage);
        }
    }
}
