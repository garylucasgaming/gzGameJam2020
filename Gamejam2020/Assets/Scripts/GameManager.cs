using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] public List<GameObject> _itemList;
    [SerializeField] private List<GameObject> _groceryList;
    [SerializeField] private int _groceryListSize = 6;
    [SerializeField] private GameObject _groceryListTextObject;
    private TextMeshProUGUI _groceryListText;
    [SerializeField]private List<GameObject> _toggleList;
    [SerializeField] public List<GameObject> _collectedItemList;

    [SerializeField] public List<GameObject> spawnedItemsList;

    private void Awake()
    {
        _groceryListText = _groceryListTextObject.GetComponent<TextMeshProUGUI>();
        
    }


    private void Start()
    {

        
        CreateGroceryList();
        UpdateGroceryList();

        UpdateToggles();
    }

    public void AddToCollectedList(GameObject go)
    {
        _collectedItemList.Add(go);
    }

    public void RemoveFromCollectedList(GameObject go)
    {
        if(_collectedItemList == null)
        {
            Debug.Log("_collectedItemList in GameManager is null.  RemoveFromCollectedList did nothing");
            return;
        } 

        if(_collectedItemList.Contains(go))
        {
            _collectedItemList.Remove(go);
        }
    }


    public void UpdateGroceryList()
    {
        _groceryListText.text = "";

        foreach(GameObject item in _groceryList)
        {
            _groceryListText.text += ("" + item.GetComponent<Item>().itemType + "\n");
        }

    }


    public void UpdateToggles()
    {
        
        if(_groceryList == null)
            return;

        if (_collectedItemList.Count == 0)
        {
            
            foreach(GameObject toggle in _toggleList)
            {
                print(toggle.name);
                toggle.gameObject.transform.Find("Background").transform.Find("Checkmark").gameObject.SetActive(false);
            }
            return;
        }

        foreach(GameObject groceryItem in _groceryList)
        {
            int i = 0;
            foreach(GameObject item in _collectedItemList)
            {
                
                if(item.GetComponent<Item>().itemType == groceryItem.GetComponent<Item>().itemType)
                {
                    _toggleList[i].gameObject.transform.Find("Background").transform.Find("Checkmark").gameObject.SetActive(true);
                    
                }
                
                i++;
            }

        }
        

    }


    public void CreateGroceryList()
    {
        for(int i = 0; i < _groceryListSize; i++)
        {
            _groceryList.Add(spawnedItemsList[UnityEngine.Random.Range(0, spawnedItemsList.Count)]);
        }
    }
}
