using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZEffects;
[RequireComponent(typeof(AudioSource))]
public class WeaponScript : MonoBehaviour {

    public int damage = 1;
    public GameObject controllerRight;
    public EffectTracer tracerEffect;
    public Transform muzzleTransform;
    public AudioClip gunShot;
    public GameObject gameManager;
    AudioSource audio;


    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device contDevice;
    private SteamVR_TrackedController controller;
    private Ray ray;
    private RaycastHit hit;
    private int shootableMask;
    // Use this for initialization
    void Start() {
        controller = controllerRight.GetComponent<SteamVR_TrackedController>();
        trackedObj = controllerRight.GetComponent<SteamVR_TrackedObject>();
        contDevice = SteamVR_Controller.Input((int)trackedObj.index);
        controller.TriggerClicked += TriggerPressed;
        shootableMask = LayerMask.GetMask("Shootable");
        audio = GetComponent<AudioSource>();

    }

    private void TriggerPressed(object sender, ClickedEventArgs e) {
         ShootWeapon();
        audio.Play();


    }
    
    public void ShootWeapon() {
        hit = new RaycastHit();
        ray = new Ray(muzzleTransform.position, muzzleTransform.forward);


        contDevice.TriggerHapticPulse(750);
        tracerEffect.ShowTracerEffect(muzzleTransform.position, muzzleTransform.forward, 250f);

        if(Physics.Raycast(ray, out hit, 5000f, shootableMask)) {
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

            if(enemyHealth != null) {
                enemyHealth.TakeDamage(damage);
            }
        }

        if(Physics.Raycast(ray,out hit, 5000f)) {
            if (hit.collider.tag == "Start") {
                print("hello");
                WaveSpawner waveSpawner;
                waveSpawner = gameManager.GetComponent<WaveSpawner>();
                waveSpawner.enabled = true;
                hit.collider.transform.root.gameObject.SetActive(false);

                StartMusic musicStart;
                musicStart = gameManager.GetComponent<StartMusic>();
                musicStart.enabled = true;
                
                
            }
        }

        

    }
    

    // Update is called once per frame
    void Update() {

    }
}
