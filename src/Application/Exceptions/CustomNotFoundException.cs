namespace Application.Exceptions;

public class CustomNotFoundException : Exception
{
    public string EntityName { get; }
    public string Identifier { get; }

    public CustomNotFoundException(string entityName, string identifier)
        : base($"{entityName} with ID '{identifier}' not found.")
    {
        EntityName = entityName;
        Identifier = identifier;
    }
}