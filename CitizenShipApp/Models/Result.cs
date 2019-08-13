using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenShipApp.Models
{
    public class Result<T> : IResult<T>
    {
        public bool IsSucess { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
