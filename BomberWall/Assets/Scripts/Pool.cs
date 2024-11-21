using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField]
    private GameObject _bombe;

    public Stack<GameObject> _stack = new();

    public static Pool Instance;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _stack.Clear();
        for (int i = 0; i < 10; i++) //On cr�e 30 pr�fab (par d�faut ils sont d�sactiv�).
        {
            GameObject newPrefab = Instantiate(_bombe);
            newPrefab.transform.parent = transform; //on range
            _stack.Push(newPrefab);
        }
    }


    public void Spawn()
    {
        GameObject Object = _stack.Pop(); //On le sort de la pile
        Object.SetActive(true);
        _stack.Push(Object); //puis on le remet � la fin
    }
}