namespace Chilicki.Ptsa.Domain.ManualMappers.Search.Base
{
    public interface IToDomainManualMapper<TSource, TDestination>
    {
        TDestination ToDomain(TSource source);
    }
}
