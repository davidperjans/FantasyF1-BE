using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = new();
        public T? Data { get; set; }

        public static OperationResult<T> Success(T data) =>
            new() { IsSuccess = true, Data = data };

        public static OperationResult<T> Failure(string error) =>
            new() { IsSuccess = false, Errors = new List<string> { error } };

        public static OperationResult<T> Failure(IEnumerable<string> errors) =>
            new() { IsSuccess = false, Errors = errors.ToList() };
    }
}
