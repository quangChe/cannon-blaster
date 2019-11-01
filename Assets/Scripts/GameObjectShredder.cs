using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectShredder : MonoBehaviour
{
    public LevelSpawnController spawnCtrl;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject.transform.parent.gameObject;
        BallData data = obj.GetComponent<BallConfigurations>().data;
        spawnCtrl.RemoveFromActive(obj, data);
        Destroy(obj);
    }
}
