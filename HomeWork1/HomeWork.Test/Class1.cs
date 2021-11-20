
using HomeWork1;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace HomeWork.Test
{
    public class Class1 
    {

        [Fact]
        public void IndexViewResultNotNull()
        {
            string result = HW1.CustomsDuty(300) as string;

            Assert.NotNull(result);
        }

        [Fact]
        public void IndexViewType()
        {
            string result = HW1.CustomsDuty(300) as string;

            Assert.IsType<string>(result);
        }

    }
}