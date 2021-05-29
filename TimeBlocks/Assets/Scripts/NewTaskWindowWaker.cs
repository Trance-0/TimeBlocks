﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTaskWindowWaker : MonoBehaviour
{
    public DataManager dataManager;
    public NewTaskWindowManager newTaskWindowManager;
    public Button controlButton;
    public Sprite closeWindowIcon;
    public Sprite openWindowIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick() {
        if (dataManager.isAddNewTaskWindowAwake)
        {
            newTaskWindowManager.CloseWindow();
            controlButton.image.sprite= openWindowIcon;
        }
        else{
            newTaskWindowManager.OpenWindow();
            controlButton.image.sprite = closeWindowIcon;
        }
    }
}
