using UnityEngine;

public class EnvironmentSetter : MonoBehaviour
{
    [SerializeField] private Renderer _groundRenderer;
    [SerializeField] private Transform _container;

    public void Set(LevelConfig levelConfig)
    {
        _groundRenderer.material = levelConfig.EnvironmentMaterial;
        GameObject building = Instantiate(levelConfig.Building, _container);
        Renderer buildingRenderer = building.GetComponent<Renderer>();

        if (buildingRenderer != null)
        {
            buildingRenderer.material = levelConfig.EnvironmentMaterial;
        }
    }
}
