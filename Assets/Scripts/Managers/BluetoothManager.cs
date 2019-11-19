using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluetoothManager : MonoBehaviour
{
    public static BluetoothManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private SpawnController spawnCtrl;

    private readonly string DeviceName = "fitmi-puck";
    private readonly string ServiceUUID = "6e400001-b5a3-f393-e0a9-e50e24dcca9e";
    //private readonly string RXUUID = "6e400002-b5a3-f393-e0a9-e50e24dcca9e";
    private readonly string TXUUID = "6e400003-b5a3-f393-e0a9-e50e24dcca9e";

    public bool connected = false;
    public GameManager Game;
    public GameObject deinitializeButton;

    private string deviceAddress;
    private bool gameInitialized = false;
    private bool foundTXUUID = false;
    //private bool foundRXUUID = false;
    //private bool rssiOnly = false;
    //private int rssi = 0;
    private float timeout;

    enum States
    {
        None,
        Scan,
        //ScanRSSI,
        Connect,
        Subscribe,
        //Unsubscribe,
        //Disconnect,
    }
    private States state = States.None;

    void SetState(States newState, float newTimeout)
    {
        state = newState;
        timeout = newTimeout;
    }

    void Reset()
    {
        connected = false;
        timeout = 0f;
        state = States.None;
        deviceAddress = null;
        foundTXUUID = false;
        //foundRXUUID = false;
        //rssi = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        deinitializeButton.SetActive(Application.isEditor);
        InitializeBluetooth();
    }

    void Update()
    {
        RunBluetoothSequence();
    }

    public void MountToLevel(SpawnController controller)
    {
        spawnCtrl = controller;
        gameInitialized = true;
    }

    private void InitializeBluetooth()
    {
        Reset();
        BluetoothLEHardwareInterface.Initialize(true, false, () =>
        {
            SetState(States.Scan, 0.1f);

        }, (error) =>
        {

            BluetoothLEHardwareInterface.Log("Error during initialize: " + error);

        });
    }

    private void RunBluetoothSequence()
    {
        if (timeout > 0f)
        {
            timeout -= Time.fixedDeltaTime;

            if (timeout <= 0f)
            {
                timeout = 0f;

                switch (state)
                {
                    case States.None:
                        break;

                    case States.Scan:
                        SearchForTargetDevice();
                        break;

                    case States.Connect:
                        ConnectToTargetDevice();
                        break;

                    case States.Subscribe:
                        SubscribeToTargetDevice();
                        break;
                }
            }
        }
    }

    private void SearchForTargetDevice()
    {
        BluetoothLEHardwareInterface.Log("Scanning for " + DeviceName);

        Debug.Log("This is running!");

        // Seems to stop here...

        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
        {
            Debug.Log("This is not running...");
            if (name.Contains(DeviceName))
            {
                BluetoothLEHardwareInterface.StopScan();
                deviceAddress = address;
                SetState(States.Connect, 0.5f);
            }
        }
        //, (address, name, deviceRssi, bytes) =>
        //{
        //    Debug.Log("This is not running...");
        //    BluetoothLEHardwareInterface.Log("****3" + address);
        //    if (name.Contains(DeviceName))
        //    {
        //        BluetoothLEHardwareInterface.Log("Found with method 2! " + bytes[0]);
        //        BluetoothLEHardwareInterface.StopScan();
        //        //rssi = deviceRssi;
        //        deviceAddress = address;
        //        SetState(States.Connect, 0.5f);
        //    }
        //}
        );
    }

    private void ConnectToTargetDevice()
    {
        BluetoothLEHardwareInterface.Log("Connecting to " + DeviceName);
        foundTXUUID = false;
        //foundRXUUID = false;

        BluetoothLEHardwareInterface.ConnectToPeripheral(deviceAddress, null, null,
            (address, serviceUUID, characteristicUUID) => { 
     
            if (IsEqual(serviceUUID, ServiceUUID))
            {
                BluetoothLEHardwareInterface.Log("Connected to Puck UUID: " + serviceUUID);
                foundTXUUID = foundTXUUID || IsEqual(characteristicUUID, TXUUID);
                //foundRXUUID = foundRXUUID || IsEqual(characteristicUUID, RXUUID);

                // Make sure there is enough timeout that if the device is still enumerating other characteristics
                // it finishes before we try to subscribe
                //if (foundTXUUID && foundRXUUID)
                if (foundTXUUID)
                {
                    SetState(States.Subscribe, 2f);
                }
            }
        },
        (err) =>
        {
            connected = false;
            SetState(States.Connect, 0.5f);
        }
        );
    }

    private void SubscribeToTargetDevice()
    {
        BluetoothLEHardwareInterface.Log("Subscribing to characteristics...");

        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(deviceAddress, ServiceUUID, TXUUID, (notifyAddress, notifyCharacteristic) =>
        {

            BluetoothLEHardwareInterface.Log("Waiting for user action (1)...");
            connected = true;

            state = States.None;

            // read the initial state of the button
            BluetoothLEHardwareInterface.ReadCharacteristic(deviceAddress, ServiceUUID, TXUUID, (characteristic, bytes) =>
            {
                ProcessButton(bytes);
            });

        }, (address, characteristicUUID, bytes) =>
        {
            BluetoothLEHardwareInterface.Log("Waiting for user action (2)...");
            connected = true;

            if (state != States.None)
            {
                state = States.None;
            }

            // we received some data from the puck
            ProcessButton(bytes);
        });
    }

    private void ProcessButton(byte[] bytes)
    {
        if (gameInitialized && !Game.paused)
        {
            if (bytes[0] == 1)
            {
                Debug.Log("CLICKED");
                spawnCtrl.DestroyActiveObject("LS");
            }
            if (bytes[0] == 3)
            {
                Debug.Log("CLICKED");
                spawnCtrl.DestroyActiveObject("DK");
            }
        }
    }

    string FullUUID(string uuid)
    {
        string fullUUID = uuid;
        if (fullUUID.Length == 4)
            fullUUID = "6e40" + uuid + "-b5a3-f393-e0a9-e50e24dcca9e";

        return fullUUID;
    }

    private bool IsEqual(string uuid1, string uuid2)
    {
        if (uuid1.Length == 4)
            uuid1 = FullUUID(uuid1);
        if (uuid2.Length == 4)
            uuid2 = FullUUID(uuid2);

        return (uuid1.ToUpper().Equals(uuid2.ToUpper()));
    }

    //void SendByte(byte value)
    //{
    //    byte[] data = { value, (byte)0x0, (byte)0x2, (byte)0x1 };
    //    //byte[] data = { value };
    //    BluetoothLEHardwareInterface.WriteCharacteristic(deviceAddress, ServiceUUID, RXUUID, data, data.Length, true, (characteristicUUID) =>
    //    {
    //        BluetoothLEHardwareInterface.Log("Write Succeeded with characteristic: " + characteristicUUID);
    //    });
    //}

    public void DeinitializeBluetooth()
    {
        BluetoothLEHardwareInterface.DeInitialize(() =>
        {
            Debug.Log("Bluetooth has shut down.");
        });
    }
}
