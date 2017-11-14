using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace VRWeapons.InteractionSystems.VRTK
{

    [RequireComponent(typeof(VRTK_SnapDropZone))]
        
    public class MagPouch : MonoBehaviour
    {
        Collider magCollider;
        VRTK_SnapDropZone dropZone;

        [Tooltip("If checked, this will disable the magazine's collider when it is inserted. Solves issues with grabbing magazines instead of the gun, in cases like pistols."), SerializeField]
        bool disableColliderOnMagIn;

        private void Start()
        {
            dropZone = GetComponent<VRTK_SnapDropZone>();
            dropZone.ObjectSnappedToDropZone += new SnapDropZoneEventHandler(ObjectSnapped);
            dropZone.ObjectUnsnappedFromDropZone += new SnapDropZoneEventHandler(ObjectUnsnapped);
        }

        void ObjectSnapped(object sender, SnapDropZoneEventArgs e)
        {
            magCollider = e.snappedObject.GetComponent<Collider>();
              magCollider.isTrigger = true;
        }

        void ObjectUnsnapped(object sender, SnapDropZoneEventArgs e)
        {
            IMagazine mag = e.snappedObject.GetComponent<IMagazine>();
            //Stop listening for mag drop event so we won't redundantly unsnap
            magCollider.isTrigger = false;
            magCollider = null;

            //This is necessary for the initial mag so it won't revert to child of weapon
            var interactable = e.snappedObject.GetComponent<VRTK_InteractableObject>();
            if (interactable != null)
            {
                interactable.SaveCurrentState();
            }

        }

    }
}