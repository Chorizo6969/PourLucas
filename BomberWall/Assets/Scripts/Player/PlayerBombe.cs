using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBombe : MonoBehaviour
{
    public static PlayerBombe Instance;

    bool hasBomb = false;
    [SerializeField] GameObject bomb;

    [SerializeField] GameObject hasBombSprite;

    public event Action<GameObject> OnTakeBomb;

    private void Awake()
    {
        Instance = this;
        hasBombSprite.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bomb>() == null) return;

        if (!hasBomb && collision.GetComponent<Bomb>().CanBeTake)
        {
            bomb = collision.gameObject;
            hasBomb = true;
            hasBombSprite.SetActive(true);
            collision.gameObject.SetActive(false);

            OnTakeBomb?.Invoke(bomb);
        }
    }

    /// <summary>
    /// Quand le joueur drop sa bombe
    /// </summary>
    /// <param name="callbackContext"></param>
    public void OnDropBomb(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started && bomb != null)
        {
            bomb.GetComponent<Bomb>().CanBeTake = false;
            bomb.transform.position = transform.position;
            bomb.SetActive(true);
            bomb.GetComponent<Bomb>().Explode();
            hasBomb = false;
            hasBombSprite.SetActive(false);
            bomb = null;
        }
    }
}