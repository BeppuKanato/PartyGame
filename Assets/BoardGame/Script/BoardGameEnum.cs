using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�{�[�h�Q�[���̏��
public enum BoardGameState
{
    None,
    Preparation,    //�Q�[���J�n�O�̏���
    OrderDice,      //���ԗp�̃_�C�X��U��
    DecideOrder,    //���Ԃ����߂�
    SelectAct,      //�s���I��
    MoveDice,       //�ړ��ʂ����肷��
    Move,           //�ړ���
}