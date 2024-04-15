using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OfflineStateProcessManager : BaseStateProcessManager
{
    public OfflineStateProcessManager(OfflineCharManager charManager, ActionOrderManager actionOrder, Common.InputSystemManager inputSystem) : base()
    {
        SetProcess(charManager, actionOrder, inputSystem);
    }

    //処理クラスの初期設定
    void SetProcess(OfflineCharManager charManager, ActionOrderManager actionOrder, Common.InputSystemManager inputSystem)
    {
        InitializeStateProcesses(charManager, actionOrder);

        base.SetInputProcess(inputSystem);
    }
    //状態毎の処理クラスの初期化
    protected override void InitializeStateProcesses(BaseCharManager charManager, ActionOrderManager actionOrder)
    {
        stateProcess.Add(BoardGameState.Preparation, new OfflinePreparationStateProcess((OfflineCharManager)charManager, 0));
        stateProcess.Add(BoardGameState.OrderDice, new OfflineDiceStateProcess(this, false));
        stateProcess.Add(BoardGameState.DecideOrder, new OfflineDecideOrderStateProcess(this, actionOrder));
        stateProcess.Add(BoardGameState.SelectAct, new OfflineSelectActStateProcess(this));
        stateProcess.Add(BoardGameState.MoveDice, new OfflineDiceStateProcess(this, true));
        stateProcess.Add(BoardGameState.Move, new OfflineMoveStateProcess((OfflineCharManager)charManager, 0));
    }
}
