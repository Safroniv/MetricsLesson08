using MetricsManager.Controllers;
using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Priority;

namespace MetricsManagerTests
{
    public class AgentsControllerTests
    {
        private AgentsController _agentsController;
        private AgentPool _agentPool;

        public AgentsControllerTests()
        {
            _agentPool = LazySingleton.Instance;
            _agentsController = new AgentsController(_agentPool);
        }

        [Theory]
        [Priority(1)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void RegisterAgentTest(int agentId)
        {
            AgentInfo agentInfo = new AgentInfo() { AgentId = agentId, Enable = true };
            IActionResult actionResult = _agentsController.RegisterAgent(agentInfo);
            Assert.IsAssignableFrom<IActionResult>(actionResult);
        }

        [Fact]
        [Priority(2)]
        public void GetAgentsTest()
        {
            ActionResult<AgentInfo[]> actionResult = _agentsController.GetAllAgents();
            //ActionResult<AgentInfo[]> result = Assert.IsAssignableFrom<ActionResult<AgentInfo[]>>(actionResult);
            Assert.NotNull(((OkObjectResult)actionResult.Result).Value);
            Assert.NotEmpty((AgentInfo[])((OkObjectResult)actionResult.Result).Value);
        }

    }
}
