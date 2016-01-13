import javax.swing.*;
import java.awt.*;

public class SideInformationPanel extends JPanel {

    private Statistics gameStatistics;

    public SideInformationPanel(Statistics gameStatistics){

        this.gameStatistics = gameStatistics;
        setBackground(Color.BLACK);
        setPreferredSize(new Dimension(200, 200));
    }

    @Override
    protected void paintComponent(Graphics graphic){

        super.paintComponent(graphic);

        graphic.setColor(Color.WHITE);
        graphic.drawString("Snake Game", 20, 40);
        graphic.drawString("Fruits eaten: " + gameStatistics.getFruitsEaten(), 20, 80);
        graphic.drawString("Total score: " + gameStatistics.getTotalScore(), 20, 100);
        graphic.drawString("Current fruit points : " + gameStatistics.getFruitCurrentPointsRemaining(), 20, 120);
        graphic.drawString("Up: Up arrow / W", 20, 160);
        graphic.drawString("Down: Down arrow / S", 20, 180);
        graphic.drawString("Left: Left arrow / A", 20, 200);
        graphic.drawString("Right: Right arrow / D", 20, 220);
        graphic.drawString("Pause/Resume: P", 20, 240);
        if(gameStatistics.getIsGameOver()) {
            graphic.setColor(Color.MAGENTA);
            graphic.drawString("New Game: N", 20, 260);
        }

    }
}
