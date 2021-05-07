using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using ValheimVRMod.Scripts;
using ValheimVRMod.Utilities;
using ValheimVRMod.VRCore;
using Valve.VR;

namespace ValheimVRMod.Patches
{
    [HarmonyPatch(typeof(VisEquipment), "SetRightHandEquiped")]
    class PatchSetRightHandEquiped
    {
        static void Postfix(bool __result, string ___m_rightItem, ref GameObject ___m_rightItemInstance)
        {

            if (!__result || ___m_rightItemInstance == null) {
                return;
            }

            MeshFilter meshFilter = ___m_rightItemInstance.GetComponentInChildren<MeshFilter>();

            if (meshFilter == null)
            {
                return;
            }
            
            Player player = ___m_rightItemInstance.GetComponentInParent<Player>();
            // only local player must trigger this
            if (player == null || Player.m_localPlayer != player)
            {
                return;
            }

            Transform item = meshFilter.transform;
            VRPlayer.collisionCube().GetComponent<CollisionDetection>().setColliderParent(item, ___m_rightItem);
        }
    }
    
    [HarmonyPatch(typeof(VisEquipment), "SetLeftHandEquiped")]
    class PatchSetLeftHandEquiped
    {
        static void Postfix(bool __result, ref GameObject ___m_leftItemInstance)
        {

            if (!__result || ___m_leftItemInstance == null) {
                return;
            }

            MeshFilter meshFilter = ___m_leftItemInstance.GetComponentInChildren<MeshFilter>();

            if (meshFilter == null)
            {
                return;
            }
            
            Player player = ___m_leftItemInstance.GetComponentInParent<Player>();
            // only local player must trigger this
            if (player == null || Player.m_localPlayer != player)
            {
                return;
            }
            
            Debug.Log("LEFT ITEM TYPE: " + Player.m_localPlayer.GetLeftItem().m_shared.m_itemType);
            
            if (VRPlayer.isUsingBow())
            {
                meshFilter.gameObject.AddComponent<BowManager>();
            }
        }
    }
    
    [HarmonyPatch(typeof(Attack), "Start")]
    class PatchAttackStart
    {
        
        private static MethodInfo getStaminaUsageMethod = AccessTools.Method(typeof(Attack), "GetStaminaUsage");
        private static MethodInfo getLevelDamageFactorMethod =
            AccessTools.Method(typeof(Attack), "GetLevelDamageFactor");
        private static MethodInfo spawnOnHitTerrainMethod = AccessTools.Method(typeof(Attack), "SpawnOnHitTerrain",
            new Type[] {typeof(Vector3), typeof(GameObject)});

        /**
         * in Start Patch we put some logic from original Start method and some more logic from original DoMeleeAttack
         */
        static bool Prefix(
            Humanoid character,
            CharacterAnimEvent animEvent,
            ItemDrop.ItemData weapon,
            ref Humanoid ___m_character,
            ref CharacterAnimEvent ___m_animEvent,
            ref ItemDrop.ItemData ___m_weapon,
            ref Attack __instance,
            ref EffectList ___m_hitEffect,
            ref Skills.SkillType ___m_specialHitSkill,
            ref DestructibleType ___m_specialHitType,
            bool ___m_lowerDamagePerHit,
            float ___m_forceMultiplier,
            float ___m_staggerMultiplier,
            float ___m_damageMultiplier,
            int ___m_attackChainLevels,
            int ___m_currentAttackCainLevel,
            ref DestructibleType ___m_resetChainIfHit,
            ref int ___m_nextAttackChainLevel,
            ref EffectList ___m_hitTerrainEffect,
            float ___m_attackHitNoise,
            GameObject ___m_spawnOnTrigger
        ) {
            // if character is not local player, use original Start method
            if (character != Player.m_localPlayer 
                || character.GetLeftItem().m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow) {
                return true;
            }

            ___m_character = character;
            ___m_animEvent = animEvent;
            ___m_weapon = weapon;
            
            float staminaUsage = (float)getStaminaUsageMethod.Invoke(__instance, null);
            if (staminaUsage > 0.0f && !character.HaveStamina(staminaUsage + 0.1f))
            {
                if (character.IsPlayer())
                    Hud.instance.StaminaBarNoStaminaFlash();
                return false;
            }

            VRPlayer.rightHand.hapticAction.Execute(0, 0.2f, 100, 0.3f, SteamVR_Input_Sources.RightHand);
            character.UseStamina(staminaUsage);

            CollisionDetection colliderDetection = VRPlayer.collisionCube().GetComponent<CollisionDetection>();
            if (colliderDetection.itemIsTool)
            {
                return false;
            }
            
            Collider col = colliderDetection.lastHitCollider;
            Vector3 pos = colliderDetection.lastHitPoint;

            Debug.Log("WeaponType: " + weapon.GetType());
            LogUtils.LogParentComponents(col.transform);
            
            
            // all rest is copied stuff from original DoMeleeAttack:
            Vector3 zero = Vector3.zero;
            bool flag2 = false; //rename
            HashSet<Skills.SkillType> skillTypeSet = new HashSet<Skills.SkillType>();
            bool hitOccured = false;
            
            ___m_weapon.m_shared.m_hitEffect.Create(pos, Quaternion.identity);
            ___m_hitEffect.Create(pos, Quaternion.identity);

            GameObject hitObject = Projectile.FindHitObject(col);

            if (!(hitObject == ___m_character.gameObject))
            {
                Vagon component1 = hitObject.GetComponent<Vagon>();
                if (!(bool) (UnityEngine.Object) component1 || !component1.IsAttached(___m_character))
                {
                    Character component2 = hitObject.GetComponent<Character>();
                    if (!(component2 != null) ||
                        (___m_character.IsPlayer() || BaseAI.IsEnemy(___m_character, component2)) &&
                        (!___m_weapon.m_shared.m_dodgeable || !component2.IsDodgeInvincible()))
                    {
                        hitOccured = true;
                    }
                }
            }

            if (!hitOccured)
            {
                return false;
            }

            IDestructible component = hitObject.GetComponent<IDestructible>();

            if (component != null)
            {
                DestructibleType destructibleType = component.GetDestructibleType();
                Skills.SkillType skill = ___m_weapon.m_shared.m_skillType;

                if (___m_specialHitSkill != Skills.SkillType.None &&
                    (destructibleType & ___m_specialHitType) != DestructibleType.None)
                {
                    skill = ___m_specialHitSkill;
                }

                float randomSkillFactor = ___m_character.GetRandomSkillFactor(skill);

                if (___m_lowerDamagePerHit)
                {
                    randomSkillFactor /= 0.75f;
                }

                HitData hitData = new HitData();
                hitData.m_toolTier = ___m_weapon.m_shared.m_toolTier;
                hitData.m_statusEffect = (bool) (UnityEngine.Object) ___m_weapon.m_shared.m_attackStatusEffect
                    ? ___m_weapon.m_shared.m_attackStatusEffect.name
                    : "";
                hitData.m_pushForce = ___m_weapon.m_shared.m_attackForce * randomSkillFactor * ___m_forceMultiplier;
                hitData.m_backstabBonus = ___m_weapon.m_shared.m_backstabBonus;
                hitData.m_staggerMultiplier = ___m_staggerMultiplier;
                hitData.m_dodgeable = ___m_weapon.m_shared.m_dodgeable;
                hitData.m_blockable = ___m_weapon.m_shared.m_blockable;
                hitData.m_skill = skill;
                hitData.m_damage = ___m_weapon.GetDamage();
                hitData.m_point = pos;
                hitData.m_dir = pos.normalized;
                hitData.m_hitCollider = col;
                hitData.SetAttacker(___m_character);
                hitData.m_damage.Modify(___m_damageMultiplier);
                hitData.m_damage.Modify(randomSkillFactor);
                hitData.m_damage.Modify((float) getLevelDamageFactorMethod.Invoke(__instance, null));
                if (___m_attackChainLevels > 1 && ___m_currentAttackCainLevel == ___m_attackChainLevels - 1)
                {
                    hitData.m_damage.Modify(2f);
                    hitData.m_pushForce *= 1.2f;
                }

                ___m_character.GetSEMan().ModifyAttack(skill, ref hitData);
                if (component is Character)
                    flag2 = true;
                component.Damage(hitData);
                if ((destructibleType & ___m_resetChainIfHit) != DestructibleType.None)
                    ___m_nextAttackChainLevel = 0;
                skillTypeSet.Add(skill);
            }

            ___m_weapon.m_shared.m_hitTerrainEffect.Create(pos,
                Quaternion.identity); // Quaternion.identity might need to be replaced
            ___m_hitTerrainEffect.Create(pos, Quaternion.identity);

            if ((bool) (UnityEngine.Object) ___m_weapon.m_shared.m_spawnOnHitTerrain)
            {
                spawnOnHitTerrainMethod.Invoke(__instance,
                    new object[] {pos, ___m_weapon.m_shared.m_spawnOnHitTerrain});
            }

            if (___m_weapon.m_shared.m_useDurability && ___m_character.IsPlayer())
                ___m_weapon.m_durability -= ___m_weapon.m_shared.m_useDurabilityDrain;
            ___m_character.AddNoise(___m_attackHitNoise);
            
            if ((bool) (UnityEngine.Object) ___m_weapon.m_shared.m_spawnOnHit)
                UnityEngine.Object.Instantiate(___m_weapon.m_shared.m_spawnOnHit, pos,
                        Quaternion.identity).GetComponent<IProjectile>()
                    ?.Setup(___m_character, zero, ___m_attackHitNoise, null, ___m_weapon);
            foreach (Skills.SkillType skill in skillTypeSet)
                ___m_character.RaiseSkill(skill, flag2 ? 1.5f : 1f);

            if (!(bool) (UnityEngine.Object) ___m_spawnOnTrigger)
                return false;
            UnityEngine.Object.Instantiate(___m_spawnOnTrigger, zero,
                Quaternion.identity).GetComponent<IProjectile>()?.Setup(___m_character,
                ___m_character.transform.forward, -1f, null, ___m_weapon);

            return false;

        }
    }
}