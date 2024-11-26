using UnityEngine;

public class WallLife : MonoBehaviour
{
    [SerializeField] int life;

    /// <summary>
    /// applique 1 de d�g�ts aux murs
    /// </summary>
    public void TakeDamage()
    {
        life --;
        if (life <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}