using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluetoothPopup : MonoBehaviour
{
    private BluetoothManager bt;
    private GameManager Game;
    private bool popUpActive = false;
    private GameObject activePopUp;

    public GameObject bluetoothPopup;

    private void Start()
    {
        bt = BluetoothManager.Instance;
        Game = GameManager.Instance;
    }
    void Update()
    {
        if (!bt.connected && !popUpActive)
        {
            PopUpConnectionNotice();
        }

        if (bt.connected && popUpActive)
        {
            ClosePopUp();
        }
    }

    private void PopUpConnectionNotice()
    { 
        Game.paused = true;
        popUpActive = true;
        Time.timeScale = 0;
        activePopUp = Instantiate(bluetoothPopup, new Vector3(0, 0, -9), transform.rotation);
    }

    private void ClosePopUp()
    {
        Game.paused = false;
        popUpActive = false;
        Destroy(activePopUp);
        Time.timeScale = 1;
    }
}
