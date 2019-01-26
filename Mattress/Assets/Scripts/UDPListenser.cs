using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UDPListenser : MonoBehaviour
{

    public static event Action<string, float> UDPReceived;

    [SerializeField] private int _port = 10552;

    private UdpClient _client;
    private IPEndPoint _remoteIP;
    private Thread _udpThread;

    private float _lastUpdateTime = 0;

    private List<string> _receivedData;

    // Start is called before the first frame update
    void Start()
    {
        Setup(_port);
    }

    public void Setup(int port)
    {
        _receivedData = new List<string>();

        _client = new UdpClient(port);
        _remoteIP = new IPEndPoint(IPAddress.Any, 0);
        _udpThread = new Thread(new ThreadStart(UDPRead));
        _udpThread.IsBackground = true;
        _udpThread.Start();

        Debug.Log("UDPListener Initialized:" + " Listening to: " + _remoteIP + " Port: " + port);
    }

    private void OnDestroy()
    {
#if UNITY_EDITOR
        // This is only called in builds, so simulate it inside editor as OnDestroy will always be called
        OnApplicationQuit();
#endif
    }

    private void OnApplicationQuit()
    {
        if (_udpThread != null) _udpThread.Abort();
        if (_client != null) _client.Close();
    }

    private void UDPRead()
    {
        while (true)
        {
            try
            {
                byte[] receiveBytes = _client.Receive(ref _remoteIP);
                if (receiveBytes != null && receiveBytes.Length != 0)
                {
                    string returnData = Encoding.ASCII.GetString(receiveBytes);
                    if (returnData.Length != 0)
                    {
                        _receivedData.Add(returnData);
                    }

                }
            }
            catch (Exception e)
            {
                Debug.Log("[BasicUDPListener.UDPRead] Exception: " + e.Message);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_receivedData.Count > 0)
        {
            UDPReceived?.Invoke(_receivedData[_receivedData.Count - 1], Time.time - _lastUpdateTime);
            _lastUpdateTime = Time.time;
            _receivedData.Clear();
        }
    }
}
