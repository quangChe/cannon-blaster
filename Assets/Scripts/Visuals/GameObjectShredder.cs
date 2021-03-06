﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectShredder : MonoBehaviour
{
    public SpawnController spawnCtrl;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject target = other.gameObject.transform.parent.gameObject;
        BallObject data = target.GetComponent<BallConfigurations>().data;
        spawnCtrl.RemoveFromActive(data);
        Destroy(target);
    }
}
