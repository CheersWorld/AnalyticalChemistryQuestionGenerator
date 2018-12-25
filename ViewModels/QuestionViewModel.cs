using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Analytik_Altfragengenerator.Models;
using Analytik_Altfragengenerator.ViewModels;

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
            SetQuestionParameters();
        }

        public void GenerateQuestion()
        {
            //You'll either get a question about Titrations, or about statistics.
            if(((Random.NextDouble() * 2).ToString()[0].ToString()) != "0") {
                TitrationQuestion.GenerateQuestion();
                DisplayString = TitrationQuestion.QuestionContent;
                SolutionMethodDelegate = TitrationQuestion.GetSolution;
            } else
            {
                StatisticsQuestion.GenerateQuestion();
                DisplayString = StatisticsQuestion.QuestionContent;
                SolutionMethodDelegate = StatisticsQuestion.GetSolution;
            }

        }

        public void SetQuestionParameters()
        {
            //An inelegant design decision early on in the process of coding this makes this code necessary. I intended for this to be stored in an XML file, however that would break the scope of this project
            StatisticsQuestion.RandomParameterCount = 5;
            StatisticsQuestion.RandomParamterBaseValue = 200;
            StatisticsQuestion.BaseValueDeviation = 5;
            StatisticsQuestion.RandomParameterBaseValueMoifier = 10;
            StatisticsQuestion.GenerateQuestion();
        }
        public void GetSolution() {
            MessageBox.Show(SolutionMethodDelegate());
        }
    }
}
