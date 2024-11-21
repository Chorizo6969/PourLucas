using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class IaController : MonoBehaviour
{
    public static IaController Instance;

    [SerializeField] private int MoveSpeed;

    [SerializeField] private bool hasBomb = false;
    [SerializeField] private bool isSearchingBomb = false;

    [SerializeField] GameObject bomb;

    private Astar _astar;

    [SerializeField] private List<GameObject> bombList = new();

    [SerializeField] private Node _wallNode;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _astar = GetComponent<Astar>();
        BombeEmplacement.Instance.OnBombSpawn += BombSpawnNotify;
        PlayerBombe.Instance.OnTakeBomb += PlayerTakeBomb;
    }

    /// <summary>
    /// Déclanché lorsqu'une bombe spawn sur la map
    /// </summary>
    /// <param name="targetBomb">bombe qui vient de spawn</param>
    public void BombSpawnNotify(GameObject targetBomb)
    {
        bombList.Add(targetBomb);


        if (!hasBomb /*&& !isSearchingBomb*/)
        {
            _astar.ActiveList.Clear();

            isSearchingBomb = true;
            Debug.Log("J'ai une bombe laisse moi marcher");
            _astar.MoveToTarget(_astar.ClosestNode(GetClosestBomb()));
        }
        
    }

    /// <summary>
    /// déclanché lorsque le joueur prend une bombe
    /// </summary>
    /// <param name="targetBomb">bombe que le joueur prend</param>
    public void PlayerTakeBomb(GameObject targetBomb)
    {
        Debug.Log("Al batard il touche aux bombes");

        if (bombList.Contains(targetBomb))
        {
            bombList.Remove(targetBomb);
            Debug.Log("olala il manque une bombe dans ma liste");
            
            if (hasBomb) return;
            
            _astar.MoveToTarget(_astar.ClosestNode(GetClosestBomb()));
        }
    }

    /// <summary>
    /// récupère la bombe la plus proche de l'ia
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
    /// gère où l'ia est envoyé
    /// </summary>
    public async void IaMoveToTargetPoint()
    {
        isSearchingBomb = false;


        foreach (Node node in Astar.Instance.ActiveList)
        {
            await Task.Delay(MoveSpeed);
            transform.position = node.transform.position;
        }

        await Task.Delay(50);

        if (hasBomb && _astar.ClosestNode(this.gameObject) == _wallNode)
        {
            DropBomb();
        }

        //_astar.NodesList[Random.Range(0, _astar.NodesList.Count-1)].GetComponent<Node>()

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
        bomb.GetComponent<Bomb>().Drop();
        hasBomb = false;
        bomb = null;
    }
}