using FactoryPattern.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryPattern
{
    
    public partial class Form1 : Form
    {
        List<Ball> _balls = new List<Ball>();

        private BallFactory _ballFactory;

        public BallFactory BallFactory
        {
            get { return _ballFactory; }
            set { _ballFactory = value; }
        }

        public Form1()
        {
            InitializeComponent();
            BallFactory = new BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = BallFactory.CreateNew();
            _balls.Add(ball);
            ball.Left = -ball.Width;
            mainPanel.Controls.Add(ball);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var lastPosition = 0;

            foreach (var ball in _balls)
            {
                ball.MoveBall();
                if (ball.Left > lastPosition)
                    lastPosition = ball.Left;
            }
            if (lastPosition > 1000)
            {
                var oldestBall = _balls[0];
                mainPanel.Controls.Remove(oldestBall);
                _balls.Remove(oldestBall);
            }
        }
    }
}
