namespace SitecoreSend.SDK;

public delegate Task<T?> Wrapper<T>(Func<CancellationToken, Task<T?>> originalFunction,
    CancellationToken cancellationToken);