using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bluetooth : MonoBehaviour
{
    private string DeviceName = "fitmi-puck";
    private string ServiceUUID = "6e400001-b5a3-f393-e0a9-e50e24dcca9e";
    private string RXUUID = "6e400002-b5a3-f393-e0a9-e50e24dcca9e";
    private string TXUUID = "6e400003-b5a3-f393-e0a9-e50e24dcca9e";


    private string _deviceAddress;
    private bool _connected = false;
    private bool _foundTXUUID = false;
    private bool _foundRXUUID = false;
    //private bool _rssiOnly = false;
    private int _rssi = 0;
    private float _timeout;
    public int ButtonCode = -1;

    enum States
    {
        None,
        Scan,
        //ScanRSSI,
        Connect,
        Subscribe,
        Unsubscribe,
        Disconnect,
    }
    private States _state = States.None;

    void SetState(States newState, float timeout)
    {
        _state = newState;
        _timeout = timeout;
    }

    void Reset()
    {
        _connected = false;
        _timeout = 0f;
        _state = States.None;
        _deviceAddress = null;
        _foundTXUUID = false;
        _foundRXUUID = false;
        _rssi = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeBluetooth();
    }

    void Update()
    {
        RunBluetoothSequence();
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
        if (_timeout > 0f)
        {
            _timeout -= Time.deltaTime;

            if (_timeout <= 0f)
            {
                _timeout = 0f;

                switch (_state)
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
        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, null, (address, name, rssi, bytes) =>
        {
            if (name.Contains(DeviceName))
            {
                BluetoothLEHardwareInterface.Log("Found with method 2! " + bytes[0]);
                BluetoothLEHardwareInterface.StopScan();
                _rssi = rssi;
                _deviceAddress = address;
                SetState(States.Connect, 0.5f);
            }
        });
    }

    private void ConnectToTargetDevice()
    {
        BluetoothLEHardwareInterface.Log("Connecting to other device...");
        _foundTXUUID = false;
        _foundRXUUID = false;

        BluetoothLEHardwareInterface.ConnectToPeripheral(_deviceAddress, null, null, (address, serviceUUID, characteristicUUID) =>
        {
            if (IsEqual(serviceUUID, ServiceUUID))
            {
                BluetoothLEHardwareInterface.Log("Connected to Puck UUID: " + serviceUUID);
                _foundTXUUID = _foundTXUUID || IsEqual(characteristicUUID, TXUUID);
                _foundRXUUID = _foundRXUUID || IsEqual(characteristicUUID, RXUUID);

                // Make sure there is enough timeout that if the device is still enumerating other characteristics
                // it finishes before we try to subscribe
                if (_foundTXUUID && _foundRXUUID)
                {
                    _connected = true;
                    SetState(States.Subscribe, 1f);
                }
            }
        });
    }

    private void SubscribeToTargetDevice()
    {
        BluetoothLEHardwareInterface.Log("Subscribing to characteristics...");

        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_deviceAddress, ServiceUUID, TXUUID, (notifyAddress, notifyCharacteristic) =>
        {
            BluetoothLEHardwareInterface.Log("Waiting for user action (1)...");
            _state = States.None;

            // read the initial state of the button
            BluetoothLEHardwareInterface.ReadCharacteristic(_deviceAddress, ServiceUUID, TXUUID, (characteristic, bytes) =>
            {
                StartCoroutine(ProcessButton(bytes));
            });

        }, (address, characteristicUUID, bytes) =>
        {
            if (_state != States.None)
            {
                // some devices do not properly send the notification state change which calls
                // the lambda just above this one so in those cases we don't have a great way to
                // set the state other than waiting until we actually got some data back.
                // The esp32 sends the notification above, but if yuor device doesn't you would have
                // to send data like pressing the button on the esp32 as the sketch for this demo
                // would then send data to trigger this.
                _state = States.None;
            }

            // we received some data from the device
            StartCoroutine(ProcessButton(bytes));
        });
    }

    private void DisconnectFromDevice()
    {
        throw new NotImplementedException();
    }

    private IEnumerator ProcessButton(byte[] bytes)
    {
        ButtonCode = (int)bytes[0];
        //if ButtonCode == 1: spwnController.destroyActiveObject();
        Debug.Log("??????" + ButtonCode.GetType());
        yield return new WaitForSeconds(0.05f);
        //Debug.Log("Puck message: " + bytes[0]);
        //Debug.Log("Puck color: " + bytes[1]);
        //Debug.Log("Puck message type: " + bytes[2]);
        //Debug.Log("Puck byte length: " + bytes[3]);
        //if (bytes[0] == 2)
        //{
        //    SendByte((byte)0x02);
        //}
        ButtonCode = -1;
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

    void SendByte(byte value)
    {
        byte[] data = { value, (byte)0x0, (byte)0x2, (byte)0x1 };
        //byte[] data = { value };
        BluetoothLEHardwareInterface.WriteCharacteristic(_deviceAddress, ServiceUUID, RXUUID, data, data.Length, true, (characteristicUUID) =>
        {
            BluetoothLEHardwareInterface.Log("Write Succeeded with characteristic: " + characteristicUUID);
        });
    }
}
