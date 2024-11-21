using NaughtyAttributes;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Astar : MonoBehaviour
{
    public static Astar Instance;

    [Dropdown("NodesList")]
    public GameObject DefaultStartNode;

    [Dropdown("NodesList")]
    public GameObject DefaultEndNode;

    public List<GameObject> NodesList;

    public Node _nodeDepart;

    public Node _endNode;

    public List<List<Node>> PathList = new();

    public Stack<Node> ActiveList = new();

    public Node _activeNode;

    public int ReflectionDelay;

    public List<Node> _closeNode = new();
    public List<Node> _openNode = new();


    private void Awake()
    {
        Instance = this;
    }

    public async void MoveToTarget(Node endNode)
    {
        Debug.Log("START ASTAR");

        _nodeDepart = ClosestNode(this.gameObject);

        ActiveList.Clear();
        _openNode.Clear();
        _closeNode.Clear();

        _endNode = endNode;

        foreach(GameObject node in NodesList)
        {
            node.GetComponent<SpriteRenderer>().color = Color.white;
        }

        SetUpNodeCost();

        _activeNode = _nodeDepart;

        int secureCount = 0;

        while (_activeNode != _endNode)
        {

            await Task.Delay(ReflectionDelay);


            if (secureCount == 1000)
            {
                Debug.LogError("Sécurité");
                return;
            }

            _activeNode.GetComponent<SpriteRenderer>().color = Color.red;


            _closeNode.Add(_activeNode);
            _openNode.Remove(_activeNode);

            foreach (Node node in _activeNode._listNodesVoisin)
            {
                if (!_openNode.Contains(node) && !_closeNode.Contains(node))
                {
                    _openNode.Add(node);

                    node.GetComponent<SpriteRenderer>().color = Color.blue;

                    node.NodeThatOpenedThisNode = _activeNode;
                    node._gCost = node.NodeThatOpenedThisNode._gCost + 1;
                    node._fCost = node._gCost + node._hCost;
                }
            }

            if (_openNode.Count >= 0)
            {
                Node tempoClosestNode = _openNode[0];
                float tempoCost = tempoClosestNode._fCost;
                foreach (Node node in _openNode)
                {
                    if (node._fCost <= tempoCost)
                    {
                        tempoClosestNode = node;
                    }
                }

                _activeNode = tempoClosestNode;
            }

            secureCount++;
        }

        Node tempoNode = _endNode;
        while (tempoNode != _nodeDepart)
        {
            tempoNode.GetComponent<SpriteRenderer>().color = Color.black;
            ActiveList.Push(tempoNode);
            tempoNode = tempoNode.NodeThatOpenedThisNode;
        }



        _activeNode.GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("trouvé ?");
        GetComponent<IaController>().IaMoveToTargetPoint();
    }

    public void SetUpNodeCost()
    {
        foreach(GameObject node in NodesList)
        {
            node.GetComponent<Node>()._pointArrivé = _endNode;
            node.GetComponent<Node>().SetCost();
        }
    }

    public Node ClosestNode(GameObject myObject)
    {
        GameObject tempoNode = NodesList[0];
        foreach (GameObject node in NodesList)
        {
            if((myObject.transform.position -  node.transform.position).magnitude < 0.001f)
            {
                tempoNode = node;
            }
        }
        return tempoNode.GetComponent<Node>();
    }
}