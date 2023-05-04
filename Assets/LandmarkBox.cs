using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LandmarkBox : MonoBehaviour
{
    public LandmarkPanelController panelController;
    public LandmarkInfo info;
    public ChangeColor weather;

    public void Start() {
        // this.info = this.gameObject.GetComponent<LandmarkInfo>();
        // this.panelController = this.gameObject.GetComponent<LandmarkPanelController>();
    }

    private void OnMouseDown(){
        panelController.ShowInfoOnUI(this.info, this.weather);
    }
}
