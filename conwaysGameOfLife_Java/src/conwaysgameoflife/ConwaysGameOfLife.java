package conwaysgameoflife;
import java.lang.Thread;
import java.io.IOException;
import java.util.*;



public class ConwaysGameOfLife {

    
    public static void main(String[] args) throws IOException {
        Scanner sc = new Scanner(System.in);
        System.out.println("Enter in size. Recommend 20 x 20 or 50 x 50 if you go full screen as anything to big kinda hurts your eyes and is slow.");
        
        System.out.println("\nWidth: ");
        int width = Integer.parseInt(sc.nextLine());
        //Runtime.getRuntime().exec("clear");
        
        System.out.println("\nHeight: ");
        int height = Integer.parseInt(sc.nextLine());
        
        
        
        System.out.println("\nSleep time (ms) Recommend 100 (25 for 50 x 50 and fullscreen): ");
        int speed = Integer.parseInt(sc.nextLine());
        clearScreen();
        
        System.out.println("\nGenerations: (-1 for no limit):");
        int genCount = Integer.parseInt(sc.nextLine());
        clearScreen();
        
        Cell[][] world = init(width,height);
        loop(world,speed,genCount,0);
    }

    public static void loop(Cell[][] world, int speed, int gens, int currentGen) {
        if(currentGen == gens ){
            System.out.println("Simulation finished");
        }

        else{
            
            nextGeneration(world);
            try {
                Thread.sleep(speed);
            } catch (InterruptedException e) {
                System.out.println(e.getMessage());
            }
            clearScreen();
            draw(world);
            currentGen++;
            System.out.println("Current generation: " + currentGen);
            loop(world,speed,gens,currentGen);
        }
    }
    public static Cell[][] init(int width, int height){

        Cell[][] world = new Cell[width][height];
        
        for(int x = 0; x < world[0].length; x++){
            for(int y =0; y < world[1].length; y++){
            /*
            * Creation of new cell with random number
            * Even though the cells state can only be 0 or 1 im making the random number a double because when you seed the random number
            * its based on your computers time because there is no way to get "true randomness".
            * Since the random number is based on the seed. The sample can only be so many numbers. In this case if I did 0 to 1
            * It would only ever be 0 or 1, so I need to generate a double, which can be a wide range of decimal numbers and round the number
            */

                world[x][y] = new Cell(Math.random());
            }

        }

        return (world);
    }

    public static void draw(Cell[][] world){
        for(int x = 0; x < world[0].length; x++){
            for(int y =0; y < world[1].length; y++){
                System.out.print(world[x][y].getState());
            }
            System.out.println("");
        }
    }

    public static Cell[][] nextGeneration(Cell[][] world){
        for(int x = 0; x < world[0].length; x++){
            for(int y =0; y < world[1].length; y++){
                world[x][y].calcNextState(world,x,y);
            }

        }

        for(int x = 0; x < world[0].length; x++){
            for(int y =0; y < world[1].length; y++){
                world[x][y].switchState();
                
            }
        }

        return world;
    }
    public static void clearScreen(){
        for(int i = 0; i < 50; i++){
            System.out.println();
        }
    }
    
}
