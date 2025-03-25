using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private GameObject[] bonuses;
    [SerializeField][Range(0, 100)] private int spawnChancePercent = 10;
    [SerializeField] private ParticleSystem breakEffect;
    [SerializeField] private AudioClip DestroySound;
    [SerializeField] private AudioSource DestroyBlockSound;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.AddScore();
        gameManager.BlockDestroyed();
        RandomBonusSpawn();
        var effect = Instantiate(breakEffect, transform.position, transform.rotation);
        Destroy(effect, 3f);
        DestroyBlockSound.PlayOneShot(DestroySound);
        Destroy(gameObject);
    }

    private void RandomBonusSpawn()
    {
        float chance = spawnChancePercent / 100f;

        if (Random.Range(0f, 1f) <= chance)
        {
            int randomIndex = Random.Range(0, bonuses.Length);
            Instantiate(bonuses[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
