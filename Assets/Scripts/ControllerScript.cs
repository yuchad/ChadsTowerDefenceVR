using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZEffects;

public class ControllerScript : MonoBehaviour {

    public GameObject controllerRight;
    public EffectTracer tracerEffect;
    public Transform muzzleTransform;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device contDevice;
    private SteamVR_TrackedController controller;
	// Use this for initialization
	void Start () {
        controller = controllerRight.GetComponent<SteamVR_TrackedController>();
        trackedObj = this.GetComponent<SteamVR_TrackedObject>();
        controller.TriggerClicked += TriggerPressed;
        
	}

    private void TriggerPressed(object sender, ClickedEventArgs e) {
       // ShootWeapon();
    }
    /*
    public void ShootWeapon() {
        RaycastHit = new RaycastHit();
        Ray ray = (muzzleTransform.position, muzzleTransform.forward);

        contDevice = SteamVR_Controller.Input((int)trackedObj.index);
        contDevice.TriggerHapticPulse(750);
        tracerEffect.ShowTracerEffect(muzzleTransform.position, muzzleTransform.forward, 250f);

        if(Physics.Raycast(ray, out hit, 5000f)) {
            if()
        }

    }
    */


	
	// Update is called once per frame
	void Update () {
		
	}
}
