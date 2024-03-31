using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectModeManager : MonoBehaviour
{
    UserParams userParams;
    // Start is called before the first frame update
    void Start()
    {
        userParams = UserParams.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            userParams.SetIsOnline(true);
            SceneManager.LoadScene("SelectRoom");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            userParams.SetIsOnline(false);
            userParams.SetCharPrefabName("TempModel");
            SceneManager.LoadScene("BoardGame");
        }
    }
}
