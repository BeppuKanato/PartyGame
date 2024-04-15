using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflinePreparationStateProcess : BasePreparationStateProcess
{
    public OfflinePreparationStateProcess(OfflineCharManager charManager, int actorNumer) : base(charManager, actorNumer)
    {

    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("オフライン時の処理です");
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

        Debug.Log("オフライン時の処理です");
    }
}
