using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BallObject
{
    /*
        Shortened exercise values:

        Light switch: "LS",
        Pouring cup: "CP",
        Doorknob: "DK",
        Zipper: "ZP",
    */
    public int id;
    public float fallDelay;
    public string exercise;
    public float timeDelay;
}
