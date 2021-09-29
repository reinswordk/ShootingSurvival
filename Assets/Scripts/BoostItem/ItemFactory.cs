using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour, IFactory
{
    [SerializeField]
    GameObject[] itemPrefab;

    public GameObject FactoryMethod(int tag, Transform spawnPoint)
    {
        GameObject item = Instantiate(itemPrefab[tag],spawnPoint);
        return item;
    }
}
