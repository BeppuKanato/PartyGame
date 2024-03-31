using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NetworkHandler : MonoBehaviourPun
{
    public Common.PhotonNetWorkManager photonNetWorkManager { get; private set;}
    [SerializeField]
    PhotonView view;
    private void Awake()
    {
        photonNetWorkManager = Common.PhotonNetWorkManager.GetInstance();
    }
    public List<SendData> receiveData { get; private set; } = new List<SendData>();
    //RPCの受信メソッド
    [PunRPC]
    private void ReceiveRPC(byte[] data)
    {
        string receiveJson = System.Text.Encoding.UTF8.GetString(data);
        receiveData.Add(JsonUtility.FromJson<SendData>(receiveJson));
        Debug.Log($"受信データ = {System.Text.Encoding.UTF8.GetString(data)}");
    }
    //データ送信要求をPhotonNetworkManagerに出す
    public void SendData(string contentJson, RpcTarget rpcTarget = RpcTarget.All)
    {
        SendData sendData = new SendData(photonNetWorkManager.actorNumber, contentJson);
        string jsonData = JsonUtility.ToJson(sendData);
        photonNetWorkManager.SendData(view, jsonData, rpcTarget);
    }

    //ルーム内のクライアント全員がデータを送信したかを確認
    public bool CheckDataComplete()
    {
        bool result = false;
        if (photonNetWorkManager.actorNumber == receiveData.Count)
        {
            result = true;
        }

        return result;
    }
}
