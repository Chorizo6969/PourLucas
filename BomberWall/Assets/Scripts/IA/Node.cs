using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> _listNodesVoisin = new List<Node>();
    [SerializeField]
    private GameObject _pointArrivé;

    public float _hCost;
    public float _fCost;

    public bool _isVisited;
    public bool _isOpen;

    public bool _isGood;

    private void Awake()
    {
        _hCost = (gameObject.transform.position - _pointArrivé.transform.position).magnitude;
    }
    //Il faut que chaque node connaisse la distance par rapport à l'arrivée
    //2 listes, 1 de liste de node parcourue,
}