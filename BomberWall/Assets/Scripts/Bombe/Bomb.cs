using System.Threading.Tasks;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool CanBeTake;

    public void Drop()
    {
        CanBeTake = false;
        Explode();
    }
    public async void Explode()
    {
        await Task.Delay(3000);
        RaycastHit2D hit;
        int layerMask = 1 << 8;
        hit = Physics2D.CircleCast(transform.position, 1 ,Vector2.zero, 0 ,layerMask);
        if (hit.collider != null) //Vérification que l'on touche bien un collider (sinon null ref)
        {
            if (hit.collider.gameObject.layer == 8)
            {
                Debug.Log("PRENDS DES DÉGATS stp");
                hit.collider.gameObject.GetComponent<WallLife>().TakeDamage();
            }
        }
        Pool.Instance._stack.Push(gameObject);
        BombeEmplacement.Instance.ChoseBombEmplacement();
        CanBeTake = true;

    }

    private void Update()
    {
        Debug.DrawRay(transform.position + Vector3.left, Vector3.left, Color.blue);
    }
}