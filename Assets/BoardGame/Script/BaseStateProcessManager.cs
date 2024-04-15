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
    //全ての状態での入力処理を設定する
    protected void SetInputProcess(Common.InputSystemManager inputSystem)
    {
        foreach(var keyValue in stateProcess)
        {
            keyValue.Value.SetInputProcess($"{keyValue.Key}State", inputSystem);
        }
    }
    //状態での処理、状態遷移を実行
    public void RunCurrentStateProcess(Common.InputSystemManager inputSystem)
    {
        int result = stateMachine.ExeProcess(stateProcess[currentState]);
        BoardGameState resultState = stateTrasitionManager.StateTransition(result, currentState);

        SetActionMapEnable(currentState, resultState, inputSystem);

        currentState = resultState;
    }
    //現在の状態のActionMapをDisableに、次の状態のActionMapをEnableにする
    void SetActionMapEnable(BoardGameState currentState, BoardGameState nextState, Common.InputSystemManager inputSystem)
    {
        inputSystem.DisableActionMap($"{currentState}State", 1);
        inputSystem.EnableActionMap($"{nextState}State", 1);
    }

    //抽象メソッド
    protected abstract void InitializeStateProcesses(BaseCharManager charManager, ActionOrderManager actionOrder);
}
