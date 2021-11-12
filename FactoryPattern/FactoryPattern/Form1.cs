using FactoryPattern.Abstractions;
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
        List<Toy> _toys = new List<Toy>();

        public IToyFactory _toyFactory;

        public IToyFactory ToyFactory
        {
            get { return _toyFactory; }
            set { _toyFactory = value; }
        }

        public Form1()
        {
            InitializeComponent();
            ToyFactory = new BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            Ball toy = (Ball)ToyFactory.CreateNew();
            _toys.Add(toy);
            toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var lastPosition = 0;

            foreach (var ball in _toys)
            {
                ball.MoveBall();
                if (ball.Left > lastPosition)
                    lastPosition = ball.Left;
            }
            if (lastPosition > 1000)
            {
                var oldestToy = _toys[0];
                mainPanel.Controls.Remove(oldestToy);
                _toys.Remove(oldestToy);
            }
        }
    }
}
