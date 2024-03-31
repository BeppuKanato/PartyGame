using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��Ԗ��̏����N���X����������C���^�[�t�F�[�X

namespace Common
{
    namespace Interface
    {
        public interface StateProcess
        {
            void Enter();           //��Ԃɓ��������̏���������
            int Process();          //��Ԃɂ���Ԃ̏��������� �߂�l = int�^�ɕϊ�������ԗ񋓑�
            void Exit();            //��Ԃ��o�鎞�̏���������

            int DecideNextState();  //���̏�Ԃ����肷�鏈��������

            void SetInputProcess(string mapName, InputSystemManager inputSystem);    //���͎��̏�����ݒ肷��
        }
    }
}