using Course.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.ConrollerBases
{
    public class CustomBaseController:ControllerBase
    {
        /// <summary>
        /// Controller içerisindeki metotlarda kullanılabilecek response içerisindeki statuscode'a göre bir response metodu oluşturduk. bu metot ile controller içerisinde if kontrollerine gerek kalmadı.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
