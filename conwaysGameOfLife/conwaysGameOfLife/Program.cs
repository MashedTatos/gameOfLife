using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace conwaysGameOfLife
{
    class Program
    {

        //Seeding new random number
        public static Random random = new Random();
        static void Main(string[] args)
        {
            //Console prompts
            Console.WriteLine("Enter in size. Recommend 20 x 20 and 50 x 50 if you go full screen as anything to big kinda hurts your eyes and is slow. ");
            Console.WriteLine("Width: ");
            int width = Convert.ToInt32( Console.ReadLine());

            Console.WriteLine("Height: ");
            int height = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Sleep time (ms) Recommend 100: ");
            int speed = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Generations: (-1 for no limit): ");
            int gens = Convert.ToInt32(Console.ReadLine());
            Console.Clear();


            //Initializing new cell world
            cell[,] cells = init(width, height);

            //Running through game loop
            loop(cells,speed, gens,0);
            
            
        }

        //Recursion loop that draws cells
        public static void loop(cell[,] cells, int speed, int gens, int currentGen)
        {
            
            if (currentGen == gens)
            {
                Console.WriteLine("Simulation finished");
            }

            else
            {

                //Creating new cell 2d array
                cell[,] nextGen;

                //Getting next generation based on current generation
                nextGen = nextGeneration(cells);

                //Process sets cursor position so it looks smooth when each generation gets drawn
                Console.SetCursorPosition(0, Console.WindowTop);

                //Sleep the thread for x amount of time
                Thread.Sleep(speed);

                //Draw
                draw(nextGen);

                //Increase the current generation
                currentGen++;
                Console.WriteLine("\nCurrent generation: " + currentGen);

                //Call loop function again
                loop(nextGen, speed, gens,currentGen);
            }

            
            

        }

        //Initialize function
        public static cell[,] init(int width, int height)
        {
            
            //Create new cell 2d array based on width and height
            cell[,] cells = new cell[width,height];

            //Loop through 2d array
            for(int i = 0; i < cells.GetLength(0); i++)
            {
                
                for (int x =0; x < cells.GetLength(1); x++)
                {
                    
                    /*
                     * Creation of new cell with random number
                     * Even though the cells state can only be 0 or 1 im making the random number a double because when you seed the random number
                     * its based on your computers time because there is no way to get "true randomness".
                     * Since the random number is based on the seed. The sample can only be so many numbers. In this case if I did 0 to 1
                     * It would only ever be 0 or 1, so I need to generate a double, which can be a wide range of decimal numbers and round the number
                     */
                    cells[i, x] = new cell(random.NextDouble());
                }

                
            }
            return cells;
        }


        public static void draw(cell[,] cells)
        {

            //Loops through array
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    //Write row
                    Console.Write(cells[x, y].getState());
                }

                //New column so create new line
                Console.WriteLine();
            }

            
            
        }

        
        public static cell[,] nextGeneration(cell[,] gen)
        {

            //Init new array with dimensions of last array
            cell[,] nextGen = new cell[gen.GetLength(0), gen.GetLength(1)];

            //Loop
            for (int x =0; x< gen.GetLength(0); x++)
            {
                for(int y = 0; y < gen.GetLength(1); y++)
                {

                    //Getting each cell to calculate its next state
                    gen[x, y].calcNextState(gen,x,y);
                }
            }

            
            for (int x = 0; x < gen.GetLength(0); x++)
            {
                for (int y = 0; y < gen.GetLength(1); y++)
                {
                    //Switch cell state
                    gen[x, y].switchState();

                    //Populating the new cell array. Cells state will be last cells switched state
                    nextGen[x, y] = new cell(gen[x,y].getState());
                }
            }

            /*
             * The reason why I calculate the state then switch the state is because if I did it all in one go. Each cells new state would
             * be based on the last cells switched state. Basically the new generation would be based on its self
             */

            return nextGen;
        }
    }
}
