﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortBy : MonoBehaviour
{
    public DataManager dataManager;
    public BlockChain blockChain;
    public Button controlButton;
    public Sprite sortByTimeIcon;
    public Sprite sortByPriorityIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick() {
        if (!dataManager.isAddNewTaskWindowAwake)
        {
            if (dataManager.sortingByTime)
            {
                controlButton.image.sprite = sortByPriorityIcon;
                dataManager.sortingByTime = false;
                blockChain.ShowBlockChain();
            }
            else
            {
                controlButton.image.sprite = sortByTimeIcon;
                dataManager.sortingByTime = true;
                blockChain.ShowBlockChain();
            }
        }
    }
}