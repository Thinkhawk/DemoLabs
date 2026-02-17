namespace Demo_DI_Services.Services;

public interface IMySingletonService
{
    Guid Id { get; }
}

public interface IMyTransientService
{
    Guid Id { get; }
}

public interface IMyScopedService
{
    Guid Id { get; }
}

public class MyService
    : IMySingletonService, IMyTransientService, IMyScopedService
{
    public Guid Id { get;  set; } = Guid.NewGuid();

    //public MyService()
    //{
    //    Id = Guid.NewGuid();
    //}
}
