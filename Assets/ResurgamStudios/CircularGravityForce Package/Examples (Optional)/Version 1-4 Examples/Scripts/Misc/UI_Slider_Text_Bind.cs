/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for binding the slider values to text.
*******************************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CircularGravityForce
{
    public class UI_Slider_Text_Bind : MonoBehaviour
    {
        #region Properties

        //Input to spawn
        [SerializeField, Tooltip("Slider to be binded.")]
        private Slider slider;
        public Slider Slider
        {
            get { return slider; }
            set { slider = value; }
        }

        private Text textLabel;

        #endregion

        #region Unity Functions

        // Use this for initialization
        void Start()
        {
            textLabel = this.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            textLabel.text = Mathf.Round(Slider.value).ToString();
        }

        #endregion
    }
}