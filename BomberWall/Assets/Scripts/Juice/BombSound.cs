using UnityEngine;

public class BombSound : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;

    private void Awake()
    {
        GetComponent<Bomb>().BombExplodeNotify += ExplosionSound;
    }

    public void ExplosionSound()
    {
        AudioSourceStaticRef.Instance.GetComponent<AudioSource>().PlayOneShot(_audioClip);
    }
}