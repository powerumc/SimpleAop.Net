# SimpleAop

SimpleAop is library for your Aop or Dynamic Proxy programming. I converted it more simply from the [old framework](https://github.com/powerumc/UmcCore/tree/master/Src/Base%20Frameworks/Src/Core/Dynamic/Proxy).

Here includes following libraries.
- SimpleAop  
  It needs Aop or Dynamic Proxy programming.
  
- SimpleAop.Extensions.DependencyInjection  
  Here includes `IServiceCollection` extension class for .NET Core or ASP.NET Core.


To install, run following command or Nuget browser of visual studio.

Functionally list.
- [x] Dynamic Proxy
- [x] Aop of methods and classes.
- [x] General methods.
- [x] yield return
- [x] async/await
- [x] Generate constructors.

```bash
dotnet add package SimpleAop
```

```bash
dotnet add package SimpleAop.Extensions.DependencyInjection
```

## Dynamic Proxy Programming

It's simple. We just need **Interface** declaring and **Implementation** class.

```csharp
public interface IPrint
{
    void PrintMessage(string message);
}

public class Print : IPrint
{
    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }
}
```

And to use it,

```csharp
var type = DynamicProxyFactory.Create<IPrint, Print>();
var obj = (IPrint) Activator.CreateInstance(type);

obj.PrintMessage("Hello World");
```

The `type` generate random character from the `Guid`. It's just first SimpleAop version.

## Aop Programming

Additionally, provider `OnMethodBoundAspect` attribute class. We just inherites it.

```csharp
public class LoggingAspectAttribute : OnMethodBoundAspectAttribute
{
    public override void OnBefore(IAspectInvocation invocation)
    {
        Console.WriteLine($"--- Before: {invocation.Method}, {invocation.Object}, {string.Join(",", invocation.Parameters)} ---");
    }

    public override void OnAfter(IAspectInvocation invocation)
    {
        Console.WriteLine("--- After ---");
    }
}
``` 

And add logging attribute,

```csharp
[LoggingAspect]
public class Print : IPrint
{
    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }
}
```

So result is,

```
--- Before: Void PrintMessage(System.String), 9a19fdd7e64943c9b22ae2c79a886b50, Hello World ---
Hello World
--- After ---
```

### ASP.NET Core or .NET Core

Provide some methods is,

- AddTransientWithProxy
- AddScopedWithProxy
- AddSingletonWithProxy

In `Startup.cs` file,

```csharp
services.AddSingletonWithProxy<ITestService, TestService>();
```

I did make `SimpleAop.Sample.AspNetCoreWeb` project. Let's run it.


## Have a issues?

https://github.com/powerumc/SimpleAop/issues