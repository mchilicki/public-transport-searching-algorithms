using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Configurations.ProjectConfiguration
{
    public enum ModuleType
    {
        ImportGtfs = 0,
        ConvertGtfsToGraph = 1,
        GenerateRandomSearchInputs = 2,
        SearchWithDijkstra = 3,
        SearchWithMultipleCriterionDijkstra = 4,
        ExecuteBenchmark = 5,
        CreateDataSummary = 6,
        CreateDataSummaryFromCityCenter = 7,
    }
}
