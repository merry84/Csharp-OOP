﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Models
{
    public class Student :IStudent
    {
        private int id;
        private string firstName;
        private string lastName;
        private List<int> coveredExams;
        private IUniversity university;


        public Student(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            coveredExams = new List<int>();
        }

        public int Id
        {
            get=>id;
            private set => id = value;
        }

        public string FirstName
        {
            get=>firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    string.Format(ExceptionMessages.NameNullOrWhitespace);
                firstName = value;
            }
        }

        public string LastName
        {
            get=>lastName;
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                    string.Format(ExceptionMessages.NameNullOrWhitespace);
                lastName = value;

            }
        }
        public IReadOnlyCollection<int> CoveredExams => coveredExams.AsReadOnly();
        public IUniversity University => university;
        public void CoverExam(ISubject subject)
        {
            coveredExams.Add(subject.Id);
        }

        public void JoinUniversity(IUniversity university)
        {
            this.university = university;
        }
    }
}
