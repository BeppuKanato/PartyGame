using BoardGame;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BasePreparationStateProcess : Common.Interface.StateProcess
{

    protected BaseCharManager charManager;
    protected UserParams userParams;
    protected int actorNumber;    //ƒ‹[ƒ€“à‚ÅˆêˆÓ‚ÌID

    public BasePreparationStateProcess(BaseCharManager charManager, int actorNumber)
    {
        Debug.Log("Preparion");
        userParams = UserParams.GetInstance();
        this.charManager = charManager;
        this.actorNumber = actorNumber;

    }

    public virtual void Enter()
    {
        Debug.Log("Preparationó‘Ô‚É“ü‚è‚Ü‚µ‚½");
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
        Debug.Log("Preparationó‘Ô‚ğI—¹‚µ‚Ü‚·");
    }

    public int DecideNextState()
    {
        return (int)BoardGameState.OrderDice;
    }

    public void SetInputProcess(string mapName, Common.InputSystemManager inputSystem)
    {
    }
}
