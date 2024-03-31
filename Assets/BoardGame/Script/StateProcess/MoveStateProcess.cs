using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class MoveStateProcess : Common.Interface.StateProcess
{
    CharManager charManager;
    NetworkHandler networkHandler;
    int actorNumber;                //���[�����ň�ӂ�ID
    int selectBranchIndex = 0;

    bool reachTarget;
    int moveCount;                  //�ړ���
    bool decideBranch;              //�����I���������̃g���K�[
    int nNowSquareBranch;           //���݂̃}�X�̕���
    bool spuareHasBranch;           //��������}�X��
    public MoveStateProcess(CharManager charManager, int actorNumber, NetworkHandler networkHandler)
    {
        this.charManager = charManager;
        this.actorNumber = actorNumber;
        this.networkHandler = networkHandler;
    }
    public void Enter()
    {
        Debug.Log($"Move��Ԃɓ���܂���");
        selectBranchIndex = 0;
        reachTarget = false;

        //�󂯎�����_�C�X�̒l���擾
        DiceStateProcess.SendDataStruct dicaData = JsonUtility.FromJson<DiceStateProcess.SendDataStruct>(networkHandler.receiveData.Last().content);
        moveCount = dicaData.random;
        decideBranch = false;

        nNowSquareBranch = 0;
        spuareHasBranch = false;
    }

    public int Process()
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

    public void Exit()
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
        inputSystem.AddCallBack(mapName, "SelectPrev", SelectPrev);
        inputSystem.AddCallBack(mapName, "SelectNext", SelectNext);
        inputSystem.AddCallBack(mapName, "Submit", DecideBranchIndex);
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
    void ReachTargetProcess()
    {
        moveCount--;
        Debug.Log($"�c��̈ړ��}�X�� = {moveCount}");
        decideBranch = false;
        selectBranchIndex = 0;
    }
}
