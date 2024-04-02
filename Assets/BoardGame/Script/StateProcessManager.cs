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
    //�����N���X�̏����ݒ�
    void SetProcess(NetworkHandler networkHandler, CharManager charManager, ActionOrderManager actionOrder, Common.InputSystemManager inputSystem)
    {
        InitializeStateProcesses(networkHandler, charManager, actionOrder);

        SetInputProcess(inputSystem);
    }
    //��Ԗ��̏����N���X�̏�����
    void InitializeStateProcesses(NetworkHandler networkHandler, CharManager charManager, ActionOrderManager actionOrder)
    {
        stateProcess.Add(BoardGameState.Preparation, new OnlinePreparationStateProcess(charManager, networkHandler.photonNetWorkManager.actorNumber));
        stateProcess.Add(BoardGameState.OrderDice, new OnlineDiceStateProcess(this, networkHandler, false));
        stateProcess.Add(BoardGameState.DecideOrder, new OnlineDecideOrderStateProcess(this, networkHandler, actionOrder));
        stateProcess.Add(BoardGameState.SelectAct, new OnlineSelectActStateProcess(this));
        stateProcess.Add(BoardGameState.MoveDice, new OnlineDiceStateProcess(this, networkHandler, true));
        stateProcess.Add(BoardGameState.Move, new OnlineMoveStateProcess(charManager, networkHandler.photonNetWorkManager.actorNumber, networkHandler));
    }
    //�S�Ă̏�Ԃł̓��͏�����ݒ肷��
    void SetInputProcess(Common.InputSystemManager inputSystem)
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
}
