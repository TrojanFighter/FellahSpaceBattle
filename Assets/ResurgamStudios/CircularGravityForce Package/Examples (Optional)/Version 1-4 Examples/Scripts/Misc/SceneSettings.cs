/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for managing the demo scenes.
*******************************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_4_9 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
using UnityEngine.SceneManagement;
#endif

namespace CircularGravityForce
{
    public class SceneSettings : MonoBehaviour
    {
        //Singleton Logic
        private static SceneSettings _instance;
        public static SceneSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = GameObject.FindObjectOfType<SceneSettings>();

                return _instance;
            }
        }

        #region Properties

        [SerializeField, Tooltip("Enable/disable's the main menu."), Header("Scene Options:")]
        private bool toggleMainMenu = false;
        public bool ToggleMainMenu
        {
            get { return toggleMainMenu; }
            set { toggleMainMenu = value; }
        }
        [SerializeField, Tooltip("Locks the mouse.")]
        private bool lockMouse = false;
        public bool LockMouse
        {
            get { return lockMouse; }
            set { lockMouse = value; }
        }
        [SerializeField, Tooltip("Locks the mouse.")]
        private bool showMouseOnlock = true;
        public bool ShowMouseOnlock
        {
            get { return showMouseOnlock; }
            set { showMouseOnlock = value; }
        }

        [SerializeField, Tooltip("GUI canvas gameobject."), Header("Canvas Objects:")]
        private GameObject guiCanvas;
        public GameObject GUICanvas
        {
            get { return guiCanvas; }
            set { guiCanvas = value; }
        }
        [SerializeField, Tooltip("Help text.")]
        private Text helpUIText;
        public Text HelpUIText
        {
            get { return helpUIText; }
            set { helpUIText = value; }
        }
        [SerializeField, Tooltip("Gameobject that contatins the panel for the main menu.")]
        private GameObject panel_MainMenu;
        public GameObject Panel_MainMenu
        {
            get { return panel_MainMenu; }
            set { panel_MainMenu = value; }
        }

        [SerializeField, Tooltip("FPS gun script."), Header("FPS Scene Objects:")]
        private Gun fps_GunScript;
        public Gun Fps_GunScript
        {
            get { return fps_GunScript; }
            set { fps_GunScript = value; }
        }
        [SerializeField, Tooltip("firstPersonController script.")]
        private UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;
        public UnityStandardAssets.Characters.FirstPerson.FirstPersonController FirstPersonController
        {
            get { return firstPersonController; }
            set { firstPersonController = value; }
        }

        private bool toggleCGF = false;
        public bool ToggleCGF
        {
            get { return toggleCGF; }
            set { toggleCGF = value; }
        }

        private bool toggleGUI = true;
        public bool ToggleGUI
        {
            get { return toggleGUI; }
            set { toggleGUI = value; }
        }

        #endregion

        #region Unity Functions

        void Update()
        {
            //Main Key
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                ToggleMainMenu = !ToggleMainMenu;
            }
            //Reset Scene Key
            if (Input.GetKeyUp(KeyCode.R))
            {
#if !(UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_4_9 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#else
                Application.LoadLevel(Application.loadedLevelName);
#endif
            }
            //Toggle CGF Lines
            if (Input.GetKeyUp(KeyCode.T))
            {
                ToggleCGFLines();
            }

            //Toggles GUI
            if (Input.GetKeyUp(KeyCode.Home))
            {
                ToggleGUILayout();
            }

            //Help ESC text
            if (HelpUIText != null)
            {
                if (ToggleMainMenu)
                    HelpUIText.text = "Press <b>ESC</b> to go back.";
                else
                    HelpUIText.text = "Press <b>ESC</b> for menu.";
            }

            if (Panel_MainMenu != null)
                Panel_MainMenu.SetActive(ToggleMainMenu);

            if (Fps_GunScript != null)
                Fps_GunScript.enabled = (!ToggleMainMenu);

            if (FirstPersonController != null)
                FirstPersonController.enabled = (!ToggleMainMenu);

#if !(UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_4_9)
            if ((!ToggleMainMenu && LockMouse))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = ShowMouseOnlock;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
#else
            Screen.lockCursor = (!ToggleMainMenu && LockMouse); ;
#endif
        }

        #endregion

        #region Functions

        //Toggle CGF Lines function
        public void ToggleCGFLines()
        {
            ToggleCGF = !ToggleCGF;

            //2D
            CGF[] cgfs = GameObject.FindObjectsOfType<CGF>();
            foreach (var cgf in cgfs)
            {
                cgf._drawGravityProperties.DrawGravityForce = ToggleCGF;
            }

            //3D
            CGF2D[] cgfs2D = GameObject.FindObjectsOfType<CGF2D>();
            foreach (var cgf in cgfs2D)
            {
                cgf._drawGravityProperties.DrawGravityForce = ToggleCGF;
            }
        }

        public void ToggleGUILayout()
        {
            ToggleGUI = !ToggleGUI;

            GUICanvas.SetActive(ToggleGUI);
        }

        //Load scene function
        public void LoadScene(string sceneName)
        {
#if !(UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_4_9 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
            SceneManager.LoadScene(sceneName);
#else
            Application.LoadLevel(sceneName);
#endif
        }

        #endregion
    }
}