using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineMoveStateProcess : BaseMoveStateProcess
{
    public OfflineMoveStateProcess(OfflineCharManager charManager, int actorNumber) : base(charManager, actorNumber)
    {

    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("オンライン時の処理です");
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

        Debug.Log("オンライン時の処理です");
    }
}
