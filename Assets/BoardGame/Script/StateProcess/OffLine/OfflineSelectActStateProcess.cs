using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineSelectActStateProcess : BaseSelectActStateProcess
{
    public OfflineSelectActStateProcess(OfflineStateProcessManager stateProcessManager) : base(stateProcessManager)
    {

    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("�I�����C�����̏����ł�");
    }

    public override int Process()
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

    public override void Exit()
    {
        base.Exit();
    }
}
