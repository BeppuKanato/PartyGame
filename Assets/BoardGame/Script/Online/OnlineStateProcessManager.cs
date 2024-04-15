using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineStateProcessManager : BaseStateProcessManager
{
    NetworkHandler networkHandler;
    public OnlineStateProcessManager(NetworkHandler networkHandler, OnlineCharManager charManager, ActionOrderManager actionOrder, Common.InputSystemManager inputSystem) : base()
    {
        this.networkHandler = networkHandler;
        SetProcess(charManager, actionOrder, inputSystem);
    }

    //処理クラスの初期設定
    void SetProcess(OnlineCharManager charManager, ActionOrderManager actionOrder, Common.InputSystemManager inputSystem)
    {
        InitializeStateProcesses(charManager, actionOrder);

        base.SetInputProcess(inputSystem);
    }

    //状態毎の処理クラスの初期化
    protected override void InitializeStateProcesses(BaseCharManager charManager, ActionOrderManager actionOrder)
    {
        stateProcess.Add(BoardGameState.Preparation, new OnlinePreparationStateProcess((OnlineCharManager)charManager, networkHandler.photonNetWorkManager.actorNumber));
        stateProcess.Add(BoardGameState.OrderDice, new OnlineDiceStateProcess(this, networkHandler, false));
        stateProcess.Add(BoardGameState.DecideOrder, new OnlineDecideOrderStateProcess(this, networkHandler, actionOrder));
        stateProcess.Add(BoardGameState.SelectAct, new OnlineSelectActStateProcess(this));
        stateProcess.Add(BoardGameState.MoveDice, new OnlineDiceStateProcess(this, networkHandler, true));
        stateProcess.Add(BoardGameState.Move, new OnlineMoveStateProcess((OnlineCharManager)charManager, networkHandler.photonNetWorkManager.actorNumber, networkHandler));
    }
}
