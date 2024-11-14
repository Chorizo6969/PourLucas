using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBombe : MonoBehaviour
{
    bool hasBomb = false;
    [SerializeField] GameObject bomb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bomb>() == null) return;

        if (!hasBomb && collision.GetComponent<Bomb>().CanBeTake)
        {
            bomb = collision.gameObject;
            hasBomb = true;
            collision.gameObject.SetActive(false);
        }
    }

    public void DropBomb()
    {

    }

    public void OnDropBomb(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started && bomb != null)
        {
            bomb.GetComponent<Bomb>().CanBeTake = false;
            bomb.transform.position = transform.position;
            bomb.SetActive(true);
            bomb.GetComponent<Bomb>().Drop();
            hasBomb = false;
            bomb = null;
        }
    }
}
