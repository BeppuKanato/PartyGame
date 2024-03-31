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

        public int actorNumber { get; private set; }    //���[�����ł̈�ӂ�ID

        private PhotonNetWorkManager()
        {
            //userParams = UserParams.GetInstance();
        }

        //�V���O���g���݌v
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

        //Photon�T�[�o�[�ւ̒ʐM���s��
        public void ConnectToServer(string nickName)
        {
            PhotonNetwork.NickName = nickName;
            PhotonNetwork.ConnectUsingSettings();
        }

        //���[���ɎQ������
        public void JoinRoom(string roomName)
        {
            RoomOptions roomOptions = new RoomOptions();
            //�v���C���[�̍ő吔
            roomOptions.MaxPlayers = 4;

            PhotonNetwork.JoinRoom(roomName);
        }

        //�V�������[�����쐬����
        public void CreateNewRoom(string roomName)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;

            PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
        }

        //�w��̃v���n�u���C���X�^���X������
        public GameObject InstantiatePrefab(string name, Vector3 pos, Quaternion angle)
        {
            GameObject result = PhotonNetwork.Instantiate(name, pos, angle);

            return result;
        }

        //���[���ɕω����N���������ʂ̏���
        public void SetRoomInfos()
        {
            currentRoom = PhotonNetwork.CurrentRoom;
            nClientInRoom = currentRoom.PlayerCount;
        }

        public void SendData(PhotonView view, string jsonData, RpcTarget rpcTarget)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(jsonData);
            Debug.Log(view);
            //�N���C�A���g��RECEIVE_RPC_NAME���\�b�h�Ŏ�M���s���悤�ɐݒ�
            view.RPC(RECEIVE_RPC_NAME, rpcTarget, byteData);
        }

        //�ڑ��ɐ��������ꍇ�̏���
        public override void OnConnectedToMaster()
        {
            Debug.Log("Photon�T�[�o�[�ɐڑ����Ă��܂�");
            PhotonNetwork.JoinLobby();
        }

        //���������r�[�ɎQ�������ꍇ�̏���
        public override void OnJoinedLobby()
        {
            Debug.Log("���r�[�ɎQ�����܂���");
            joinedInLoby = true;
        }

        //���[���ɒN�����Q�������ꍇ�̏���
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            SetRoomInfos();
            Debug.Log($"{newPlayer.NickName}�����[���ɎQ�����܂���");
        }

        //���[���ɎQ�������ꍇ�̏���
        public override void OnJoinedRoom()
        {
            SetRoomInfos();
            actorNumber = currentRoom.Players.Last().Value.ActorNumber;
            Debug.Log($"���[��{PhotonNetwork.CurrentRoom.Name}�ɎQ�����܂���");
        }

        //���[���ɎQ���o���Ȃ������ꍇ�̏���
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("���[�������݂��Ă��܂��� ���[�����쐬���܂�");
        }

        //���[�����쐬�����ꍇ�̏���
        public override void OnCreatedRoom()
        {
            Debug.Log($"���[��{PhotonNetwork.CurrentRoom.Name}���쐬���܂���");
        }

        //���[���ɍX�V���������ꍇ�̏���
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            roomInfos = new List<RoomInfo>(roomList);
        }
    }
}