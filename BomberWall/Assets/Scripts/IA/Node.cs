using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> _listNodesVoisin = new List<Node>();
    
    public Node _pointArrivé;

    public float _hCost;
    public float _gCost;
    public float _fCost;

    public bool _isGood;

    public Node NodeThatOpenedThisNode;

    /// <summary>
    /// calcul le hCost par rapport au point d'arrivé
    /// </summary>
    public void SetCost()
    {
        _hCost = (gameObject.transform.position - _pointArrivé.transform.position).magnitude;
    }

    //dessine un gizmo pour vérifier que les listes de voisins sont faite correctement
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