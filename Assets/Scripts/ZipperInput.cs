using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipperInput : MonoBehaviour
{
    BallData data;
    LevelSpawnController spawnCtrl;

    private void Start()
    {
        spawnCtrl = FindObjectOfType<LevelSpawnController>();
    }

    public void StoreObjectData(BallData d)
    {
        data = d;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            spawnCtrl.DestroyActiveObject(data);
        }
    }
}
