using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineDiceStateProcess : BaseDiceStateProcess
{
    public OfflineDiceStateProcess(OfflineStateProcessManager stateProcessManager, bool canPrev) : base(stateProcessManager, canPrev)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("ƒIƒ“ƒ‰ƒCƒ“‚Ìˆ—‚Å‚·");
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
            Debug.Log("–ß‚é‚ª‘I‘ğ‚³‚ê‚Ü‚µ‚½");
            nextState = stateProcessManager.PREV_RETURN_NUMBER;
        }
        return nextState;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
