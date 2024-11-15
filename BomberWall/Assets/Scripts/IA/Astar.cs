using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class Astar : MonoBehaviour
{
    public static Astar Instance;

    [Dropdown("NodesList")]
    public GameObject DefaultStartNode;

    [Dropdown("NodesList")]
    public GameObject DefaultEndNode;

    public List<GameObject> NodesList;


    private int _security = 0;

    [SerializeField]
    private Node _nodeDepart;

    public Node _nodeFin;

    public List<Node> _closeNode = new();
    public List<Node> _openNode = new();

    private int _gCost = 1;

    public int tkt = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        /*_nodeDepart = DefaultStartNode.GetComponent<Node>();
        _nodeFin = DefaultEndNode.GetComponent<Node>();

        MoveToTarget(DefaultEndNode.gameObject);
        _nodeDepart = DefaultStartNode.GetComponent<Node>();*/

    }

    public void GetNearestWaypoint()
    {
        
        foreach (GameObject node in NodesList)
        {
            if ((transform.position - node.transform.position).magnitude < 0.01f)
            {
                //Debug.Log("start node " + node.name);
                _nodeDepart = node.GetComponent<Node>();
            }
        }
    }

    public void GetEndPoint(GameObject targetObject)
    {
        foreach (GameObject node in NodesList)
        {
            if ((targetObject.transform.position - node.transform.position).magnitude < 0.01f)
            {
                //Debug.Log("End node " + node.name);
                _nodeFin = node.GetComponent<Node>();
            }
        }
    }

    public void MoveToTarget(GameObject nodeToGo)
    {
        

        SetPointToGo(nodeToGo);

        float tempoFCost = 0; //Initialise les variables
        Node nodeWithLowCost = _nodeDepart; //Peu importe
        _closeNode.Add(_nodeDepart); //On ferme le node de départ

        while (_closeNode[_closeNode.Count - 1]._hCost > 0.5f /*|| _security <= 9*/) // Tant que nous sommes pas arrivé
        {
            if (tkt > 100)
            {
                //EditorApplication.isPlaying = false;
                Debug.LogError("Urgent Break From a While Loop");
                return;
            }

            tkt++;

            _nodeDepart = _closeNode[_closeNode.Count - 1];

            foreach (Node node in _closeNode)
            {
                node._isOpen = false;
            }
            _nodeDepart._isOpen = true;

            foreach (Node node in _nodeDepart._listNodesVoisin) //pour tous ses voisins
            {
                if (!_openNode.Contains(node))
                {
                    //_openNode.Add(node); //On l'ouvre
                    node._fCost = _gCost + node._hCost; // on calcule leurs f Cost
                    AddInListInOrderToFCost(node, _openNode);
                }

            }

            if (tkt == 210) //arrete l'éxécution du script à une boucle précise
            {
                Debug.Log(tkt);
            }


            if (tempoFCost == 0) //Pour éviter de remettre à 0
            {
                tempoFCost = _openNode[0]._fCost;
                //nodeWithLowCost = _openNode[0];
            }
            nodeWithLowCost = ReturnClosestPointFromTargetThatIsNotInCloseNodeList();

            tempoFCost = _openNode[0]._fCost;

            /*foreach (Node node in _openNode) //Pour chaque node ouvert on va chercher celui avec le meilleur f
            {
                if (node._listNodesVoisin.Contains(_nodeDepart))//est un voisin de _nodeDepart
                {
                    if (tempoFCost >= node._fCost)
                    {
                        tempoFCost = node._fCost;
                        nodeWithLowCost = node;
                    }
                }
            }*/

            //Debug.Log(nodeWithLowCost + " " + nodeWithLowCost._fCost + " " + tkt);

            if (!_closeNode.Contains(nodeWithLowCost))
            {
                _closeNode.Add(nodeWithLowCost); //On ajoute le meilleur a la liste des fermés
            }

            _openNode.Clear();
            
            _gCost++;
            _security++;
        }
        Debug.Log("Arrivé");//Problème : Duplicata des éléments dans les listes + il faut que dans le foreach on compare que les éléments voisins du dernier ajout de _closeNode
        StartCoroutine(IaController.Instance.IaMoveToTargetPoint());
    }

    public void SetPointToGo(GameObject PointToGo)
    {
        foreach(SpriteRenderer node in BombeEmplacement.Instance._listNode)
        {
            node.GetComponent<Node>()._pointArrivé = PointToGo;
            node.GetComponent<Node>().SetCost();
        }
    }

    public void AddInListInOrderToFCost(Node node, List<Node> _list)
    {
        if (_list.Contains(node)) return;

        if (_list.Count == 0)
        {
            _list.Add(node);
        }
        else
        {
            for (int count = 0; count == _list.Count-1; count ++)
            {
                if (node._fCost < _list[count]._fCost)
                {
                    _list.Insert(count, node);
                }
                count ++;
            }
            if (_list.Contains(node)) return;
            _list.Add(node);
        }
    }

    public Node ReturnClosestPointFromTargetThatIsNotInCloseNodeList()
    {
        Node realClosestPoint;
        foreach (Node node in _openNode)
        {
            if (!_closeNode.Contains(node))
            {
                realClosestPoint = node;
                return realClosestPoint;
            }
        }
        Debug.LogError("Aled");
        return null;
    }
}