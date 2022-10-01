public class ProducerVisual : PlacedBuildingVisual
{
    public static ProducerVisual Create(Building building)
    {
        ProducerVisual visual = CreateGameObject().AddComponent<ProducerVisual>();
        visual.Initialize(building);
        return visual;
    }
}