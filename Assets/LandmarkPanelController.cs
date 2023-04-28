using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandmarkPanelController : MonoBehaviour
{
    public Text text;

    public void ShowInfoOnUI(LandmarkInfo info) {
        text.text = "Name: " + info.name + "\n UV index: " + info.uv_index;
    }
}
