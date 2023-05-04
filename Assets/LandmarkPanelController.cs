using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandmarkPanelController : MonoBehaviour
{
    public Text text;

    public void ShowInfoOnUI(LandmarkInfo info, ChangeColor weather) {
        text.text = "Location - " + info.name + "\n - Temperature: " + weather.temp + "\n - Humidity: " + weather.humidity + "\n - Pressure: " + weather.pressure;
    }
}
