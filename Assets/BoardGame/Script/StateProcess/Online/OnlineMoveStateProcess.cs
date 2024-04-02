using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineMoveStateProcess : BaseMoveStateProcess
{
    public OnlineMoveStateProcess(CharManager charManager, int actorNumber, NetworkHandler networkHandler) : base(charManager, actorNumber, networkHandler)
    {

    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("�I�����C�����̏����ł�");
    }

    public override int Process()
    {
        BaseSquareComponent nowSqusre = charManager.charClones[actorNumber].nowSquare;
        nNowSquareBranch = nowSqusre.nextSquare.Count;
        spuareHasBranch = nowSqusre.GetIsBranch();

        if (!spuareHasBranch)
        {
            decideBranch = true;
        }

        if (decideBranch)
        {
            reachTarget = charManager.ExeCharMove(actorNumber, selectBranchIndex);
            if (reachTarget)
            {
                ReachTargetProcess();
            }
        }
        int nextState = DecideNextState();

        return nextState;
    }

    public override void Exit() 
    {
        base.Exit();

        Debug.Log("�I�����C�����̏����ł�");
    }
}
