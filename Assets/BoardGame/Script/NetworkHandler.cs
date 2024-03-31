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
    //RPC�̎�M���\�b�h
    [PunRPC]
    private void ReceiveRPC(byte[] data)
    {
        string receiveJson = System.Text.Encoding.UTF8.GetString(data);
        receiveData.Add(JsonUtility.FromJson<SendData>(receiveJson));
        Debug.Log($"��M�f�[�^ = {System.Text.Encoding.UTF8.GetString(data)}");
    }
    //�f�[�^���M�v����PhotonNetworkManager�ɏo��
    public void SendData(string contentJson, RpcTarget rpcTarget = RpcTarget.All)
    {
        SendData sendData = new SendData(photonNetWorkManager.actorNumber, contentJson);
        string jsonData = JsonUtility.ToJson(sendData);
        photonNetWorkManager.SendData(view, jsonData, rpcTarget);
    }

    //���[�����̃N���C�A���g�S�����f�[�^�𑗐M���������m�F
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
