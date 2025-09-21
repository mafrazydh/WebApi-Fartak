using Domin.Entities;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers
{
    public class CustomActionResult<T>
    {
        private bool v;
        private Product value;

        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public CustomActionResult(bool success, string message, T result = default, List<string> errors = null)
        {
            Success = success;
            Message = message;
            Result = result;
            Errors = errors ?? new List<string>();
        }

      
    }


}
