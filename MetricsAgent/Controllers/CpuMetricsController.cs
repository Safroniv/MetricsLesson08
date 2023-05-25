using AutoMapper;
using MetricsAgent.Converters;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        #region Services

        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly IMapper _mapper;

        #endregion


        public CpuMetricsController(
            ICpuMetricsRepository cpuMetricsRepository,
            ILogger<CpuMetricsController> logger,
            IMapper mapper)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            // Вариант 1
            //_cpuMetricsRepository.Create(new Models.CpuMetric
            //{
            //    Value = request.Value,
            //    Time = (int)request.Time.TotalSeconds
            //});

            // Вариант 2 (AutoMapper)
            _cpuMetricsRepository.Create(_mapper.Map<CpuMetric>(request));
            return Ok();
        }

        /// <summary>
        /// Получить статистику по нагрузке на ЦП за период
        /// </summary>
        /// <param name="fromTime">Время начала периода</param>
        /// <param name="toTime">Время окончания периода</param>
        /// <returns></returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<GetCpuMetricsResponse> GetCpuMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get cpu metrics call.");

            // Вариант 1:
            //List<CpuMetricDto> list = new List<CpuMetricDto>();
            //foreach(var metric in _cpuMetricsRepository.GetByTimePeriod(fromTime, toTime))
            //{
            //    list.Add(new CpuMetricDto
            //    {
            //        Value = metric.Value,
            //        Time = metric.Time
            //    });
            //}

            // Вариант 2:
            //return Ok(_cpuMetricsRepository.GetByTimePeriod(fromTime, toTime).Select(metric => new CpuMetricDto
            //{
            //    Time = metric.Time,
            //    Value = metric.Value
            //}).ToList());

            // Вариант 3 (AutoMapper)

            return Ok(new GetCpuMetricsResponse
            {
                Metrics = _cpuMetricsRepository.GetByTimePeriod(fromTime, toTime)
                        .Select(metric => _mapper.Map<CpuMetricDto>(metric)).ToList()
            });
            //Random random = new Random();
            //switch (random.Next(2))
            //{
            //    case 0:
            //        return Ok(new GetCpuMetricsResponse
            //        {
            //            Metrics = _cpuMetricsRepository.GetByTimePeriod(fromTime, toTime)
            //            .Select(metric => _mapper.Map<CpuMetricDto>(metric)).ToList()
            //        });
            //    case 1:
            //        throw new Exception("Internal Server Error.");
            //}
            //throw new Exception("Internal Server Error.");
        }

        [HttpGet("all")]
        public ActionResult<IList<CpuMetricDto>> GetAllCpuMetrics()
        {
            return Ok(_cpuMetricsRepository.GetAll()
                    .Select(metric => _mapper.Map<CpuMetricDto>(metric)).ToList());
            //Random random = new Random();
            //switch (random.Next(2))
            //{
            //    case 0:
            //        return Ok(_cpuMetricsRepository.GetAll()
            //        .Select(metric => _mapper.Map<CpuMetricDto>(metric)).ToList());
            //    case 1:
            //        throw new Exception("Internal Server Error.");
            //}
            //throw new Exception("Internal Server Error.");
        }

        // TODO: Домашнее задание [Пункт 2]
        // В проект агента сбора метрик добавьте контроллеры для сбора метрик, аналогичные
        // менеджеру сбора метрик.Добавьте методы для получения метрик с агента, доступные по
        //следующим путям
        // a. api/metrics/cpu/from/{fromTime}/to/{toTime} [ВЫПОЛНИЛИ ВМЕСТЕ]
        // b. api / metrics / dotnet / errors - count / from /{ fromTime}/ to /{ toTime}
        // c. api / metrics / network / from /{ fromTime}/ to /{ toTime}
        // d. api / metrics / hdd / left / from /{ fromTime}/ to /{ toTime}
        // e. api / metrics / ram / available / from /{ fromTime}/ to /{ toTime}


    }
}
