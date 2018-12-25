﻿using System;
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
            StatisticsQuestion.Initialize();
            GenerateQuestion();
        }

        public void GenerateQuestion()
        {
            //You'll either get a question about Titrations, or about statistics.
            switch((Random.NextDouble() * 3).ToString()[0].ToString()) {
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
                default:
                    break;
            } 
        }
        public void GetSolution() {
            MessageBox.Show(SolutionMethodDelegate());
        }
    }
}
