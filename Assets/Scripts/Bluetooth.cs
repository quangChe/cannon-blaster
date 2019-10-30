using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bluetooth : MonoBehaviour
{
    private float _timeout;
    private float _startScanTimeout = 10f;
    private float _startScanDelay = 0.5f;
    private bool _startScan = true;
    private Dictionary<string, ScannedItemScript> _scannedItems;

    // Start is called before the first frame update
    void Start()
    {
        BluetoothLEHardwareInterface.Log("Start");
        _scannedItems = new Dictionary<string, ScannedItemScript>();

        BluetoothLEHardwareInterface.Initialize(true, false, () => {
            Debug.Log("It's working!");
            _timeout = _startScanDelay;
        },
        (error) => {

            BluetoothLEHardwareInterface.Log("Error: " + error);

            if (error.Contains("Bluetooth LE Not Enabled"))
                BluetoothLEHardwareInterface.BluetoothEnable(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
