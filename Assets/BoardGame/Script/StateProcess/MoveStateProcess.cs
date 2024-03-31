using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class MoveStateProcess : Common.Interface.StateProcess
{
    CharManager charManager;
    NetworkHandler networkHandler;
    int actorNumber;                //ルーム内で一意のID
    int selectBranchIndex = 0;

    bool reachTarget;
    int moveCount;                  //移動量
    bool decideBranch;              //分岐を選択したかのトリガー
    int nNowSquareBranch;           //現在のマスの分岐数
    bool spuareHasBranch;           //分岐を持つマスか
    public MoveStateProcess(CharManager charManager, int actorNumber, NetworkHandler networkHandler)
    {
        this.charManager = charManager;
        this.actorNumber = actorNumber;
        this.networkHandler = networkHandler;
    }
    public void Enter()
    {
        Debug.Log($"Move状態に入りました");
        selectBranchIndex = 0;
        reachTarget = false;

        //受け取ったダイスの値を取得
        DiceStateProcess.SendDataStruct dicaData = JsonUtility.FromJson<DiceStateProcess.SendDataStruct>(networkHandler.receiveData.Last().content);
        moveCount = dicaData.random;
        decideBranch = false;

        nNowSquareBranch = 0;
        spuareHasBranch = false;
    }

    public int Process()
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

    public void Exit()
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
        inputSystem.AddCallBack(mapName, "SelectPrev", SelectPrev);
        inputSystem.AddCallBack(mapName, "SelectNext", SelectNext);
        inputSystem.AddCallBack(mapName, "Submit", DecideBranchIndex);
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
    void ReachTargetProcess()
    {
        moveCount--;
        Debug.Log($"残りの移動マス数 = {moveCount}");
        decideBranch = false;
        selectBranchIndex = 0;
    }
}
