using BoardGame;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BasePreparationStateProcess : Common.Interface.StateProcess
{

    protected CharManager charManager;
    protected UserParams userParams;
    protected int actorNumber;    //ルーム内で一意のID

    public BasePreparationStateProcess(CharManager charManager, int actorNumber)
    {
        Debug.Log("Preparion");
        userParams = UserParams.GetInstance();
        this.charManager = charManager;
        this.actorNumber = actorNumber;

    }

    public virtual void Enter()
    {
        Debug.Log("Preparation状態に入りました");
    }

    public virtual int Process()
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

    public virtual void Exit()
    {
        Debug.Log("Preparation状態を終了します");
    }

    public int DecideNextState()
    {
        return (int)BoardGameState.OrderDice;
    }

    public void SetInputProcess(string mapName, Common.InputSystemManager inputSystem)
    {
    }
}
