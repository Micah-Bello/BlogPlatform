using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Enums
{
    public enum ReadyStatus
    {
        Incomplete,
        [Description("Production Ready")]
        ProductionReady,
        [Description("Preview Ready")]
        PreviewReady
    }
}
