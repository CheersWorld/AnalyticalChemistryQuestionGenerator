using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Analytik_Altfragengenerator.Models;
using Analytik_Altfragengenerator.ViewModels;
using Analytik_Altfragengenerator.Databases;

namespace Analytik_Altfragengenerator.ViewModels
{
    class QuestionViewModel : ModelBase
    {
        private RelayCommand _buttonCommand;
        public RelayCommand ButtonCommand { get { return _buttonCommand; }
            set {
                _buttonCommand = value;
                this.OnPropertyChanged();
            }
        }
        private RelayCommand _checkCommand;
        public RelayCommand CheckCommand
        {
            get { return _checkCommand; }
            set
            {
                _checkCommand = value;
                this.OnPropertyChanged();
            }
        }

        private StatisticsQuestion _statisticsQuestion;
        public StatisticsQuestion StatisticsQuestion
        {
            get { return _statisticsQuestion; }
            set
            {
                _statisticsQuestion = value;
                this.OnPropertyChanged();
            }
        }private TitrationQuestion _titrationQuestion;
        public TitrationQuestion TitrationQuestion
        {
            get { return _titrationQuestion; }
            set
            {
                _titrationQuestion = value;
                this.OnPropertyChanged();
            }
        }

        private DeviationQuestion _deviationQuestion;
        public DeviationQuestion DeviationQuestion
        {
            get { return _deviationQuestion; }
            set
            {
                _deviationQuestion = value;
                this.OnPropertyChanged();
            }
        }

        private GravimetricTitrationQuestion _gravimetricTitrationQuestion;
        public GravimetricTitrationQuestion GravimetricTitrationQuestion
        {
            get { return _gravimetricTitrationQuestion; }
            set
            {
                _gravimetricTitrationQuestion = value;
                this.OnPropertyChanged();
            }
        }

        private string _displayString;
        public string DisplayString
        {
            get { return _displayString; }
            set
            {
                _displayString = value;
                this.OnPropertyChanged();
            }
        }
        private Random Random;

        public delegate string SolutionDelegate();
        SolutionDelegate SolutionMethodDelegate;

        public QuestionViewModel()
        {
            Random = new Random();
            ButtonCommand = new RelayCommand(o => GenerateQuestion());
            CheckCommand = new RelayCommand(o => GetSolution());
            StatisticsQuestion = new StatisticsQuestion();
            TitrationQuestion = new TitrationQuestion();
            DeviationQuestion = new DeviationQuestion();
            GravimetricTitrationQuestion = new GravimetricTitrationQuestion();
            Database Database = new Database();
            StatisticsQuestion.Initialize();
            Thread.Sleep(15);
            GenerateQuestion();
        }

        public void GenerateQuestion()
        {
            //This randomizes the order of questions
            switch((Random.NextDouble() * 4).ToString()[0].ToString()) {
                case "0":
                TitrationQuestion.GenerateQuestion();
                DisplayString = TitrationQuestion.QuestionContent;
                SolutionMethodDelegate = TitrationQuestion.GetSolution;
                    break;
                case "1":
                    StatisticsQuestion.GenerateQuestion();
                    DisplayString = StatisticsQuestion.QuestionContent;
                    SolutionMethodDelegate = StatisticsQuestion.GetSolution;
                    break;
                case "2":
                    DeviationQuestion.GenerateQuestion();
                    DisplayString = DeviationQuestion.QuestionContent;
                    SolutionMethodDelegate = DeviationQuestion.GetSolution;
                    break;
                case "3":
                    GravimetricTitrationQuestion.GenerateQuestion();
                    DisplayString = GravimetricTitrationQuestion.QuestionContent;
                    SolutionMethodDelegate = GravimetricTitrationQuestion.GetSolution;
                    break;
                default:
                    break;
            }
        }
        public void GetSolution() {
            MessageBox.Show(SolutionMethodDelegate());
        }
    }
}
