using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public abstract class BaseMoveStateProcess : Common.Interface.StateProcess
{
    protected CharManager charManager;
    NetworkHandler networkHandler;
    protected int actorNumber;                //ルーム内で一意のID
    protected int selectBranchIndex = 0;

    protected bool reachTarget;
    protected int moveCount;                  //移動量
    protected bool decideBranch;              //分岐を選択したかのトリガー
    protected int nNowSquareBranch;           //現在のマスの分岐数
    protected bool spuareHasBranch;           //分岐を持つマスか
    public BaseMoveStateProcess(CharManager charManager, int actorNumber, NetworkHandler networkHandler)
    {
        this.charManager = charManager;
        this.actorNumber = actorNumber;
        this.networkHandler = networkHandler;
    }
    public virtual void Enter()
    {
        Debug.Log($"Move状態に入りました");
        selectBranchIndex = 0;
        reachTarget = false;

        //受け取ったダイスの値を取得
        BaseDiceStateProcess.SendDataStruct dicaData = JsonUtility.FromJson<BaseDiceStateProcess.SendDataStruct>(networkHandler.receiveData.Last().content);
        moveCount = dicaData.random;
        decideBranch = false;

        nNowSquareBranch = 0;
        spuareHasBranch = false;
    }

    public virtual int Process()
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

    public virtual void Exit()
    {
        Debug.Log($"Move状態を終了します");
        //受信データをリセット
        networkHandler.receiveData.Clear();
    }

    public int DecideNextState()
    {
        //ダイスの値分移動が完了したら
        if (moveCount <= 0)
        {
            return (int)BoardGameState.SelectAct;
        }
        else
        {
            return (int)BoardGameState.Move;
        }
    }

    public void SetInputProcess(string mapName, Common.InputSystemManager inputSystem)
    {
        inputSystem.AddCallBackToAllPlayerInput(mapName, "SelectPrev", SelectPrev);
        inputSystem.AddCallBackToAllPlayerInput(mapName, "SelectNext", SelectNext);
        inputSystem.AddCallBackToAllPlayerInput(mapName, "Submit", DecideBranchIndex);
    }

    //選択を一個前にする
    void SelectPrev(InputAction.CallbackContext context)
    {
        if (spuareHasBranch)
        {
            selectBranchIndex = Mathf.Max(0, selectBranchIndex - 1);
            Debug.Log($"選択 = {selectBranchIndex}");
        }
    }

    //選択を一個先にする
    void SelectNext(InputAction.CallbackContext context)
    {
        if (spuareHasBranch)
        {
            selectBranchIndex = Mathf.Min(nNowSquareBranch - 1, selectBranchIndex + 1);
            Debug.Log($"選択 = {selectBranchIndex}");
        }
    }

    //進む先を決定する
    void DecideBranchIndex(InputAction.CallbackContext context)
    {
        if (spuareHasBranch)
        {
            Debug.Log($"進む方向を決定");
            decideBranch = true;
        }
    }

    //一マス進んだ時の処理
    protected void ReachTargetProcess()
    {
        moveCount--;
        Debug.Log($"残りの移動マス数 = {moveCount}");
        decideBranch = false;
        selectBranchIndex = 0;
    }
}
