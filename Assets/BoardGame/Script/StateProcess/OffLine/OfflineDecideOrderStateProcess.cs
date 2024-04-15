using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineDecideOrderStateProcess : BaseDecideOrderStateProcess
{
    public OfflineDecideOrderStateProcess(OfflineStateProcessManager stateProcess, ActionOrderManager actionOrder) : base(stateProcess, actionOrder)
    {
    }
    public override void Enter()
    {
        base.Enter();

        //オンライン時特有の処理を行う

        Debug.Log("オフライン時の処理です");
    }

    public override int Process()
    {
        int nextState = (int)stateProcessManager.currentState;

        return nextState;
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("オフライン時の処理です");
    }
}
