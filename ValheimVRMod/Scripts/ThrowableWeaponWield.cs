using UnityEngine;
using ValheimVRMod.Utilities;
using ValheimVRMod.VRCore;
using Valve.VR;

namespace ValheimVRMod.Scripts
{
    class ThrowableWeaponWield : WeaponWield
    {
        private SpearManager spearManager;
        void Awake()
        {
            MeshFilter meshFilter = gameObject.GetComponentInChildren<MeshFilter>();
            if (EquipScript.isSpearEquippedRadialForward())
            {
                meshFilter.gameObject.transform.localRotation *= Quaternion.AngleAxis(180, Vector3.right);
                switch (Player.m_localPlayer.m_rightItem.m_shared.m_name)
                {
                    case "$item_spear_chitin":
                        meshFilter.gameObject.transform.localPosition = new Vector3(0, 0, -0.2f);
                        break;
                    case "item_spear_ancientbark":
                    case "item_spear_bronze":
                    case "item_spear_carapace":
                        meshFilter.gameObject.transform.localPosition = new Vector3(0, 0, -1.15f);
                        break;
                }
            }

            // TODO: consider renaming this ThrowableManager
            spearManager = meshFilter.gameObject.AddComponent<SpearManager>();
            spearManager.weaponWield = this;
        }

        protected override Quaternion GetSingleHandedWeaponPointingDir(Quaternion originalRotation)
        {
            // TODO: consider use this instead of the rotating the mesh filter for inversed spear wield:
            // return EquipScript.isSpearEquippedUlnarForward() ? originalRotation : originalRotation * Quaternion.euler(180, 0, 0);
            return base.GetSingleHandedWeaponPointingDir(originalRotation);
        }

        protected override bool TemporaryDisableTwoHandedWield()
        {
            return EquipScript.isSpearEquipped() && (SpearManager.IsAiming() || SpearManager.isThrowing);
        }

        protected override Vector3 GetSingleHandedWeaponForward()
        {
            Vector3 roughDirection = EquipScript.isSpearEquippedRadialForward() ? transform.forward : -transform.forward;
            return Vector3.Project(roughDirection, base.GetSingleHandedWeaponForward()).normalized;
        }
    }
}
