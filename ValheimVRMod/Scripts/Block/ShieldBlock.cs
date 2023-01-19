using UnityEngine;
using ValheimVRMod.Utilities;
using ValheimVRMod.VRCore;
using Valve.VR;

namespace ValheimVRMod.Scripts.Block {
    public class ShieldBlock : Block {

        public string itemName;
        private const float MIN_PARRY_ENTRY_SPEED = 1.5f;
        private const float MIN_PARRY_SWING_DIST = 0.5f;
        private const float MAX_PARRY_ANGLE = 150f;
        private const float PARRY_EXIT_SPEED = 0.2f;

        private float scaling = 1f;
        private Vector3 posRef;
        private Vector3 scaleRef;
        private bool attemptingParry;

        public static ShieldBlock instance;

        private void OnDisable() {
            instance = null;
        }
        
        protected override void Awake() {
            base.Awake();
            _meshCooldown = gameObject.AddComponent<MeshCooldown>();
            instance = this;
            InitShield();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            // Parry time restriction is made more lenient for realistic blocking to balance difficulty.
            blockTimer += (VHVRConfig.BlockingType() == "Realistic" ? Time.fixedDeltaTime * 0.5f : Time.fixedDeltaTime);
        }

        private void InitShield()
        {
            posRef = _meshCooldown.transform.localPosition;
            scaleRef = _meshCooldown.transform.localScale;
            hand = VHVRConfig.LeftHanded() ? VRPlayer.rightHand.transform : VRPlayer.leftHand.transform;
            offhand = VHVRConfig.LeftHanded() ? VRPlayer.leftHand.transform : VRPlayer.rightHand.transform;
        }

        public override void setBlocking(HitData hitData) {
            if (VHVRConfig.BlockingType() == "GrabButton")
            {
                _blocking = SteamVR_Actions.valheim_Grab.GetState(VRPlayer.nonDominantHandInputSource);
            }
            else if (VHVRConfig.BlockingType() == "Realistic")
            {
                _blocking = Vector3.Dot(hitData.m_dir, getForward()) > 0.3f && hitIntersectsBlockBox(hitData);
                ParryCheck();
            }
            else {
                _blocking = Vector3.Dot(hitData.m_dir, getForward()) > 0.5f;
                ParryCheck();
            }
        }

        private Vector3 getForward() {
            switch (itemName)
            {
                case "ShieldWood":
                case "ShieldBanded":
                    return StaticObjects.shieldObj().transform.forward;
                case "ShieldKnight":
                    return -StaticObjects.shieldObj().transform.right;
                case "ShieldBronzeBuckler":
                case "ShieldIronBuckler":
                    return VHVRConfig.LeftHanded() ? StaticObjects.shieldObj().transform.up : -StaticObjects.shieldObj().transform.up;
            }
            return -StaticObjects.shieldObj().transform.forward;
        }

        protected override void ParryCheck() {
            PhysicsEstimator handPhysicsEstimator = VHVRConfig.LeftHanded() ? VRPlayer.rightHandPhysicsEstimator : VRPlayer.leftHandPhysicsEstimator;
            float l = handPhysicsEstimator.GetLongestLocomotion(/* deltaT= */ 0.4f).magnitude;
            Vector3 shieldFacing = VHVRConfig.LeftHanded() ? VRPlayer.rightHand.transform.right : -VRPlayer.leftHand.transform.right;
            if (physicsEstimator.GetVelocity().magnitude > MIN_PARRY_ENTRY_SPEED && Vector3.Angle(physicsEstimator.GetVelocity(), shieldFacing) < MAX_PARRY_ANGLE) {
                if (!attemptingParry)
                {
                    blockTimer = blockTimerParry;
                    if (VHVRConfig.BlockingType() == "Realistic")
                    {
                        // Parry time restriction is made more lenient for realistic blocking to balance difficulty.
                        blockTimer *= 0.5f;
                    }
                    attemptingParry = true;
                }
            }
            else if (attemptingParry && physicsEstimator.GetAverageVelocityInSnapshots().magnitude < PARRY_EXIT_SPEED)
            {
                blockTimer = blockTimerNonParry;
                attemptingParry = false;
            }
        }

        protected override void OnRenderObject() {
            base.OnRenderObject();
            if (scaling != 1f)
            {
                transform.localScale = scaleRef * scaling;
                transform.localPosition = CalculatePos();
            }
            else if (transform.localPosition != posRef || transform.localScale != scaleRef)
            {
                transform.localScale = scaleRef;
                transform.localPosition = posRef;
            }
            StaticObjects.shieldObj().transform.position = transform.position;
            StaticObjects.shieldObj().transform.rotation = transform.rotation;

            Vector3 v = physicsEstimator.GetVelocity();
        }

        public void ScaleShieldSize(float scale)
        {
            scaling = scale;
        }

        private Vector3 CalculatePos()
        {
            return VRPlayer.leftHand.transform.InverseTransformDirection(hand.TransformDirection(posRef) *(scaleRef * scaling).x);
        }
    }
}
