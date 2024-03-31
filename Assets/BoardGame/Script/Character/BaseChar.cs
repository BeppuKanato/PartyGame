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
    public int nCoin;                 //コイン枚数
    [SerializeField]
    public int nStar;                //スター枚数
    //[SerializeField]
    //List<> item;                   //所持アイテム
    [SerializeField]
    public bool local;

    [SerializeField]
    public BaseSquareComponent nowSquare { get; private set; }  //現在止まっているマス

    Vector3 prevPos;
    //キャラクタの初期化
    public void InitializeChar(Transform parent, BaseSquareComponent nowSquare)
    {
        this.transform.SetParent(parent, false);
        this.nowSquare = nowSquare;

        prevPos = this.transform.position;
    }

    //移動中のアニメーション
    //移動完了した場合trueを返す
    public bool MoveToSquare(int branchIndex, float moveSpeed)
    {
        GameObject targetSquare = nowSquare.nextSquare[branchIndex].gameObject;
        bool reachTarget = false;
        //アニメーションの再生
        if (!animator.GetBool("IsRun"))
        {
            animator.SetBool("IsRun", true);
        }
        //座標移動
        PositionUpDate(targetSquare, moveSpeed);
        //角度変更
        //RotateUpdate();

        //一定距離まで近づいたとき
        if (Vector3.Distance(this.gameObject.transform.position, targetSquare.transform.position) <= 0.05f)
        {
            this.gameObject.transform.position = targetSquare.transform.position;
            animator.SetBool("IsRun", false);
            ReachTargetProcess(branchIndex);
            reachTarget = true;
        }

        return reachTarget;
    }

    //次のマスに到達した時の処理
    void ReachTargetProcess(int branchIndex)
    {
        nowSquare = nowSquare.nextSquare[branchIndex];
    }

    //角度を更新
    void RotateUpdate()
    {
        Vector3 nowPos = this.transform.position;
        //1ループ間での移動量計測
        Vector3 delta = nowPos - prevPos;
        prevPos = nowPos;

        if (delta.magnitude >= 0.01f)
        {
            this.gameObject.transform.localRotation = Quaternion.LookRotation(delta, Vector3.down);
        }
    }
    //座標を更新
    void PositionUpDate(GameObject targetSquare, float moveSpeed)
    {
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, targetSquare.transform.position, moveSpeed);
    }
    //おそらく足音を鳴らすイベント
    public void OnFootstep()
    {

    }
}
