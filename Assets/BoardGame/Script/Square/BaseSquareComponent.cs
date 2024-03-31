using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSquareComponent : MonoBehaviour
{
    [field: SerializeField]
    public List<BaseSquareComponent> preSquare { get; private set; }  //前のマス
    [field: SerializeField]
    public List<BaseSquareComponent> nextSquare { get; private set; } //次のマス

    //マスを通った場合の処理
    //通った時に強制的にイベントを起こす場合、このメソッドをオーバーライドする
    public void ThroughProcess()
    {
        //分岐する場合
        if (nextSquare.Count > 1)
        {

        }
        //ここからオーバーライド
    }

    //マスに止まった場合の処理
    //サブクラスで具体的な処理を実装
    public abstract void OnProcess();

    //次のマスが分岐するかを返す
    public bool GetIsBranch()
    {
        bool result = false;

        if (nextSquare.Count > 1)
        {
            result = true;
        }

        return result;
    }
}
