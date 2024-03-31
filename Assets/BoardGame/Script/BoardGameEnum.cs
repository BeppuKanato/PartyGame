using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ボードゲームの状態
public enum BoardGameState
{
    None,
    Preparation,    //ゲーム開始前の準備
    OrderDice,      //順番用のダイスを振る
    DecideOrder,    //順番を決める
    SelectAct,      //行動選択
    MoveDice,       //移動量を決定する
    Move,           //移動中
}