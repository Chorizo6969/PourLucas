using UnityEngine;

public class WallLife : MonoBehaviour
{
    [SerializeField] int life;

    /// <summary>
    /// applique 1 de dégâts aux murs
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