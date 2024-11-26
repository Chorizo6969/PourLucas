using UnityEngine;

public class AudioSourceStaticRef : MonoBehaviour
{
    public static AudioSourceStaticRef Instance;
    public AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
}