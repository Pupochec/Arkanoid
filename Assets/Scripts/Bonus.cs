using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public enum BonusType { ExpandPaddle, SlowBall, ExtraLife }
    public BonusType type;

    [SerializeField] private float fallSpeed = 2f;
    [SerializeField] private float timeSlowBallBonus = 10f;
    [SerializeField] private float timeBigPlatformBonus = 10f;

    [SerializeField] private PlayerPlatform playerPlatform;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Ball ball;
    [SerializeField] private Image bonusImage;
    [SerializeField] private AudioClip bonusSoundClip;

    private void Start()
    {
        if (bonusImage == null)
            bonusImage = GameObject.Find("Image Bonus").GetComponent<Image>();

        if (playerPlatform == null)
            playerPlatform = FindObjectOfType<PlayerPlatform>();

        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();

        if (ball == null)
            ball = FindObjectOfType<Ball>();
    }

    private void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Platform")
        {
            ApplyBonus();
            ball.GetComponent<AudioSource>().PlayOneShot(bonusSoundClip);

            Destroy(gameObject);
        }

        if (collision.CompareTag("Dead Zone"))
        {
            Destroy(gameObject);
        }
    }

    private void ApplyBonus()
    {
        switch (type)
        {
            case BonusType.ExpandPaddle:
                playerPlatform.BigPlatformEffect(timeBigPlatformBonus);
                ApplaySprite(timeBigPlatformBonus);
                break;
            case BonusType.SlowBall:
                ball.SlowBall(4f, timeSlowBallBonus);
                ApplaySprite(timeSlowBallBonus);
                break;
            case BonusType.ExtraLife:
                gameManager.AddLife();
                ApplaySprite(10f);
                break;
        }
    }

    private void ApplaySprite(float ClearTime)
    {
        bonusImage.color = new Color(255, 255, 255, 255);
        bonusImage.sprite = GetComponent<SpriteRenderer>().sprite;
        Invoke(nameof(ClearSprite), ClearTime);
    }

    private void ClearSprite()
    {
        bonusImage.color = new Color(255, 255, 255, 0);
        bonusImage.sprite = default;
    }
}