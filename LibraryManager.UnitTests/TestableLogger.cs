using Microsoft.Extensions.Logging;
using System.Reactive.Disposables;

namespace LibraryManager.UnitTests;

public abstract class TestableLogger : ILogger
{
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        where TState : notnull
    {
        this.Log(logLevel, eventId, state.ToString(), exception);
    }

    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull
    {
        return new CompositeDisposable(
            this.BeginScope(state.ToString()),
            this.BeginScopeState(state));
    }

    public abstract bool IsEnabled(LogLevel logLevel);

    public abstract IDisposable BeginScope(string message);

    public abstract IDisposable BeginScopeState<TState>(TState state);

    public abstract void Log(LogLevel logLevel, EventId eventId, string? message, Exception exception);
}

public abstract class TestableLogger<TCategoryName> : TestableLogger, ILogger<TCategoryName>
     where TCategoryName : class
{
}
