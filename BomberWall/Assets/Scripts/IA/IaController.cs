using System.Collections;
/*using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IaController : MonoBehaviour
{
    public static IaController Instance;

    public float MoveSpeed;

    public bool hasBomb = false;
    [SerializeField] GameObject bomb;

    private Astar _astar;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _astar = GetComponent<Astar>();
        BombeEmplacement.Instance.OnBombSpawn += BombSpawnNotify;
    }

    public void BombSpawnNotify(GameObject targetBomb)
    {
        if (!hasBomb)
        {
            StopAllCoroutines();
            _astar.GetEndPoint(targetBomb);
            _astar.GetNearestWaypoint();


            _astar.MoveToTarget(_astar._nodeFin.gameObject);
        }
    }

    public IEnumerator IaMoveToTargetPoint()
    {
        foreach (Node node in Astar.Instance._closeNode)
        {
            transform.position = node.transform.position;
            yield return new WaitForSeconds(MoveSpeed);
        }
        //DropBomb();
    }


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
        bomb.GetComponent<Bomb>().CanBeTake = false;
        bomb.transform.position = transform.position;
        bomb.SetActive(true);
        bomb.GetComponent<Bomb>().Drop();
        hasBomb = false;
        bomb = null;
    }
}*/