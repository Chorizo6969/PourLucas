using System.Collections.Generic;
using UnityEngine;

public class BombeEmplacement : MonoBehaviour
{
    [SerializeField]
    private List<SpriteRenderer> _listNode = new List<SpriteRenderer>();

    public static BombeEmplacement Instance;

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
        int index = Random.Range(0, _listNode.Count);
        Debug.Log(index);
        GameObject Object = Pool.Instance._stack.Pop(); //On le sort de la pile
        Object.transform.position = _listNode[index].transform.position;
        Object.SetActive(true);
        //Pool.Instance._stack.Push(Object); //puis on le remet à la fin
    }
}
