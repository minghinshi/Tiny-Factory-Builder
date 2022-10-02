using UnityEngine.UI;

public class ProducerVisual : PlacedVisual
{
    public static ProducerVisual Create()
    {
        return Instantiate(PrefabLoader.producerVisuals, TransformFinder.worldTransform).GetComponent<ProducerVisual>();
    }

    public void Initialize(Producer producer)
    {
        base.Initialize(producer);
        producer.GetTimer().SetSlider(GetComponentInChildren<Slider>());
    }
}