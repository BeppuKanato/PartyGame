using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOrderManager: MonoBehaviour
{
    public Common.IntQueManager que { get; private set; }  //操作が終了していないIDリスト
    Common.IntQueManager actedQue;                         //操作が終了しているIDリスト

    private void Start()
    {
        que = new Common.IntQueManager();
        actedQue = new Common.IntQueManager();
    }

    //操作を行うIDを返す
    public int GetActorID()
    {
        return que.GetLastValue();
    }
    //操作権を入れ替える
    public void RotateActorOrder()
    {
        actedQue.Push(que.Pop());
    }
    //全員が操作を行ったかを返す
    public bool CheckAllActed()
    {
        bool result = false;
        if (que.GetQueCount() <= 0)
        {
            ResetOrderQue();
            result = true;
        }

        return result;
    }
    //行動順キューをセットする
    public void SetQue(List<int> que)
    {
        this.que.SetQue(que);
    }
    //行動順キューをリセットする
    //行動順キューをリセットする
    void ResetOrderQue()
    {
        que.SetQue(actedQue.GetQueContent());
        actedQue.ResetQue();
    }
}
