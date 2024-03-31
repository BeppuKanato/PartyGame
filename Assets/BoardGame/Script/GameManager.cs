using Common.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Common;

//�{�[�h�Q�[����Ԃ�GameManager
namespace BoardGame
{
    public class GameManager : MonoBehaviour
    {
        //��ԗ����̊m�F�p
        public List<BoardGameState> checkState = new List<BoardGameState>();
        [field: SerializeField]
        public NetworkHandler networkHandler { get; private set; }  
        public bool isDebugMode;                            //�f�o�b�O���[�h�Ŏ��s���邩 
        [field: SerializeField]
        public CharManager charManager { get; private set; }
        [field: SerializeField]
        public ActionOrderManager actionOrder { get; private set; }
        [field: SerializeField]
        public Common.InputSystemManager inputSystem { get; private set; }

        public UserParams userParams { get; private set; }
        StateProcessManager stateProcessManager;
        
        //ID���s�����ɕ��ׂ��X�^�b�N
        private void Awake()
        {
        }
        void Start()
        {
            //----------------------------------------------------�f�o�b�O���[�h�ł̒ǉ�����------------------------------------------------------------
            if (isDebugMode)
            {
                userParams = UserParams.GetInstance();
                DebugModeProcess();
            }
            //---------------------------------------------------------�����܂�-------------------------------------------------------------------------
            stateProcessManager = new StateProcessManager(this.networkHandler, this.charManager, this.actionOrder, this.inputSystem);
        }

        // Update is called once per frame
        void Update()
        {
            stateProcessManager.RunCurrentStateProcess(inputSystem);
            if (Input.GetKeyDown(KeyCode.H))
            {
                checkState.Clear();
                foreach (int n in stateProcessManager.stateMachine.intStack.GetStackContent())
                {
                    checkState.Add((BoardGameState)n);
                }
            }
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