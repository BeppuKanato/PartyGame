using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSquareComponent : BaseSquareComponent
{
    [field: SerializeField]
    public int nCoin { get; private set; }  //貰えるコインの枚数
    //マスに止まった時の処理
    public override void OnProcess()
    {
        Debug.Log($"{nCoin}枚コインを獲得");
    }
}
