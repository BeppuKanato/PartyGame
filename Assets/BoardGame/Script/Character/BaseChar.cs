using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseChar : MonoBehaviour
{
    [SerializeField]
    Animator animator;      
    [SerializeField]
    public int nCoin;                 //�R�C������
    [SerializeField]
    public int nStar;                //�X�^�[����
    //[SerializeField]
    //List<> item;                   //�����A�C�e��
    [SerializeField]
    public bool local;

    [SerializeField]
    public BaseSquareComponent nowSquare { get; private set; }  //���ݎ~�܂��Ă���}�X

    Vector3 prevPos;
    //�L�����N�^�̏�����
    public void InitializeChar(Transform parent, BaseSquareComponent nowSquare)
    {
        this.transform.SetParent(parent, false);
        this.nowSquare = nowSquare;

        prevPos = this.transform.position;
    }

    //�ړ����̃A�j���[�V����
    //�ړ����������ꍇtrue��Ԃ�
    public bool MoveToSquare(int branchIndex, float moveSpeed)
    {
        GameObject targetSquare = nowSquare.nextSquare[branchIndex].gameObject;
        bool reachTarget = false;
        //�A�j���[�V�����̍Đ�
        if (!animator.GetBool("IsRun"))
        {
            animator.SetBool("IsRun", true);
        }
        //���W�ړ�
        PositionUpDate(targetSquare, moveSpeed);
        //�p�x�ύX
        //RotateUpdate();

        //��苗���܂ŋ߂Â����Ƃ�
        if (Vector3.Distance(this.gameObject.transform.position, targetSquare.transform.position) <= 0.05f)
        {
            this.gameObject.transform.position = targetSquare.transform.position;
            animator.SetBool("IsRun", false);
            ReachTargetProcess(branchIndex);
            reachTarget = true;
        }

        return reachTarget;
    }

    //���̃}�X�ɓ��B�������̏���
    void ReachTargetProcess(int branchIndex)
    {
        nowSquare = nowSquare.nextSquare[branchIndex];
    }

    //�p�x���X�V
    void RotateUpdate()
    {
        Vector3 nowPos = this.transform.position;
        //1���[�v�Ԃł̈ړ��ʌv��
        Vector3 delta = nowPos - prevPos;
        prevPos = nowPos;

        if (delta.magnitude >= 0.01f)
        {
            this.gameObject.transform.localRotation = Quaternion.LookRotation(delta, Vector3.down);
        }
    }
    //���W���X�V
    void PositionUpDate(GameObject targetSquare, float moveSpeed)
    {
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, targetSquare.transform.position, moveSpeed);
    }
    //�����炭������炷�C�x���g
    public void OnFootstep()
    {

    }
}
