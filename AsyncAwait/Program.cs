using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
           await PrepareBreakfast();
        }

        static async Task PrepareBreakfast()
        {
            List<Task> breakfast = new List<Task>();
            breakfast.Add(FryEggs());
            breakfast.Add(ToastBread());
            breakfast.Add(MakeCoffee());

            await Task.WhenAll(breakfast);
        }

        static async Task<int> FryEggs()
        {
            Thread.Sleep(35000);
            return 3;
        }

        static async Task<int> ToastBread()
        {
            return 2;
        }

        static async Task<int> MakeCoffee()
        {
            return 1;
        }
    }
}
