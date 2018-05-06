// Copyright (c) Rotorz Limited. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root.

using InAudioSystem.ReorderableList.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

namespace InAudioSystem.ReorderableList {
	
	/// <summary>
	/// Arguments which are passed to <see cref="AddMenuClickedEventHandler"/>.
	/// </summary>
	public sealed class AddMenuClickedEventArgs : EventArgs {

		/// <summary>
		/// Gets adaptor to reorderable list container.
		/// </summary>
		public IReorderableListAdaptor Adaptor { get; private set; }
		/// <summary>
		/// Gets position of the add menu button.
		/// </summary>
		public Rect ButtonPosition { get; internal set; }

		/// <summary>
		/// Initializes a new instance of <see cref="ItemMovedEventArgs"/>.
		/// </summary>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <param name="buttonPosition">Position of the add menu button.</param>
		public AddMenuClickedEventArgs(IReorderableListAdaptor adaptor, Rect buttonPosition) {
			this.Adaptor = adaptor;
			this.ButtonPosition = buttonPosition;
		}

	}

	/// <summary>
	/// An event handler which is invoked when the "Add Menu" button is clicked.
	/// </summary>
	/// <param name="sender">Object which raised event.</param>
	/// <param name="args">Event arguments.</param>
	public delegate void AddMenuClickedEventHandler(object sender, AddMenuClickedEventArgs args);
	
	/// <summary>
	/// Arguments which are passed to <see cref="ItemInsertedEventHandler"/>.
	/// </summary>
	public sealed class ItemInsertedEventArgs : EventArgs {

		/// <summary>
		/// Gets adaptor to reorderable list container which contains element.
		/// </summary>
		public IReorderableListAdaptor Adaptor { get; private set; }
		/// <summary>
		/// Gets zero-based index of item which was inserted.
		/// </summary>
		public int ItemIndex { get; private set; }

		/// <summary>
		/// Indicates if inserted item was duplicated from another item.
		/// </summary>
		public bool WasDuplicated { get; private set; }

		/// <summary>
		/// Initializes a new instance of <see cref="ItemInsertedEventArgs"/>.
		/// </summary>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <param name="itemIndex">Zero-based index of item.</param>
		/// <param name="wasDuplicated">Indicates if inserted item was duplicated from another item.</param>
		public ItemInsertedEventArgs(IReorderableListAdaptor adaptor, int itemIndex, bool wasDuplicated) {
			this.Adaptor = adaptor;
			this.ItemIndex = itemIndex;
			this.WasDuplicated = wasDuplicated;
		}

	}

	/// <summary>
	/// An event handler which is invoked after new list item is inserted.
	/// </summary>
	/// <param name="sender">Object which raised event.</param>
	/// <param name="args">Event arguments.</param>
	public delegate void ItemInsertedEventHandler(object sender, ItemInsertedEventArgs args);

	/// <summary>
	/// Arguments which are passed to <see cref="ItemRemovingEventHandler"/>.
	/// </summary>
	public sealed class ItemRemovingEventArgs : CancelEventArgs {

		/// <summary>
		/// Gets adaptor to reorderable list container which contains element.
		/// </summary>
		public IReorderableListAdaptor Adaptor { get; private set; }
		/// <summary>
		/// Gets zero-based index of item which is being removed.
		/// </summary>
		public int ItemIndex { get; internal set; }

		/// <summary>
		/// Initializes a new instance of <see cref="ItemRemovingEventArgs"/>.
		/// </summary>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <param name="itemIndex">Zero-based index of item.</param>
		public ItemRemovingEventArgs(IReorderableListAdaptor adaptor, int itemIndex) {
			this.Adaptor = adaptor;
			this.ItemIndex = itemIndex;
		}

	}

	/// <summary>
	/// An event handler which is invoked before a list item is removed.
	/// </summary>
	/// <remarks>
	/// <para>Item removal can be cancelled by setting <see cref="CancelEventArgs.Cancel"/>
	/// to <c>true</c>.</para>
	/// </remarks>
	/// <param name="sender">Object which raised event.</param>
	/// <param name="args">Event arguments.</param>
	public delegate void ItemRemovingEventHandler(object sender, ItemRemovingEventArgs args);

	/// <summary>
	/// Arguments which are passed to <see cref="ItemMovingEventHandler"/>.
	/// </summary>
	public sealed class ItemMovingEventArgs : CancelEventArgs {

		/// <summary>
		/// Gets adaptor to reorderable list container which contains element.
		/// </summary>
		public IReorderableListAdaptor Adaptor { get; private set; }
		/// <summary>
		/// Gets current zero-based index of item which is going to be moved.
		/// </summary>
		public int ItemIndex { get; internal set; }
		/// <summary>
		/// Gets the new candidate zero-based index for the item.
		/// </summary>
		/// <seealso cref="NewItemIndex"/>
		public int DestinationItemIndex { get; internal set; }

		/// <summary>
		/// Gets zero-based index of item <strong>after</strong> it has been moved.
		/// </summary>
		/// <seealso cref="DestinationItemIndex"/>
		public int NewItemIndex {
			get {
				int result = DestinationItemIndex;
				if (result > ItemIndex)
					--result;
				return result;
			}
		}

		/// <summary>
		/// Initializes a new instance of <see cref="ItemMovingEventArgs"/>.
		/// </summary>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <param name="itemIndex">Zero-based index of item.</param>
		/// <param name="destinationItemIndex">Xero-based index of item destination.</param>
		public ItemMovingEventArgs(IReorderableListAdaptor adaptor, int itemIndex, int destinationItemIndex) {
			this.Adaptor = adaptor;
			this.ItemIndex = itemIndex;
			this.DestinationItemIndex = destinationItemIndex;
		}

	}

	/// <summary>
	/// An event handler which is invoked before a list item is moved.
	/// </summary>
	/// <remarks>
	/// <para>Moving of item can be cancelled by setting <see cref="CancelEventArgs.Cancel"/>
	/// to <c>true</c>.</para>
	/// </remarks>
	/// <param name="sender">Object which raised event.</param>
	/// <param name="args">Event arguments.</param>
	public delegate void ItemMovingEventHandler(object sender, ItemMovingEventArgs args);

	/// <summary>
	/// Arguments which are passed to <see cref="ItemMovedEventHandler"/>.
	/// </summary>
	public sealed class ItemMovedEventArgs : EventArgs {

		/// <summary>
		/// Gets adaptor to reorderable list container which contains element.
		/// </summary>
		public IReorderableListAdaptor Adaptor { get; private set; }
		/// <summary>
		/// Gets old zero-based index of the item which was moved.
		/// </summary>
		public int OldItemIndex { get; internal set; }
		/// <summary>
		/// Gets new zero-based index of the item which was moved.
		/// </summary>
		public int NewItemIndex { get; internal set; }

		/// <summary>
		/// Initializes a new instance of <see cref="ItemMovedEventArgs"/>.
		/// </summary>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <param name="oldItemIndex">Old zero-based index of item.</param>
		/// <param name="newItemIndex">New zero-based index of item.</param>
		public ItemMovedEventArgs(IReorderableListAdaptor adaptor, int oldItemIndex, int newItemIndex) {
			this.Adaptor = adaptor;
			this.OldItemIndex = oldItemIndex;
			this.NewItemIndex = newItemIndex;
		}

	}

	/// <summary>
	/// An event handler which is invoked after a list item is moved.
	/// </summary>
	/// <param name="sender">Object which raised event.</param>
	/// <param name="args">Event arguments.</param>
	public delegate void ItemMovedEventHandler(object sender, ItemMovedEventArgs args);

	/// <summary>
	/// Base class for custom reorderable list control.
	/// </summary>
	public class ReorderableListControl {

		/// <summary>
		/// Invoked to draw list item.
		/// </summary>
		/// <remarks>
		/// <para>GUI controls must be positioned absolutely within the given rectangle since
		/// list items must be sized consistently.</para>
		/// </remarks>
		/// <example>
		/// <para>The following listing presents a text field for each list item:</para>
		/// <code language="csharp"><![CDATA[
        /// using InAudioSystem.ReorderableList;
		/// using System.Collections.Generic;
		/// using UnityEditor;
		/// using UnityEngine;
		/// 
		/// public class ExampleWindow : EditorWindow {
		///     public List<string> wishlist = new List<string>();
		/// 
		///     private void OnGUI() {
		///         ReorderableListGUI.ListField(wishlist, DrawListItem);
		///     }
		/// 
		///     private string DrawListItem(Rect position, string value) {
		///         // Text fields do not like `null` values!
		///         if (value == null)
		///             value = "";
		///         return EditorGUI.TextField(position, value);
		///     }
		/// }
		/// ]]></code>
        /// <code language="unityscript"><![CDATA[
        /// import InAudioSystem.ReorderableList;
		/// import System.Collections.Generic;
		/// 
		/// class ExampleWindow extends EditorWindow {
		///     var wishlist:List.<String>;
		/// 
		///     function OnGUI() {
		///         ReorderableListGUI.ListField(wishlist, DrawListItem);
		///     }
		/// 
		///     function DrawListItem(position:Rect, value:String):String {
		///         // Text fields do not like `null` values!
		///         if (value == null)
		///             value = '';
		///         return EditorGUI.TextField(position, value);
		///     }
		/// }
		/// ]]></code>
		/// </example>
		/// <typeparam name="T">Type of item list.</typeparam>
		/// <param name="position">Position of list item.</param>
		/// <param name="item">The list item.</param>
		/// <returns>
		/// The modified value.
		/// </returns>
		public delegate T ItemDrawer<T>(Rect position, T item);

		/// <summary>
		/// Invoked to draw content for empty list.
		/// </summary>
		/// <remarks>
		/// <para>Callback should make use of <c>GUILayout</c> to present controls.</para>
		/// </remarks>
		/// <example>
		/// <para>The following listing displays a label for empty list control:</para>
        /// <code language="csharp"><![CDATA[
        /// using InAudioSystem.ReorderableList;
		/// using System.Collections.Generic;
		/// using UnityEditor;
		/// using UnityEngine;
		/// 
		/// public class ExampleWindow : EditorWindow {
		///     private List<string> _list;
		/// 
		///     private void OnEnable() {
		///         _list = new List<string>();
		///     }
		///     private void OnGUI() {
		///         ReorderableListGUI.ListField(_list, ReorderableListGUI.TextFieldItemDrawer, DrawEmptyMessage);
		///     }
		/// 
		///     private string DrawEmptyMessage() {
		///         GUILayout.Label("List is empty!", EditorStyles.miniLabel);
		///     }
		/// }
		/// ]]></code>
        /// <code language="unityscript"><![CDATA[
        /// import InAudioSystem.ReorderableList;
		/// import System.Collections.Generic;
		/// 
		/// class ExampleWindow extends EditorWindow {
		///     private var _list:List.<String>;
		/// 
		///     function OnEnable() {
		///         _list = new List.<String>();
		///     }
		///     function OnGUI() {
		///         ReorderableListGUI.ListField(_list, ReorderableListGUI.TextFieldItemDrawer, DrawEmptyMessage);
		///     }
		/// 
		///     function DrawEmptyMessage() {
		///         GUILayout.Label('List is empty!', EditorStyles.miniLabel);
		///     }
		/// }
		/// ]]></code>
		/// </example>
		public delegate void DrawEmpty();
		/// <summary>
		/// Invoked to draw content for empty list with absolute positioning.
		/// </summary>
		/// <param name="position">Position of empty content.</param>
		public delegate void DrawEmptyAbsolute(Rect position);

		#region Custom Styles

		/// <summary>
		/// Background color of anchor list item.
		/// </summary>
		public static readonly Color AnchorBackgroundColor;
		/// <summary>
		/// Background color of target slot when dragging list item.
		/// </summary>
		public static readonly Color TargetBackgroundColor;

		/// <summary>
		/// Style for right-aligned label for element number prefix.
		/// </summary>
		private static GUIStyle s_RightAlignedLabelStyle;

		static ReorderableListControl() {
			s_CurrentItemIndex = new Stack<int>();
			s_CurrentItemIndex.Push(-1);

			if (EditorGUIUtility.isProSkin) {
				AnchorBackgroundColor = new Color(85f / 255f, 85f / 255f, 85f / 255f, 0.85f);
				TargetBackgroundColor = new Color(0, 0, 0, 0.5f);
			}
			else {
				AnchorBackgroundColor = new Color(225f / 255f, 225f / 255f, 225f / 255f, 0.85f);
				TargetBackgroundColor = new Color(0, 0, 0, 0.5f);
			}
		}

		#endregion

		#region Utility

		/// <summary>
		/// Generate and draw control from state object.
		/// </summary>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <param name="drawEmpty">Delegate for drawing empty list.</param>
		/// <param name="flags">Optional flags to pass into list field.</param>
		public static void DrawControlFromState(IReorderableListAdaptor adaptor, DrawEmpty drawEmpty, ReorderableListFlags flags) {
			int controlID = GUIUtility.GetControlID(FocusType.Passive);

			var control = GUIUtility.GetStateObject(typeof(ReorderableListControl), controlID) as ReorderableListControl;
			control.Flags = flags;
			control.Draw(controlID, adaptor, drawEmpty);
		}

		/// <summary>
		/// Generate and draw control from state object.
		/// </summary>
		/// <param name="position">Position of control.</param>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <param name="drawEmpty">Delegate for drawing empty list.</param>
		/// <param name="flags">Optional flags to pass into list field.</param>
		public static void DrawControlFromState(Rect position, IReorderableListAdaptor adaptor, DrawEmptyAbsolute drawEmpty, ReorderableListFlags flags) {
			int controlID = GUIUtility.GetControlID(FocusType.Passive);

			var control = GUIUtility.GetStateObject(typeof(ReorderableListControl), controlID) as ReorderableListControl;
			control.Flags = flags;
			control.Draw(position, controlID, adaptor, drawEmpty);
		}

		#endregion

		/// <summary>
		/// Position of mouse upon anchoring item for drag.
		/// </summary>
		private static float s_AnchorMouseOffset;
		/// <summary>
		/// Zero-based index of anchored list item.
		/// </summary>
		private static int s_AnchorIndex = -1;
		/// <summary>
		/// Zero-based index of target list item for reordering.
		/// </summary>
		private static int s_TargetIndex = -1;

		/// <summary>
		/// Unique ID of list control which should be automatically focused. A value
		/// of zero indicates that no control is to be focused.
		/// </summary>
		private static int s_AutoFocusControlID = 0;
		/// <summary>
		/// Zero-based index of item which should be focused.
		/// </summary>
		private static int s_AutoFocusIndex = -1;

		/// <summary>
		/// Zero-based index of list item which is currently being drawn.
		/// </summary>
		private static Stack<int> s_CurrentItemIndex;

		/// <summary>
		/// Gets zero-based index of list item which is currently being drawn;
		/// or a value of -1 if no item is currently being drawn.
		/// </summary>
		/// <remarks>
		/// <para>Use <see cref="ReorderableListGUI.CurrentItemIndex"/> instead.</para>
		/// </remarks>
		internal static int CurrentItemIndex {
			get { return s_CurrentItemIndex.Peek(); }
		}

		#region Properties

		[SerializeField]
		private ReorderableListFlags _flags;

		/// <summary>
		/// Gets or sets flags which affect behavior of control.
		/// </summary>
		public ReorderableListFlags Flags {
			get { return _flags; }
			set { _flags = value; }
		}

		/// <summary>
		/// Gets a value indicating whether one or more footer buttons are shown.
		/// </summary>
		private bool HasFooterButtons {
			get { return HasAddButton || HasAddMenuButton; }
		}
		/// <summary>
		/// Gets a value indicating whether add button is shown.
		/// </summary>
		private bool HasAddButton {
			get { return (_flags & ReorderableListFlags.HideAddButton) == 0; }
		}
		/// <summary>
		/// Gets a value indicating whether add menu button is shown.
		/// </summary>
		private bool HasAddMenuButton { get; set; }

		/// <summary>
		/// Gets a value indicating whether remove buttons are shown.
		/// </summary>
		private bool HasRemoveButtons {
			get { return (_flags & ReorderableListFlags.HideRemoveButtons) == 0; }
		}

		[SerializeField]
		private GUIStyle _containerStyle;
		[SerializeField]
		private GUIStyle _footerButtonStyle;
		[SerializeField]
		private GUIStyle _itemButtonStyle;

		/// <summary>
		/// Gets or sets style used to draw background of list control.
		/// </summary>
		/// <seealso cref="ReorderableListStyles.Container"/>
		public GUIStyle ContainerStyle {
			get { return _containerStyle; }
			set { _containerStyle = value; }
		}
		/// <summary>
		/// Gets or sets style used to draw footer buttons.
		/// </summary>
		/// <seealso cref="ReorderableListStyles.FooterButton"/>
		public GUIStyle FooterButtonStyle {
			get { return _footerButtonStyle; }
			set { _footerButtonStyle = value; }
		}
		/// <summary>
		/// Gets or sets style used to draw list item buttons (like the remove button).
		/// </summary>
		/// <seealso cref="ReorderableListStyles.ItemButton"/>
		public GUIStyle ItemButtonStyle {
			get { return _itemButtonStyle; }
			set { _itemButtonStyle = value; }
		}

		#endregion

		#region Events

		private event AddMenuClickedEventHandler _addMenuClicked;
		private int _addMenuClickedSubscriberCount = 0;

		/// <summary>
		/// Occurs when add menu button is clicked.
		/// </summary>
		/// <remarks>
		/// <para>Add menu button is only shown when there is at least one subscriber to this event.</para>
		/// </remarks>
		public event AddMenuClickedEventHandler AddMenuClicked {
			add {
				if (value == null)
					return;
				_addMenuClicked += value;
				++_addMenuClickedSubscriberCount;
				HasAddMenuButton = _addMenuClickedSubscriberCount != 0;
			}
			remove {
				if (value == null)
					return;
				_addMenuClicked -= value;
				--_addMenuClickedSubscriberCount;
				HasAddMenuButton = _addMenuClickedSubscriberCount != 0;
			}
		}

		/// <summary>
		/// Raises event when add menu button is clicked.
		/// </summary>
		/// <param name="args">Event arguments.</param>
		protected virtual void OnAddMenuClicked(AddMenuClickedEventArgs args) {
			if (_addMenuClicked != null)
				_addMenuClicked(this, args);
		}

		/// <summary>
		/// Occurs after list item is inserted or duplicated.
		/// </summary>
		public event ItemInsertedEventHandler ItemInserted;

		/// <summary>
		/// Raises event after list item is inserted or duplicated.
		/// </summary>
		/// <param name="args">Event arguments.</param>
		protected virtual void OnItemInserted(ItemInsertedEventArgs args) {
			if (ItemInserted != null)
				ItemInserted(this, args);
		}

		/// <summary>
		/// Occurs before list item is removed and allowing for remove operation to be cancelled.
		/// </summary>
		public event ItemRemovingEventHandler ItemRemoving;

		/// <summary>
		/// Raises event before list item is removed and provides oppertunity to cancel.
		/// </summary>
		/// <param name="args">Event arguments.</param>
		protected virtual void OnItemRemoving(ItemRemovingEventArgs args) {
			if (ItemRemoving != null)
				ItemRemoving(this, args);
		}

		/// <summary>
		/// Occurs immediately before list item is moved allowing for move operation to be cancelled.
		/// </summary>
		public event ItemMovingEventHandler ItemMoving;

		/// <summary>
		/// Raises event immediately before list item is moved and provides oppertunity to cancel.
		/// </summary>
		/// <param name="args">Event arguments.</param>
		protected virtual void OnItemMoving(ItemMovingEventArgs args) {
			if (ItemMoving != null)
				ItemMoving(this, args);
		}

		/// <summary>
		/// Occurs after list item has been moved.
		/// </summary>
		public event ItemMovedEventHandler ItemMoved;

		/// <summary>
		/// Raises event after list item has been moved.
		/// </summary>
		/// <param name="args">Event arguments.</param>
		protected virtual void OnItemMoved(ItemMovedEventArgs args) {
			if (ItemMoved != null)
				ItemMoved(this, args);
		}

		#endregion

		#region Construction

		/// <summary>
		/// Initializes a new instance of <see cref="ReorderableListControl"/>.
		/// </summary>
		public ReorderableListControl() {
			_containerStyle = ReorderableListStyles.Container;
			_footerButtonStyle = ReorderableListStyles.FooterButton;
			_itemButtonStyle = ReorderableListStyles.ItemButton;
		}

		/// <summary>
		/// Initializes a new instance of <see cref="ReorderableListControl"/>.
		/// </summary>
		/// <param name="flags">Optional flags which affect behavior of control.</param>
		public ReorderableListControl(ReorderableListFlags flags)
			: this() {
			this.Flags = flags;
		}

		#endregion

		#region Control State

		/// <summary>
		/// Unique Id of control.
		/// </summary>
		private int _controlID;
		/// <summary>
		/// Visible rectangle of control.
		/// </summary>
		private Rect _visibleRect;
		/// <summary>
		/// Width of index label in pixels (zero indicates no label).
		/// </summary>
		private float _indexLabelWidth;
		/// <summary>
		/// Indicates whether item is currently being dragged within control.
		/// </summary>
		private bool _tracking;
		/// <summary>
		/// Indicates if reordering is allowed.
		/// </summary>
		private bool _allowReordering;

		/// <summary>
		/// Prepare initial state for list control.
		/// </summary>
		/// <param name="controlID">Unique ID of list control.</param>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		private void PrepareState(int controlID, IReorderableListAdaptor adaptor) {
			_controlID = controlID;
			_visibleRect = GUIHelper.VisibleRect();

			if ((Flags & ReorderableListFlags.ShowIndices) != 0) {
				int digitCount = Mathf.Max(2, Mathf.CeilToInt(Mathf.Log10((float)adaptor.Count)));
				_indexLabelWidth = digitCount * 8 + 8;
			}
			else {
				_indexLabelWidth = 0;
			}

			_tracking = IsTrackingControl(controlID);

			_allowReordering = (Flags & ReorderableListFlags.DisableReordering) == 0;
		}

		#endregion

		#region Event Handling

		// Keep track of previously known mouse position (in screen space).
//		private static Vector2 s_MousePosition;

		/// <summary>
		/// Indicate that first control of list item should be automatically focused
		/// if possible.
		/// </summary>
		/// <param name="controlID">Unique ID of list control.</param>
		/// <param name="itemIndex">Zero-based index of list item.</param>
		private void AutoFocusItem(int controlID, int itemIndex) {
			if ((Flags & ReorderableListFlags.DisableAutoFocus) == 0) {
				s_AutoFocusControlID = controlID;
				s_AutoFocusIndex = itemIndex;
			}
		}

		/// <summary>
		/// Draw remove button.
		/// </summary>
		/// <param name="position">Position of button.</param>
		/// <param name="visible">Indicates if control is visible within GUI.</param>
		/// <returns>
		/// A value of <c>true</c> if clicked; otherwise <c>false</c>.
		/// </returns>
		private bool DoRemoveButton(Rect position, bool visible) {
			var iconNormal = ReorderableListResources.GetTexture(ReorderableListTexture.Icon_Remove_Normal);
			var iconActive = ReorderableListResources.GetTexture(ReorderableListTexture.Icon_Remove_Active);

			return GUIHelper.IconButton(position, visible, iconNormal, iconActive, ItemButtonStyle);
		}

		private static bool s_TrackingCancelBlockContext;

		/// <summary>
		/// Begin tracking drag and drop within list.
		/// </summary>
		/// <param name="controlID">Unique ID of list control.</param>
		/// <param name="itemIndex">Zero-based index of item which is going to be dragged.</param>
		private static void BeginTrackingReorderDrag(int controlID, int itemIndex) {
			GUIUtility.hotControl = controlID;
			GUIUtility.keyboardControl = 0;
			s_AnchorIndex = itemIndex;
			s_TargetIndex = itemIndex;
			s_TrackingCancelBlockContext = false;
		}

		/// <summary>
		/// Stop tracking drag and drop.
		/// </summary>
		private static void StopTrackingReorderDrag() {
			GUIUtility.hotControl = 0;
			s_AnchorIndex = -1;
			s_TargetIndex = -1;
		}

		/// <summary>
		/// Gets a value indicating whether item in current list is currently being tracked.
		/// </summary>
		/// <param name="controlID">Unique ID of list control.</param>
		/// <returns>
		/// A value of <c>true</c> if item is being tracked; otherwise <c>false</c>.
		/// </returns>
		private static bool IsTrackingControl(int controlID) {
			return !s_TrackingCancelBlockContext && GUIUtility.hotControl == controlID;
		}

		/// <summary>
		/// Accept reordering.
		/// </summary>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		private void AcceptReorderDrag(IReorderableListAdaptor adaptor) {
			try {
				// Reorder list as needed!
				s_TargetIndex = Mathf.Clamp(s_TargetIndex, 0, adaptor.Count + 1);
				if (s_TargetIndex != s_AnchorIndex && s_TargetIndex != s_AnchorIndex + 1)
					MoveItem(adaptor, s_AnchorIndex, s_TargetIndex);
			}
			finally {
				StopTrackingReorderDrag();
			}
		}

		private static Rect s_DragItemPosition;

		// Micro-optimisation to avoid repeated construction.
		private static Rect s_RemoveButtonPosition;
		
		private void DrawListItem(EventType eventType, Rect position, IReorderableListAdaptor adaptor, int itemIndex) {
			bool visible = (position.y < _visibleRect.yMax && position.yMax > _visibleRect.y);
			bool draggable = _allowReordering && adaptor.CanDrag(itemIndex);

			Rect itemContentPosition = position;
			itemContentPosition.x = position.x + 2;
			itemContentPosition.y += 1;
			itemContentPosition.width = position.width - 4;
			itemContentPosition.height = position.height - 4;

			// Make space for grab handle?
			if (draggable) {
				itemContentPosition.x += 20;
				itemContentPosition.width -= 20;
			}

			// Make space for element index.
			if (_indexLabelWidth != 0) {
				itemContentPosition.width -= _indexLabelWidth;

				if (eventType == EventType.Repaint && visible)
					s_RightAlignedLabelStyle.Draw(new Rect(itemContentPosition.x, position.y, _indexLabelWidth, position.height - 4), itemIndex + ":", false, false, false, false);

				itemContentPosition.x += _indexLabelWidth;
			}

			// Make space for remove button?
			if (HasRemoveButtons)
				itemContentPosition.width -= 27;

			try {
				s_CurrentItemIndex.Push(itemIndex);
				EditorGUI.BeginChangeCheck();

				if (eventType == EventType.Repaint && visible) {
					// Draw background of list item.
					var backgroundPosition = new Rect(position.x, position.y, position.width, position.height - 1);
					adaptor.DrawItemBackground(backgroundPosition, itemIndex);

					// Draw grab handle?
					if (draggable) {
						var texturePosition = new Rect(position.x + 6, position.y + position.height / 2f - 3, 9, 5);
						GUIHelper.DrawTexture(texturePosition, ReorderableListResources.GetTexture(ReorderableListTexture.GrabHandle));
					}

					// Draw splitter between list items.
					if (itemIndex != 0 && (!_tracking || itemIndex != s_AnchorIndex)) {
						var texturePosition = new Rect(position.x, position.y - 1, position.width, 1);
						GUIHelper.DrawTexture(texturePosition, ReorderableListResources.texItemSplitter);
					}
				}

				// Allow control to be automatically focused.
				if (s_AutoFocusIndex == itemIndex)
					GUI.SetNextControlName("AutoFocus_" + _controlID + "_" + itemIndex);

				// Present actual control.
				adaptor.DrawItem(itemContentPosition, itemIndex);

				if (EditorGUI.EndChangeCheck())
					ReorderableListGUI.IndexOfChangedItem = itemIndex;

				// Draw remove button?
				if (HasRemoveButtons && adaptor.CanRemove(itemIndex)) {
					s_RemoveButtonPosition = position;
					s_RemoveButtonPosition.width = 27;
					s_RemoveButtonPosition.x = itemContentPosition.xMax + 2;
					s_RemoveButtonPosition.height -= 2;

					if (DoRemoveButton(s_RemoveButtonPosition, visible))
						RemoveItem(adaptor, itemIndex);
				}

				// Check for context click?
				if (eventType == EventType.ContextClick && position.Contains(Event.current.mousePosition) && (Flags & ReorderableListFlags.DisableContextMenu) == 0) {
					ShowContextMenu(_controlID, itemIndex, adaptor);
					Event.current.Use();
				}
			}
			finally {
				s_CurrentItemIndex.Pop();
			}
		}

		private void DrawFloatingListItem(EventType eventType, IReorderableListAdaptor adaptor, float targetSlotPosition) {
			if (eventType == EventType.Repaint) {
				Color restoreColor = GUI.color;

				// Fill background of target area.
				Rect targetPosition = s_DragItemPosition;
				targetPosition.y = targetSlotPosition - 1;
				targetPosition.height = 1;

				GUIHelper.DrawTexture(targetPosition, ReorderableListResources.texItemSplitter);

				--targetPosition.x;
				++targetPosition.y;
				targetPosition.width += 2;
				targetPosition.height = s_DragItemPosition.height - 1;

				GUI.color = TargetBackgroundColor;
				GUIHelper.DrawTexture(targetPosition, EditorGUIUtility.whiteTexture);
				
				// Fill background of item which is being dragged.
				--s_DragItemPosition.x;
				s_DragItemPosition.width += 2;
				--s_DragItemPosition.height;

				GUI.color = AnchorBackgroundColor;
				GUIHelper.DrawTexture(s_DragItemPosition, EditorGUIUtility.whiteTexture);

				++s_DragItemPosition.x;
				s_DragItemPosition.width -= 2;
				++s_DragItemPosition.height;

				// Draw horizontal splitter above and below.
				GUI.color = new Color(0f, 0f, 0f, 0.6f);
				targetPosition.y = s_DragItemPosition.y - 1;
				targetPosition.height = 1;
				GUIHelper.DrawTexture(targetPosition, EditorGUIUtility.whiteTexture);

				targetPosition.y += s_DragItemPosition.height;
				GUIHelper.DrawTexture(targetPosition, EditorGUIUtility.whiteTexture);

				GUI.color = restoreColor;
			}

			DrawListItem(eventType, s_DragItemPosition, adaptor, s_AnchorIndex);
		}

		/// <summary>
		/// Draw list container and items.
		/// </summary>
		/// <param name="position">Position of list control in GUI.</param>
		/// <param name="controlID">Unique ID of list control.</param>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		private void DrawListContainerAndItems(Rect position, int controlID, IReorderableListAdaptor adaptor) {
			// Get local copy of event information for efficiency.
			EventType eventType = Event.current.GetTypeForControl(controlID);
			Vector2 mousePosition = Event.current.mousePosition;
            //if (Event.current.isMouse)
            //    s_MousePosition = GUIUtility.GUIToScreenPoint(mousePosition);

			int newTargetIndex = s_TargetIndex;
			
			// Position of first item in list.
			float firstItemY = position.y + ContainerStyle.padding.top;

			switch (eventType) {
				case EventType.MouseDown:
					if (_tracking) {
						// Cancel drag when other mouse button is pressed.
						s_TrackingCancelBlockContext = true;
						Event.current.Use();
					}
					break;

				case EventType.MouseDrag:
					if (_tracking) {
						// Reset target index and adjust when looping through list items.
						if (mousePosition.y < firstItemY)
							newTargetIndex = 0;
						else if (mousePosition.y >= position.yMax)
							newTargetIndex = adaptor.Count;

						s_DragItemPosition.y = Mathf.Clamp(mousePosition.y + s_AnchorMouseOffset, firstItemY, position.yMax - s_DragItemPosition.height - 1);
					}
					break;

				case EventType.MouseUp:
					if (controlID == GUIUtility.hotControl) {
						// Allow user code to change control over reordering during drag.
						if (!s_TrackingCancelBlockContext && _allowReordering)
							AcceptReorderDrag(adaptor);
						else
							StopTrackingReorderDrag();
						Event.current.Use();
					}
					break;

				case EventType.KeyDown:
					if (_tracking && Event.current.keyCode == KeyCode.Escape) {
						StopTrackingReorderDrag();
						Event.current.Use();
					}
					break;

				case EventType.ExecuteCommand:
					if (s_ContextControlID == controlID) {
						int itemIndex = s_ContextItemIndex;
						try {
							DoCommand(s_ContextCommandName, itemIndex, adaptor);
							Event.current.Use();
						}
						finally {
							s_ContextControlID = 0;
							s_ContextItemIndex = 0;
						}
					}
					break;

				case EventType.Repaint:
					// Draw caption area of list.
					ContainerStyle.Draw(position, GUIContent.none, false, false, false, false);
					break;
			}

			ReorderableListGUI.IndexOfChangedItem = -1;

			// Draw list items!
			Rect itemPosition = new Rect(position.x + 2, firstItemY, position.width - 4, 0);
			float targetSlotPosition = position.yMax - s_DragItemPosition.height - 1;

			float lastMidPoint = 0f;
			float lastHeight = 0f;

			int count = adaptor.Count;
			for (int i = 0; i < count; ++i) {
				itemPosition.y = itemPosition.yMax;
				itemPosition.height = 0;

				if (_tracking) {
					// Does this represent the target index?
					if (i == s_TargetIndex) {
						targetSlotPosition = itemPosition.y;
						itemPosition.y += s_DragItemPosition.height;
					}

					// Do not draw item if it is currently being dragged.
					// Draw later so that it is shown in front of other controls.
					if (i == s_AnchorIndex)
						continue;

					lastMidPoint = itemPosition.y - lastHeight / 2f;
				}

				// Update position for current item.
				itemPosition.height = adaptor.GetItemHeight(i) + 4;
				lastHeight = itemPosition.height;

				if (_tracking && eventType == EventType.MouseDrag) {
					float midpoint = itemPosition.y + itemPosition.height / 2f;

					if (s_TargetIndex < i) {
						if (s_DragItemPosition.yMax > lastMidPoint && s_DragItemPosition.yMax < midpoint)
							newTargetIndex = i;
					}
					else if (s_TargetIndex > i) {
						if (s_DragItemPosition.y > lastMidPoint && s_DragItemPosition.y < midpoint)
							newTargetIndex = i;
					}

					/*if (s_DragItemPosition.y > itemPosition.y && s_DragItemPosition.y <= midpoint)
						newTargetIndex = i;
					else if (s_DragItemPosition.yMax > midpoint && s_DragItemPosition.yMax <= itemPosition.yMax)
						newTargetIndex = i + 1;*/
				}

				// The following may break use of tab key to navigate through controls :/
				if ((Flags & ReorderableListFlags.DisableClipping) == 0) {
					// Clip list item? Performance boost!
					if (itemPosition.yMax < _visibleRect.y - itemPosition.height) {
						// Let's try and trick Unity into maintaining tab key support...
						GUIUtility.GetControlID(FocusType.Keyboard, itemPosition);
						continue;
					}
					if (itemPosition.y > _visibleRect.yMax + itemPosition.height)
						break;
				}

				// Draw list item.
				DrawListItem(eventType, itemPosition, adaptor, i);

				// Did list count change (i.e. item removed)?
				if (adaptor.Count < count) {
					// We assume that it was this item which was removed, so --i allows us
					// to process the next item as usual.
					count = adaptor.Count;
					--i;
					continue;
				}
				
				// Event has already been used, skip to next item.
				if (Event.current.type != EventType.Used) {
					switch (eventType) {
						case EventType.MouseDown:
							if (GUI.enabled && itemPosition.Contains(mousePosition)) {
								// Remove input focus from control before attempting a context click or drag.
								GUIUtility.keyboardControl = 0;

								if (_allowReordering && adaptor.CanDrag(i) && Event.current.button == 0) {
									s_DragItemPosition = itemPosition;

									BeginTrackingReorderDrag(controlID, i);
									s_AnchorMouseOffset = itemPosition.y - mousePosition.y;
									s_TargetIndex = i;

									Event.current.Use();
								}
							}
							break;
/* DEBUG
						case EventType.Repaint:
							GUI.color = Color.red;
							GUI.DrawTexture(new Rect(0, lastMidPoint, 10, 1), EditorGUIUtility.whiteTexture);
							GUI.color = Color.yellow;
							GUI.DrawTexture(new Rect(5, itemPosition.y + itemPosition.height / 2f, 10, 1), EditorGUIUtility.whiteTexture);
							GUI.color = Color.white;
							break;
//*/
					}
				}
			}

			// Item which is being dragged should be shown on top of other controls!
			if (IsTrackingControl(controlID)) {
				lastMidPoint = position.yMax - lastHeight / 2f;

				if (eventType == EventType.MouseDrag) {
					if (s_DragItemPosition.yMax >= lastMidPoint)
						newTargetIndex = count;

					// Force repaint to occur so that dragging rectangle is visible.
					s_TargetIndex = newTargetIndex;
					Event.current.Use();
				}

				DrawFloatingListItem(eventType, adaptor, targetSlotPosition);
/* DEBUG
				if (eventType == EventType.Repaint) {
					GUI.color = Color.blue;
					GUI.DrawTexture(new Rect(100, lastMidPoint, 20, 1), EditorGUIUtility.whiteTexture);
					GUI.color = Color.white;
				}
//*/
			}
			
			// Fake control to catch input focus if auto focus was not possible.
			GUIUtility.GetControlID(FocusType.Keyboard);
		}

		/// <summary>
		/// Checks to see if list control needs to be automatically focused.
		/// </summary>
		/// <param name="controlID">Unique ID of list control.</param>
		private void CheckForAutoFocusControl(int controlID) {
			if (Event.current.type == EventType.Used)
				return;

			// Automatically focus control!
			if (s_AutoFocusControlID == controlID) {
				s_AutoFocusControlID = 0;
				GUIHelper.FocusTextInControl("AutoFocus_" + controlID + "_" + s_AutoFocusIndex);
				s_AutoFocusIndex = -1;
			}
		}
		
		/// <summary>
		/// Draw additional controls below list control and highlight drop target.
		/// </summary>
		/// <param name="position">Position of list control in GUI.</param>
		/// <param name="controlID">Unique ID of list control.</param>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		private void DrawFooterControls(Rect position, int controlID, IReorderableListAdaptor adaptor) {
			if (HasFooterButtons) {
				Rect buttonPosition = new Rect(position.xMax - 30, position.yMax - 1, 30, FooterButtonStyle.fixedHeight);

				Rect menuButtonPosition = buttonPosition;
				var menuIconNormal = ReorderableListResources.GetTexture(ReorderableListTexture.Icon_AddMenu_Normal);
				var menuIconActive = ReorderableListResources.GetTexture(ReorderableListTexture.Icon_AddMenu_Active);

				if (HasAddButton) {
					// Draw add menu drop-down button.
					if (HasAddMenuButton) {
						menuButtonPosition.x = buttonPosition.xMax - 14;
						menuButtonPosition.xMax = buttonPosition.xMax;
						menuIconNormal = ReorderableListResources.GetTexture(ReorderableListTexture.Icon_Menu_Normal);
						menuIconActive = ReorderableListResources.GetTexture(ReorderableListTexture.Icon_Menu_Active);
						buttonPosition.width -= 5;
						buttonPosition.x = menuButtonPosition.x - buttonPosition.width + 1;
					}

					// Draw add item button.
					var iconNormal = ReorderableListResources.GetTexture(ReorderableListTexture.Icon_Add_Normal);
					var iconActive = ReorderableListResources.GetTexture(ReorderableListTexture.Icon_Add_Active);

					if (GUIHelper.IconButton(buttonPosition, true, iconNormal, iconActive, FooterButtonStyle)) {
						// Append item to list.
						GUIUtility.keyboardControl = 0;
						AddItem(adaptor);
					}
				}
				
				if (HasAddMenuButton) {
					// Draw add menu drop-down button.
					if (GUIHelper.IconButton(menuButtonPosition, true, menuIconNormal, menuIconActive, FooterButtonStyle)) {
						GUIUtility.keyboardControl = 0;
						Rect totalAddButtonPosition = buttonPosition;
						totalAddButtonPosition.xMax = position.xMax;
						OnAddMenuClicked(new AddMenuClickedEventArgs(adaptor, totalAddButtonPosition));

						// This will be helpful in many circumstances; including by default!
						GUIUtility.ExitGUI();
					}
				}
			}
		}

		/// <summary>
		/// Cache of container heights mapped by control ID.
		/// </summary>
		private static Dictionary<int, float> s_ContainerHeightCache = new Dictionary<int, float>();

		/// <summary>
		/// Do layout version of list field.
		/// </summary>
		/// <param name="controlID">Unique ID of list control.</param>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <returns>
		/// Position of list container area in GUI (excludes footer area).
		/// </returns>
		private Rect DrawLayoutListField(int controlID, IReorderableListAdaptor adaptor) {
			float totalHeight;

			// Calculate position of list field using layout engine.
			if (Event.current.type == EventType.Layout) {
				totalHeight = CalculateListHeight(adaptor);
				s_ContainerHeightCache[controlID] = totalHeight;
			}
			else {
				totalHeight = s_ContainerHeightCache.ContainsKey(controlID)
					? s_ContainerHeightCache[controlID]
					: 0;
			}

			Rect position = GUILayoutUtility.GetRect(GUIContent.none, ContainerStyle, GUILayout.Height(totalHeight));

			// Make room for footer buttons?
			if (HasFooterButtons)
				position.height -= FooterButtonStyle.fixedHeight;

			// Draw list as normal.
			DrawListContainerAndItems(position, controlID, adaptor);

			CheckForAutoFocusControl(controlID);

			return position;
		}

		/// <summary>
		/// Draw content for empty list (layout version).
		/// </summary>
		/// <param name="drawEmpty">Callback to draw empty content.</param>
		/// <returns>
		/// Position of list container area in GUI (excludes footer area).
		/// </returns>
		private Rect DrawLayoutEmptyList(DrawEmpty drawEmpty) {
			Rect r = EditorGUILayout.BeginVertical(ContainerStyle);
			{
				if (drawEmpty != null)
					drawEmpty();
				else
					GUILayout.Space(5);
			}
			EditorGUILayout.EndVertical();

			// Allow room for footer buttons?
			if (HasFooterButtons)
				GUILayoutUtility.GetRect(0, FooterButtonStyle.fixedHeight - 1);

			return r;
		}

		/// <summary>
		/// Draw content for empty list (layout version).
		/// </summary>
		/// <param name="position">Position of list control in GUI.</param>
		/// <param name="drawEmpty">Callback to draw empty content.</param>
		private void DrawEmptyListControl(Rect position, DrawEmptyAbsolute drawEmpty) {
			if (Event.current.type == EventType.Repaint)
				ContainerStyle.Draw(position, GUIContent.none, false, false, false, false);

			// Take padding into consideration when drawing empty content.
			position.x += ContainerStyle.padding.left;
			position.y += ContainerStyle.padding.top;
			position.width -= ContainerStyle.padding.horizontal;
			position.height -= ContainerStyle.padding.vertical;

			if (drawEmpty != null)
				drawEmpty(position);
		}

		/// <summary>
		/// Correct if for some reason one or more styles are missing!
		/// </summary>
		private void FixStyles() {
			ContainerStyle = ContainerStyle ?? ReorderableListStyles.Container;
			FooterButtonStyle = FooterButtonStyle ?? ReorderableListStyles.FooterButton;
			ItemButtonStyle = ItemButtonStyle ?? ReorderableListStyles.ItemButton;

			if (s_RightAlignedLabelStyle == null) {
				s_RightAlignedLabelStyle = new GUIStyle(GUI.skin.label);
				s_RightAlignedLabelStyle.alignment = TextAnchor.MiddleRight;
				s_RightAlignedLabelStyle.padding.right = 4;
			}
		}

		/// <summary>
		/// Draw layout version of list control.
		/// </summary>
		/// <param name="controlID">Unique ID of list control.</param>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <param name="drawEmpty">Delegate for drawing empty list.</param>
		private void Draw(int controlID, IReorderableListAdaptor adaptor, DrawEmpty drawEmpty) {
			FixStyles();
			PrepareState(controlID, adaptor);

			Rect position;

			if (adaptor.Count > 0)
				position = DrawLayoutListField(controlID, adaptor);
			else
				position = DrawLayoutEmptyList(drawEmpty);

			DrawFooterControls(position, controlID, adaptor);
		}

		/// <inheritdoc cref="Draw(int, IReorderableListAdaptor, DrawEmpty)"/>
		public void Draw(IReorderableListAdaptor adaptor, DrawEmpty drawEmpty) {
			int controlID = GUIUtility.GetControlID(FocusType.Passive);
			Draw(controlID, adaptor, drawEmpty);
		}

		/// <inheritdoc cref="Draw(int, IReorderableListAdaptor, DrawEmpty)"/>
		public void Draw(IReorderableListAdaptor adaptor) {
			int controlID = GUIUtility.GetControlID(FocusType.Passive);
			Draw(controlID, adaptor, null);
		}

		/// <summary>
		/// Draw list control with absolute positioning.
		/// </summary>
		/// <param name="position">Position of list control in GUI.</param>
		/// <param name="controlID">Unique ID of list control.</param>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <param name="drawEmpty">Delegate for drawing empty list.</param>
		private void Draw(Rect position, int controlID, IReorderableListAdaptor adaptor, DrawEmptyAbsolute drawEmpty) {
			FixStyles();
			PrepareState(controlID, adaptor);

			// Allow for footer area.
			if (HasFooterButtons)
				position.height -= FooterButtonStyle.fixedHeight;

			if (adaptor.Count > 0) {
				DrawListContainerAndItems(position, controlID, adaptor);
				CheckForAutoFocusControl(controlID);
			}
			else {
				DrawEmptyListControl(position, drawEmpty);
			}

			DrawFooterControls(position, controlID, adaptor);
		}

		/// <summary>
		/// Draw list control with absolute positioning.
		/// </summary>
		/// <param name="position">Position of list control in GUI.</param>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <param name="drawEmpty">Delegate for drawing empty list.</param>
		public void Draw(Rect position, IReorderableListAdaptor adaptor, DrawEmptyAbsolute drawEmpty) {
			int controlID = GUIUtility.GetControlID(FocusType.Passive);
			Draw(position, controlID, adaptor, drawEmpty);
		}

		/// <inheritdoc cref="Draw(Rect, IReorderableListAdaptor, DrawEmptyAbsolute)"/>
		public void Draw(Rect position, IReorderableListAdaptor adaptor) {
			int controlID = GUIUtility.GetControlID(FocusType.Passive);
			Draw(position, controlID, adaptor, null);
		}

		#endregion

		#region Context Menu

		/// <summary>
		/// Content for "Move to Top" command.
		/// </summary>
		protected static readonly GUIContent CommandMoveToTop = new GUIContent("Move to Top");
		/// <summary>
		/// Content for "Move to Bottom" command.
		/// </summary>
		protected static readonly GUIContent CommandMoveToBottom = new GUIContent("Move to Bottom");
		/// <summary>
		/// Content for "Insert Above" command.
		/// </summary>
		protected static readonly GUIContent CommandInsertAbove = new GUIContent("Insert Above");
		/// <summary>
		/// Content for "Insert Below" command.
		/// </summary>
		protected static readonly GUIContent CommandInsertBelow = new GUIContent("Insert Below");
		/// <summary>
		/// Content for "Duplicate" command.
		/// </summary>
		protected static readonly GUIContent CommandDuplicate = new GUIContent("Duplicate");
		/// <summary>
		/// Content for "Remove" command.
		/// </summary>
		protected static readonly GUIContent CommandRemove = new GUIContent("Remove");
		/// <summary>
		/// Content for "Clear All" command.
		/// </summary>
		protected static readonly GUIContent CommandClearAll = new GUIContent("Clear All");

		// Command control id and item index are assigned when context menu is shown.
		private static int s_ContextControlID;
		private static int s_ContextItemIndex;

		// Command name is assigned by default context menu handler.
		private static string s_ContextCommandName;

		private void ShowContextMenu(int controlID, int itemIndex, IReorderableListAdaptor adaptor) {
			GenericMenu menu = new GenericMenu();

			s_ContextControlID = controlID;
			s_ContextItemIndex = itemIndex;

			AddItemsToMenu(menu, itemIndex, adaptor);

			if (menu.GetItemCount() > 0)
				menu.ShowAsContext();
		}

		/// <summary>
		/// Default functionality to handle context command.
		/// </summary>
		/// <example>
		/// <para>Can be used when adding custom items to the context menu:</para>
		/// <code language="csharp"><![CDATA[
		/// protected override void AddItemsToMenu(GenericMenu menu, int itemIndex, IReorderableListAdaptor adaptor) {
		///     var specialCommand = new GUIContent("Special Command");
		///     menu.AddItem(specialCommand, false, defaultContextHandler, specialCommand);
		/// }
		/// ]]></code>
		/// <code language="unityscript"><![CDATA[
		/// function AddItemsToMenu(menu:GenericMenu, itemIndex:int, list:IReorderableListAdaptor) {
		///     var specialCommand = new GUIContent('Special Command');
		///     menu.AddItem(specialCommand, false, defaultContextHandler, specialCommand);
		/// }
		/// ]]></code>
		/// </example>
		/// <seealso cref="AddItemsToMenu"/>
		protected static readonly GenericMenu.MenuFunction2 DefaultContextHandler = DefaultContextMenuHandler;

		private static void DefaultContextMenuHandler(object userData) {
			var commandContent = userData as GUIContent;
			if (commandContent == null || string.IsNullOrEmpty(commandContent.text))
				return;

			s_ContextCommandName = commandContent.text;

			var e = EditorGUIUtility.CommandEvent("ReorderableListContextCommand");
			EditorWindow.focusedWindow.SendEvent(e);
		}

		/// <summary>
		/// Invoked to generate context menu for list item.
		/// </summary>
		/// <param name="menu">Menu which can be populated.</param>
		/// <param name="itemIndex">Zero-based index of item which was right-clicked.</param>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		protected virtual void AddItemsToMenu(GenericMenu menu, int itemIndex, IReorderableListAdaptor adaptor) {
			if ((Flags & ReorderableListFlags.DisableReordering) == 0) {
				if (itemIndex > 0)
					menu.AddItem(CommandMoveToTop, false, DefaultContextHandler, CommandMoveToTop);
				else
					menu.AddDisabledItem(CommandMoveToTop);

				if (itemIndex + 1 < adaptor.Count)
					menu.AddItem(CommandMoveToBottom, false, DefaultContextHandler, CommandMoveToBottom);
				else
					menu.AddDisabledItem(CommandMoveToBottom);

				if (HasAddButton) {
					menu.AddSeparator("");

					menu.AddItem(CommandInsertAbove, false, DefaultContextHandler, CommandInsertAbove);
					menu.AddItem(CommandInsertBelow, false, DefaultContextHandler, CommandInsertBelow);

					if ((Flags & ReorderableListFlags.DisableDuplicateCommand) == 0)
						menu.AddItem(CommandDuplicate, false, DefaultContextHandler, CommandDuplicate);
				}
			}

			if (HasRemoveButtons) {
				if (menu.GetItemCount() > 0)
					menu.AddSeparator("");

				menu.AddItem(CommandRemove, false, DefaultContextHandler, CommandRemove);
				menu.AddSeparator("");
				menu.AddItem(CommandClearAll, false, DefaultContextHandler, CommandClearAll);
			}
		}

		#endregion

		#region Command Handling

		/// <summary>
		/// Invoked to handle context command.
		/// </summary>
		/// <remarks>
		/// <para>It is important to set the value of <c>GUI.changed</c> to <c>true</c> if any
		/// changes are made by command handler.</para>
		/// <para>Default command handling functionality can be inherited:</para>
		/// <code language="csharp"><![CDATA[
		/// protected override bool HandleCommand(string commandName, int itemIndex, IReorderableListAdaptor adaptor) {
		///     if (base.HandleCommand(itemIndex, adaptor))
		///         return true;
		///     
		///     // Place custom command handling code here...
		///     switch (commandName) {
		///         case "Your Command":
		///             return true;
		///     }
		/// 
		///     return false;
		/// }
		/// ]]></code>
		/// <code language="unityscript"><![CDATA[
		/// function HandleCommand(commandName:String, itemIndex:int, adaptor:IReorderableListAdaptor):boolean {
		///     if (base.HandleCommand(itemIndex, adaptor))
		///         return true;
		///     
		///     // Place custom command handling code here...
		///     switch (commandName) {
		///         case 'Your Command':
		///             return true;
		///     }
		/// 
		///     return false;
		/// }
		/// ]]></code>
		/// </remarks>
		/// <param name="commandName">Name of command. This is the text shown in the context menu.</param>
		/// <param name="itemIndex">Zero-based index of item which was right-clicked.</param>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <returns>
		/// A value of <c>true</c> if command was known; otherwise <c>false</c>.
		/// </returns>
		protected virtual bool HandleCommand(string commandName, int itemIndex, IReorderableListAdaptor adaptor) {
			switch (commandName) {
				case "Move to Top":
					MoveItem(adaptor, itemIndex, 0);
					return true;
				case "Move to Bottom":
					MoveItem(adaptor, itemIndex, adaptor.Count);
					return true;

				case "Insert Above":
					InsertItem(adaptor, itemIndex);
					return true;
				case "Insert Below":
					InsertItem(adaptor, itemIndex + 1);
					return true;
				case "Duplicate":
					DuplicateItem(adaptor, itemIndex);
					return true;

				case "Remove":
					RemoveItem(adaptor, itemIndex);
					return true;
				case "Clear All":
					ClearAll(adaptor);
					return true;

				default:
					return false;
			}
		}

		/// <summary>
		/// Call to manually perform command.
		/// </summary>
		/// <remarks>
		/// <para>Warning message is logged to console if attempted to execute unknown command.</para>
		/// </remarks>
		/// <param name="commandName">Name of command. This is the text shown in the context menu.</param>
		/// <param name="itemIndex">Zero-based index of item which was right-clicked.</param>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <returns>
		/// A value of <c>true</c> if command was known; otherwise <c>false</c>.
		/// </returns>
		public bool DoCommand(string commandName, int itemIndex, IReorderableListAdaptor adaptor) {
			if (!HandleCommand(s_ContextCommandName, itemIndex, adaptor)) {
				Debug.LogWarning("Unknown context command.");
				return false;
			}
			return true;
		}

		/// <summary>
		/// Call to manually perform command.
		/// </summary>
		/// <remarks>
		/// <para>Warning message is logged to console if attempted to execute unknown command.</para>
		/// </remarks>
		/// <param name="command">Content representing command.</param>
		/// <param name="itemIndex">Zero-based index of item which was right-clicked.</param>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <returns>
		/// A value of <c>true</c> if command was known; otherwise <c>false</c>.
		/// </returns>
		public bool DoCommand(GUIContent command, int itemIndex, IReorderableListAdaptor adaptor) {
			return DoCommand(command.text, itemIndex, adaptor);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Calculate height of list control in pixels.
		/// </summary>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <returns>
		/// Required list height in pixels.
		/// </returns>
		public float CalculateListHeight(IReorderableListAdaptor adaptor) {
			FixStyles();

			float totalHeight = ContainerStyle.padding.vertical - 1;

			// Take list items into consideration.
			int count = adaptor.Count;
			for (int i = 0; i < count; ++i)
				totalHeight += adaptor.GetItemHeight(i);
			// Add spacing between list items.
			totalHeight += 4 * count;

			// Add height of footer buttons.
			if (HasFooterButtons)
				totalHeight += FooterButtonStyle.fixedHeight;

			return totalHeight;
		}

		/// <summary>
		/// Calculate height of list control in pixels.
		/// </summary>
		/// <param name="itemCount">Count of items in list.</param>
		/// <param name="itemHeight">Fixed height of list item.</param>
		/// <returns>
		/// Required list height in pixels.
		/// </returns>
		public float CalculateListHeight(int itemCount, float itemHeight) {
			FixStyles();

			float totalHeight = ContainerStyle.padding.vertical - 1;

			// Take list items into consideration.
			totalHeight += (itemHeight + 4) * itemCount;

			// Add height of footer buttons.
			if (HasFooterButtons)
				totalHeight += FooterButtonStyle.fixedHeight;

			return totalHeight;
		}

		/// <summary>
		/// Move item from source index to destination index.
		/// </summary>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <param name="sourceIndex">Zero-based index of source item.</param>
		/// <param name="destIndex">Zero-based index of destination index.</param>
		protected void MoveItem(IReorderableListAdaptor adaptor, int sourceIndex, int destIndex) {
			// Raise event before moving item so that the operation can be cancelled.
			var movingEventArgs = new ItemMovingEventArgs(adaptor, sourceIndex, destIndex);
			OnItemMoving(movingEventArgs);
			if (!movingEventArgs.Cancel) {
				adaptor.Move(sourceIndex, destIndex);

				// Item was actually moved!
				int newIndex = destIndex;
				if (newIndex > sourceIndex)
					--newIndex;
				OnItemMoved(new ItemMovedEventArgs(adaptor, sourceIndex, newIndex));

				GUI.changed = true;
			}
			ReorderableListGUI.IndexOfChangedItem = -1;
		}

		/// <summary>
		/// Add item at end of list and raises the event <see cref="ItemInserted"/>.
		/// </summary>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		protected void AddItem(IReorderableListAdaptor adaptor) {
			adaptor.Add();
			AutoFocusItem(s_ContextControlID, adaptor.Count - 1);

			GUI.changed = true;
			ReorderableListGUI.IndexOfChangedItem = -1;

			var args = new ItemInsertedEventArgs(adaptor, adaptor.Count - 1, false);
			OnItemInserted(args);
		}

		/// <summary>
		/// Insert item at specified index and raises the event <see cref="ItemInserted"/>.
		/// </summary>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <param name="itemIndex">Zero-based index of item.</param>
		protected void InsertItem(IReorderableListAdaptor adaptor, int itemIndex) {
			adaptor.Insert(itemIndex);
			AutoFocusItem(s_ContextControlID, itemIndex);

			GUI.changed = true;
			ReorderableListGUI.IndexOfChangedItem = -1;

			var args = new ItemInsertedEventArgs(adaptor, itemIndex, false);
			OnItemInserted(args);
		}

		/// <summary>
		/// Duplicate specified item and raises the event <see cref="ItemInserted"/>.
		/// </summary>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <param name="itemIndex">Zero-based index of item.</param>
		protected void DuplicateItem(IReorderableListAdaptor adaptor, int itemIndex) {
			adaptor.Duplicate(itemIndex);
			AutoFocusItem(s_ContextControlID, itemIndex + 1);

			GUI.changed = true;
			ReorderableListGUI.IndexOfChangedItem = -1;

			var args = new ItemInsertedEventArgs(adaptor, itemIndex + 1, true);
			OnItemInserted(args);
		}

		/// <summary>
		/// Remove specified item.
		/// </summary>
		/// <remarks>
		/// <para>The event <see cref="ItemRemoving"/> is raised prior to removing item
		/// and allows removal to be cancelled.</para>
		/// </remarks>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <param name="itemIndex">Zero-based index of item.</param>
		/// <returns>
		/// Returns a value of <c>false</c> if operation was cancelled.
		/// </returns>
		protected bool RemoveItem(IReorderableListAdaptor adaptor, int itemIndex) {
			var args = new ItemRemovingEventArgs(adaptor, itemIndex);
			OnItemRemoving(args);
			if (args.Cancel)
				return false;

			adaptor.Remove(itemIndex);

			GUI.changed = true;
			ReorderableListGUI.IndexOfChangedItem = -1;

			return true;
		}

		/// <summary>
		/// Remove all items from list.
		/// </summary>
		/// <remarks>
		/// <para>The event <see cref="ItemRemoving"/> is raised for each item prior to
		/// clearing array and allows entire operation to be cancelled.</para>
		/// </remarks>
		/// <param name="adaptor">Reorderable list adaptor.</param>
		/// <returns>
		/// Returns a value of <c>false</c> if operation was cancelled.
		/// </returns>
		protected bool ClearAll(IReorderableListAdaptor adaptor) {
			if (adaptor.Count == 0)
				return true;

			var args = new ItemRemovingEventArgs(adaptor, 0);
			int count = adaptor.Count;
			for (int i = 0; i < count; ++i) {
				args.ItemIndex = i;
				OnItemRemoving(args);
				if (args.Cancel)
					return false;
			}

			adaptor.Clear();

			GUI.changed = true;
			ReorderableListGUI.IndexOfChangedItem = -1;

			return true;
		}

		#endregion

	}

}
