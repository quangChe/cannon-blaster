using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMapper
{
    public static void MountScript(GameObject ball, GameObject explosion, string exercise)
    {
        if (exercise == "LS")
        {
            LightswitchInput script = ball.AddComponent<LightswitchInput>();
            script.SetGameObjects(ball, explosion);
        }
        else if (exercise == "DK")
        {
            DoorknobInput script = ball.AddComponent<DoorknobInput>();
            script.SetGameObjects(ball, explosion);
        }
        else if (exercise == "ZP")
        {
            ZipperInput script = ball.AddComponent<ZipperInput>();
            script.SetGameObjects(ball, explosion);
        }
        else if (exercise == "CP")
        {
            CupInput script = ball.AddComponent<CupInput>();
            script.SetGameObjects(ball, explosion);
        }
    }
}
