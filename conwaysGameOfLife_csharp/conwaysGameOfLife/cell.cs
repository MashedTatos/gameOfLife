using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conwaysGameOfLife
{
    class cell
    {


        private int state;
        private int nextState;
        
        //Constructor for new game where the cells state is random
        public cell(double state)
        {

            //Round number to get proper state
            this.state = Convert.ToInt32( Math.Round(state));
            
        }

        //Constructor for when I need the cells state to not be random
        public cell(int state)
        {
            setState(state);
        }


        //Get number of neighbors and figures out state based on that
        public void calcNextState(cell[,] array, int x , int y)
        {

            //Get number of neighbors
            int neighbors = countNeighbors(array,x,y);
            int deadNeighbors = 8 - neighbors;

            //State decisions based on Conways Game of Life rules. Can be found on https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
            if (getState() == 1)
            {
                if (neighbors < 2)
                {
                    nextState = 0;
                }

                else if (neighbors == 2 || neighbors == 3)
                {
                    nextState = getState();
                }

                else if (neighbors > 3)
                {
                    nextState = 0;
                }
            }

            else if (getState() == 0)
            {
                if (neighbors == 3)
                {
                    nextState = 1;
                }



            }
        }

        //Params (cell 2d array, cell row and cel column)
        public int countNeighbors(cell[,] array, int row, int col)
        {
            int sum = 0;

            /*
             * To get offsets just look at a 2d array
             * 1|1|0  The current cell will be the middle one. If you wanna get the one to the right just set shift one column to the right and set the row off set to 0
             * 0|1|1
             * 1|0|1
             */
            sum += getNeighbourState(array, array.Length, row, col, 1, 0);
            sum += getNeighbourState(array, array.Length, row, col, 0, 1);
            sum += getNeighbourState(array, array.Length, row, col, 1, 1);

            sum += getNeighbourState(array, array.Length, row, col, 0, -1);
            sum += getNeighbourState(array, array.Length, row, col, -1, 0);
            sum += getNeighbourState(array, array.Length, row, col, -1, -1);

            sum += getNeighbourState(array, array.Length, row, col, +1, -1);
            sum += getNeighbourState(array, array.Length, row, col, -1, +1);
               
            

            
            return sum;
        }


        //Offset means position relative to centre cell
        public int getNeighbourState(cell[,] world, int size,int x,int y, int offsetX, int offsetY)
        {
            //Get row lengths
            int rowLength = world.GetLength(0);
            int colLength = world.GetLength(1);

            /*
             * This is for making the cells wrap around
             * Example:
             * X:
             * (15 + 7 + 20) =  42
             * 42 % 20 = 2
             * 
             * Y: (5 + 9 +20) % 20 = 14. Here the + 20 and % 20 cancel eachother out so it doesnt make a difference
             * 
             * So the position that we will get now is 2, 14
             * 
             * if the x was 12 then  we would get position 19,14
             */
            int proposedoffSetX = (x + offsetX + rowLength) % rowLength;
            int proposedoffSetY = (y + offsetY + colLength) % colLength;
            //Console.WriteLine(proposedoffSetY);
            int result = 0;

            //Return off set
            result = world[proposedoffSetX, proposedoffSetY].getState();
            

            

            return result;
        }

        public void switchState()
        {
            state = nextState;
        }

        public void setState(int s)
        {
            state = s;
        }

        public int getState()
        {
            return state;
        }

    }
}
