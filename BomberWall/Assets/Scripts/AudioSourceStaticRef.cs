using UnityEngine;

public class AudioSourceStaticRef : MonoBehaviour
{
    public static AudioSourceStaticRef Instance;

    private void Awake()
    {
        Instance = this;
    }
}