using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ValheimVRMod.Utilities;
using ValheimVRMod.VRCore;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace ValheimVRMod.Scripts
{

    // Manager of poles that can be swung to launch a projectile from its tip.
    public class SwingLaunchManager : MonoBehaviour
    {
        private const float MIN_THROW_SPEED = 3f;
        private const float FULL_THROW_SPEED = 5f;

        public static float attackDrawPercentage { get; private set; }
        public static Vector3 spawnPoint { get; private set; }
        public static Vector3 aimDir { get; private set; }
        public static bool isThrowing;
        private static bool preparingThrow;

        protected SteamVR_Action_Boolean dominantHandInputAction { get { return VHVRConfig.LeftHanded() ? SteamVR_Actions.valheim_UseLeft : SteamVR_Actions.valheim_Use; } }
        private WeaponWield weaponWield { get { return gameObject.GetComponentInParent<WeaponWield>(); } }
        private float peakSpeed = 0;

        protected virtual void OnRenderObject()
        {
            if (dominantHandInputAction.GetStateDown(VRPlayer.dominantHandInputSource))
            {
                preparingThrow = true;
                peakSpeed = 0;
            }

            if (dominantHandInputAction.state)
            {
                UpdateThrowDirAndSpeed();
            }
            
            MaybeReleaseProjectile();

            if (!dominantHandInputAction.state)
            {
                preparingThrow = false;
            }
        }

        private void UpdateThrowDirAndSpeed()
        {
            spawnPoint = GetProjectileSpawnPoint();

            Vector3 v;
            if (WeaponWield.isCurrentlyTwoHanded() && weaponWield != null)
            {
                v = weaponWield.physicsEstimator.GetVelocityOfPoint(spawnPoint);
                aimDir = v.normalized;
            }
            else
            {
                PhysicsEstimator physicsEstimator = (VHVRConfig.LeftHanded() ? VRPlayer.leftHandPhysicsEstimator : VRPlayer.rightHandPhysicsEstimator);
                v = physicsEstimator.GetVelocityOfPoint(spawnPoint);
                // When wielding single-handed, use the hand movement direction instead of spawn point movement direction for more intuitive control.
                aimDir = physicsEstimator.GetAverageVelocityInSnapshots().normalized;
            }

            float currentSpeed = v.magnitude;
            if (currentSpeed > peakSpeed)
            {
                peakSpeed = currentSpeed;
            }

            attackDrawPercentage = currentSpeed / FULL_THROW_SPEED;
        }

        private void MaybeReleaseProjectile() {
            if (!preparingThrow || isThrowing || peakSpeed < MIN_THROW_SPEED)
            {
                return;
            }

            if (ReleaseTriggerToAttack() && !dominantHandInputAction.GetStateUp(VRPlayer.dominantHandInputSource))
            {
                return;
            }
            
            isThrowing = true;
            preparingThrow = false;
        }

        protected virtual Vector3 GetProjectileSpawnPoint()
        {
            // TODO: Consider moving MagicWeaponManager.GetProjectileSpawnPoint() to WeaponUtils since its logic is not specific to magic weapons.
            return MagicWeaponManager.GetProjectileSpawnPoint(Player.m_localPlayer.GetRightItem().m_shared.m_attack);
        }

        protected virtual bool ReleaseTriggerToAttack()
        {
            return true;
        }
    }
}
