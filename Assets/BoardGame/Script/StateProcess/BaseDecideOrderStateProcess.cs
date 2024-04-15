using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public abstract class BaseDecideOrderStateProcess : Common.Interface.StateProcess
{
    protected BaseStateProcessManager stateProcessManager;
    protected ActionOrderManager actionOrder;

    public BaseDecideOrderStateProcess(BaseStateProcessManager stateProcessManager, ActionOrderManager actionOrder)
    {
        this.stateProcessManager = stateProcessManager;
        this.actionOrder = actionOrder;

    } 

    public virtual void Enter()
    {
        Debug.Log("DecideOrderProcess��Ԃɓ���܂���");
    }

    public virtual int Process()
    {
        //int nextState = (int)stateProcessManager.currentState;

        //bool isComplete = networkHandler.CheckDataComplete();

        ////�N���C�A���g���f�[�^����M������
        //if (isComplete) 
        //{
        //    Dictionary<int, BaseDiceStateProcess.SendDataStruct> dataList = new Dictionary<int, BaseDiceStateProcess.SendDataStruct>();
        //    //json�f�[�^���\���̃f�[�^�ɕϊ�
        //    foreach (SendData data in networkHandler.receiveData)
        //    {
        //        dataList[data.actorNumber] = JsonUtility.FromJson<BaseDiceStateProcess.SendDataStruct>(data.content);
        //    }

        //    SetActionOrderQue(dataList);

        //    nextState = DecideNextState();
        //}
        //else
        //{
        //    Debug.Log($"���[���l��{networkHandler.photonNetWorkManager.currentRoom.PlayerCount}, ��M�f�[�^��{networkHandler.receiveData.Count}");
        //    Debug.Log("�l�����̃f�[�^����M���Ă��܂���");
        //}

        //return nextState;

        return 1;
    }

    public virtual void Exit()
    {
        Debug.Log("DecideOrder��Ԃ��I�����܂�");
        //��M�f�[�^�����Z�b�g
    }

    public void SetInputProcess(string mapName, Common.InputSystemManager inputSystem)
    {
    }

    public int DecideNextState()
    {
        return (int)BoardGameState.SelectAct;
    }
    //�s�����L���[��ݒ�
    protected void SetActionOrderQue(Dictionary<int, BaseDiceStateProcess.SendDataStruct> dataList)
    {
        //�_�C�X�̐��l�Ń\�[�g����ID���X�g���쐬
        IEnumerable<int> list = from data in dataList
                                orderby data.Value.random descending
                                select data.Key;

        actionOrder.SetQue(list.ToList());
    }
}
