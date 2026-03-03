using FluentValidation.Results;

namespace SchedFrex.Api.Extensions;

public static class FluentValidationExtensions
{
    public static Dictionary<string, string[]> ToJsonResponse(this ValidationResult result)
    {
        var errors = result.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

        return errors;
    }
}