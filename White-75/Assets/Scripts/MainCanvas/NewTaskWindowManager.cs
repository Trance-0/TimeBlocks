﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTaskWindowManager : MonoBehaviour { 
        //local configurations
    public BlockChain blockChain;
    public GameObject SimpleWindow;
    public GameObject AdvancedWindow;
    public TimeBlock newTimeBlock;
    public InputField TaskNameS;
    public InputField TaskNameA;
    public Dropdown Year;
    public Dropdown Month;
    public Dropdown Day;
    public Dropdown Chunk;
    public Dropdown Tags;
    public InputField EstimateTime;
    public Dictionary<string,int> tempDictionary;

    //global configurations
    public DataManager dataManager;
    public ConfigManager configManager;

    // Start is called before the first frame update
    void Start()
    {
        CloseWindow();
        Invoke("LateInit",0.1f);
    }

   public void LateInit()
    {
        blockChain.LateInit();
        DateTime deadline = DateTime.Now.Add(dataManager.defaultDeadline);
        Year.options.Clear();
        for (int i = 0; i < 10; i++)
        {
            Year.options.Add(new Dropdown.OptionData((deadline.Year + i).ToString()));
        }
        Year.value = Year.options.IndexOf(new Dropdown.OptionData(deadline.Year.ToString()));
        Month.options.Clear();
        for (int i = 0; i < 12; i++)
        {
            Month.options.Add(new Dropdown.OptionData(((deadline.Month + i - 1) % 12 + 1).ToString()));
        }
        Month.value = Month.options.IndexOf(new Dropdown.OptionData(deadline.Month.ToString()));
        Day.options.Clear();
        int dayInTime = DateTime.DaysInMonth(int.Parse(Year.options[Year.value].text), int.Parse(Month.options[Month.value].text));
        for (int i = 0; i < dayInTime; i++)
        {
            Day.options.Add(new Dropdown.OptionData(((deadline.Day + i) % dayInTime + 1).ToString()));
        }
        Day.value = Day.options.IndexOf(new Dropdown.OptionData(deadline.Day.ToString()));
        Chunk.options.Clear();
        Chunk.options.Add(new Dropdown.OptionData("Morning"));
        Chunk.options.Add(new Dropdown.OptionData("Afternoon"));
        Chunk.options.Add(new Dropdown.OptionData("Evening"));
        tempDictionary = new Dictionary<string, int>();
        Tags.options.Clear();
        for (int i= 0;i<7;i++)
        {
            if (tempDictionary.ContainsKey(dataManager.tags[i]._name)) {
                dataManager.tags[i]._name += "(0)";

            }
            Debug.Log(string.Format("Adding Tag {0} to dictionary with ID {1}",dataManager.tags[i]._name, i));
            tempDictionary.Add(dataManager.tags[i]._name, i);
            Tags.options.Add(new Dropdown.OptionData(dataManager.tags[i]._name));
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
                Debug.Log(e);
                timeToFinish = -1;
            }
            int year = int.Parse(Year.options[Year.value].text);
            int month = int.Parse(Month.options[Month.value].text);
            int day = int.Parse(Day.options[Day.value].text);
            int tagId =tempDictionary[Tags.options[Tags.value].text];

            newTimeBlock = new TimeBlock(taskname, year,month,day,chunkid, timeToFinish, tagId);
        }
        else {
            string taskname = TaskNameS.text;
            newTimeBlock = Instantiate<TimeBlock>(ScriptableObject.CreateInstance<TimeBlock>());
            newTimeBlock._name = taskname;
            DateTime a = DateTime.Now.Add(dataManager.defaultDeadline);
            newTimeBlock._deadline = newTimeBlock.SetTime(a);
            newTimeBlock._tagId = dataManager.defaultTagIndex;
        }
        configManager.lastInput = newTimeBlock;
        Debug.Log(newTimeBlock.name);
    }
    //this fuction is used to change day when month or year is modified during creating task.
    public void ChangeDay() {
        try
        {
            Day.options.Clear();
            int dayInTime = DateTime.DaysInMonth(int.Parse(Year.options[Year.value].text), int.Parse(Month.options[Month.value].text));
            for (int i = 0; i < dayInTime; i++)
            {
                Day.options.Add(new Dropdown.OptionData(((DateTime.Today.Day + i) % dayInTime + 1).ToString()));
            }
        }
        catch (Exception e) {
            Debug.LogWarning(e);
        }
    }
    public void Save() {
        BuildBlock();
        Debug.Log("Adding new block: "+newTimeBlock.ToString());
        dataManager.AddBlock(newTimeBlock);
        dataManager.SaveData();
        blockChain.LateInit();
    }
    public void CloseWindow() {
        configManager.isAddNewTaskWindowAwake = false;
            AdvancedWindow.SetActive(false);
            SimpleWindow.SetActive(false);
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
