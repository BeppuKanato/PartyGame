using BoardGame;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PreparationStateProcess : Common.Interface.StateProcess
{

    CharManager charManager;
    UserParams userParams;
    int actorNumber;    //ƒ‹[ƒ€“à‚ÅˆêˆÓ‚ÌID

    public PreparationStateProcess(CharManager charManager, int actorNumber)
    {
        Debug.Log("Preparion");
        userParams = UserParams.GetInstance();
        this.charManager = charManager;
        this.actorNumber = actorNumber;

    }

    public void Enter()
    {
        Debug.Log("Preparationó‘Ô‚É“ü‚è‚Ü‚µ‚½");
    }

    public int Process()
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

    public void Exit()
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
