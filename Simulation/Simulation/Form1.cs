using Simulation.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Users\Regina\source\repos\Simulation_files\nép-teszt.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Users\Regina\source\repos\Simulation_files\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Users\Regina\source\repos\Simulation_files\halál.csv");
            

        }

        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> _population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    _population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender=(Gender)Enum.Parse(typeof(Gender),line[1]),
                        NbrOfChildren=int.Parse(line[2])
                    });

                }
            }

            return _population;
        }


        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> _birthProbabilities = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    _birthProbabilities.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        Numchild = byte.Parse(line[1]),
                        P = double.Parse(line[2])
                    });

                }
            }

            return _birthProbabilities;
        }

        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> _deathProbabilities = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    _deathProbabilities.Add(new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[1]),
                        P = double.Parse(line[2])
                    });

                }
            }

            return _deathProbabilities;
        }
    }

}
