﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBlockUI : MonoBehaviour
{
    public Image icon;
    public Text taskName;
    public Image backGround;
    public Transform horizontalLayOut;
    public TimeBlock timeBlock;
    public BlockChain blockChain;
    public Collider Delete;
    public Collider Finished;
    public Collider TaskBody;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TaskFinished()
    {
        blockChain.MarkAsFinished(timeBlock);
        Debug.Log("Finished");
    }
    public void TaskDeleted()
    {
        Debug.Log("Delete");
        blockChain.DeleteBlock(timeBlock);
    }
    }