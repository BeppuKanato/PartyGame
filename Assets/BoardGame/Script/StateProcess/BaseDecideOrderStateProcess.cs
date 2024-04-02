using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public abstract class BaseDecideOrderStateProcess : Common.Interface.StateProcess
{
    NetworkHandler networkHandler;
    protected StateProcessManager stateProcessManager;
    protected ActionOrderManager actionOrder;

    public BaseDecideOrderStateProcess(StateProcessManager stateProcessManager, NetworkHandler networkHandler, ActionOrderManager actionOrder)
    {
        this.networkHandler = networkHandler;
        this.stateProcessManager = stateProcessManager;
        this.actionOrder = actionOrder;

    } 

    public virtual void Enter()
    {
        Debug.Log("DecideOrderProcess状態に入りました");
    }

    public virtual int Process()
    {
        int nextState = (int)stateProcessManager.currentState;

        bool isComplete = networkHandler.CheckDataComplete();

        //クライアント分データを受信したら
        if (isComplete) 
        {
            Dictionary<int, BaseDiceStateProcess.SendDataStruct> dataList = new Dictionary<int, BaseDiceStateProcess.SendDataStruct>();
            //jsonデータを構造体データに変換
            foreach (SendData data in networkHandler.receiveData)
            {
                dataList[data.actorNumber] = JsonUtility.FromJson<BaseDiceStateProcess.SendDataStruct>(data.content);
            }

            SetActionOrderQue(dataList);

            nextState = DecideNextState();
        }
        else
        {
            Debug.Log($"ルーム人数{networkHandler.photonNetWorkManager.currentRoom.PlayerCount}, 受信データ数{networkHandler.receiveData.Count}");
            Debug.Log("人数分のデータを受信していません");
        }

        return nextState;
    }

    public virtual void Exit()
    {
        Debug.Log("DecideOrder状態を終了します");
        //受信データをリセット
        networkHandler.receiveData.Clear();
    }

    public void SetInputProcess(string mapName, Common.InputSystemManager inputSystem)
    {
    }

    public int DecideNextState()
    {
        return (int)BoardGameState.SelectAct;
    }
    //行動順キューを設定
    protected void SetActionOrderQue(Dictionary<int, BaseDiceStateProcess.SendDataStruct> dataList)
    {
        //ダイスの数値でソートしたIDリストを作成
        IEnumerable<int> list = from data in dataList
                                orderby data.Value.random descending
                                select data.Key;

        actionOrder.SetQue(list.ToList());
    }
}
