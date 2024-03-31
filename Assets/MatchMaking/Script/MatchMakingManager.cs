using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchMakingManager : MonoBehaviour
{
    Common.PhotonNetWorkManager netWorkManager;

    [SerializeField]
    List<Transform> showTransforms = new List<Transform>();
    void Start()
    {
        if (netWorkManager == null)
        {
            netWorkManager = Common.PhotonNetWorkManager.GetInstance();
        }
        Debug.Log(netWorkManager.nClientInRoom);
        Transform transform = showTransforms[netWorkManager.nClientInRoom - 1];
        netWorkManager.InstantiatePrefab("TempModel", transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (netWorkManager.nClientInRoom >= 1)
            {
                SceneManager.LoadScene("BoardGame");
            }
        }
    }
}
