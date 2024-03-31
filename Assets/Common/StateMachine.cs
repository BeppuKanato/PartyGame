using Common.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
namespace Common
{
    public class StateMachine
    {
        public Common.IntStackManager intStack { get; private set; }
        public StateMachine()
        {
            intStack = new Common.IntStackManager();
        }

        //状態を変更するメソッド 状態が変化したらtrueを返す
        public bool ChangeState(StateProcess current, StateProcess next)
        {
            bool result = false;
            if (current != next)
            {
                //状態の処理を呼び出す
                current.Exit();
                next.Enter();
                result = true;
            }

            return result;
        }

        //Processを実行する
        public int ExeProcess(StateProcess stateProcess)
        {
            int result = stateProcess.Process();

            return result;
        }
    }
}