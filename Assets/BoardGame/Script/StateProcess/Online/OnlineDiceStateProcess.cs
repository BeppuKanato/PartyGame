using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineDiceStateProcess : BaseDiceStateProcess
{
    NetworkHandler networkHandler;
    public OnlineDiceStateProcess(StateProcessManager stateProcessManager, NetworkHandler networkHandler, bool canPrev) : base(stateProcessManager, networkHandler, canPrev)
    {
        this.networkHandler = networkHandler;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("オンライン時の処理です");
    }

    public override int Process()
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
        return nextState;
    }

    public override void Exit() 
    {
        base.Exit();
    }
}
