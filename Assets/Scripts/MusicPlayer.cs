using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance; // Singleton

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}