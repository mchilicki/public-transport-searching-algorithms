namespace Chilicki.Ptsa.Domain.ManualMappers.Search.Base
{
    public interface IToDTOManualMapper<TSource, TDestination>
    {
        TDestination ToDTO(TSource source);
    }
}
