using UnityEngine;

public class MoveSoundManager : MonoBehaviour
{
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private AudioClip _audioclip;

    // Start is called before the first frame update
    void Start()
    {
        _playerMove.PlayerMoveNotify += PlaySound;
        IaController.Instance.IaMoveNotify += PlaySound;
    }

    public void PlaySound()
    {
        AudioSourceStaticRef.Instance.audioSource.PlayOneShot(_audioclip);
    }
}