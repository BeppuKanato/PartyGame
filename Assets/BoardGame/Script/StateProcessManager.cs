using BoardGame;
using Common.Interface;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateProcessManager
{
    public int PREV_RETURN_NUMBER { get; private set; } = 99;
    BoardGame.GameManager gameManager;
    public Common.StateMachine stateMachine { get; private set; }
    StateTrasitionManager stateTrasitionManager;
    public BoardGameState currentState { get; private set; }
    public Dictionary<BoardGameState, StateProcess> stateProcess { get; private set;} = new Dictionary<BoardGameState, StateProcess>();
    
    public StateProcessManager(NetworkHandler networkHandler, CharManager charManager, ActionOrderManager actionOrder, Common.InputSystemManager inputSystem)
    {
        stateMachine = new Common.StateMachine();
        stateTrasitionManager= new StateTrasitionManager(this, stateMachine);
        currentState = BoardGameState.Preparation;

        SetProcess(networkHandler, charManager, actionOrder, inputSystem);
    }
    //処理クラスの初期設定
    void SetProcess(NetworkHandler networkHandler, CharManager charManager, ActionOrderManager actionOrder, Common.InputSystemManager inputSystem)
    {
        InitializeStateProcesses(networkHandler, charManager, actionOrder);

        SetInputProcess(inputSystem);
    }
    //状態毎の処理クラスの初期化
    void InitializeStateProcesses(NetworkHandler networkHandler, CharManager charManager, ActionOrderManager actionOrder)
    {
        stateProcess.Add(BoardGameState.Preparation, new PreparationStateProcess(charManager, networkHandler.photonNetWorkManager.actorNumber));
        stateProcess.Add(BoardGameState.OrderDice, new DiceStateProcess(this, networkHandler, false));
        stateProcess.Add(BoardGameState.DecideOrder, new DecideOrderStateProcess(this, networkHandler, actionOrder));
        stateProcess.Add(BoardGameState.SelectAct, new SelectActStateProcess(this));
        stateProcess.Add(BoardGameState.MoveDice, new DiceStateProcess(this, networkHandler, true));
        stateProcess.Add(BoardGameState.Move, new MoveStateProcess(charManager, networkHandler.photonNetWorkManager.actorNumber, networkHandler));
    }
    //全ての状態での入力処理を設定する
    void SetInputProcess(Common.InputSystemManager inputSystem)
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
        inputSystem.DisableActionMap($"{currentState}State");
        inputSystem.EnableActionMap($"{nextState}State");
    }
}
