using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineDecideOrderStateProcess : BaseDecideOrderStateProcess
{
    NetworkHandler networkHandler;
    public OnlineDecideOrderStateProcess(BaseStateProcessManager stateProcess, NetworkHandler networkHandler, ActionOrderManager actionOrder):base(stateProcess, actionOrder)
    {
        this.networkHandler = networkHandler;
    }
    public override void Enter()
    {
        base.Enter();

        //�I�����C�������L�̏������s��

        Debug.Log("�I�����C�����̏����ł�");
    }

    public override int Process()
    {
        int nextState = (int)stateProcessManager.currentState;

        bool isComplete = networkHandler.CheckDataComplete();

        //�N���C�A���g���f�[�^����M������
        if (isComplete)
        {
            Dictionary<int, BaseDiceStateProcess.SendDataStruct> dataList = new Dictionary<int, BaseDiceStateProcess.SendDataStruct>();
            //json�f�[�^���\���̃f�[�^�ɕϊ�
            foreach (SendData data in networkHandler.receiveData)
            {
                dataList[data.actorNumber] = JsonUtility.FromJson<BaseDiceStateProcess.SendDataStruct>(data.content);
            }

            SetActionOrderQue(dataList);

            nextState = DecideNextState();
        }
        else
        {
            Debug.Log($"���[���l��{networkHandler.photonNetWorkManager.currentRoom.PlayerCount}, ��M�f�[�^��{networkHandler.receiveData.Count}");
            Debug.Log("�l�����̃f�[�^����M���Ă��܂���");
        }

        return nextState;
    }

    public override void Exit() 
    {
        base.Exit();

        Debug.Log("�I�t���C�����̏����ł�");
    }
}
