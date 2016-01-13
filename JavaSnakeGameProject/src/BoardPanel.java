import javax.swing.*;
import java.awt.*;

public class BoardPanel extends JPanel {

    public final int ROWS = 13;
    public final int COLUMNS = 10;
    private final int TILE_SIZE = 30;
    private SnakeGame game;

    public BoardPanel(SnakeGame game){

        setBackground(Color.BLACK);
        setPreferredSize(new Dimension(ROWS * TILE_SIZE, COLUMNS * TILE_SIZE));
        this.game = game;
    }

    @Override
    protected void paintComponent(Graphics graphics){

        super.paintComponent(graphics);

        for (int i = 0; i < ROWS; i++) {
            for (int j = 0; j < COLUMNS; j++) {
                graphics.setColor(Color.blue);
                graphics.drawRect(i * TILE_SIZE, j * TILE_SIZE, TILE_SIZE, TILE_SIZE);
            }
        }

        graphics.setColor(Color.RED);
        graphics.fillRoundRect(game.fruit.x * TILE_SIZE, game.fruit.y * TILE_SIZE, TILE_SIZE - 1, TILE_SIZE - 1, 40, 40);

        graphics.setColor(Color.GREEN);
        for (Point snakePart : game.snake) {
            graphics.fillRect(snakePart.x * TILE_SIZE, snakePart.y * TILE_SIZE, TILE_SIZE - 1, TILE_SIZE - 1);
        }

        graphics.setColor(Color.MAGENTA);
        graphics.drawRoundRect(game.snake.get(0).x * TILE_SIZE, game.snake.get(0).y * TILE_SIZE, TILE_SIZE - 2, TILE_SIZE - 2, 8, 8);

    }
}

