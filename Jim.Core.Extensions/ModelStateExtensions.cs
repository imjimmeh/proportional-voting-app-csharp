using Jim.Core.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Jim.Core.Extensions
{
    public static class ModelStateExtensions
    {
        private static Func<ErrorMessage, ResponseError> defaultErrorMessageFormatter => (pair) => new ResponseError($"{pair.Key}: {pair.Message}");

        public static IEnumerable<ResponseError> GetErrorMessages(this ModelStateDictionary modelState) => modelState.GetErrorMessages(defaultErrorMessageFormatter);

        public static IEnumerable<ResponseError> GetErrorMessages(this ModelStateDictionary modelState, Func<ErrorMessage, ResponseError>? formatter)
        {
            foreach (var errorGroup in modelState.Where(n => n.Value.Errors.Count > 0))
                foreach (var error in errorGroup.Value.Errors)
                    yield return formatter?.Invoke(new ErrorMessage(errorGroup.Key, error.ErrorMessage)) ?? defaultErrorMessageFormatter(new ErrorMessage(errorGroup.Key, error.ErrorMessage));
        }
    }
}
