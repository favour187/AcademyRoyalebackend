using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public enum PickupType { Weapon, Ammo, HealthPack, ArmorPack, PowerUp }

public class PickupItem : NetworkBehaviour
{
    public PickupType pickupType;
    public string weaponName;
    public AmmoType ammoType;
    public int amount;
    public PowerType powerType;

    private void Start()
    {
        GameObject sparkleGo = new GameObject("LootSparkle"); sparkleGo.transform.SetParent(transform, false); sparkleGo.transform.localPosition = Vector3.zero;
        ParticleSystem ps = sparkleGo.AddComponent<ParticleSystem>(); ParticleSystemRenderer pr = sparkleGo.GetComponent<ParticleSystemRenderer>(); pr.material = new Material(ProceduralArt.GetSafeShader("Sprites/Default"));
        var main = ps.main; main.duration = 1f; main.loop = false; main.startColor = Color.white; main.startSize = 0.15f; main.startSpeed = 2f; main.startLifetime = 0.5f;
        var em = ps.emission; em.rateOverTime = 0; em.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0f, 15) });
        var shape = ps.shape; shape.shapeType = ParticleSystemShapeType.Sphere; shape.radius = 0.3f; ps.Play();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * 60f * Time.deltaTime);
        float newY = 1f + Mathf.Sin(Time.time * 3f + GetInstanceID()) * 0.25f;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

public class LootSpawner : MonoBehaviour
{
    public static LootSpawner Instance;

    private void Awake() { Instance = this; }

    public void SpawnInitialLoot()
    {
        if (NetworkManager.Singleton != null && !NetworkManager.Singleton.IsServer) return;
        Debug.Log("Spawning initial polished loot across BloodRing Apex Network...");
        GameObject lootContainer = new GameObject("LootContainer");

        string[] weapons = new string[] { "Pistol", "Rifle", "Shotgun" };
        for (int i = 0; i < 30; i++) { SpawnWeaponPickup(weapons[Random.Range(0, weapons.Length)], GetRandomPosition(), lootContainer.transform); }

        AmmoType[] ammos = new AmmoType[] { AmmoType.PistolAmmo, AmmoType.RifleAmmo, AmmoType.ShotgunAmmo };
        for (int i = 0; i < 50; i++) { AmmoType aType = ammos[Random.Range(0, ammos.Length)]; int amt = (aType == AmmoType.PistolAmmo) ? 24 : ((aType == AmmoType.RifleAmmo) ? 60 : 12); SpawnAmmoPickup(aType, amt, GetRandomPosition(), lootContainer.transform); }

        for (int i = 0; i < 20; i++) { SpawnConsumablePickup(PickupType.HealthPack, 50, GetRandomPosition(), lootContainer.transform); }
        for (int i = 0; i < 15; i++) { SpawnConsumablePickup(PickupType.ArmorPack, 50, GetRandomPosition(), lootContainer.transform); }

        PowerType[] powers = (PowerType[])System.Enum.GetValues(typeof(PowerType));
        for (int i = 0; i < 15; i++) { SpawnPowerUpPickup(powers[Random.Range(0, powers.Length)], GetRandomPosition(), lootContainer.transform); }
    }

    public GameObject SpawnWeaponPickup(string wName, Vector3 pos, Transform parent = null)
    {
        GameObject go = BloodRing.Art.BloodRingArtLibrary.GetPrimitive3D("Cube.obj"); go.name = "Pickup_Weapon_" + wName; go.transform.position = pos + new Vector3(0, 1f, 0); go.transform.localScale = new Vector3(1.2f, 0.4f, 0.4f); if (parent != null) go.transform.SetParent(parent);
        Collider col = go.GetComponent<Collider>(); col.isTrigger = true;
        NetworkObject netObj = go.AddComponent<NetworkObject>(); PickupItem p = go.AddComponent<PickupItem>(); p.pickupType = PickupType.Weapon; p.weaponName = wName;
        Color c = wName == "Pistol" ? Color.yellow : (wName == "Rifle" ? Color.cyan : Color.red); go.GetComponent<Renderer>().material.color = c;
        AddMinimapBlip(go, Color.yellow); if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer) netObj.Spawn(true); return go;
    }

    public GameObject SpawnAmmoPickup(AmmoType aType, int amount, Vector3 pos, Transform parent = null)
    {
        GameObject go = BloodRing.Art.BloodRingArtLibrary.GetPrimitive3D("Cube.obj"); go.name = "Pickup_Ammo_" + aType; go.transform.position = pos + new Vector3(0, 1f, 0); go.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f); if (parent != null) go.transform.SetParent(parent);
        Collider col = go.GetComponent<Collider>(); col.isTrigger = true;
        NetworkObject netObj = go.AddComponent<NetworkObject>(); PickupItem p = go.AddComponent<PickupItem>(); p.pickupType = PickupType.Ammo; p.ammoType = aType; p.amount = amount;
        Color c = aType == AmmoType.PistolAmmo ? new Color(0.8f, 0.8f, 0f) : (aType == AmmoType.RifleAmmo ? new Color(0f, 0.8f, 0.8f) : new Color(0.8f, 0f, 0f)); go.GetComponent<Renderer>().material.color = c;
        AddMinimapBlip(go, Color.yellow); if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer) netObj.Spawn(true); return go;
    }

    public GameObject SpawnConsumablePickup(PickupType type, int amount, Vector3 pos, Transform parent = null)
    {
        GameObject go = BloodRing.Art.BloodRingArtLibrary.GetPrimitive3D("Capsule.obj"); go.name = "Pickup_" + type; go.transform.position = pos + new Vector3(0, 1f, 0); go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); if (parent != null) go.transform.SetParent(parent);
        Collider col = go.GetComponent<Collider>(); col.isTrigger = true;
        NetworkObject netObj = go.AddComponent<NetworkObject>(); PickupItem p = go.AddComponent<PickupItem>(); p.pickupType = type; p.amount = amount; go.GetComponent<Renderer>().material.color = (type == PickupType.HealthPack) ? Color.green : Color.blue;
        AddMinimapBlip(go, Color.yellow); if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer) netObj.Spawn(true); return go;
    }

    public GameObject SpawnPowerUpPickup(PowerType pType, Vector3 pos, Transform parent = null)
    {
        GameObject go = BloodRing.Art.BloodRingArtLibrary.GetPrimitive3D("Sphere.obj"); go.name = "Pickup_Power_" + pType; go.transform.position = pos + new Vector3(0, 1f, 0); go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f); if (parent != null) go.transform.SetParent(parent);
        Collider col = go.GetComponent<Collider>(); col.isTrigger = true;
        NetworkObject netObj = go.AddComponent<NetworkObject>(); PickupItem p = go.AddComponent<PickupItem>(); p.pickupType = PickupType.PowerUp; p.powerType = pType;

        Color c = Color.magenta; switch (pType) { case PowerType.SpeedBoost: c = Color.yellow; break; case PowerType.ShieldBurst: c = Color.blue; break; case PowerType.RageMode: c = Color.red; break; case PowerType.Invisibility: c = Color.gray; break; case PowerType.HealSurge: c = Color.green; break; case PowerType.DoubleJump: c = Color.cyan; break; case PowerType.Magnet: c = Color.magenta; break; }
        Material mat = new Material(ProceduralArt.GetSafeShader("Standard")); mat.color = c; mat.EnableKeyword("_EMISSION"); mat.SetColor("_EmissionColor", c * 1.5f); go.GetComponent<Renderer>().material = mat;

        GameObject lightGo = new GameObject("OrbLight"); lightGo.transform.SetParent(go.transform, false); Light light = lightGo.AddComponent<Light>(); light.type = LightType.Point; light.color = c; light.range = 5f; light.intensity = 1.5f;
        AddMinimapBlip(go, Color.yellow); if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer) netObj.Spawn(true); return go;
    }

    private void AddMinimapBlip(GameObject target, Color col) { MinimapBlip b = target.AddComponent<MinimapBlip>(); b.blipColor = col; }
    private Vector3 GetRandomPosition() { float x = Random.Range(-220f, 220f); float z = Random.Range(-220f, 220f); return new Vector3(x, 0, z); }
}


