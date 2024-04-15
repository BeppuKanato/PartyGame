using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineDecideOrderStateProcess : BaseDecideOrderStateProcess
{
    NetworkHandler networkHandler;
    public OnlineDecideOrderStateProcess(BaseStateProcessManager stateProcess, NetworkHandler networkHandler, ActionOrderManager actionOrder):base(stateProcess, actionOrder)
    {
        this.networkHandler = networkHandler;
    }
    public override void Enter()
    {
        base.Enter();

        //オンライン時特有の処理を行う

        Debug.Log("オンライン時の処理です");
    }

    public override int Process()
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

    public override void Exit() 
    {
        base.Exit();

        Debug.Log("オフライン時の処理です");
    }
}
