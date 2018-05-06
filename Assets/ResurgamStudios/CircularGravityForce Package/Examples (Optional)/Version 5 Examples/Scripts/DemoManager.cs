/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Main scene manager.
*******************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CircularGravityForce
{
    public class DemoManager : MonoBehaviour
    {
        //DemoManager Singleton
        static public DemoManager Instance { get { return instance; } }
        static protected DemoManager instance;

        private const string mainSceneName = "_MainMenu";

        private bool toggleCGF = false;

        //Awake is called when the script instance is being loaded
        void Awake()
        {
            instance = this;
        }

        //Restart scene
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            
        }

        //Goes back to the main scene
        public void GoBack()
        {
#if !UNITY_EDITOR
            if (SceneManager.GetActiveScene().name == mainSceneName)
            {
                Application.Quit();    
                return;
            }

            SceneManager.LoadScene(mainSceneName);
#endif
        }

        //Loads a scene
        public void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        //Toggle CGF Lines function
        public void ToggleCGFLines()
        {
            toggleCGF = !toggleCGF;

            //2D
            CGF[] cgfs = GameObject.FindObjectsOfType<CGF>();
            foreach (var cgf in cgfs)
            {
                cgf._drawGravityProperties.DrawGravityForce = toggleCGF;
            }

            //3D
            CGF2D[] cgfs2D = GameObject.FindObjectsOfType<CGF2D>();
            foreach (var cgf in cgfs2D)
            {
                cgf._drawGravityProperties.DrawGravityForce = toggleCGF;
            }
        }
    }
}