using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController// 120/150
    {
        //•	subjects – SubjectRepository
        // •	students – StudentRepository
        // •	universities - UniversityRepository
        private readonly SubjectRepository subjects;
        private readonly StudentRepository students;
        private readonly UniversityRepository universities;

        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }
        public string AddSubject(string subjectName, string subjectType)
        {

            if (subjectType != nameof(EconomicalSubject)
                && subjectType != nameof(HumanitySubject)
                && subjectType != nameof(TechnicalSubject))
            {
                return string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }
            if (subjects.FindByName(subjectName) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }

            ISubject subject;
            //. Every new Subject will take the next consecutive number in the repository, starting from 1
            int subjectId = subjects.Models.Count + 1;
            if (subjectType == nameof(EconomicalSubject))
            {
                subject = new EconomicalSubject(subjectId, subjectName);
            }
            else if (subjectType != nameof(HumanitySubject))
            {
                subject = new HumanitySubject(subjectId, subjectName);
            }
            else
            {
                subject = new TechnicalSubject(subjectId, subjectName);
            }
            subjects.AddModel(subject);

            return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName,
                nameof(SubjectRepository));
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            //if there is already added a University with the given name, return the following message: "{universityName} is already added in the repository."
            if (universities.FindByName(universityName) is not null)
            {
                return string.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }
            //If the above case is not reached, convert the given collection of requiredSubjects into collection of integers,
            //containing every required subject’s id. The subjects will be already added into the SubjectRepository.
            //Create a new University and add it to the UniversityRepository.
            //Return the following message: "{universityName} university is created and added to the {relevantRepositoryTypeName}!"

            List<int> requiredSubjectsIds = new List<int>();
            requiredSubjects.ForEach(rs => 
                requiredSubjectsIds
                    .Add
                        (subjects
                            .FindByName(rs)
                            .Id));

            IUniversity university = new University(universities.Models.Count + 1, universityName, category, capacity, requiredSubjectsIds);
            universities.AddModel(university);

            return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, universities.GetType().Name);
        }

        public string AddStudent(string firstName, string lastName)
        {
            if (students.FindByName($"{firstName} {lastName}") != null)
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);

            IStudent student = new Student(students.Models.Count + 1, firstName, lastName);
            students.AddModel(student);
            return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName,
                nameof(StudentRepository));
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student = students.FindById(studentId);
            ISubject subject = subjects.FindById(subjectId);

            if (student == null)
                return string.Format(OutputMessages.InvalidStudentId);

            if (subject == null)
                return string.Format(OutputMessages.InvalidSubjectId);

            if (student.CoveredExams.Contains(subjectId))
                return string.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName,
                    subject.Name);

            student.CoverExam(subject);
            return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName,
                subject.Name);
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            IStudent student = students.FindByName(studentName);
            if (student == null)
            {
                string[] fullName = studentName.Split(" ");
                return string.Format(OutputMessages.StudentNotRegitered, fullName[0], fullName[1]);
            }

            IUniversity university = universities.FindByName(universityName);
            if (universityName == null)
                return string.Format(OutputMessages.UniversityNotRegitered, universityName);
            //•	If the Student with the given studentName has not covered all the required exams for the University with the given name,
            //return the following message: "{studentName} has not covered all the required exams for {universityName} university!"
            if (university.RequiredSubjects.Any(subject =>
                        !student
                        .CoveredExams
                        .Contains(subject)))
            return string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);

            if (student.University is not null && student.University.Name == universityName)
                return string.Format(OutputMessages.StudentAlreadyJoined, student.FirstName, student.LastName,
                    university.Name);

            student.JoinUniversity(university);
            return string.Format(OutputMessages.StudentSuccessfullyJoined, student.FirstName, student.LastName,
                university.Name);
        }

        public string UniversityReport(int universityId)
        {
            //•	Find the University with the given universityId.
            //•	Returns the following string report:
            // "*** {universityName} ***
            // Profile: {universityCategory}
            // Students admitted: {studentsCount}
            // University vacancy: {capacityLeft}"
            //Note: studentsCount => the count of all students admitted in the given university
            // Note: capacityLeft => the university capacity – the count of all admitted students in the university
            
            var sb =new StringBuilder();
            IUniversity university = universities.FindById(universityId);
            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            int studentCount = students.Models.Count(x => x.University != null &&
                                                          x.University.Name == university.Name);
            sb.AppendLine($"Students admitted: {studentCount}");

            int capacityLeft = university.Capacity - studentCount;
            sb.AppendLine($"University vacancy: {capacityLeft}");
            return sb.ToString().TrimEnd();

        }
    }
}
