using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public�@abstract class BaseSelectActStateProcess : Common.Interface.StateProcess
{
    BoardGame.BaseGameManager gameManager;
    protected BaseStateProcessManager stateProcessManager;
    protected bool isSelected;
    protected int selectActNumber;
    private class SendDataStruct
    {
        public string act;
    }

    public BaseSelectActStateProcess(BaseStateProcessManager stateProcessManager)
    {
        this.stateProcessManager = stateProcessManager;
    }
    public virtual void Enter()
    {
        isSelected = false;
        selectActNumber = 0;
        Debug.Log($"SelectAct��ԂɂȂ����Ƃ��̏���");
    }

    public virtual int Process()
    {
        int nextState = (int)stateProcessManager.currentState;

        if (isSelected)
        {
            nextState = DecideNextState();
        }
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Debug.Log($"�_�C�X��I��");
        //    nextState = DecideNextState();
        //}
        return nextState;
    }
    public virtual void Exit() 
    {
        Debug.Log($"SelectAct��Ԃ𔲂��鎞�̏���");
    }
    public int DecideNextState()
    {

        return selectActNumber;
    }

    public void SetInputProcess(string mapName, Common.InputSystemManager inputSystem)
    {
        inputSystem.AddCallBackToAllPlayerInput(mapName, "Dice", SelectDice);
    }
    //�_�C�X��U���I��
    void SelectDice(InputAction.CallbackContext context)
    {
        isSelected = true;
        selectActNumber = (int)BoardGameState.MoveDice;
    }
}
