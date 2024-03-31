using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSquareComponent : MonoBehaviour
{
    [field: SerializeField]
    public List<BaseSquareComponent> preSquare { get; private set; }  //�O�̃}�X
    [field: SerializeField]
    public List<BaseSquareComponent> nextSquare { get; private set; } //���̃}�X

    //�}�X��ʂ����ꍇ�̏���
    //�ʂ������ɋ����I�ɃC�x���g���N�����ꍇ�A���̃��\�b�h���I�[�o�[���C�h����
    public void ThroughProcess()
    {
        //���򂷂�ꍇ
        if (nextSquare.Count > 1)
        {

        }
        //��������I�[�o�[���C�h
    }

    //�}�X�Ɏ~�܂����ꍇ�̏���
    //�T�u�N���X�ŋ�̓I�ȏ���������
    public abstract void OnProcess();

    //���̃}�X�����򂷂邩��Ԃ�
    public bool GetIsBranch()
    {
        bool result = false;

        if (nextSquare.Count > 1)
        {
            result = true;
        }

        return result;
    }
}
