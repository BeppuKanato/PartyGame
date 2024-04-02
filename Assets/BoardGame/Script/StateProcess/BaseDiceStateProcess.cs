using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Switch;

public abstract class BaseDiceStateProcess : Common.Interface.StateProcess
{
    protected StateProcessManager stateProcessManager;
    NetworkHandler networkHandler;

    protected bool canPrev;       //�O�̏�Ԃɖ߂�邩
    protected bool selectSubmit;  //�T�C�R����U������
    protected bool selectCancel;  //�L�����Z����I��������
    [Serializable]
    public struct SendDataStruct
    {
        public SendDataStruct(int random)
        {
            this.random = random;
        }
        public int random;      //�_�C�X�̖�
    }

    public BaseDiceStateProcess(StateProcessManager stateProcessManager, NetworkHandler networkHandler, bool canPrev)
    {
        this.stateProcessManager = stateProcessManager;
        this.canPrev = canPrev;
        this.networkHandler = networkHandler;
    }
    public virtual void Enter()
    {
        Debug.Log($"Dice��ԂɂȂ����ۂ̏���");
        selectSubmit = false;
        selectCancel = false;


    }

    public virtual int Process()
    {
        int nextState = (int)stateProcessManager.currentState;
        if (selectSubmit)
        {
            nextState = DecideNextState();
        }
        if (selectCancel && canPrev)
        {
            Debug.Log("�߂邪�I������܂���");
            nextState = stateProcessManager.PREV_RETURN_NUMBER;
        }
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    //�T�C�R����U��
        //    random = UnityEngine.Random.Range(1, 7);
        //    Debug.Log($"�o���ڂ�{random}�ł�");

        //    SendDataStruct data = new SendDataStruct(random);
        //    string jsonData = JsonUtility.ToJson(data);

        //    networkHandler.SendData(jsonData);

        //    nextState = DecideNextState();
        //}

        //if (canPrev)
        //{
        //    if (Input.GetKeyDown(KeyCode.Backspace))
        //    {
        //        nextState = stateProcessManager.PREV_RETURN_NUMBER;
        //    }
        //}

        return nextState;
    }

    public virtual void Exit()
    {
        Debug.Log($"Dice��Ԃ��o��ۂ̏���");
    }

    public void SetInputProcess(string mapName, Common.InputSystemManager inputSystem)
    {
        inputSystem.AddCallBackToAllPlayerInput(mapName, "Submit", DiceRoll);
        inputSystem.AddCallBackToAllPlayerInput(mapName, "Cancel", CancelProcess);
    }

    public int DecideNextState()
    {
        //�ŐV�̏�Ԃ��擾
        int lastState = stateProcessManager.stateMachine.intStack.GetLastValue();
        int result;
        switch (lastState) 
        {
            case (int)BoardGameState.Preparation:
                result = (int)BoardGameState.DecideOrder;
                break;
            case (int)BoardGameState.SelectAct:
                result = (int)BoardGameState.Move;
                break;
            default:
                Debug.Log("�\�����Ă��Ȃ���ԑJ�ڂł�");
                result = -1;
                break;
        }

        return result;
    }
    //�T�C�R����U�郁�\�b�h
    void DiceRoll(InputAction.CallbackContext context)
    {
        //�T�C�R����U��
        int random = UnityEngine.Random.Range(1, 7);
        Debug.Log($"�o���ڂ�{random}�ł�");

        SendDataStruct data = new SendDataStruct(random);
        string jsonData = JsonUtility.ToJson(data);

        networkHandler.SendData(jsonData);

        selectSubmit = true;
    }

    //�߂�̏������\�b�h
    void CancelProcess(InputAction.CallbackContext context)
    {
        Debug.Log("Dice�Ŗ߂��I�����܂���");
        selectCancel = true;
    }
}
