using UnityEngine;
using System.Collections.Generic;

public enum AmmoType { PistolAmmo, RifleAmmo, ShotgunAmmo, SniperAmmo, SMGAmmo }

[CreateAssetMenu(fileName = "NewWeapon", menuName = "BloodRing/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float damage;
    public int maxAmmo;
    public bool isAutomatic;
    public float fireRate;
    public float spread;
    public float reloadTime;
    public int pellets = 1;
    public AmmoType ammoType;
    public Color iconColor = Color.white;

    // Attachments & Mastery
    public bool hasScope = false;
    public bool hasSilencer = false;
    public bool hasExtMag = false;
    public bool hasGrip = false;
    public bool hasStock = false;
    public int masteryLevel = 1;
    public string activeSkin = "Default";

    public static WeaponData GetDefaultWeapon(string name)
    {
        WeaponData w = ScriptableObject.CreateInstance<WeaponData>(); w.weaponName = name; w.masteryLevel = 1; w.activeSkin = "Default";
        
        // 50+ Weapons Catalog
        switch (name)
        {
            // SMGs
            case "MP40": w.damage = 22f; w.maxAmmo = 20; w.isAutomatic = true; w.fireRate = 0.07f; w.spread = 0.04f; w.reloadTime = 1.5f; w.ammoType = AmmoType.SMGAmmo; w.iconColor = Color.cyan; break;
            case "Vector": w.damage = 19f; w.maxAmmo = 25; w.isAutomatic = true; w.fireRate = 0.06f; w.spread = 0.05f; w.reloadTime = 1.4f; w.ammoType = AmmoType.SMGAmmo; w.iconColor = Color.cyan; break;
            case "P90": w.damage = 21f; w.maxAmmo = 50; w.isAutomatic = true; w.fireRate = 0.08f; w.spread = 0.04f; w.reloadTime = 2.2f; w.ammoType = AmmoType.SMGAmmo; w.iconColor = Color.cyan; break;
            case "UMP": w.damage = 24f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.1f; w.spread = 0.035f; w.reloadTime = 1.8f; w.ammoType = AmmoType.SMGAmmo; w.iconColor = Color.cyan; break;
            case "Mac10": w.damage = 20f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.075f; w.spread = 0.045f; w.reloadTime = 1.5f; w.ammoType = AmmoType.SMGAmmo; w.iconColor = Color.cyan; break;
            case "Thompson": w.damage = 25f; w.maxAmmo = 42; w.isAutomatic = true; w.fireRate = 0.09f; w.spread = 0.04f; w.reloadTime = 2.0f; w.ammoType = AmmoType.SMGAmmo; w.iconColor = Color.cyan; break;
            case "Bizon": w.damage = 23f; w.maxAmmo = 60; w.isAutomatic = true; w.fireRate = 0.095f; w.spread = 0.042f; w.reloadTime = 2.5f; w.ammoType = AmmoType.SMGAmmo; w.iconColor = Color.cyan; break;
            case "MP5": w.damage = 23f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.085f; w.spread = 0.038f; w.reloadTime = 1.7f; w.ammoType = AmmoType.SMGAmmo; w.iconColor = Color.cyan; break;
            case "CG15": w.damage = 26f; w.maxAmmo = 20; w.isAutomatic = true; w.fireRate = 0.12f; w.spread = 0.025f; w.reloadTime = 1.9f; w.ammoType = AmmoType.SMGAmmo; w.iconColor = Color.cyan; break;
            case "MP9": w.damage = 21f; w.maxAmmo = 25; w.isAutomatic = true; w.fireRate = 0.072f; w.spread = 0.041f; w.reloadTime = 1.45f; w.ammoType = AmmoType.SMGAmmo; w.iconColor = Color.cyan; break;

            // Assault Rifles
            case "AK47": w.damage = 32f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.12f; w.spread = 0.055f; w.reloadTime = 2.3f; w.ammoType = AmmoType.RifleAmmo; w.iconColor = Color.yellow; break;
            case "SCAR": w.damage = 28f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.11f; w.spread = 0.04f; w.reloadTime = 2.0f; w.ammoType = AmmoType.RifleAmmo; w.iconColor = Color.yellow; break;
            case "Groza": w.damage = 34f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.115f; w.spread = 0.038f; w.reloadTime = 2.1f; w.ammoType = AmmoType.RifleAmmo; w.iconColor = Color.yellow; break;
            case "FAMAS": w.damage = 26f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.09f; w.spread = 0.035f; w.reloadTime = 1.9f; w.ammoType = AmmoType.RifleAmmo; w.iconColor = Color.yellow; break;
            case "M4A1": w.damage = 27f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.105f; w.spread = 0.038f; w.reloadTime = 1.9f; w.ammoType = AmmoType.RifleAmmo; w.iconColor = Color.yellow; break;
            case "XM8": w.damage = 29f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.11f; w.spread = 0.036f; w.reloadTime = 2.0f; w.ammoType = AmmoType.RifleAmmo; w.iconColor = Color.yellow; break;
            case "AN94": w.damage = 31f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.125f; w.spread = 0.048f; w.reloadTime = 2.2f; w.ammoType = AmmoType.RifleAmmo; w.iconColor = Color.yellow; break;
            case "AUG": w.damage = 28f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.11f; w.spread = 0.037f; w.reloadTime = 2.1f; w.ammoType = AmmoType.RifleAmmo; w.iconColor = Color.yellow; break;
            case "Parafal": w.damage = 33f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.13f; w.spread = 0.05f; w.reloadTime = 2.4f; w.ammoType = AmmoType.RifleAmmo; w.iconColor = Color.yellow; break;
            case "Kingfisher": w.damage = 27f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.095f; w.spread = 0.035f; w.reloadTime = 1.8f; w.ammoType = AmmoType.RifleAmmo; w.iconColor = Color.yellow; break;
            case "G36": w.damage = 28f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.105f; w.spread = 0.039f; w.reloadTime = 2.0f; w.ammoType = AmmoType.RifleAmmo; w.iconColor = Color.yellow; break;
            case "FAL": w.damage = 35f; w.maxAmmo = 20; w.isAutomatic = false; w.fireRate = 0.2f; w.spread = 0.025f; w.reloadTime = 2.5f; w.ammoType = AmmoType.RifleAmmo; w.iconColor = Color.yellow; break;

            // Shotguns
            case "M1887": w.damage = 18f; w.maxAmmo = 2; w.isAutomatic = false; w.fireRate = 0.6f; w.spread = 0.12f; w.reloadTime = 1.8f; w.pellets = 10; w.ammoType = AmmoType.ShotgunAmmo; w.iconColor = Color.red; break;
            case "M1014": w.damage = 14f; w.maxAmmo = 6; w.isAutomatic = false; w.fireRate = 0.4f; w.spread = 0.15f; w.reloadTime = 2.6f; w.pellets = 8; w.ammoType = AmmoType.ShotgunAmmo; w.iconColor = Color.red; break;
            case "SPAS12": w.damage = 16f; w.maxAmmo = 5; w.isAutomatic = false; w.fireRate = 0.5f; w.spread = 0.13f; w.reloadTime = 2.4f; w.pellets = 8; w.ammoType = AmmoType.ShotgunAmmo; w.iconColor = Color.red; break;
            case "MAG7": w.damage = 13f; w.maxAmmo = 8; w.isAutomatic = false; w.fireRate = 0.3f; w.spread = 0.14f; w.reloadTime = 2.2f; w.pellets = 7; w.ammoType = AmmoType.ShotgunAmmo; w.iconColor = Color.red; break;
            case "ChargeBuster": w.damage = 20f; w.maxAmmo = 3; w.isAutomatic = false; w.fireRate = 0.7f; w.spread = 0.1f; w.reloadTime = 2.0f; w.pellets = 8; w.ammoType = AmmoType.ShotgunAmmo; w.iconColor = Color.red; break;
            case "Trogon": w.damage = 15f; w.maxAmmo = 12; w.isAutomatic = false; w.fireRate = 0.35f; w.spread = 0.15f; w.reloadTime = 2.8f; w.pellets = 8; w.ammoType = AmmoType.ShotgunAmmo; w.iconColor = Color.red; break;
            case "Striker12": w.damage = 12f; w.maxAmmo = 12; w.isAutomatic = false; w.fireRate = 0.3f; w.spread = 0.16f; w.reloadTime = 3.0f; w.pellets = 8; w.ammoType = AmmoType.ShotgunAmmo; w.iconColor = Color.red; break;

            // Sniper Rifles
            case "AWM": w.damage = 90f; w.maxAmmo = 5; w.isAutomatic = false; w.fireRate = 1.5f; w.spread = 0.005f; w.reloadTime = 3.5f; w.ammoType = AmmoType.SniperAmmo; w.iconColor = Color.magenta; w.hasScope = true; break;
            case "Kar98k": w.damage = 75f; w.maxAmmo = 5; w.isAutomatic = false; w.fireRate = 1.2f; w.spread = 0.008f; w.reloadTime = 3.0f; w.ammoType = AmmoType.SniperAmmo; w.iconColor = Color.magenta; w.hasScope = true; break;
            case "M82B": w.damage = 85f; w.maxAmmo = 5; w.isAutomatic = false; w.fireRate = 1.4f; w.spread = 0.006f; w.reloadTime = 3.2f; w.ammoType = AmmoType.SniperAmmo; w.iconColor = Color.magenta; w.hasScope = true; break;
            case "M24": w.damage = 80f; w.maxAmmo = 5; w.isAutomatic = false; w.fireRate = 1.3f; w.spread = 0.007f; w.reloadTime = 3.1f; w.ammoType = AmmoType.SniperAmmo; w.iconColor = Color.magenta; w.hasScope = true; break;
            case "SVD": w.damage = 55f; w.maxAmmo = 10; w.isAutomatic = false; w.fireRate = 0.5f; w.spread = 0.015f; w.reloadTime = 2.5f; w.ammoType = AmmoType.SniperAmmo; w.iconColor = Color.magenta; w.hasScope = true; break;
            case "Woodpecker": w.damage = 60f; w.maxAmmo = 12; w.isAutomatic = false; w.fireRate = 0.45f; w.spread = 0.012f; w.reloadTime = 2.3f; w.ammoType = AmmoType.SniperAmmo; w.iconColor = Color.magenta; w.hasScope = true; break;
            case "AC80": w.damage = 65f; w.maxAmmo = 10; w.isAutomatic = false; w.fireRate = 0.5f; w.spread = 0.014f; w.reloadTime = 2.4f; w.ammoType = AmmoType.SniperAmmo; w.iconColor = Color.magenta; w.hasScope = true; break;
            case "M14": w.damage = 48f; w.maxAmmo = 15; w.isAutomatic = false; w.fireRate = 0.35f; w.spread = 0.018f; w.reloadTime = 2.2f; w.ammoType = AmmoType.SniperAmmo; w.iconColor = Color.magenta; break;

            // Pistols / Sidearms
            case "DesertEagle": w.damage = 45f; w.maxAmmo = 7; w.isAutomatic = false; w.fireRate = 0.5f; w.spread = 0.015f; w.reloadTime = 1.6f; w.ammoType = AmmoType.PistolAmmo; w.iconColor = Color.white; break;
            case "G18": w.damage = 18f; w.maxAmmo = 15; w.isAutomatic = true; w.fireRate = 0.1f; w.spread = 0.035f; w.reloadTime = 1.3f; w.ammoType = AmmoType.PistolAmmo; w.iconColor = Color.white; break;
            case "M500": w.damage = 50f; w.maxAmmo = 5; w.isAutomatic = false; w.fireRate = 0.6f; w.spread = 0.012f; w.reloadTime = 1.8f; w.ammoType = AmmoType.PistolAmmo; w.iconColor = Color.white; w.hasScope = true; break;
            case "USP": w.damage = 22f; w.maxAmmo = 12; w.isAutomatic = false; w.fireRate = 0.25f; w.spread = 0.025f; w.reloadTime = 1.2f; w.ammoType = AmmoType.PistolAmmo; w.iconColor = Color.white; break;
            case "MiniUzi": w.damage = 16f; w.maxAmmo = 20; w.isAutomatic = true; w.fireRate = 0.065f; w.spread = 0.045f; w.reloadTime = 1.4f; w.ammoType = AmmoType.PistolAmmo; w.iconColor = Color.white; break;
            case "TreatmentGun": w.damage = 15f; w.maxAmmo = 12; w.isAutomatic = false; w.fireRate = 0.3f; w.spread = 0.02f; w.reloadTime = 2.0f; w.ammoType = AmmoType.PistolAmmo; w.iconColor = Color.green; break;

            // Melee / Throwables / Specials
            case "Katana": w.damage = 60f; w.maxAmmo = 1; w.isAutomatic = false; w.fireRate = 0.7f; w.spread = 0f; w.reloadTime = 0f; w.ammoType = AmmoType.PistolAmmo; w.iconColor = Color.gray; break;
            case "Pan": w.damage = 55f; w.maxAmmo = 1; w.isAutomatic = false; w.fireRate = 0.8f; w.spread = 0f; w.reloadTime = 0f; w.ammoType = AmmoType.PistolAmmo; w.iconColor = Color.gray; break;
            case "Machete": w.damage = 58f; w.maxAmmo = 1; w.isAutomatic = false; w.fireRate = 0.75f; w.spread = 0f; w.reloadTime = 0f; w.ammoType = AmmoType.PistolAmmo; w.iconColor = Color.gray; break;
            case "Bat": w.damage = 52f; w.maxAmmo = 1; w.isAutomatic = false; w.fireRate = 0.7f; w.spread = 0f; w.reloadTime = 0f; w.ammoType = AmmoType.PistolAmmo; w.iconColor = Color.gray; break;
            case "Crossbow": w.damage = 70f; w.maxAmmo = 1; w.isAutomatic = false; w.fireRate = 1.5f; w.spread = 0.01f; w.reloadTime = 2.5f; w.ammoType = AmmoType.SniperAmmo; w.iconColor = Color.magenta; break;
            case "M79": w.damage = 150f; w.maxAmmo = 1; w.isAutomatic = false; w.fireRate = 2.0f; w.spread = 0.05f; w.reloadTime = 3.5f; w.ammoType = AmmoType.ShotgunAmmo; w.iconColor = Color.red; break;
            case "Gatling": w.damage = 25f; w.maxAmmo = 120; w.isAutomatic = true; w.fireRate = 0.05f; w.spread = 0.05f; w.reloadTime = 4.5f; w.ammoType = AmmoType.RifleAmmo; w.iconColor = Color.yellow; break;

            default: w.damage = 25f; w.maxAmmo = 30; w.isAutomatic = true; w.fireRate = 0.1f; w.spread = 0.05f; w.reloadTime = 2.0f; w.ammoType = AmmoType.RifleAmmo; w.iconColor = Color.yellow; break;
        }
        return w;
    }

    public static List<string> GetAllWeaponNames()
    {
        return new List<string> { "MP40", "Vector", "P90", "UMP", "Mac10", "Thompson", "Bizon", "MP5", "CG15", "MP9", "AK47", "SCAR", "Groza", "FAMAS", "M4A1", "XM8", "AN94", "AUG", "Parafal", "Kingfisher", "G36", "FAL", "M1887", "M1014", "SPAS12", "MAG7", "ChargeBuster", "Trogon", "Striker12", "AWM", "Kar98k", "M82B", "M24", "SVD", "Woodpecker", "AC80", "M14", "DesertEagle", "G18", "M500", "USP", "MiniUzi", "TreatmentGun", "Katana", "Pan", "Machete", "Bat", "Crossbow", "M79", "Gatling" };
    }
}


