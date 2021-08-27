﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTaskWindowManager : MonoBehaviour
{
    public BlockChain blockChain;
    public GameObject SimpleWindow;
    public TimeBlock newTimeBlock;
    public GameObject AdvancedWindow;
    public InputField TaskNameS;
    public InputField TaskNameA;
    public Dropdown Year;
    public Dropdown Month;
    public Dropdown Day;
    public Dropdown Chunk;
    public Dropdown Tags;
    public InputField EstimateTime;

    public Dictionary<string, Tag> tempDictionary;

    public DataManager dataManager;
    public ConfigManager configManager;

    // Start is called before the first frame update
    void Start()
    {
        UpdateInfo();
    }

   public void UpdateInfo()
    {

        DateTime today = DateTime.Today;
        Year.options.Clear();
        for (int i = 0; i < 10; i++)
        {
            Year.options.Add(new Dropdown.OptionData((today.Year + i).ToString()));
        }
        Month.options.Clear();
        for (int i = 0; i < 12; i++)
        {
            Month.options.Add(new Dropdown.OptionData(((today.Month + i - 1) % 12 + 1).ToString()));
        }
        Day.options.Clear();
        int dayInTime = DateTime.DaysInMonth(int.Parse(Year.options[Year.value].text), int.Parse(Month.options[Month.value].text));
        for (int i = 0; i < 31; i++)
        {
            Day.options.Add(new Dropdown.OptionData(((today.Day + i) % 31 + 1).ToString()));
        }
        Chunk.options.Clear();
        Chunk.options.Add(new Dropdown.OptionData("Morning"));
        Chunk.options.Add(new Dropdown.OptionData("Afternoon"));
        Chunk.options.Add(new Dropdown.OptionData("Evening"));
        tempDictionary = new Dictionary<string, Tag>();
        Tags.options.Clear();
        foreach (int i in dataManager.tagDictionary.Keys)
        {
            tempDictionary.Add(dataManager.tagDictionary[i]._name, dataManager.tagDictionary[i]);
            Tags.options.Add(new Dropdown.OptionData(dataManager.tagDictionary[i]._name));
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
   
    public void BuildBlock() {
       
        if (configManager.isAdvanced) {
            string taskname = TaskNameA.text;
            int chunkid = 0;
            if (Chunk.options[Chunk.value].text == "Morning")
            {
                chunkid = 1;
            }
            else if (Chunk.options[Chunk.value].text == "Afternoon")
            {
                chunkid = 2;
            }
            else if (Chunk.options[Chunk.value].text == "Evening")
            {
                chunkid = 3;
            }
            int timeToFinish;
            try{
                timeToFinish = int.Parse(EstimateTime.text);
            }
            catch (Exception e) {
                timeToFinish = -1;
            }
            newTimeBlock = new TimeBlock(taskname, int.Parse(Year.options[Year.value].text), int.Parse(Month.options[Month.value].text), int.Parse(Day.options[Day.value].text),chunkid, timeToFinish, tempDictionary[Tags.options[Tags.value].text]._tagId);
        }
        else {
            string taskname = TaskNameS.text;
            newTimeBlock = new TimeBlock(taskname);
        }
        configManager.lastInput = newTimeBlock;
        Debug.Log(newTimeBlock.name);
    }
    public void ChangeDay() {
        DateTime today = DateTime.Today;
        Day.options.Clear();
        int dayInTime = DateTime.DaysInMonth(int.Parse(Year.options[Year.value].text), int.Parse(Month.options[Month.value].text));
        for (int i = 0; i < dayInTime; i++)
        {
            Day.options.Add(new Dropdown.OptionData(((today.Day + i) % dayInTime + 1).ToString()));
        }
    }
    public void Save() {
       BuildBlock();
        blockChain.AddBlock(newTimeBlock);
        blockChain.ShowBlockChain();
    }
    public void CloseWindow() {
        configManager.isAddNewTaskWindowAwake = false;
        if (configManager.isAdvanced)
        {
            AdvancedWindow.SetActive(false);
        }
        else
        {
            SimpleWindow.SetActive(false);
        }
    }
    public void OpenWindow() {
        configManager.isAddNewTaskWindowAwake = true;
        if (configManager.isAdvanced)
        {
            AdvancedWindow.SetActive(true);
            TaskNameA.text = configManager.lastInput._name;
        }
        else
        {
            SimpleWindow.SetActive(true);
            TaskNameS.text = configManager.lastInput._name;
        }
    }
    public void SetAdvancedModeOff() {
        BuildBlock();
        CloseWindow();
        configManager.isAdvanced = false;
        OpenWindow();
    }
    public void SetAdvancedModeOn()
    {
        BuildBlock();
        CloseWindow();
        configManager.isAdvanced = true;
        OpenWindow();
    }
}