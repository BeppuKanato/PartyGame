using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class DecideOrderStateProcess : Common.Interface.StateProcess
{
    NetworkHandler networkHandler;
    StateProcessManager stateProcessManager;
    ActionOrderManager actionOrder;

    public DecideOrderStateProcess(StateProcessManager stateProcessManager, NetworkHandler networkHandler, ActionOrderManager actionOrder)
    {
        this.networkHandler = networkHandler;
        this.stateProcessManager = stateProcessManager;
        this.actionOrder = actionOrder;

    } 

    public void Enter()
    {
        Debug.Log("DecideOrderProcess��Ԃɓ���܂���");
    }

    public int Process()
    {
        int nextState = (int)stateProcessManager.currentState;

        bool isComplete = networkHandler.CheckDataComplete();

        //�N���C�A���g���f�[�^����M������
        if (isComplete) 
        {
            Dictionary<int, DiceStateProcess.SendDataStruct> dataList = new Dictionary<int, DiceStateProcess.SendDataStruct>();
            //json�f�[�^���\���̃f�[�^�ɕϊ�
            foreach (SendData data in networkHandler.receiveData)
            {
                dataList[data.actorNumber] = JsonUtility.FromJson<DiceStateProcess.SendDataStruct>(data.content);
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

    public void Exit()
    {
        Debug.Log("DecideOrder��Ԃ��I�����܂�");
        //��M�f�[�^�����Z�b�g
        networkHandler.receiveData.Clear();
    }

    public void SetInputProcess(string mapName, Common.InputSystemManager inputSystem)
    {
    }

    public int DecideNextState()
    {
        return (int)BoardGameState.SelectAct;
    }
    //�s�����L���[��ݒ�
    void SetActionOrderQue(Dictionary<int, DiceStateProcess.SendDataStruct> dataList)
    {
        //�_�C�X�̐��l�Ń\�[�g����ID���X�g���쐬
        IEnumerable<int> list = from data in dataList
                                orderby data.Value.random descending
                                select data.Key;

        actionOrder.SetQue(list.ToList());
    }
}
