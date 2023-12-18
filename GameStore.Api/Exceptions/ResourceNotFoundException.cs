namespace GameStore.Api.Exceptions;

public class ResourceNotFoundException
{
    private readonly string _resource;
    private readonly string _field;
    private readonly object _value;

    public ResourceNotFoundException(string resource, string field, object value)
    {
        _resource = resource;
        _field = field;
        _value = value;
    }

    public string Message => $"{_resource} not found with {_field}: {_value}.";

}
