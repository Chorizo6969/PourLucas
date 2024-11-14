using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    private int _security = 0;

    [SerializeField]
    private Node _nodeDepart;

    private List<Node> _closeNode = new();
    private List<Node> _openNode = new();

    private int _gCost = 1;

    private void Start()
    {
        MoveToTarget();
    }

    public void MoveToTarget()
    {
        float tempoFCost = 0; //Initialise les variables
        Node nodeWithLowCost = _nodeDepart; //Peu importe
        _closeNode.Add(_nodeDepart); //On ferme le node de d�part

        while (/*_closeNode[_closeNode.Count - 1]._hCost > 0.5f ||*/ _security <= 9) // Tant que nous sommes pas arriv�
        {
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
                    _openNode.Add(node); //On l'ouvre
                    node._fCost = _gCost + node._hCost; // on calcule leurs f Cost
                }

            }

            if (tempoFCost == 0) //Pour �viter de remettre � 0
            {
                tempoFCost = _openNode[0]._fCost;
                nodeWithLowCost = _openNode[0];
            }

            tempoFCost = _openNode[0]._fCost;

            foreach (Node node in _openNode) //Pour chaque node ouvert on va chercher celui avec le meilleur f
            {
                if (node._listNodesVoisin.Contains(_nodeDepart))//est un voisin de _nodeDepart
                {
                    if (tempoFCost > node._fCost)
                    {
                        tempoFCost = node._fCost;
                        nodeWithLowCost = node;
                    }
                }
            }

            Debug.Log(nodeWithLowCost);
            _closeNode.Add(nodeWithLowCost); //On ajoute le meilleur a la liste des ferm�s
            _gCost++;
            _security++;
        }
        Debug.Log("Arriv�");//Probl�me : Duplicata des �l�ments dans les listes + il faut que dans le foreach on compare que les �l�ments voisins du dernier ajout de _closeNode
    }
}

