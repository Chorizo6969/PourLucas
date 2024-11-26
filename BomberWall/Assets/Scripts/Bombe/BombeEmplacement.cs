using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BombeEmplacement : MonoBehaviour
{
    public List<SpriteRenderer> _listNode = new List<SpriteRenderer>();

    public static BombeEmplacement Instance;

    public event Action<GameObject> OnBombSpawn;

    [SerializeField] private int bombOnMap;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < bombOnMap; i++) //met le nombre max de bombe sur le terrain (défini dans l'inspecteur)
        {
            ChoseBombEmplacement();
        }
    }

    /// <summary>
    /// Met une bombe à une position aléatoire sur la map
    /// </summary>
    public void ChoseBombEmplacement()
    {
        int index = UnityEngine.Random.Range(0, _listNode.Count);
        GameObject Object = Pool.Instance._stack.Pop(); //On le sort de la pile
        Object.transform.position = _listNode[index].transform.position;
        Object.GetComponent<Bomb>().CanBeTake = true;
        Object.SetActive(true);
        OnBombSpawn?.Invoke(Object);
    }

    
    //Fonctions Naughty Attributes

    //ajoute à chaque node de la liste global des nodes le composant Node et fais les listes de voison de chaque node
    [Button("Set up node")]
    public void SetUpNode()
    {
        for (int i = 0; i <= _listNode.Count - 1; i++)
        {
            if (!_listNode[i].GetComponent<Node>())
            {
                _listNode[i].gameObject.AddComponent<Node>();
            }
        }

        for (int i = 0; i <= _listNode.Count - 1; i++)
        {
            for (int j = 0; j <= _listNode.Count - 1; j++)
            {
                if ((_listNode[i].transform.position - _listNode[j].transform.position).magnitude <= 1.05 && _listNode[i] != _listNode[j] && !_listNode[i].GetComponent<Node>()._listNodesVoisin.Contains(_listNode[j].GetComponent<Node>()))
                {
                    _listNode[i].GetComponent<Node>()._listNodesVoisin.Add(_listNode[j].GetComponent<Node>());
                }
            }
        }
    }

    //Reset chaque node de la liste
    [Button("Node Reset")]
    public void ResetNode()
    {
        for (int i = 0; i <= _listNode.Count - 1; i++)
        {
            DestroyImmediate(_listNode[i].gameObject.GetComponent<Node>());
        }
    }

    //change les noms des node afin qu'on puisse mieux se repérer que s'ils s'appellent tous WayPoint
    [Button("Je met des noms correct pour mes nodes de merde")]
    public void ChangeName()
    {
        int count = 1;
        foreach (SpriteRenderer node in _listNode)
        {
            node.name = "Node " + "(" + count + ")";
            count++;
        }
    }

    //Desactive le sprite Renderer des node 
    [Button("Unactive Sprite Renderer")]
    public void Unactive()
    {
        foreach (SpriteRenderer node in _listNode)
        {
            node.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    //active le sprite Renderer des node pour rendre visible l'algorithme astar
    [Button("Active Sprite Renderer")]
    public void Active()
    {
        foreach (SpriteRenderer node in _listNode)
        {
            node.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}