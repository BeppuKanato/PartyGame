using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOrderManager: MonoBehaviour
{
    public Common.IntQueManager que { get; private set; }  //���삪�I�����Ă��Ȃ�ID���X�g
    Common.IntQueManager actedQue;                         //���삪�I�����Ă���ID���X�g

    private void Start()
    {
        que = new Common.IntQueManager();
        actedQue = new Common.IntQueManager();
    }

    //������s��ID��Ԃ�
    public int GetActorID()
    {
        return que.GetLastValue();
    }
    //���쌠�����ւ���
    public void RotateActorOrder()
    {
        actedQue.Push(que.Pop());
    }
    //�S����������s��������Ԃ�
    public bool CheckAllActed()
    {
        bool result = false;
        if (que.GetQueCount() <= 0)
        {
            ResetOrderQue();
            result = true;
        }

        return result;
    }
    //�s�����L���[���Z�b�g����
    public void SetQue(List<int> que)
    {
        this.que.SetQue(que);
    }
    //�s�����L���[�����Z�b�g����
    //�s�����L���[�����Z�b�g����
    void ResetOrderQue()
    {
        que.SetQue(actedQue.GetQueContent());
        actedQue.ResetQue();
    }
}
