using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace VRWeapons.InteractionSystems.VRTK
{

	[RequireComponent(typeof(VRTK_SnapDropZone))]
        
	public class WeaponHolster : MonoBehaviour
	{
		VRWeapons.Weapon weapon;
		VRTK_SnapDropZone dropZone;

		[Tooltip("If checked, this will enable the second hand grip col to be disabled."), SerializeField]
		bool disableSecondHandCol;

		private void Start()
		{
			dropZone = GetComponent<VRTK_SnapDropZone>();
			dropZone.ObjectSnappedToDropZone += new SnapDropZoneEventHandler(ObjectSnapped);
			dropZone.ObjectUnsnappedFromDropZone += new SnapDropZoneEventHandler(ObjectUnsnapped);
		}

		void ObjectSnapped(object sender, SnapDropZoneEventArgs e)
		{
			weapon = e.snappedObject.GetComponent<VRWeapons.Weapon>();
			weapon.weaponBodyCollider.isTrigger = true;
			if (disableSecondHandCol)
			{
				weapon.secondHandGripCollider.isTrigger = true;
			}
		}

		void ObjectUnsnapped(object sender, SnapDropZoneEventArgs e)
		{
			IMagazine mag = e.snappedObject.GetComponent<IMagazine>();
			//Stop listening for mag drop event so we won't redundantly unsnap
			weapon.weaponBodyCollider.isTrigger = false;
			if (disableSecondHandCol)
			{
				weapon.secondHandGripCollider.isTrigger = false;
			}

			//This is necessary for the initial mag so it won't revert to child of weapon
			var interactable = e.snappedObject.GetComponent<VRTK_InteractableObject>();
			if (interactable != null)
			{
				interactable.SaveCurrentState();
			}

		}

	}
}