using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlinePreparationStateProcess : BasePreparationStateProcess
{
    public OnlinePreparationStateProcess(CharManager charManager, int actorNumber) : base(charManager, actorNumber)
    {

    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("オンライン時の処理");
    }

    public override int Process()
    {
        GameObject cloneObj;
        int key;
        Debug.Log(userParams);
        if (userParams.isOnline)
        {
            cloneObj = charManager.InstantiateCharInOnline(userParams.charPrefabName);
            key = actorNumber;
        }
        else
        {
            cloneObj = charManager.InstantiateCharInOffline(userParams.charPrefabName);
            key = 1;
        }
        charManager.InitializeProcess(cloneObj, key);

        return DecideNextState();
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("オンライン時の処理です");
    }
}
