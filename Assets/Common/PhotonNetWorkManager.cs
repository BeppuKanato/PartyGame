using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
namespace Common
{

    public class PhotonNetWorkManager : MonoBehaviourPunCallbacks
    {
        const string RECEIVE_RPC_NAME = "ReceiveRPC";

        private static PhotonNetWorkManager instance;
        public List<RoomInfo> roomInfos { get; private set; } = new List<RoomInfo>();
        public int nClientInRoom { get; private set; }
        public bool joinedInLoby { get; private set; } = false;

        public Room currentRoom { get; private set; }

        public int actorNumber { get; private set; }    //ルーム内での一意のID

        private PhotonNetWorkManager()
        {
            //userParams = UserParams.GetInstance();
        }

        //シングルトン設計
        public static PhotonNetWorkManager GetInstance()
        {
            if (instance == null)
            {
                GameObject obj = GameObject.FindWithTag("PhotonNetworkManager");

                if (obj == null)
                {
                    GameObject newObj = new GameObject("PhotonNetworkManager");
                    newObj.AddComponent<PhotonNetWorkManager>();
                    DontDestroyOnLoad(newObj);
                    instance = newObj.GetComponent<PhotonNetWorkManager>();
                }
                else
                {
                    instance = obj.GetComponent<PhotonNetWorkManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }

        //Photonサーバーへの通信を行う
        public void ConnectToServer(string nickName)
        {
            PhotonNetwork.NickName = nickName;
            PhotonNetwork.ConnectUsingSettings();
        }

        //ルームに参加する
        public void JoinRoom(string roomName)
        {
            RoomOptions roomOptions = new RoomOptions();
            //プレイヤーの最大数
            roomOptions.MaxPlayers = 4;

            PhotonNetwork.JoinRoom(roomName);
        }

        //新しくルームを作成する
        public void CreateNewRoom(string roomName)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;

            PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
        }

        //指定のプレハブをインスタンス化する
        public GameObject InstantiatePrefab(string name, Vector3 pos, Quaternion angle)
        {
            GameObject result = PhotonNetwork.Instantiate(name, pos, angle);

            return result;
        }

        //ルームに変化が起きた時共通の処理
        public void SetRoomInfos()
        {
            currentRoom = PhotonNetwork.CurrentRoom;
            nClientInRoom = currentRoom.PlayerCount;
        }

        public void SendData(PhotonView view, string jsonData, RpcTarget rpcTarget)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(jsonData);
            Debug.Log(view);
            //クライアントのRECEIVE_RPC_NAMEメソッドで受信を行うように設定
            view.RPC(RECEIVE_RPC_NAME, rpcTarget, byteData);
        }

        //接続に成功した場合の処理
        public override void OnConnectedToMaster()
        {
            Debug.Log("Photonサーバーに接続しています");
            PhotonNetwork.JoinLobby();
        }

        //自分がロビーに参加した場合の処理
        public override void OnJoinedLobby()
        {
            Debug.Log("ロビーに参加しました");
            joinedInLoby = true;
        }

        //ルームに誰かが参加した場合の処理
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            SetRoomInfos();
            Debug.Log($"{newPlayer.NickName}がルームに参加しました");
        }

        //ルームに参加した場合の処理
        public override void OnJoinedRoom()
        {
            SetRoomInfos();
            actorNumber = currentRoom.Players.Last().Value.ActorNumber;
            Debug.Log($"ルーム{PhotonNetwork.CurrentRoom.Name}に参加しました");
        }

        //ルームに参加出来なかった場合の処理
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("ルームが存在していません ルームを作成します");
        }

        //ルームを作成した場合の処理
        public override void OnCreatedRoom()
        {
            Debug.Log($"ルーム{PhotonNetwork.CurrentRoom.Name}を作成しました");
        }

        //ルームに更新があった場合の処理
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            roomInfos = new List<RoomInfo>(roomList);
        }
    }
}