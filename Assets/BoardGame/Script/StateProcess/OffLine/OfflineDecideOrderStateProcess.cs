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

        //�I�����C�������L�̏������s��

        Debug.Log("�I�t���C�����̏����ł�");
    }

    public override int Process()
    {
        int nextState = (int)stateProcessManager.currentState;

        return nextState;
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("�I�t���C�����̏����ł�");
    }
}
