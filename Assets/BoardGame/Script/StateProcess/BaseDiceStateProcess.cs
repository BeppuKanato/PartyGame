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

    protected bool canPrev;       //前の状態に戻れるか
    protected bool selectSubmit;  //サイコロを振ったか
    protected bool selectCancel;  //キャンセルを選択したか
    [Serializable]
    public struct SendDataStruct
    {
        public SendDataStruct(int random)
        {
            this.random = random;
        }
        public int random;      //ダイスの目
    }

    public BaseDiceStateProcess(StateProcessManager stateProcessManager, NetworkHandler networkHandler, bool canPrev)
    {
        this.stateProcessManager = stateProcessManager;
        this.canPrev = canPrev;
        this.networkHandler = networkHandler;
    }
    public virtual void Enter()
    {
        Debug.Log($"Dice状態になった際の処理");
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
            Debug.Log("戻るが選択されました");
            nextState = stateProcessManager.PREV_RETURN_NUMBER;
        }
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    //サイコロを振る
        //    random = UnityEngine.Random.Range(1, 7);
        //    Debug.Log($"出た目は{random}です");

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
        Debug.Log($"Dice状態を出る際の処理");
    }

    public void SetInputProcess(string mapName, Common.InputSystemManager inputSystem)
    {
        inputSystem.AddCallBackToAllPlayerInput(mapName, "Submit", DiceRoll);
        inputSystem.AddCallBackToAllPlayerInput(mapName, "Cancel", CancelProcess);
    }

    public int DecideNextState()
    {
        //最新の状態を取得
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
                Debug.Log("予期していない状態遷移です");
                result = -1;
                break;
        }

        return result;
    }
    //サイコロを振るメソッド
    void DiceRoll(InputAction.CallbackContext context)
    {
        //サイコロを振る
        int random = UnityEngine.Random.Range(1, 7);
        Debug.Log($"出た目は{random}です");

        SendDataStruct data = new SendDataStruct(random);
        string jsonData = JsonUtility.ToJson(data);

        networkHandler.SendData(jsonData);

        selectSubmit = true;
    }

    //戻るの処理メソッド
    void CancelProcess(InputAction.CallbackContext context)
    {
        Debug.Log("Diceで戻るを選択しました");
        selectCancel = true;
    }
}
