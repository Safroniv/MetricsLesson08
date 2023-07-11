using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManagerTests
{
    public class LazySingleton
    {
        private static readonly Lazy<AgentPool> _instance =  new Lazy<AgentPool>(() => new AgentPool());

        public static AgentPool Instance
        {
            get { return _instance.Value; }
        }

    }

}
