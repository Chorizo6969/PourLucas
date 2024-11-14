using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> _listNodesVoisin = new List<Node>();
    
    public GameObject _pointArrivé;

    public float _hCost;
    public float _fCost;

    public bool _isVisited;
    public bool _isOpen;

    public bool _isGood;

    public void SetCost()
    {
        _hCost = (gameObject.transform.position - _pointArrivé.transform.position).magnitude;
    }
    //Il faut que chaque node connaisse la distance par rapport à l'arrivée
    //2 listes, 1 de liste de node parcourue,

    private void OnDrawGizmos()
    {
        if (_listNodesVoisin.Count > 0)
        {
            foreach (Node go in _listNodesVoisin)
            {
                if (go.GetComponent<Node>()._listNodesVoisin.Contains(this))
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(transform.position, go.transform.position);
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(transform.position, go.transform.position);
                }
            }
        }
    }
}