using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab0
{
    internal class FizzBuzz
    {
        int TopNumber = 0;
        public FizzBuzz(int TopNumber) {
            this.TopNumber = TopNumber;
        }

        public void Run() {
            for (int i = 1; i <= TopNumber; i++) {
                if (i % 3 == 0 && i % 5 == 0) {
                    Console.WriteLine("FizzBuzz");
                } else if (i % 3 == 0) {
                    Console.WriteLine("Fizz");
                } else if (i % 5 == 0) {
                    Console.WriteLine("Buzz");
                } else {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
