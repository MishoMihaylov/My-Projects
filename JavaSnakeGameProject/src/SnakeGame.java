import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;
import java.util.LinkedList;

public class SnakeGame extends JFrame implements ActionListener, Statistics {

    private final int snakeHeadIndex = 0;
    public LinkedList<Point> snake;
    public Point fruit;
    private Direction snakeDirection;
    private BoardPanel board;
    private SideInformationPanel sidePanel;
    private Timer timer;
    private int gameSpeed;
    private int fruitsEaten;
    private int totalScore;
    private int currentFruitPointsRemaining;
    private boolean fruitInTheBoard;
    private boolean gameOver;
    private boolean gamePaused;
    private boolean tick;

    public static void main(String[] args) {
            SnakeGame newSnakeGame = new SnakeGame();
    }

    public SnakeGame(){
        this.setSize(600, 330);
        this.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        this.setVisible(true);
        this.setResizable(false);
        newGameInit();
        tick = true;

        this.addKeyListener(new KeyAdapter() {
            @Override
            public void keyPressed(KeyEvent e) {
                super.keyPressed(e);

                switch (e.getKeyCode()) {
                    case KeyEvent.VK_W:
                    case KeyEvent.VK_UP:
                        if (snakeDirection != Direction.Down) {
                            if(tick){
                                changeSnakeDirection(Direction.Up);
                            }
                        }
                        break;
                    case KeyEvent.VK_S:
                    case KeyEvent.VK_DOWN:
                        if (snakeDirection != Direction.Up) {
                            if(tick){
                                changeSnakeDirection(Direction.Down);
                            }
                        }
                        break;
                    case KeyEvent.VK_D:
                    case KeyEvent.VK_RIGHT:
                        if (snakeDirection != Direction.Left) {
                            if(tick){
                                changeSnakeDirection(Direction.Right);
                            }
                        }
                        break;
                    case KeyEvent.VK_A:
                    case KeyEvent.VK_LEFT:
                        if (snakeDirection != Direction.Right) {
                            if(tick){
                                changeSnakeDirection(Direction.Left);
                            }
                        }
                        break;
                    case KeyEvent.VK_P:
                        if(!gamePaused){
                            gamePaused = true;
                        }else{
                            gamePaused = false;
                        }
                        break;
                    case KeyEvent.VK_N:

                        if(gameOver){
                            resetGame();
                        }
                        System.out.println("a");
                        break;
                    case KeyEvent.VK_ESCAPE:
                        Runtime.getRuntime().exit(0);
                }
            }
        });
    }

    @Override
    public void actionPerformed(ActionEvent e) {

        if (!gameOver && !gamePaused) {
            if (!fruitInTheBoard) {
                setFruit();
            }
            updateSnake();
            checkIfSnakeIsOutsideTheBoard();
            checkForSnakePartsCollision();
            checkForSnakeFruitCollision();
            this.board.repaint();
            this.sidePanel.repaint();
            tick = true;
        }
    }

    @Override
    public int getFruitsEaten() {
        return this.fruitsEaten;
    }

    @Override
    public int getTotalScore() {
        return this.totalScore;
    }

    @Override
    public int getFruitCurrentPointsRemaining() {
        return this.currentFruitPointsRemaining;
    }

    @Override
    public boolean getIsGameOver(){
        return this.gameOver;
    }

    private void updateSnake(){

        Point snakeHeadLocation = snake.getFirst().getLocation();

        switch(snakeDirection){
            case Right:
                snake.getFirst().setLocation(snakeHeadLocation.x + 1, snakeHeadLocation.y);
                break;
            case Left:
                snake.getFirst().setLocation(snakeHeadLocation.x - 1, snakeHeadLocation.y);
                break;
            case Up:
                snake.getFirst().setLocation(snakeHeadLocation.x, snakeHeadLocation.y - 1);
                break;
            case Down:
                snake.getFirst().setLocation(snakeHeadLocation.x, snakeHeadLocation.y + 1);
                break;
        }

        Point lastElementLocation = snakeHeadLocation;
        Point currentLocation;
        for (int i = 1; i < snake.size(); i++) {
            currentLocation = snake.get(i).getLocation();
            snake.set(i, lastElementLocation);
            lastElementLocation = currentLocation;
        }
    }

    private void checkIfSnakeIsOutsideTheBoard(){

        if(snake.get(snakeHeadIndex).x < 0 || snake.get(snakeHeadIndex).x >= board.ROWS){
            gameOver = true;
        }

        if(snake.get(snakeHeadIndex).y < 0 || snake.get(snakeHeadIndex).y >= board.COLUMNS){
            gameOver = true;
        }
    }

    private void setFruit(){

        byte fruitX, fruitY;

        while(true){

            boolean setFruitPossible = true;
            fruitX = (byte)(Math.random() * board.ROWS);
            fruitY = (byte)(Math.random() * board.COLUMNS);

            for (Point snakePart : snake) {
                if(snakePart.x == fruitX && snakePart.y == fruitY){
                    setFruitPossible = false;
                }
            }

            if(setFruitPossible){
                break;
            }
        }

        this.fruit = new Point(fruitX,fruitY);
        this.fruitInTheBoard = true;
    }

    private void checkForSnakePartsCollision(){

        for (int i = 1; i < snake.size(); i++) {
            if (snake.get(i).x == snake.get(snakeHeadIndex).x && snake.get(i).y == snake.get(snakeHeadIndex).y){
                this.gameOver = true;
            }
        }
    }

    private void checkForSnakeFruitCollision(){
        if(snake.get(snakeHeadIndex).x == fruit.x && snake.get(snakeHeadIndex).y == fruit.y){
            this.fruitInTheBoard = false;
            addSnakePart();
            this.fruitsEaten++;
            this.totalScore += currentFruitPointsRemaining;
            this.currentFruitPointsRemaining = 100;
        }else{
            this.currentFruitPointsRemaining -=5;
            if(currentFruitPointsRemaining < 0){
                this.currentFruitPointsRemaining = 0;
            }
        }
    }

    private void addSnakePart(){
        this.snake.addFirst(new Point(fruit.x, fruit.y));
    }

    private void changeSnakeDirection(Direction newDirection){
        snakeDirection = newDirection;
        tick = false;
    }

    private void newGameInit(){

        this.board = new BoardPanel(this);
        this.add(board, BorderLayout.WEST);
        this.sidePanel = new SideInformationPanel(this);
        this.add(sidePanel, BorderLayout.EAST);
        this.snakeDirection = Direction.Right;
        this.snake =  new LinkedList<Point>();
        this.snake.add(new Point(3, 3));
        this.snake.add(new Point(2, 3));
        this.snake.add(new Point(1, 3));
        this.snake.add(new Point(0, 3));
        this.fruitInTheBoard = false;
        this.gameOver = false;
        this.gamePaused = false;
        this.gameSpeed = 300;
        this.timer = new Timer(gameSpeed, this);
        this.timer.start();
        this.fruitsEaten = 0;
        this.totalScore = 0;
        this.currentFruitPointsRemaining = 100;

        setFruit();
    }

    private void resetGame(){

        this.snake.clear();
        this.snake.add(new Point(3, 3));
        this.snake.add(new Point(2, 3));
        this.snake.add(new Point(1, 3));
        this.snake.add(new Point(0, 3));
        this.snakeDirection = Direction.Right;
        this.gameOver = false;
        this.fruitsEaten = 0;
        this.totalScore = 0;
        this.currentFruitPointsRemaining = 100;

        setFruit();
    }
}
