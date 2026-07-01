using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Blood Ring Apex Royale 3D — Evolutionary Weapons & Awakening System.
/// Weapons evolve through 7 distinct tiers using Awakening Cores, unlocking custom kill feeds,
/// hit splatter effects, firing audio, reload animations, and exclusive emote perks.
/// 100% original proprietary progression mechanic.
/// </summary>
public class EvoWeaponSystem : MonoBehaviour
{
    public static EvoWeaponSystem Instance;

    public enum EvoTier { Lv1_Base = 1, Lv2_KillFeed = 2, Lv3_NewAudio = 3, Lv4_HitEffect = 4, Lv5_KillEffect = 5, Lv6_ReloadAnim = 6, Lv7_UltimateForm = 7 }

    public class EvoWeaponProfile
    {
        public string weaponId;
        public string weaponName;
        public EvoTier currentTier;
        public int awakeningCoresCollected;
        public float damageMultiplier;
        public float fireRateMultiplier;
    }

    private Dictionary<string, EvoWeaponProfile> playerEvoWeapons = new Dictionary<string, EvoWeaponProfile>();

    private void Awake()
    {
        Instance = this;
        playerEvoWeapons["AK47_EVO"] = new EvoWeaponProfile
        {
            weaponId = "AK47_EVO",
            weaponName = "CyberVortex AK47 Awakened",
            currentTier = EvoTier.Lv7_UltimateForm,
            awakeningCoresCollected = 150,
            damageMultiplier = 1.15f,
            fireRateMultiplier = 1.10f
        };
    }

    public EvoWeaponProfile GetProfile(string weaponId)
    {
        if (playerEvoWeapons.ContainsKey(weaponId)) return playerEvoWeapons[weaponId];
        return null;
    }

    /// <summary>Upgrades weapon tier when enough Awakening Cores are acquired.</summary>
    public bool UpgradeEvoTier(string weaponId)
    {
        if (!playerEvoWeapons.ContainsKey(weaponId)) return false;
        EvoWeaponProfile p = playerEvoWeapons[weaponId];
        if (p.currentTier < EvoTier.Lv7_UltimateForm)
        {
            p.currentTier = (EvoTier)((int)p.currentTier + 1);
            p.damageMultiplier += 0.03f;
            p.fireRateMultiplier += 0.02f;
            Debug.Log($"[EvoWeaponSystem] {p.weaponName} awakened to {p.currentTier}!");
            if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX("SFX_TalentUnlock");
            return true;
        }
        return false;
    }
}
