using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Core.Application.Helpers
{
    public class ProductCodeGenerator
    {
        public string ProductCodeGen()
        {
            Random randomNumber = new Random();
            int number = randomNumber.Next(1, 10000000);
            string generatedCode = number.ToString("0000000");
            return generatedCode;
        }
    }
}
