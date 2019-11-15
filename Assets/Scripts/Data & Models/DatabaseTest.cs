using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;
using System.Data;

public class DatabaseTest : MonoBehaviour
{
    void Start()
    {
        //LevelsData levelData = new LevelsData();

        //levelData.addData(new LevelsModel(1, 2, 3, 4));
        //levelData.addData(new LevelsModel(2, 3, 4, 5));
        //levelData.addData(new LevelsModel(2, 4, 5, 6));

        //levelData.close();

        LevelsData levelData2 = new LevelsData();

        IDataReader reader = levelData2.getAllData();
        List<LevelsModel> levelsList = new List<LevelsModel>();

        while (reader.Read())
        {
            levelsList.Add(
                new LevelsModel(
                    //Convert.ToInt32(reader[0]), <-- This is the id
                    Convert.ToInt32(reader[1]),
                    Convert.ToInt32(reader[2]),
                    Convert.ToInt32(reader[3]),
                    Convert.ToInt32(reader[4]),
                    reader[5].ToString())
            );
        }

        levelData2.close();

        foreach (LevelsModel level in levelsList)
        {
            Debug.Log("Level Number: " + level._levelNumber);
            Debug.Log("Stars: " + level._starsCollected);
            Debug.Log("Success: " + level._successCount);
            Debug.Log("Total: " + level._totalCount);
            Debug.Log("Date: " + level._date);
        }
        
    }
}
