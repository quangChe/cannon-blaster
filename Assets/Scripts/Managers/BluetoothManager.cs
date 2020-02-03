using System;
using System.Linq;
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

    private readonly string DeviceName = "fitmi-plus";
    private readonly string ServiceUUID = "6e400001-b5a3-f393-e0a9-e50e24dcca9e";
    private readonly string RXUUID = "6e400002-b5a3-f393-e0a9-e50e24dcca9e";
    private readonly string TXUUID = "6e400003-b5a3-f393-e0a9-e50e24dcca9e";

    public bool connected = false;
    public GameManager Game;
    public GameObject deinitializeButton;

    private string deviceAddress;
    public bool gameInitialized = false;
    private bool foundTXUUID = false;
    private bool foundRXUUID = false;
    //private bool rssiOnly = false;
    //private int rssi = 0;
    private float timeout;


    private bool deltaChangeStarted;
    private float deltaChangeTimer = 0;

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
        foundRXUUID = false;
        //rssi = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //deinitializeButton.SetActive(Application.isEditor);
        InitializeBluetooth();
    }

    void Update()
    {
        RunBluetoothSequence();
        if (deltaChangeStarted)
        {
  
        }
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

        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
        {
            if (name.Contains(DeviceName))
            {
                BluetoothLEHardwareInterface.StopScan();
                deviceAddress = address;
                SetState(States.Connect, 0.5f);
            }
        });
    }

    private void ConnectToTargetDevice()
    {
        BluetoothLEHardwareInterface.Log("Connecting to " + DeviceName);
        foundTXUUID = false;
        foundRXUUID = false;

        BluetoothLEHardwareInterface.ConnectToPeripheral(deviceAddress, null, null,
            (address, serviceUUID, characteristicUUID) => { 
     
            if (IsEqual(serviceUUID, ServiceUUID))
            {
                BluetoothLEHardwareInterface.Log("Connected to Puck UUID: " + serviceUUID);
                foundTXUUID = foundTXUUID || IsEqual(characteristicUUID, TXUUID);
                foundRXUUID = foundRXUUID || IsEqual(characteristicUUID, RXUUID);

                    // Make sure there is enough timeout that if the device is still enumerating other characteristics
                    // it finishes before we try to subscribe
                if (foundTXUUID && foundRXUUID)
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

        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(deviceAddress, ServiceUUID, TXUUID, null,
            (address, characteristicUUID, bytes) =>
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

    private void ProcessButton(byte[] input)
    {
        //if (!gameInitialized || Game.paused) return;
        switch (input[0])
        {
            case 16:
                spawnCtrl.DestroyActiveObject("LS");
                break;
            case 2:

                float rotationDelta = BitConverter.ToSingle(input, 4);
                Debug.Log("!!!" + rotationDelta);
                if (rotationDelta >= 320f && !deltaChangeStarted)
                    deltaChangeStarted = true;
                else if (rotationDelta <= 310f && deltaChangeStarted)
                {
                    deltaChangeStarted = false;
                    spawnCtrl.DestroyActiveObject("DK");
                }
                break;
            default:
                break;
        }

        Debug.Log(input[0]);
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

    public IEnumerator BlinkSuccess(int numberOfblinks, int[] deviceNumber)
    {
        List<byte> data = new List<byte>() {
            6, /* Message code to set LED */
            3, /* Fitmi Plus Device Code */
            6, /* Type of UINT8 */
            4, /* Number of items sent */
            //(byte)0xe8, /* LSB */
            //(byte)0x03, /* LSB 2*/
            //(byte)0x00, /* LSB 3*/
            //(byte)0x00,  /* MSB */
        };
        
        data.Add(0);
        data.Add(255);
        data.Add(0);
        data.Add(0);

        foreach (int number in deviceNumber)
        {
            for (var i = 0; i < numberOfblinks; i++)
            {
                data[5] = 255;
                data[8] = (byte)number;
                WriteCharacteristic(data.ToArray());
                yield return new WaitForSeconds(.2f);
                data[5] = 0;
                WriteCharacteristic(data.ToArray());
            }
        }

        
    }

    private void WriteCharacteristic(byte[] data)
    {
        BluetoothLEHardwareInterface.WriteCharacteristic(deviceAddress, ServiceUUID, RXUUID, data.ToArray(), data.Length, true, (characteristicUUID) =>
        {
            BluetoothLEHardwareInterface.Log("Write Succeeded with characteristic: " + characteristicUUID);
        });
    }

    public void DeinitializeBluetooth()
    {
        BluetoothLEHardwareInterface.DeInitialize(() =>
        {
            Debug.Log("Bluetooth has shut down.");
        });
    }

    private void OnApplicationQuit()
    {
        BluetoothLEHardwareInterface.StopScan();
        BluetoothLEHardwareInterface.DisconnectAll();
        BluetoothLEHardwareInterface.DeInitialize(() =>
        {
            Debug.Log("Bluetooth has shut down on application quit.");
        });
    }

    private void OnDestroy()
    {
        Debug.Log("BluetoothManager - OnDestroy()");

        if (Instance == this)
        {
            try
            {
                BluetoothLEHardwareInterface.StopScan();
                BluetoothLEHardwareInterface.DisconnectAll();
                BluetoothLEHardwareInterface.DeInitialize(() => {
                    Debug.Log("Bluetooth has shut down on destroy.");
                });
            }
            catch (Exception e)
            {
                Debug.Log("Bluetooth has already shut down: " + e.Message);
            }

        }
    }
}
