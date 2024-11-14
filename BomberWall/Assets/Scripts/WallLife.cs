using UnityEngine;

public class WallLife : MonoBehaviour
{
    [SerializeField] int life;

    public void TakeDamage()
    {
        life --;
        if (life <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
