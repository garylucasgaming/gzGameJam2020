using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] public List<GameObject> _itemList;
    [SerializeField] public List<GameObject> _groceryList;
    [SerializeField] private int _groceryListSize = 6;
    [SerializeField] private GameObject _groceryListTextObject;
    private TextMeshProUGUI _groceryListText;
    [SerializeField] private TextMeshProUGUI _TimerText;
    [SerializeField] private Timer GMtimer;
    [SerializeField]public List<GameObject> _toggleList;
    [SerializeField] public List<GameObject> _collectedItemList ;

    [SerializeField] public List<GameObject> spawnedItemsList;

    [SerializeField] public GameObject WinPanel;
    [SerializeField] public GameObject LosePanel;
    [SerializeField] public TextMeshProUGUI winLostText;




    private void Awake()
    {
        _groceryListText = _groceryListTextObject.GetComponent<TextMeshProUGUI>();
        WinPanel.gameObject.SetActive(false);
        LosePanel.gameObject.SetActive(false);
    }

  

    private void Update()
    {
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        if(GMtimer.timer <= 0)
        {
            _TimerText.text = "Times Up!";
            return;
        }
        
        _TimerText.text = "Timer: " + TimeSpan.FromSeconds(GMtimer.timer).ToString(@"mm\:ss");
    }

    private void Start()
    {

        
        CreateGroceryList();
       
        UpdateGroceryList();
        UpdateToggles();
        //AddToCollectedList(_groceryList[UnityEngine.Random.Range(0, _groceryList.Count)]);
        //AddToCollectedList(_groceryList[UnityEngine.Random.Range(0, _groceryList.Count)]);
        //AddToCollectedList(_groceryList[UnityEngine.Random.Range(0, _groceryList.Count)]);
        //AddToCollectedList(_groceryList[UnityEngine.Random.Range(0, _groceryList.Count)]);


        _TimerText.text = "Timer: 10:00";
    }

    public void AddToCollectedList(GameObject go)
    {
        _collectedItemList.Add(go);
        UpdateToggles();
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
        if(_collectedItemList.Count == 0)
        {
            foreach(GameObject toggle in _toggleList)
            {
                toggle.gameObject.transform.Find("Background").transform.Find("Checkmark").gameObject.SetActive(false);
            }
            return;
        }

        List<GameObject> tempList = new List<GameObject>(_collectedItemList.Count);

        foreach (GameObject go in _collectedItemList)
        {
            tempList.Add(go);
        }
        print("Updating Toggles, here is tempList: ");
        foreach (var x in tempList)
        {
            Debug.Log(x.ToString());
        }

        for (int i = 0; i < _groceryList.Count; i++)
        {
            GameObject groceryItemGO = _groceryList[i];

            for (int j = tempList.Count - 1; j >= 0; j--)
            {
                GameObject itemGO = tempList[j];

                if (itemGO.name == groceryItemGO.name)
                {
                    tempList.Remove(itemGO);
                    _toggleList[i].gameObject.transform.Find("Background").transform.Find("Checkmark").gameObject.SetActive(true);
                    break;
                }
            }
        }
        //foreach(GameObject groceryItem in _groceryList)
        //{

        //    int i = 0;
        //    //foreach(GameObject item in tempList)
        //    //{

        //    //    if(item.name == groceryItem.name)
        //    //    {
        //    //        tempList.Remove(item);
        //    //        _toggleList[i].gameObject.transform.Find("Background").transform.Find("Checkmark").gameObject.SetActive(true);


        //    //    }

        //    //}
        //    for (int j = 0; j < tempList.Count; j++)
        //    {

        //    }
        //    i++;
        //}


    }


    public void CreateGroceryList()
    {
        for(int i = 0; i < _groceryListSize; i++)
        {
            _groceryList.Add(spawnedItemsList[UnityEngine.Random.Range(0, spawnedItemsList.Count)]);
        }
    }
}
