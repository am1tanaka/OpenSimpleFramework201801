using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AM1;

public class ExampleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LevelChanger.SetPostFadeIn(onFadeIn);
        SoundController.PlayBGM(SoundController.BGM.BGM, true);
        GameSystem.InitGame();
	}
	
    void onFadeIn()
    {
        Debug.Log("Fade In Done Test.");
    }

}
