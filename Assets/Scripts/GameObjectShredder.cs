using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectShredder : MonoBehaviour
{
    public LevelSpawnController spawnCtrl;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject target = other.gameObject.transform.parent.gameObject;
        BallData data = target.GetComponent<BallConfigurations>().data;
        spawnCtrl.RemoveFromActive(data);
        Destroy(target);
    }
}
