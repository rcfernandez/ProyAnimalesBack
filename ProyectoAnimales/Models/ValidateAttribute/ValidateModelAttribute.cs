using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProyectoAnimales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProyAnimalesBack.Models
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            if (!context.ModelState.IsValid)
            {

                Reply oR = new Reply();
                
                var result = new Dictionary<string, string>();
                var countErrors = 0;
                foreach (var key in context.ModelState.Keys)
                {
                    result.Add(key, String.Join(", ", context.ModelState[key].Errors.Select(p => p.ErrorMessage)));
                    countErrors++;
                }

                oR.Result = 3;
                oR.Message = "Modelo no valido";
                oR.Count = countErrors;
                oR.Data = result;

                context.Result = new ObjectResult(oR);
            }

        }

    }
}
