using System;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Have fun practicing sorting algorithms!");

            var numbers = new int[] { 3,7,8,5,2,1,9,5,4 };
            Console.WriteLine("Initial Order:");
            Console.WriteLine(String.Join(", ", numbers));
            
            QuickSort(numbers, 0, numbers.Length - 1);

            Console.WriteLine("Final order:");
            Console.WriteLine(String.Join(", ", numbers));
        }

        static void QuickSort(int[] array, int lo, int hi) {
            if (lo < hi) {
                var p = Partition(array, lo, hi);
                QuickSort(array, lo, p - 1);
                QuickSort(array, p + 1, hi);
            }
        }

        static int Partition (int[] array, int lo, int hi) {
            var pivot = array[hi];
            var i = lo;

            for(int j = lo; j < hi - 1; j++) {
                if (array[j] < pivot) {
                    var t = array[i];
                    array[i] = array[j];
                    array[j] = t;
                    i++;
                }
            }

            var temp = array[i];
            array[i] = array[hi];
            array[hi] = temp;

            return i;
        }
    }
}
