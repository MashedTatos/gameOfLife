/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package conwaysgameoflife;

/**
 *
 * @author Nathan
 */
public class Cell {
    private int state;
    private int nextState;

    Cell(double state){
        this.state = (int)Math.round(state);
    }

    Cell(int state){
        this.state = state;
    }

    public void calcNextState(Cell[][] world, int x, int y){

        //Get number of neighbors
        int neighbors = countNeighbors(world,x,y);
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

    public int countNeighbors(Cell[][] world, int row, int col){
        int sum = 0;

            /*
             * To get offsets just look at a 2d array
             * 1|1|0  The current cell will be the middle one. If you wanna get the one to the right just set shift one column to the right and set the row off set to 0
             * 0|1|1
             * 1|0|1
             */
            sum += getNeighbourState(world, world.length, row, col, 1, 0);
            sum += getNeighbourState(world, world.length, row, col, 0, 1);
            sum += getNeighbourState(world, world.length, row, col, 1, 1);

            sum += getNeighbourState(world, world.length, row, col, 0, -1);
            sum += getNeighbourState(world, world.length, row, col, -1, 0);
            sum += getNeighbourState(world, world.length, row, col, -1, -1);

            sum += getNeighbourState(world, world.length, row, col, +1, -1);
            sum += getNeighbourState(world, world.length, row, col, -1, +1);
               
            

            
            return sum;
    }

    public int getNeighbourState(Cell[][] world, int size, int x, int y, int offSetX, int offSetY){
        int rowLength = world[0].length;
        int colLength = world[1].length;

        int proposedoffSetX = (x + offSetX + rowLength) % rowLength;
        int proposedoffSetY = (y + offSetY + colLength) % colLength;
        return world[proposedoffSetX][proposedoffSetY].getState();
    }

    public void switchState(){
        this.state = this.nextState;
    }
    /**
     * @return the state
     */
    public int getState() {
        return state;
    }

    /**
     * @param state the state to set
     */
    public void setState(int state) {
        this.state = state;
    }

    /**
     * @return the nextState
     */
    public int getNextState() {
        return nextState;
    }

    /**
     * @param nextState the nextState to set
     */
    public void setNextState(int nextState) {
        this.nextState = nextState;
    }
    
}
