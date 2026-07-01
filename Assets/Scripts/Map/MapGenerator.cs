using UnityEngine;
using Unity.AI.Navigation;
using Unity.Netcode;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;

    public void GenerateMap(string mapChoice = "IslaVerde")
    {
        Debug.Log("Generating Polished BloodRing Apex Map with Real 3D Models: " + mapChoice);
        ProceduralArt.SetupSkybox();

        GameObject terrain = BloodRing.Art.BloodRingArtLibrary.GetPrimitive3D("Plane.obj"); terrain.name = "Terrain"; terrain.transform.position = Vector3.zero; terrain.transform.localScale = new Vector3(50f, 1f, 50f);
        
        Material groundMat; Color skyHorizon = new Color(0.85f, 0.35f, 0.15f);
        if (mapChoice == "RedSands") { groundMat = ProceduralArt.GetMaterial("Mat_RedSands", ProceduralArt.GenerateRockTexture()); groundMat.color = new Color(0.7f, 0.6f, 0.3f); skyHorizon = new Color(0.9f, 0.6f, 0.2f); }
        else if (mapChoice == "IronGorge") { groundMat = ProceduralArt.GetMaterial("Mat_IronGorge", ProceduralArt.GenerateHeavyArmorTexture()); groundMat.color = new Color(0.2f, 0.25f, 0.2f); skyHorizon = new Color(0.3f, 0.2f, 0.4f); }
        else { groundMat = ProceduralArt.GetMaterial("Mat_IslaVerde", ProceduralArt.GenerateGroundTexture()); }

        groundMat.mainTextureScale = new Vector2(50, 50); terrain.GetComponent<Renderer>().material = groundMat; terrain.isStatic = true; terrain.layer = LayerMask.NameToLayer("Default");
        RenderSettings.ambientSkyColor = skyHorizon;

        GameObject water = BloodRing.Art.BloodRingArtLibrary.GetPrimitive3D("Plane.obj"); water.name = "WaterPlane"; water.transform.position = new Vector3(0, -0.2f, 0); water.transform.localScale = new Vector3(80f, 1f, 80f);
        Material waterMat = new Material(ProceduralArt.GetSafeShader("Standard")); waterMat.color = new Color(0.1f, 0.3f, 0.8f, 0.8f); waterMat.SetFloat("_Mode", 3); waterMat.SetInt("_ZWrite", 0); waterMat.renderQueue = 3000; water.GetComponent<Renderer>().material = waterMat; Destroy(water.GetComponent<Collider>());
        BoxCollider wCol = water.AddComponent<BoxCollider>(); wCol.isTrigger = true; wCol.size = new Vector3(10f, 1f, 10f);

        navMeshSurface = terrain.AddComponent<NavMeshSurface>(); navMeshSurface.collectObjects = CollectObjects.All;
        GameObject envContainer = new GameObject("Environment");
        Material buildingMat = ProceduralArt.GetMaterial("Mat_Building", ProceduralArt.GenerateBuildingTexture()); buildingMat.mainTextureScale = new Vector2(2, 2);

        for (int i = 0; i < 80; i++) { Vector3 pos = GetRandomMapPosition(); GameObject tree = ProceduralArt.CreateTreeMesh(); tree.name = "Tree_" + i; tree.transform.position = pos; tree.transform.SetParent(envContainer.transform); tree.isStatic = true; }
        for (int i = 0; i < 40; i++) { Vector3 pos = GetRandomMapPosition(); GameObject rock = ProceduralArt.CreateRockMesh(); rock.name = "Rock_" + i; rock.transform.position = pos; rock.transform.SetParent(envContainer.transform); rock.isStatic = true; GameObject ledge = new GameObject("Ledge"); ledge.transform.SetParent(rock.transform); ledge.transform.localPosition = new Vector3(0, 1.5f, 0); ledge.AddComponent<LedgeClimb>(); }
        
        GameObject realBuildingPrefab = Resources.Load<GameObject>("Models/Building");
        for (int i = 0; i < 15; i++)
        {
            Vector3 pos = GetRandomMapPosition(); if (Vector3.Distance(pos, Vector3.zero) < 20f) pos += new Vector3(25f, 0, 25f);
            GameObject building = new GameObject("Building_" + i); building.transform.position = pos; building.transform.SetParent(envContainer.transform); building.isStatic = true;
            
            if (realBuildingPrefab != null)
            {
                GameObject realB = Object.Instantiate(realBuildingPrefab, building.transform); realB.name = "RealBuildingMesh"; realB.transform.localPosition = Vector3.zero; realB.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                foreach (Renderer r in realB.GetComponentsInChildren<Renderer>()) { r.material = buildingMat; }
                GameObject ledge = new GameObject("Ledge"); ledge.transform.SetParent(building.transform); ledge.transform.localPosition = new Vector3(0, 6.2f, 0); ledge.AddComponent<LedgeClimb>();
            }
            else
            {
                float w = 10f; float h = 6f; float l = 10f; CreateWall(building.transform, new Vector3(0, h/2, l/2), new Vector3(w, h, 0.5f), buildingMat); CreateWall(building.transform, new Vector3(-w/2, h/2, 0), new Vector3(0.5f, h, l), buildingMat); CreateWall(building.transform, new Vector3(w/2, h/2, 0), new Vector3(0.5f, h, l), buildingMat); CreateWall(building.transform, new Vector3(-w/4 - 0.5f, h/2, -l/2), new Vector3(w/2 - 1f, h, 0.5f), buildingMat); CreateWall(building.transform, new Vector3(w/4 + 0.5f, h/2, -l/2), new Vector3(w/2 - 1f, h, 0.5f), buildingMat); CreateWall(building.transform, new Vector3(0, h, 0), new Vector3(w + 0.5f, 0.5f, l + 0.5f), buildingMat); GameObject ledge = new GameObject("Ledge"); ledge.transform.SetParent(building.transform); ledge.transform.localPosition = new Vector3(0, h + 0.2f, 0); ledge.AddComponent<LedgeClimb>();
            }
        }

        for (int i = 0; i < 5; i++) { Vector3 start = GetRandomMapPosition() + new Vector3(0, 10f, 0); Vector3 end = start + new Vector3(Random.Range(30, 60), -8f, Random.Range(30, 60)); GameObject zipGo = new GameObject("Zipline_" + i); zipGo.transform.position = start; zipGo.transform.SetParent(envContainer.transform); Zipline z = zipGo.AddComponent<Zipline>(); z.endPos = end; }

        if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer)
        {
            VehicleType[] vTypes = new VehicleType[] { VehicleType.Car, VehicleType.Truck, VehicleType.Motorbike };
            for (int i = 0; i < 8; i++) { Vector3 pos = GetRandomMapPosition(); GameObject vGo = new GameObject("Vehicle_" + i); vGo.transform.position = pos + new Vector3(0, 0.5f, 0); vGo.transform.SetParent(envContainer.transform); NetworkObject netObj = vGo.AddComponent<NetworkObject>(); Vehicle v = vGo.AddComponent<Vehicle>(); v.vType = vTypes[Random.Range(0, vTypes.Length)]; netObj.Spawn(true); }
        }

        Material boundMat = new Material(ProceduralArt.GetSafeShader("Standard")); boundMat.color = new Color(0.8f, 0.1f, 0.1f, 0.5f); boundMat.SetFloat("_Mode", 3); boundMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha); boundMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha); boundMat.SetInt("_ZWrite", 0); boundMat.DisableKeyword("_ALPHATEST_ON"); boundMat.EnableKeyword("_ALPHABLEND_ON"); boundMat.DisableKeyword("_ALPHAPREMULTIPLY_ON"); boundMat.renderQueue = 3000;
        float mapSize = 500f; float boundH = 50f; float boundThick = 2f; CreateBoundaryWall(new Vector3(0, boundH/2, mapSize/2), new Vector3(mapSize, boundH, boundThick), boundMat, envContainer.transform); CreateBoundaryWall(new Vector3(0, boundH/2, -mapSize/2), new Vector3(mapSize, boundH, boundThick), boundMat, envContainer.transform); CreateBoundaryWall(new Vector3(mapSize/2, boundH/2, 0), new Vector3(boundThick, boundH, mapSize), boundMat, envContainer.transform); CreateBoundaryWall(new Vector3(-mapSize/2, boundH/2, 0), new Vector3(boundThick, boundH, mapSize), boundMat, envContainer.transform);

        Debug.Log("Baking NavMesh at runtime..."); navMeshSurface.BuildNavMesh(); Debug.Log("NavMesh baked successfully!");
    }

    private Vector3 GetRandomMapPosition() { float x = Random.Range(-220f, 220f); float z = Random.Range(-220f, 220f); return new Vector3(x, 0, z); }
    private void CreateWall(Transform parent, Vector3 localPos, Vector3 scale, Material mat) { GameObject wall = BloodRing.Art.BloodRingArtLibrary.GetPrimitive3D("Cube.obj"); wall.transform.SetParent(parent); wall.transform.localPosition = localPos; wall.transform.localScale = scale; wall.GetComponent<Renderer>().material = mat; wall.isStatic = true; wall.layer = LayerMask.NameToLayer("Default"); }
    private void CreateBoundaryWall(Vector3 pos, Vector3 scale, Material mat, Transform parent) { GameObject wall = BloodRing.Art.BloodRingArtLibrary.GetPrimitive3D("Cube.obj"); wall.name = "BoundaryWall"; wall.transform.position = pos; wall.transform.localScale = scale; wall.transform.SetParent(parent); wall.GetComponent<Renderer>().material = mat; wall.isStatic = true; }
}


