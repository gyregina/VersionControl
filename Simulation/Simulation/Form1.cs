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
        Random rng = new Random(1234);
        List<int> _females = new List<int>();
        List<int> _males = new List<int>();
        public int záróév;

        public Form1()
        {
            InitializeComponent();
            BirthProbabilities = GetBirthProbabilities(@"C:\Users\Regina\source\repos\Simulation_files\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Users\Regina\source\repos\Simulation_files\halál.csv");
            numericUpDown1.Minimum = 2005;
            numericUpDown1.Maximum = 2024;

        }

        private void Simulation()
        {
            richTextBox1.Clear();
            Population.Clear();
            záróév = (int)numericUpDown1.Value;
            Population = GetPopulation(textBox1.Text);
            for (int year = 2005; year <= záróév; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(year, Population[i]);
                }

                int numberOfFemales = (from x in Population
                                       where x.Gender == Gender.Female && x.IsAlive
                                       select x).Count();
                _females.Add(numberOfFemales);
                int numberOfMales = (from x in Population
                                     where x.Gender == Gender.Male && x.IsAlive
                                     select x).Count();
                _males.Add(numberOfMales);

            }
            DisplayResults();
        }

        public void DisplayResults()
        {
            for (int year = 2005; year <= záróév; i++)
            {
                richTextBox1.Text=(string.Format("Szimulációs év:{0}\n Fiúk:{1}\n Lányok:{2}\n\n", year, _males[year-2005], _females[year-2005]));
            }
            
        }

        private void SimStep(int year, Person person)
        {
            if (!person.IsAlive)
            {
                return;
            }

            byte age = (byte)(year - person.BirthYear);

            double Pdeath = (from x in DeathProbabilities
                             where x.Age == age && x.Gender == person.Gender
                             select x.P).FirstOrDefault();
            if (rng.NextDouble() <= Pdeath)
            {
                person.IsAlive = false;
            }
            if (!person.IsAlive || person.Gender == Gender.Male)
            {
                return;
            }

            double Pbirth = (from x in BirthProbabilities
                             where x.Age == age && x.Numchild == person.NbrOfChildren
                             select x.P).FirstOrDefault();
            if (rng.NextDouble() <= Pbirth)
            {
                person.NbrOfChildren++;
                Person Child = new Person();

                Child.IsAlive = true;
                Child.BirthYear = year;
                Child.Gender = (Gender)(rng.Next(1, 3));
                Child.NbrOfChildren = 0;
                Population.Add(Child);

            }

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
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            Simulation();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                textBox1.Text = ofd.FileName;
            }

        }
    }

}
