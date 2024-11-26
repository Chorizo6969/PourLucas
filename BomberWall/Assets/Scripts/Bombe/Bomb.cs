using System;
using System.Threading.Tasks;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool CanBeTake;

    public event Action BombExplodeNotify;

    /// <summary>
    /// fait exploser la bombe et applique des d�g�ts aux mur destructible � proximiter
    /// </summary>
    public async void Explode()
    {
        CanBeTake = false;
        await Task.Delay(3000);

        BombExplodeNotify?.Invoke();

        int layerMask = 1 << 8;

        RaycastHit2D hitVertical = Physics2D.Raycast(transform.position - Vector3.right, Vector2.right * 2, 3, layerMask); //tir un RayCast horizontal
        RaycastHit2D hitHorizontal = Physics2D.Raycast(transform.position - Vector3.up, Vector2.up * 2, 3, layerMask); //tir un RayCast horizontal


        //v�rifie si les raycast ont touuch� un mur destructible, applique des d�g�ts si c'est le cas
        if (hitVertical.collider != null) //V�rification que l'on touche bien un collider (sinon null ref)
        {
            if (hitVertical.collider.gameObject.layer == 8)
            {
                hitVertical.collider.gameObject.GetComponent<WallLife>().TakeDamage();
            }
        }
        
        if (hitHorizontal.collider != null) //V�rification que l'on touche bien un collider (sinon null ref)
        {
            if (hitHorizontal.collider.gameObject.layer == 8)
            {
                hitHorizontal.collider.gameObject.GetComponent<WallLife>().TakeDamage();
            }
        }

        //renvoie la bombe dans la pool et la remet sur la map
        Pool.Instance._stack.Push(gameObject);
        BombeEmplacement.Instance.ChoseBombEmplacement();
        CanBeTake = true;
    }
}