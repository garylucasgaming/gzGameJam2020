using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private GameManager GM;
    private GameObject _item;
    

    private void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        SpawnItem();
    }

    void Start()
    {
        
    }

    private void SpawnItem()
    {
        _item = GM._itemList[UnityEngine.Random.Range(0, GM._itemList.Count)];
        var newItem = Instantiate(_item, transform.position, Quaternion.identity);
        GM.spawnedItemsList.Add(newItem);
    }
}
