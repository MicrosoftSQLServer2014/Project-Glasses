using System.Web.Http;
using BLL.Interfaces;
using DtoLibrary;

namespace Server.Controllers
{
    public class StatisticController : ApiController
    {
        public IStatisticService StatisticService { get; set; }

        public StatisticController(IStatisticService statisticService)
        {
            StatisticService = statisticService;
        }

        public void AddStatistic(StatisticDto statisticDto)
        {
            StatisticService.Insert(statisticDto);
        }

        public void ChangeStatistic(StatisticDto statisticDto)
        {
            StatisticService.Insert(statisticDto);
        }

        public void RemoveStatistic(StatisticDto statisticDto)
        {
            StatisticService.Remove(statisticDto);
        }
    }
}
