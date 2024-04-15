using BoardGame;
using Common.Interface;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateProcessManager
{
    public int PREV_RETURN_NUMBER { get; private set; } = 99;
    public Common.StateMachine stateMachine { get; private set; }
    StateTrasitionManager stateTrasitionManager;
    public BoardGameState currentState { get; private set; }
    public Dictionary<BoardGameState, StateProcess> stateProcess { get; private set;} = new Dictionary<BoardGameState, StateProcess>();
    
    public BaseStateProcessManager()
    {
        stateMachine = new Common.StateMachine();
        stateTrasitionManager= new StateTrasitionManager(this, stateMachine);
        currentState = BoardGameState.Preparation;
    }
    //�S�Ă̏�Ԃł̓��͏�����ݒ肷��
    protected void SetInputProcess(Common.InputSystemManager inputSystem)
    {
        foreach(var keyValue in stateProcess)
        {
            keyValue.Value.SetInputProcess($"{keyValue.Key}State", inputSystem);
        }
    }
    //��Ԃł̏����A��ԑJ�ڂ����s
    public void RunCurrentStateProcess(Common.InputSystemManager inputSystem)
    {
        int result = stateMachine.ExeProcess(stateProcess[currentState]);
        BoardGameState resultState = stateTrasitionManager.StateTransition(result, currentState);

        SetActionMapEnable(currentState, resultState, inputSystem);

        currentState = resultState;
    }
    //���݂̏�Ԃ�ActionMap��Disable�ɁA���̏�Ԃ�ActionMap��Enable�ɂ���
    void SetActionMapEnable(BoardGameState currentState, BoardGameState nextState, Common.InputSystemManager inputSystem)
    {
        inputSystem.DisableActionMap($"{currentState}State", 1);
        inputSystem.EnableActionMap($"{nextState}State", 1);
    }

    //���ۃ��\�b�h
    protected abstract void InitializeStateProcesses(BaseCharManager charManager, ActionOrderManager actionOrder);
}
