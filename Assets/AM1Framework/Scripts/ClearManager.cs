using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AM1;

public class ClearManager : MonoBehaviour {

    private void Start()
    {
        Time.timeScale = 0f;
        SoundController.Play(SoundController.SE.CLICK);
        SoundController.StopBGM(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSystem.IsControllerable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                LevelChanger.ChangeScene("Game");
            }
        }
    }
}
