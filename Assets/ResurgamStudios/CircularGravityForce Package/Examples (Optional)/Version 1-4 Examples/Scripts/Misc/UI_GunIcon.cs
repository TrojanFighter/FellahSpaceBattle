/*******************************************************************************************
* Author: Lane Gresham, AKA LaneMax
* Websites: http://resurgamstudios.com
* Description: Used for managing gun icons colors in the fps demo scene.
*******************************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CircularGravityForce
{
	public class UI_GunIcon : MonoBehaviour
    {
        #region Properties

        [SerializeField, Tooltip("Gun object.")]
		private Gun gun;
		public Gun _gun
		{
			get { return gun; }
			set { gun = value; }
		}

		[SerializeField, Tooltip("Keycode button for the gun type.")]
		private KeyCode keyCodeButton;
		public KeyCode KeyCodeButton
		{
			get { return keyCodeButton; }
			set { keyCodeButton = value; }
		}

		[SerializeField, Tooltip("The type of gun you want to use.")]
		private CircularGravityForce.Gun.GunType gunType;
		public CircularGravityForce.Gun.GunType _gunType
		{
			get { return gunType; }
			set { gunType = value; }
		}

		[SerializeField, Tooltip("Icon image used for the gun type.")]
		private Image icon;
		public Image Icon
		{
			get { return icon; }
			set { icon = value; }
		}

		[SerializeField, Tooltip("Text of the gun icon.")]
		private Text text;
		public Text Text
		{
			get { return text; }
			set { text = value; }
		}

		private Color defaultColor = Color.white;
		private Color selectColor = Color.cyan;

        #endregion

        #region Unity Functions

        // Update is called once per frame
		void Update () 
		{
			if(Input.GetKeyDown(KeyCodeButton))
			{
				gun._gunType = gunType;
			}

			if(_gun._gunType == gunType)
			{
				Icon.color = selectColor;
				Text.color = selectColor;
			}
			else
			{
				Icon.color = defaultColor;
				Text.color = defaultColor;
			}
        }

        #endregion
    }
}