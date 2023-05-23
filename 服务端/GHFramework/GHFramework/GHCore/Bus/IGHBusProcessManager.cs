using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHCore.Bus
{
    public interface IGHBusProcessManager
    {
        IGHBusProcess GetProcess(string key);
        bool AndFilterProcess(IGHBusProcess process);
        bool OrFilterProcess(IGHBusProcess process);

    }
}
