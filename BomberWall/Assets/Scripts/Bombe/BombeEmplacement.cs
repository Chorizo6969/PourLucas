using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BombeEmplacement : MonoBehaviour
{
    public List<SpriteRenderer> _listNode = new List<SpriteRenderer>();

    public static BombeEmplacement Instance;

    public event Action<GameObject> OnBombSpawn;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChoseBombEmplacement();
        ChoseBombEmplacement();
    }

    public void ChoseBombEmplacement()
    {
        int index = UnityEngine.Random.Range(0, _listNode.Count);
        Debug.Log(index);
        GameObject Object = Pool.Instance._stack.Pop(); //On le sort de la pile
        Object.transform.position = _listNode[index].transform.position;
        Object.SetActive(true);
        Debug.Log(OnBombSpawn);
        Debug.Log(Object);
        OnBombSpawn?.Invoke(Object);

        //Pool.Instance._stack.Push(Object); //puis on le remet à la fin
    }


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

    [Button("aled j'en ai marre")]

    public void ResetNode()
    {
        for (int i = 0; i <= _listNode.Count - 1; i++)
        {
            DestroyImmediate(_listNode[i].gameObject.GetComponent<Node>());
        }
    }

    [Button("Je met des noms correct pour mes nodes de merde")]
    public void AAAAAAAAAAAAAAAAAAAAAAAAaaaaaaaaaaaaaaaaaaaaaaaah()
    {
        int count = 1;
        foreach (SpriteRenderer node in _listNode)
        {
            node.name = "Node " + "(" + count + ")";
            count++;
        }
    }
}