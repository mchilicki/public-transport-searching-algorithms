using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Configurations.ProjectConfiguration
{
    public class PathSettings
    {
        public string GtfsImportFolder { get; set; }
        public string DataSummaryOutputFolder { get; set; }
        public string AlgorithmMeasuresOutputFile { get; set; }

        public string PoznanSearchInputsFile { get; set; }
        public string LahtiSearchInputsFile { get; set; }
        public string StavangerSearchInputsFile { get; set; }

        public string CurrentSearchInputsFile(DatabaseType databaseType)
        {
            return databaseType switch
            {
                DatabaseType.Poznan => PoznanSearchInputsFile,
                DatabaseType.Lahti => LahtiSearchInputsFile,
                DatabaseType.Stavanger => StavangerSearchInputsFile,
                _ => string.Empty,
            };            
        }
    }
}
