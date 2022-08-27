using System;
using System.Collections.Generic;

[Serializable]
public class BuildingData
{
    public List<Conveyor> conveyors = new();
    public List<Gatherer> gatherers = new();
    public List<Machine> machines = new();

    public BuildingData(List<Building> buildings)
    {
        buildings.ForEach(x =>
        {
            if (x is Conveyor conveyor) conveyors.Add(conveyor);
            if (x is Gatherer gatherer) gatherers.Add(gatherer);
            if (x is Machine machine) machines.Add(machine);
        });
    }
}
