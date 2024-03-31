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

    //�������ʂ����ԑJ�ڂ𐧌䂷��
    public BoardGameState StateTransition(int processResult, BoardGameState currentState)
    {
        bool transitioned = false;
        BoardGameState transitionState;

        //�߂邪�I�����ꂽ�ꍇ
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
    //��Ԃ𗚗��ɒǉ�����
    void PushToStateHistory(bool transitioned, int processResult)
    {
        if (transitioned)
        {
            //�J�ړ��e���߂�ł͂Ȃ��Ƃ�
            if (processResult != stateProcessManager.PREV_RETURN_NUMBER)
            {
                stateMachine.intStack.Push((int)stateProcessManager.currentState);
            }
        }
    }
}
