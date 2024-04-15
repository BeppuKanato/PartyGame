using Common.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Common;
using UnityEngine.InputSystem;

//ボードゲーム空間のGameManager
namespace BoardGame
{
    public class BaseGameManager : MonoBehaviour
    {
        //状態履歴の確認用
        public List<BoardGameState> checkState = new List<BoardGameState>();
        [field: SerializeField]
        public NetworkHandler networkHandler { get; private set; }  
        public bool isDebugMode;                            //デバッグモードで実行するか 
        [field: SerializeField]
        public BaseCharManager charManager { get; private set; }
        [field: SerializeField]
        public ActionOrderManager actionOrder { get; private set; }
        [field: SerializeField]
        public Common.InputSystemManager inputSystem { get; private set; }

        public UserParams userParams { get; private set; }
        BaseStateProcessManager stateProcessManager;
        
        //IDを行動順に並べたスタック
        private void Awake()
        {
        }
        protected void Start()
        {
            //----------------------------------------------------デバッグモードでの追加処理------------------------------------------------------------
            if (isDebugMode)
            {
                userParams = UserParams.GetInstance();
                DebugModeProcess();
            }
            //---------------------------------------------------------ここまで-------------------------------------------------------------------------
            //stateProcessManager = new BaseStateProcessManager(this.networkHandler, this.charManager, this.actionOrder, this.inputSystem);

            Debug.Log("BaseのStart");
        }

        // Update is called once per frame
        protected void Update()
        {
            stateProcessManager.RunCurrentStateProcess(inputSystem);
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log($"{inputSystem.playerInput[0].user}");
            }

            Debug.Log("BaseのUpdate");
        }
        //-------------------------------------------デバッグモードでの追加メソッド-------------------------------------------------------------------
        void DebugModeProcess()
        {
            userParams.SetIsOnline(true);
            //ユーザ名を設定する(仮)
            userParams.SetNickName("プレイヤー1");
            //使用するキャラを選択(仮)
            userParams.SetCharPrefabName("TempModel");
            if (userParams.isOnline)
            {
                ConnectedToServer();
                StartCoroutine(CreateRoom());
            }
        }
        //Photonサーバーと接続
        void ConnectedToServer()
        { 
            networkHandler.photonNetWorkManager.ConnectToServer(userParams.nickName);
        }
        //ルームを作成
        IEnumerator CreateRoom()
        {
            yield return new WaitUntil(() => networkHandler.photonNetWorkManager.joinedInLoby);

            Debug.Log("ルームを作成します");
            networkHandler.photonNetWorkManager.CreateNewRoom("ルーム1");
        }
        //---------------------------------------------ここまで---------------------------------------------------------------------------------------
    }
}