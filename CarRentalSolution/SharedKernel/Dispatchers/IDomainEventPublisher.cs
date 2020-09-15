namespace SharedKernel.Dispatchers
{
    public interface IDomainEventPublisher
    {
        void Publish<T>(T domainEvent) 
            where T : IDomainEvent;
    }
}
