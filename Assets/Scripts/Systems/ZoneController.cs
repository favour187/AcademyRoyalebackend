using UnityEngine;

public class ZoneController : MonoBehaviour
{
    public static ZoneController Instance;

    public float currentRadius = 250f;
    public float elapsedTime = 0f;

    private GameObject zoneWall;
    private Material zoneMat;

    private int currentPhase = 0; // 0: Start wait, 1: Phase 1 shrink, 2: Wait 2, 3: Phase 2 shrink, 4: Wait 3, 5: Phase 3 shrink, 6: Final
    private string phaseText = "ZONE STABLE";
    private float phaseTimer = 120f;
    private float uvOffset = 0f;

    private void Awake() { Instance = this; }

    public void InitializeZone()
    {
        Debug.Log("Initializing Animated Shrinking Zone...");
        zoneWall = BloodRing.Art.BloodRingArtLibrary.GetPrimitive3D("Cylinder.obj"); zoneWall.name = "ZoneCylinderWall"; zoneWall.transform.position = new Vector3(0, 50f, 0); zoneWall.transform.localScale = new Vector3(currentRadius * 2f, 50f, currentRadius * 2f);
        Destroy(zoneWall.GetComponent<Collider>());

        zoneMat = new Material(ProceduralArt.GetSafeShader("Standard")); zoneMat.color = new Color(0.1f, 0.2f, 1f, 0.35f); zoneMat.SetFloat("_Mode", 3); zoneMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha); zoneMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha); zoneMat.SetInt("_ZWrite", 0); zoneMat.DisableKeyword("_ALPHATEST_ON"); zoneMat.EnableKeyword("_ALPHABLEND_ON"); zoneMat.DisableKeyword("_ALPHAPREMULTIPLY_ON"); zoneMat.renderQueue = 3000;
        zoneMat.mainTextureScale = new Vector2(20, 5);
        zoneWall.GetComponent<Renderer>().material = zoneMat; zoneWall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off; zoneWall.GetComponent<Renderer>().receiveShadows = false;

        currentPhase = 0; phaseTimer = 120f; phaseText = "SHRINK IN: " + Mathf.CeilToInt(phaseTimer) + "s";
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        uvOffset += Time.deltaTime * 0.5f;
        if (zoneMat != null) zoneMat.mainTextureOffset = new Vector2(0, uvOffset);

        if (currentPhase == 0) { phaseTimer = 120f - elapsedTime; phaseText = "SHRINK IN: " + Mathf.CeilToInt(phaseTimer) + "s"; if (elapsedTime >= 120f) { currentPhase = 1; } }
        else if (currentPhase == 1) { float t = (elapsedTime - 120f) / 90f; currentRadius = Mathf.Lerp(250f, 150f, t); phaseText = "SHRINKING: " + Mathf.CeilToInt(210f - elapsedTime) + "s"; if (elapsedTime >= 210f) { currentPhase = 2; } }
        else if (currentPhase == 2) { phaseTimer = 240f - elapsedTime; phaseText = "PHASE 2 IN: " + Mathf.CeilToInt(phaseTimer) + "s"; if (elapsedTime >= 240f) { currentPhase = 3; } }
        else if (currentPhase == 3) { float t = (elapsedTime - 240f) / 60f; currentRadius = Mathf.Lerp(150f, 75f, t); phaseText = "SHRINKING: " + Mathf.CeilToInt(300f - elapsedTime) + "s"; if (elapsedTime >= 300f) { currentPhase = 4; } }
        else if (currentPhase == 4) { phaseTimer = 330f - elapsedTime; phaseText = "PHASE 3 IN: " + Mathf.CeilToInt(phaseTimer) + "s"; if (elapsedTime >= 330f) { currentPhase = 5; } }
        else if (currentPhase == 5) { float t = (elapsedTime - 330f) / 30f; currentRadius = Mathf.Lerp(75f, 25f, t); phaseText = "SHRINKING: " + Mathf.CeilToInt(360f - elapsedTime) + "s"; if (elapsedTime >= 360f) { currentPhase = 6; } }
        else if (currentPhase == 6) { currentRadius = 25f; phaseText = "FINAL ZONE"; }

        if (zoneWall != null) { zoneWall.transform.localScale = new Vector3(currentRadius * 2f, 50f, currentRadius * 2f); }
    }

    public bool IsOutsideZone(Vector3 pos) { Vector2 p = new Vector2(pos.x, pos.z); return p.magnitude > currentRadius; }
    public float GetShrinkProgress() { return 1f - (currentRadius / 250f); }
    public string GetZoneStatusText() { return phaseText; }
}


