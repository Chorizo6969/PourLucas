using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class IaController : MonoBehaviour
{
    public static IaController Instance;

    [SerializeField] private int MoveSpeed;

    [SerializeField] private bool hasBomb = false;
    [SerializeField] private GameObject hasBombSprite;

    [SerializeField] GameObject bomb;

    private Astar _astar;

    [SerializeField] private List<GameObject> bombList = new();

    [SerializeField] private Node _wallNode;

    public event Action IaMoveNotify;

    private void Awake()
    {
        Time.timeScale = 1;
        Instance = this;
        hasBombSprite.SetActive(false);
    }

    private void Start()
    {
        _astar = GetComponent<Astar>();
        BombeEmplacement.Instance.OnBombSpawn += BombSpawnNotify;
        PlayerBombe.Instance.OnTakeBomb += PlayerTakeBomb;
    }

    /// <summary>
    /// D�clanch� lorsqu'une bombe spawn sur la map
    /// </summary>
    /// <param name="targetBomb">bombe qui vient de spawn</param>
    public void BombSpawnNotify(GameObject targetBomb)
    {
        bombList.Add(targetBomb);


        if (!hasBomb /*&& !isSearchingBomb*/)
        {
            _astar.ActiveList.Clear();

            _astar.MoveToTarget(_astar.ClosestNode(GetClosestBomb()));
        }
        
    }

    /// <summary>
    /// d�clanch� lorsque le joueur prend une bombe
    /// </summary>
    /// <param name="targetBomb">bombe que le joueur prend</param>
    public void PlayerTakeBomb(GameObject targetBomb)
    {

        if (bombList.Contains(targetBomb))
        {
            bombList.Remove(targetBomb);
            
            if (hasBomb) return;
            
            _astar.MoveToTarget(_astar.ClosestNode(GetClosestBomb()));
        }
    }

    /// <summary>
    /// r�cup�re la bombe la plus proche de l'ia
    /// </summary>
    /// <returns></returns>
    public GameObject GetClosestBomb()
    {
        GameObject tempoBomb = bombList[0];
        float tempoDistance = (gameObject.transform.position - tempoBomb.transform.position).magnitude;
        foreach (GameObject target in bombList)
        {
            if (tempoDistance >= (gameObject.transform.position - target.transform.position).magnitude)
            {
                tempoDistance = (gameObject.transform.position - target.transform.position).magnitude;
                tempoBomb = target;
            }
        }
        return tempoBomb;
    }

    /// <summary>
    /// g�re o� l'ia est envoy�
    /// </summary>
    public async void IaMoveToTargetPoint()
    {
        foreach (Node node in Astar.Instance.ActiveList)
        {
            await Task.Delay(MoveSpeed);
            transform.position = node.transform.position;
            IaMoveNotify?.Invoke();
        }

        await Task.Delay(50);

        if (hasBomb && _astar.ClosestNode(this.gameObject) == _wallNode)
        {
            DropBomb();
        }

        if (hasBomb)
        {
            await Task.Delay(250);
            _astar.MoveToTarget(_wallNode);

        }
        else if (!hasBomb)
        {
            await Task.Delay(250);
            if (bombList.Count > 0)
            {
                _astar.MoveToTarget(_astar.ClosestNode(GetClosestBomb()));
            }
        }
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

            bombList.Remove(collision.gameObject);
        }
    }

    /// <summary>
    /// lache la bombe tenue par l'ia
    /// </summary>
    public void DropBomb()
    {
        bomb.GetComponent<Bomb>().CanBeTake = false;
        bomb.transform.position = transform.position;
        bomb.SetActive(true);
        bomb.GetComponent<Bomb>().Explode();
        hasBomb = false;
        bomb = null;
        hasBombSprite.SetActive(false);
    }
}