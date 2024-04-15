using Common.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Common;
using UnityEngine.InputSystem;

//�{�[�h�Q�[����Ԃ�GameManager
namespace BoardGame
{
    public class BaseGameManager : MonoBehaviour
    {
        //��ԗ����̊m�F�p
        public List<BoardGameState> checkState = new List<BoardGameState>();
        [field: SerializeField]
        public NetworkHandler networkHandler { get; private set; }  
        public bool isDebugMode;                            //�f�o�b�O���[�h�Ŏ��s���邩 
        [field: SerializeField]
        public BaseCharManager charManager { get; private set; }
        [field: SerializeField]
        public ActionOrderManager actionOrder { get; private set; }
        [field: SerializeField]
        public Common.InputSystemManager inputSystem { get; private set; }

        public UserParams userParams { get; private set; }
        BaseStateProcessManager stateProcessManager;
        
        //ID���s�����ɕ��ׂ��X�^�b�N
        private void Awake()
        {
        }
        protected void Start()
        {
            //----------------------------------------------------�f�o�b�O���[�h�ł̒ǉ�����------------------------------------------------------------
            if (isDebugMode)
            {
                userParams = UserParams.GetInstance();
                DebugModeProcess();
            }
            //---------------------------------------------------------�����܂�-------------------------------------------------------------------------
            //stateProcessManager = new BaseStateProcessManager(this.networkHandler, this.charManager, this.actionOrder, this.inputSystem);

            Debug.Log("Base��Start");
        }

        // Update is called once per frame
        protected void Update()
        {
            stateProcessManager.RunCurrentStateProcess(inputSystem);
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log($"{inputSystem.playerInput[0].user}");
            }

            Debug.Log("Base��Update");
        }
        //-------------------------------------------�f�o�b�O���[�h�ł̒ǉ����\�b�h-------------------------------------------------------------------
        void DebugModeProcess()
        {
            userParams.SetIsOnline(true);
            //���[�U����ݒ肷��(��)
            userParams.SetNickName("�v���C���[1");
            //�g�p����L������I��(��)
            userParams.SetCharPrefabName("TempModel");
            if (userParams.isOnline)
            {
                ConnectedToServer();
                StartCoroutine(CreateRoom());
            }
        }
        //Photon�T�[�o�[�Ɛڑ�
        void ConnectedToServer()
        { 
            networkHandler.photonNetWorkManager.ConnectToServer(userParams.nickName);
        }
        //���[�����쐬
        IEnumerator CreateRoom()
        {
            yield return new WaitUntil(() => networkHandler.photonNetWorkManager.joinedInLoby);

            Debug.Log("���[�����쐬���܂�");
            networkHandler.photonNetWorkManager.CreateNewRoom("���[��1");
        }
        //---------------------------------------------�����܂�---------------------------------------------------------------------------------------
    }
}