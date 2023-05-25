using MetricsManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {

        #region Services

        private readonly AgentPool _agentPool;

        #endregion

        #region Constuctors

        public AgentsController(AgentPool agentPool)
        {
            _agentPool = agentPool;
        }

        #endregion

        #region Public Methods

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            if (agentInfo != null)
            {
                _agentPool.Add(agentInfo);
            }
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            if (_agentPool.Agents.ContainsKey(agentId))
                _agentPool.Agents[agentId].Enable = true;
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            if (_agentPool.Agents.ContainsKey(agentId))
                _agentPool.Agents[agentId].Enable = false;
            return Ok();
        }

        // TODO: Домашнее задание [Пункт 1]
        // Добавьте метод в контроллер агентов проекта, относящегося к менеджеру метрик, который
        // позволяет получить список зарегистрированных в системе объектов.
        
        [HttpGet("get")]
        public ActionResult<AgentInfo[]> GetAllAgents()
        {
            return Ok(_agentPool.Get());
        }


        #endregion

    }
}
