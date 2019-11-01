using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightswitchInput : MonoBehaviour
{
    BallData data;
    LevelSpawnController spawnCtrl;
    Bluetooth Controller;

    private void Start()
    {
        spawnCtrl = FindObjectOfType<LevelSpawnController>();
        Controller = FindObjectOfType<Bluetooth>();
    }

    public void StoreObjectData(BallData d)
    {
        data = d;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        if (Controller.ButtonCode == (int)1)
        {
            spawnCtrl.DestroyActiveObject(data);
        }
    }
}
