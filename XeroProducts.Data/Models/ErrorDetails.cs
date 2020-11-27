using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace XeroProducts.Data.Models
{
    /// <summary>
    /// Model for generic error message
    /// </summary>
    public class ErrorDetails
    {
        [Key]
        public Guid Id { get; set; }
        public string StackTrace { get; set; }
        public string Message { get; set; }
        public DateTime ExceptionDate { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
