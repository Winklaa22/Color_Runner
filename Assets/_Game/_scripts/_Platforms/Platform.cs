using UnityEngine;

public class Platform
{
    public GameObject Model { get; private set; }
    public string ID { get; private set; }

    public Platform(GameObject model)
    {
        Model = model;
        ID = System.Guid.NewGuid().ToString();
    }
}
