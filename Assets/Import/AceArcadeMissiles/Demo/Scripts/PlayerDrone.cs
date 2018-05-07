using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using FellahSpaceBattle;
using UnityScript.Macros;

public class PlayerDrone : MonoBehaviour
{
    public Transform target;

    public GameObject m_armedBoy, m_unarmedBoy;

    public bool BP47Mode = false;
    public GameObject FighterModel;

    public bool BRaycastingNova = false;
    public bool BEscaping=false;

    public float warpSpeed = 100f;

    public float thrust;
    public float novaThurstDelta;
    private float defaultThrust;
    public float yaw;
    public float pitch;

    public Text msl;
    public Text xma;
    public Text rcl;
    public Text spd;

    public Image selectMSL;
    public Image selectXMA;
    public Image selectRCL;

    new Rigidbody rigidbody;

    const float FORCEMULT = 100.0f;

    int selectedLauncherGroup = 2;
    
    Queue<AALauncher>[] launchers;
    AALauncher[] allLaunchers;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        
        launchers = new Queue<AALauncher>[3];
        for (int i=0; i<3; i++)
            launchers[i] = new Queue<AALauncher>();
    }

    void Start()
    {
        defaultThrust = thrust;
        allLaunchers = GetComponentsInChildren<AALauncher>();

        // Register all the launchers in their appropriate slots so they can be switched between.
        foreach (AALauncher launcher in allLaunchers)
        {
            if (launcher.name.StartsWith("MSL"))
            {
                launchers[0].Enqueue(launcher);
            }
                
            else if (launcher.name.StartsWith("XMA"))
            {
                launchers[1].Enqueue(launcher);
            }
                
            else if (launcher.name.StartsWith("RCL"))
            {
                launchers[2].Enqueue(launcher);
            }
        }
    }

    void Update()
    {
        // Cycle launcher groups.
        /*if (Input.GetButtonDown("Fire2"))
        {
            selectedLauncherGroup++;
            if (selectedLauncherGroup >= 3)
                selectedLauncherGroup = 0;
        }*/

        // Fire selected launcher group.
        // Rockets allowed to hold fire down.
        if (selectedLauncherGroup == 2)
        {
            if (Input.GetButton("Fire1"))
                FireWeapon();
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
                FireWeapon();
        }

        // Reset all weapons.
        /*if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (AALauncher launcher in allLaunchers)
                launcher.ResetLauncher();
        }*/

        //UpdateAmmoCounters();
        //spd.text = string.Format("{0:000}", rigidbody.velocity.magnitude);
        
    }

    public void SwitchP47Mode(bool isOn)
    {
        BP47Mode = isOn;
        if (isOn)
        {
            FighterModel.SetActive(true);
            ReloadLaunchers();
            
        }
        else
        {
            FighterModel.SetActive(false);
            ClearLaunchers();
        }
    }

    public void ReloadLaunchers()
    {
        foreach (AALauncher launcher in allLaunchers)
            launcher.ResetLauncher();
    }
    
    public void ClearLaunchers()
    {
        foreach (AALauncher launcher in allLaunchers)
            launcher.ClearLauncher();
    }

    void FixedUpdate()
    {
        if (BEscaping)
        {
            if ((transform.position - GlobalStateManager.Instance.m_Meadow.position).sqrMagnitude> 100f)
            {
                transform.position += (GlobalStateManager.Instance.m_Meadow.position - transform.position).normalized *Time.fixedDeltaTime * warpSpeed;
                rigidbody.rotation=Quaternion.Euler(0,0,0);
                rigidbody.freezeRotation = true;
                rigidbody.isKinematic = true;
            }

            return;
        }

        float inPitch = Input.GetAxis("Vertical");
        float inYaw = Input.GetAxis("Horizontal");

        rigidbody.AddRelativeForce(0.0f, 0.0f, thrust * FORCEMULT * Time.deltaTime);

        rigidbody.AddRelativeTorque(inPitch * pitch * FORCEMULT * Time.deltaTime,
                                    inYaw * yaw * FORCEMULT * Time.deltaTime,
                                    -inYaw * yaw * FORCEMULT * 0.5f * Time.deltaTime);
        
        if (BRaycastingNova)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Nova");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                if (hit.collider.GetComponent<SuperNovaExit>())
                {
                    thrust += Time.fixedDeltaTime * novaThurstDelta;
                }
                else
                {
                    thrust = defaultThrust;
                }
            }
            else
            {
                /*if ((transform.position - GlobalStateManager.Instance.m_Nova.position).sqrMagnitude < 2500f)
                {
                    BRaycastingNova = false;
                    GlobalStateManager.Instance.SwitchGlobalGameState(GlobalGameState.Escaped);
                }
                else*/
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                    Debug.Log("Did not Hit");
                    thrust = defaultThrust;
                }
            }
        }
    }

    private void FireWeapon()
    {
        // Fire the next launcher, then put it back at the end of the queue.
        if (launchers[selectedLauncherGroup].Count > 0)
        {
            AALauncher temp = launchers[selectedLauncherGroup].Dequeue();
            temp.Launch(target, rigidbody.velocity);
            launchers[selectedLauncherGroup].Enqueue(temp);
            UpdateAmmoCounters();
        }
    }

    private void UpdateAmmoCounters()
    {
        // Update the ammo counters.
        int missileCount = 0;
        int xmaCount = 0;
        int rocketCount = 0;
        int rocketMagazine = 0;

        // This whole method is pretty inefficient, especially because it's in the update, but
        // this is just for the sake of demo.
        /*foreach (AALauncher launcher in allLaunchers)
        {
            if (launcher.name.StartsWith("MSL"))
                missileCount += launcher.missileCount;
            else if (launcher.name.StartsWith("XMA"))
                xmaCount += launcher.missileCount;
            else if (launcher.name.StartsWith("RCL"))
            {
                rocketCount += launcher.missileCount;
                rocketMagazine += launcher.MagazineCount;
            }
        }

        msl.text = string.Format("{0:00}", missileCount);
        xma.text = string.Format("{0:00}", xmaCount);
        rcl.text = string.Format("{0:00}-{1:00}", rocketCount, rocketMagazine);*/
        foreach (AALauncher launcher in allLaunchers)
        {
            if (launcher.name.StartsWith("RCL"))
            {
                rocketCount += launcher.missileCount;
                rocketMagazine += launcher.MagazineCount;
            }
        }
        if (rocketCount == 0 && rocketMagazine == 0)
        {
           //SwitchP47Mode(false);
            GlobalStateManager.Instance.SwitchArmedState(false);
        }


        /*selectMSL.enabled = (selectedLauncherGroup == 0) ? true : false;
        selectXMA.enabled = (selectedLauncherGroup == 1) ? true : false;
        selectRCL.enabled = (selectedLauncherGroup == 2) ? true : false;*/
    }
    
    

    public void Escaped()
    {
        SwitchArmedBoy(false);
    }

    public void SwitchArmedBoy(bool armedboyOn)
    {
        m_armedBoy.SetActive(armedboyOn);
        m_unarmedBoy.SetActive(!armedboyOn);
        if (!armedboyOn)
        {
            rigidbody.rotation=Quaternion.Euler(0,0,0);
            rigidbody.freezeRotation = true;
            rigidbody.isKinematic = true;
            BEscaping = true;
        }
    }

}