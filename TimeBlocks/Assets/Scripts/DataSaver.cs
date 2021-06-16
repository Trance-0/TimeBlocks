﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class DataSaver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveConfig(DataManager dm,string jsonName) {
        JsonData jd = new JsonData();
        jd.SetJsonType(JsonType.Array);
        JsonData item = new JsonData();
        item["completionCheck"] = dm.completionCheck;
        item["enableTimer"] = dm.enableTimer;
        item["analyseOCT"] = dm.analyseOCT;
        item["OCT"] = dm.OCT;
        item[" OCTAuto"] = dm.OCTAuto;
        item["R"] = dm.backgroundColor.r;
        item["G"] = dm.backgroundColor.g;
        item["B"] = dm.backgroundColor.b;
        jd.Add(item);
        SaveBlocks(dm.sortByTime,"sortByTime");
        SaveBlocks(dm.sortByPriority, "sortByPriority");
        SaveBlocks(dm.deletedTask, "deletedTask");
        SaveBlocks(dm.finishedTask,"finishedTask");
        SaveTags(dm.tagDictionary, "tagDictionary");
        using (StreamWriter sw = new StreamWriter("C:/Users/Trance/Documents/Github/TimeBlocks/TimeBlocks/Assets/Resources/config.json"))
        {
            sw.Write(JsonMapper.ToJson(jd));
        }
    }
    public void SaveTags(Dictionary<string,Tag> tags,string jsonName) {
        JsonData jd = new JsonData();
        jd.SetJsonType(JsonType.Array);
        foreach (string k in tags.Keys)
        {
            Tag i = tags[k];
            JsonData item = new JsonData();
            item["_tagName"] = i._tagName;
            item["_power"] = i._power;
            item["_isDefault"] = i._isDefault;
           // item["_image"] = i._image;
            jd.Add(item);
        }
        using (StreamWriter sw = new StreamWriter("C:/Users/Trance/Documents/Github/TimeBlocks/TimeBlocks/Assets/Resources/" + jsonName + ".json"))
        {
            sw.Write(JsonMapper.ToJson(jd));
        }
    }
    public void SaveBlocks(List<TimeBlock> blocks,string jsonName) {
        JsonData jd = new JsonData();
        jd.SetJsonType(JsonType.Array);
        foreach (TimeBlock i in blocks) {
            JsonData item = new JsonData();
            item["_name"] = i._name;
            item["_year"] = i._year;
            item["_month"] = i._month;
            item["_day"] = i._day;
            item["_chunk"] = i._chunk;
            item["_timeRequired"] = i._timeRequired;
            item["_tag"] = i._tag;
            item["_priority"] = i._priority;
            item["_isOver"] = i._isOver;
            item["_isFailed;"] = i._isFailed;
            jd.Add(item);
}
        using (StreamWriter sw = new StreamWriter("C:/Users/Trance/Documents/Github/TimeBlocks/TimeBlocks/Assets/Resources/"+jsonName+".json"))
        {
            sw.Write(JsonMapper.ToJson(jd));
        }
    }
    public void LoadConfig(DataManager dm, string jsonName)
    {
        List<TimeBlock> blocks = new List<TimeBlock>();
        string json = Resources.Load(jsonName).ToString();
        JsonData jd = JsonMapper.ToObject(json);
        JsonData item =jd[0];
         dm.completionCheck=bool.Parse(item["completionCheck"].ToString());
        dm.enableTimer = bool.Parse(item["enableTimer"].ToString());
         dm.analyseOCT = bool.Parse(item["analyseOCT "].ToString());
        dm.OCT = int.Parse(item["completionCheck"].ToString());
       dm.OCTAuto = bool.Parse(item["completionCheck"].ToString());
        dm.backgroundColor.r = int.Parse(item["completionCheck"].ToString());
       dm.backgroundColor.g = int.Parse(item["completionCheck"].ToString());
        dm.backgroundColor.b = int.Parse(item["completionCheck"].ToString());
        dm.sortByTime=LoadBlocks( "sortByTime");
        dm.sortByPriority=LoadBlocks( "sortByPriority");
        dm.deletedTask= LoadBlocks( "deletedTask");
        dm.finishedTask= LoadBlocks("finishedTask");
    }
    public List<TimeBlock> LoadBlocks( string jsonName)
    {
        List<TimeBlock> blocks = new List<TimeBlock>();
        string json = Resources.Load(jsonName).ToString();
        JsonData jd = JsonMapper.ToObject(json);
        foreach (JsonData item in jd) {
            TimeBlock i = new TimeBlock();
            i._name=item["_name"].ToString();
            i._year=int.Parse( item["_year"].ToString());
           i._month= int.Parse(item["_month"].ToString());
           i._day= int.Parse(item["_day"].ToString());
            i._chunk= int.Parse(item["_chunk"].ToString());
            i._timeRequired= int.Parse(item["_timeRequired"].ToString());
             i._tag=item["_tag"].ToString();
            i._priority = int.Parse(item["_priority"].ToString());
             i._isOver=bool.Parse(item["_isOver"].ToString());
           i._isFailed = bool.Parse(item["_isFailed"].ToString());
            blocks.Add(i);
        }
        return blocks;
    }
    public void TestLoadJson() {
        List<testdata> temp = new List<testdata>();
        string json = Resources.Load("testData").ToString();
        JsonData jd = JsonMapper.ToObject(json);
        testdata td = new testdata();
        td.MissionName = jd[0]["name"].ToString();
        td.tag = jd[0]["Tag"].ToString();
        print(td);
    }
    public void TestSaveJson() {
        JsonData jd = new JsonData();
        jd.SetJsonType(JsonType.Array);
        JsonData item = new JsonData();
        item["name"] = "任务1";
        item["Tag"] = "学习";
        jd.Add(item);
        using (StreamWriter sw = new StreamWriter("C:/Users/Trance/Documents/Github/TimeBlocks/TimeBlocks/Assets/Resources/testData.json"))
        {
            sw.Write(JsonMapper.ToJson(jd));
        }
    }
}
