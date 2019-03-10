using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeOnline
{
    class Program
    {
        static void Main(string[] args)
        {
            InputReader reader1 = new InputReader("a_example.txt");
            Problem p1 = reader1.ReadInput();
            p1.Solve("a_example.out");



            InputReader reader3 = new InputReader("c_memorable_moments.txt");
            Problem p3 = reader3.ReadInput();
            p3.Solve("c_memorable_moments.out");

            InputReader reader4 = new InputReader("d_pet_pictures.txt");
            Problem p4 = reader4.ReadInput();
            p4.Solve("d_pet_pictures.out");

            InputReader reader5 = new InputReader("e_shiny_selfies.txt");
            Problem p5 = reader5.ReadInput();
            p5.Solve("e_shiny_selfies.out");

            InputReader reader2 = new InputReader("b_lovely_landscapes.txt");
            Problem p2 = reader2.ReadInput();
            p2.Solve("b_lovely_landscapes.out");
        }
    }
}
