namespace Airbnb.SharedKernel.ConnectionService.HttpConnection.Logs.TraceIdLogic.Interfaces;

/// <summary>
/// чтение трассировочных значений при создании нового scoped
/// для HTTP создаем Middleware и в нем это делаем
/// </summary>
public interface ITraceReader
{
    string Name { get; }

    void WriteValue(string value);
}