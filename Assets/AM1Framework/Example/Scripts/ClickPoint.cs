using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AM1;

public class ClickPoint : MonoBehaviour {

    private void OnMouseDown()
    {
        if (GameSystem.IsControllerable)
        {
            SoundController.Play(SoundController.SE.SWING);
            GameSystem.AddScore(100);
        }
    }
}
