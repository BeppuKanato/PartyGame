using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class StateTrasitionManager
{
    Common.StateMachine stateMachine;
    StateProcessManager stateProcessManager;
    public StateTrasitionManager(StateProcessManager processManager, Common.StateMachine stateMachine)
    {
        stateProcessManager = processManager;
        this.stateMachine = stateMachine;
    }

    //処理結果から状態遷移を制御する
    public BoardGameState StateTransition(int processResult, BoardGameState currentState)
    {
        bool transitioned = false;
        BoardGameState transitionState;

        //戻るが選択された場合
        if (processResult == stateProcessManager.PREV_RETURN_NUMBER)
        {
            transitionState = (BoardGameState)stateMachine.intStack.Pop();
        }
        else
        {
            transitionState = (BoardGameState)processResult;
        }

        transitioned = stateMachine.ChangeState(stateProcessManager.stateProcess[currentState], stateProcessManager.stateProcess[transitionState]);

        PushToStateHistory(transitioned, processResult);

        return transitionState;
    }
    //状態を履歴に追加する
    void PushToStateHistory(bool transitioned, int processResult)
    {
        if (transitioned)
        {
            //遷移内容が戻るではないとき
            if (processResult != stateProcessManager.PREV_RETURN_NUMBER)
            {
                stateMachine.intStack.Push((int)stateProcessManager.currentState);
            }
        }
    }
}
