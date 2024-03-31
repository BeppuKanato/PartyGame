using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//状態毎の処理クラスが実装するインターフェース

namespace Common
{
    namespace Interface
    {
        public interface StateProcess
        {
            void Enter();           //状態に入った時の処理を実装
            int Process();          //状態にいる間の処理を実装 戻り値 = int型に変換した状態列挙体
            void Exit();            //状態を出る時の処理を実装

            int DecideNextState();  //次の状態を決定する処理を実装

            void SetInputProcess(string mapName, InputSystemManager inputSystem);    //入力時の処理を設定する
        }
    }
}