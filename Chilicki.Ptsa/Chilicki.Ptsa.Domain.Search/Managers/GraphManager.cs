using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Data.UnitsOfWork;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories.Base;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.Managers
{
    public class GraphManager
    {
        readonly IGraphFactory<Graph> graphFactory;
        readonly StopRepository stopRepository;
        readonly GraphRepository graphRepository;
        readonly IUnitOfWork unitOfWork;

        public GraphManager(
            IGraphFactory<Graph> graphFactory,
            StopRepository stopRepository,
            GraphRepository graphRepository,
            IUnitOfWork unitOfWork)
        {
            this.graphFactory = graphFactory;
            this.stopRepository = stopRepository;
            this.graphRepository = graphRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateGraph()
        {
            var stops = await stopRepository.GetAllAsync();
            var graph = await graphFactory.CreateGraph(stops);
            await graphRepository.AddAsync(graph);
            await unitOfWork.SaveAsync();
        }
    }
}
