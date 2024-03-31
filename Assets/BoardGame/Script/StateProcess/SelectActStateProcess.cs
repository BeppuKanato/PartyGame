using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectActStateProcess : Common.Interface.StateProcess
{
    BoardGame.GameManager gameManager;
    StateProcessManager stateProcessManager;
    bool isSelected;
    int selectActNumber;
    private class SendDataStruct
    {
        public string act;
    }

    public SelectActStateProcess(StateProcessManager stateProcessManager)
    {
        this.stateProcessManager = stateProcessManager;
    }
    public void Enter()
    {
        isSelected = false;
        selectActNumber = 0;
        Debug.Log($"SelectAct状態になったときの処理");
    }

    public int Process()
    {
        int nextState = (int)stateProcessManager.currentState;

        if (isSelected)
        {
            nextState = DecideNextState();
        }
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Debug.Log($"ダイスを選択");
        //    nextState = DecideNextState();
        //}
        return nextState;
    }
    public void Exit() 
    {
        Debug.Log($"SelectAct状態を抜ける時の処理");
    }
    public int DecideNextState()
    {

        return selectActNumber;
    }

    public void SetInputProcess(string mapName, Common.InputSystemManager inputSystem)
    {
        inputSystem.AddCallBack(mapName, "Dice", SelectDice);
    }
    //ダイスを振るを選択
    void SelectDice(InputAction.CallbackContext context)
    {
        isSelected = true;
        selectActNumber = (int)BoardGameState.MoveDice;
    }
}
