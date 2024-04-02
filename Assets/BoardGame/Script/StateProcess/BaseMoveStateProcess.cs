using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public abstract class BaseMoveStateProcess : Common.Interface.StateProcess
{
    protected CharManager charManager;
    NetworkHandler networkHandler;
    protected int actorNumber;                //���[�����ň�ӂ�ID
    protected int selectBranchIndex = 0;

    protected bool reachTarget;
    protected int moveCount;                  //�ړ���
    protected bool decideBranch;              //�����I���������̃g���K�[
    protected int nNowSquareBranch;           //���݂̃}�X�̕���
    protected bool spuareHasBranch;           //��������}�X��
    public BaseMoveStateProcess(CharManager charManager, int actorNumber, NetworkHandler networkHandler)
    {
        this.charManager = charManager;
        this.actorNumber = actorNumber;
        this.networkHandler = networkHandler;
    }
    public virtual void Enter()
    {
        Debug.Log($"Move��Ԃɓ���܂���");
        selectBranchIndex = 0;
        reachTarget = false;

        //�󂯎�����_�C�X�̒l���擾
        BaseDiceStateProcess.SendDataStruct dicaData = JsonUtility.FromJson<BaseDiceStateProcess.SendDataStruct>(networkHandler.receiveData.Last().content);
        moveCount = dicaData.random;
        decideBranch = false;

        nNowSquareBranch = 0;
        spuareHasBranch = false;
    }

    public virtual int Process()
    {
        BaseSquareComponent nowSqusre = charManager.charClones[actorNumber].nowSquare;
        nNowSquareBranch = nowSqusre.nextSquare.Count;
        spuareHasBranch = nowSqusre.GetIsBranch();

        if (!spuareHasBranch)
        {
            decideBranch = true;
        }

        if (decideBranch) 
        { 
            reachTarget = charManager.ExeCharMove(actorNumber, selectBranchIndex);
            if (reachTarget)
            {
                ReachTargetProcess();
            }
        }
        int nextState = DecideNextState();

        return nextState;
    }

    public virtual void Exit()
    {
        Debug.Log($"Move��Ԃ��I�����܂�");
        //��M�f�[�^�����Z�b�g
        networkHandler.receiveData.Clear();
    }

    public int DecideNextState()
    {
        //�_�C�X�̒l���ړ�������������
        if (moveCount <= 0)
        {
            return (int)BoardGameState.SelectAct;
        }
        else
        {
            return (int)BoardGameState.Move;
        }
    }

    public void SetInputProcess(string mapName, Common.InputSystemManager inputSystem)
    {
        inputSystem.AddCallBackToAllPlayerInput(mapName, "SelectPrev", SelectPrev);
        inputSystem.AddCallBackToAllPlayerInput(mapName, "SelectNext", SelectNext);
        inputSystem.AddCallBackToAllPlayerInput(mapName, "Submit", DecideBranchIndex);
    }

    //�I������O�ɂ���
    void SelectPrev(InputAction.CallbackContext context)
    {
        if (spuareHasBranch)
        {
            selectBranchIndex = Mathf.Max(0, selectBranchIndex - 1);
            Debug.Log($"�I�� = {selectBranchIndex}");
        }
    }

    //�I�������ɂ���
    void SelectNext(InputAction.CallbackContext context)
    {
        if (spuareHasBranch)
        {
            selectBranchIndex = Mathf.Min(nNowSquareBranch - 1, selectBranchIndex + 1);
            Debug.Log($"�I�� = {selectBranchIndex}");
        }
    }

    //�i�ސ�����肷��
    void DecideBranchIndex(InputAction.CallbackContext context)
    {
        if (spuareHasBranch)
        {
            Debug.Log($"�i�ޕ���������");
            decideBranch = true;
        }
    }

    //��}�X�i�񂾎��̏���
    protected void ReachTargetProcess()
    {
        moveCount--;
        Debug.Log($"�c��̈ړ��}�X�� = {moveCount}");
        decideBranch = false;
        selectBranchIndex = 0;
    }
}
