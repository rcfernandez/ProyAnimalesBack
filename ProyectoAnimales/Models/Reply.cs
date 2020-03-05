using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAnimales.Models
{
    public class Reply
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
        public object Data { get; set; }


        public Reply()
        {
            Result = 0;
            Message = null;
            Count = 0;
            Data = null;
        }

    }
}
