using System;
using System.Threading.Tasks;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool CanBeTake;

    public event Action BombExplodeNotify;

    /// <summary>
    /// fait exploser la bombe et applique des dégâts aux mur destructible à proximiter
    /// </summary>
    public async void Explode()
    {
        CanBeTake = false;
        await Task.Delay(3000);

        BombExplodeNotify?.Invoke();

        int layerMask = 1 << 7;

        RaycastHit2D hitVertical = Physics2D.Raycast(transform.position - Vector3.right*3, Vector2.right * 6, 10, layerMask); //tir un RayCast horizontal
        RaycastHit2D hitHorizontal = Physics2D.Raycast(transform.position - Vector3.up*3, Vector2.up * 6, 10, layerMask); //tir un RayCast horizontal


        //vérifie si les raycast ont touuché un mur destructible, applique des dégâts si c'est le cas
        if (hitVertical.collider != null) //Vérification que l'on touche bien un collider (sinon null ref)
        {
            if (hitVertical.collider.gameObject.tag == "wall")
            {
                hitVertical.collider.gameObject.GetComponent<WallLife>().TakeDamage();
            }
            else if (hitVertical.collider.gameObject.tag == "entity")
            {
                hitVertical.collider.gameObject.GetComponent<Health>().TakeDamage(1);
            }
        }
        
        if (hitHorizontal.collider != null) //Vérification que l'on touche bien un collider (sinon null ref)
        {
            if (hitHorizontal.collider.gameObject.tag == "wall")
            {
                hitHorizontal.collider.gameObject.GetComponent<WallLife>().TakeDamage();
            }
            else if (hitVertical.collider.gameObject.tag == "entity")
            {
                hitVertical.collider.gameObject.GetComponent<Health>().TakeDamage(1);
            }
        }

        //renvoie la bombe dans la pool et la remet sur la map
        Pool.Instance._stack.Push(gameObject);
        BombeEmplacement.Instance.ChoseBombEmplacement();
        CanBeTake = true;
    }
}