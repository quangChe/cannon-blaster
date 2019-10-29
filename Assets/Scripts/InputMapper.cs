using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMapper
{
    public static void MountScript(GameObject ball, BallData d)
    {
        if (d.exercise == "LS")
        {
            LightswitchInput script = ball.AddComponent<LightswitchInput>();
            script.StoreObjectData(d);
        }
        else if (d.exercise == "DK")
        {
            DoorknobInput script = ball.AddComponent<DoorknobInput>();
            script.StoreObjectData(d);
        }
        else if (d.exercise == "ZP")
        {
            ZipperInput script = ball.AddComponent<ZipperInput>();
            script.StoreObjectData(d);
        }
        else if (d.exercise == "CP")
        {
            CupInput script = ball.AddComponent<CupInput>();
            script.StoreObjectData(d);
        }
    }
}
