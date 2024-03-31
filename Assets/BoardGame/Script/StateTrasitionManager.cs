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

    //ˆ—Œ‹‰Ê‚©‚çó‘Ô‘JˆÚ‚ğ§Œä‚·‚é
    public BoardGameState StateTransition(int processResult, BoardGameState currentState)
    {
        bool transitioned = false;
        BoardGameState transitionState;

        //–ß‚é‚ª‘I‘ğ‚³‚ê‚½ê‡
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
    //ó‘Ô‚ğ—š—ğ‚É’Ç‰Á‚·‚é
    void PushToStateHistory(bool transitioned, int processResult)
    {
        if (transitioned)
        {
            //‘JˆÚ“à—e‚ª–ß‚é‚Å‚Í‚È‚¢‚Æ‚«
            if (processResult != stateProcessManager.PREV_RETURN_NUMBER)
            {
                stateMachine.intStack.Push((int)stateProcessManager.currentState);
            }
        }
    }
}
