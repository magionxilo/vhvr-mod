using System.Reflection;
using HarmonyLib;
using UnityEngine;
using ValheimVRMod.Scripts;
using ValheimVRMod.Utilities;

namespace ValheimVRMod.Patches {
    
    /**
     * we check blocking by measuring if shield is forwarding to the attack hit with a Vector3.Dot of > 0.5 
     */
    [HarmonyPatch(typeof(Humanoid), "BlockAttack")]
    class PatchBlockAttack {
        
        static void Prefix(Humanoid __instance, ref HitData hit) {

            if (__instance != Player.m_localPlayer || EquipScript.getLeft() != EquipType.Shield) {
                return;
            }
   
            if (ShieldManager.isBlocking()) {
                hit.m_dir = -__instance.transform.forward;   
            } else {
                hit.m_dir = __instance.transform.forward;
            }
        }
        
        static void Postfix(Humanoid __instance, bool __result) {

            if (__instance != Player.m_localPlayer || EquipScript.getLeft() != EquipType.Shield) {
                return;
            }

            if (__result) {
                StaticObjects.leftCooldown().startCooldown();   
            }
        }
    }
    
    [HarmonyPatch(typeof(Humanoid), "IsBlocking")]
    class PatchIsBlocking {
        static bool Prefix(Humanoid __instance, ref bool __result) {

            if (__instance != Player.m_localPlayer) {
                return true;
            }

            __result = ShieldManager.isBlocking();
            
            return false;
        }
    }
    
    [HarmonyPatch(typeof(Character), "RPC_Damage")]
    class PatchRPCDamager {
        static void Prefix(Character __instance, HitData hit) {

            if (__instance != Player.m_localPlayer) {
                return;
            }
            
            ShieldManager.setBlocking(hit.m_dir);

        }
        
        static void Postfix(Character __instance) {

            if (__instance != Player.m_localPlayer) {
                return;
            }

            ShieldManager.resetBlocking();

        }
    }
    
}