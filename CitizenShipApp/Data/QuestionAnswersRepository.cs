using CitizenShipApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenShipApp.Data
{
    public class QuestionAnswersRepository: IQuestionAnswersRepository<QuestionAnswersRepository>
    {
        string _connectionString;
        public QuestionAnswersRepository()
        {
            //_connectionString = "Data Source=DESKTOP-UN8AMMQ;" + "Initial Catalog=Citizenship;Trusted_Connection=True";
            _connectionString = "Data Source=(localdb)\\mssqllocaldb;"+ "Initial Catalog = CitizenShipExam; Trusted_Connection = True";
        }

        public IResult<IEnumerable<string>> GetChapters()
        {
            List<string> chapters = new List<string>();
            IResult<IEnumerable<string>> result = new Result<IEnumerable<string>>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = _connectionString;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "GetChapters";
                        
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            chapters.Add(reader["ChapterName"].ToString());
                        }
                    }
                }
                result.IsSucess = true;
                result.Data = chapters;
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public IResult<IEnumerable<QuestionAnswers>> GetNumberOfQuestionsByChapter(string chapterName, int numberOfQuestions)
        {
            var list = new Dictionary<int, QuestionAnswers>();
            var result = new Result<IEnumerable<QuestionAnswers>>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = _connectionString;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "GetNumberOfQuestionsByChapter";
                        command.Parameters.AddWithValue("@ChapterName", chapterName);
                        command.Parameters.AddWithValue("@numberOfQuestions", numberOfQuestions);
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            QuestionAnswers questionAnswers = new QuestionAnswers();
                            questionAnswers.SetQuestion(reader["Question"].ToString());
                            int questionId = (int)reader["QuizId"];
                            list[questionId] = questionAnswers;
                        }
                    }
                }
                foreach (var keyValuePair in list)
                {
                    keyValuePair.Value.SetAnswers(GetAnswersForQuestion(keyValuePair.Key));
                }
                result.IsSucess = true;
                result.Data = list.Values;
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public IResult<IEnumerable<QuestionAnswers>> GetQuestionAnswersByChapter(int chapter)
        {
            List<QuestionAnswers> list = new List<QuestionAnswers>();
            var result = new Result<IEnumerable<QuestionAnswers>>();
            try
            {
                using(SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = _connectionString;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "GetQuestionsByChapter";
                        command.Parameters.AddWithValue("@ChapterNo", 1);
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while(reader.Read())
                        {

                        }
                    }
                }
                result.IsSucess = true;
                result.Data = list;
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public IResult<IEnumerable<QuestionAnswers>> GetQuestionsByChapter(string chapterName)
        {
            var list = new Dictionary<int, QuestionAnswers>();
            var result = new Result<IEnumerable<QuestionAnswers>>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = _connectionString;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "GetQuestionsByChapterName";
                        command.Parameters.AddWithValue("@ChapterName", chapterName);
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            QuestionAnswers questionAnswers = new QuestionAnswers();
                            questionAnswers.SetQuestion(reader["Question"].ToString());
                            int questionId = (int)reader["QuizId"];
                            list[questionId] = questionAnswers;
                        }
                    }
                }
                foreach (var keyValuePair in list)
                {
                    keyValuePair.Value.SetAnswers(GetAnswersForQuestion(keyValuePair.Key));
                }
                result.IsSucess = true;
                result.Data = list.Values;
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public IResult<IEnumerable<QuestionAnswers>> GetQuestionsByChapter(int chapterNo)
        {
            List<QuestionAnswers> list2 = new List<QuestionAnswers>();
            var list = new Dictionary<int, QuestionAnswers>();
            var result = new Result<IEnumerable<QuestionAnswers>>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = _connectionString;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "GetQuestionsByChapter";
                        command.Parameters.AddWithValue("@ChapterNo", chapterNo);
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            QuestionAnswers questionAnswers = new QuestionAnswers();
                            questionAnswers.SetQuestion(reader["Question"].ToString());
                            int questionId = (int)reader["QuizId"];
                            list[questionId] = questionAnswers;
                        }
                    }
                }
                foreach (var keyValuePair in list)
                {
                    keyValuePair.Value.SetAnswers(GetAnswersForQuestion(keyValuePair.Key));
                }
                result.IsSucess = true;
                result.Data = list.Values;
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        private List<Answer> GetAnswersForQuestion(int questionId)
        {
            List<Answer> answers = new List<Answer>();
            try
            {
                using(SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = _connectionString;
                    using (SqlCommand command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "GetAnswersByQuestionId";
                        command.Parameters.AddWithValue("@QuestionId", questionId);
                        var reader = command.ExecuteReader();
                        while(reader.Read())
                        {
                            Answer answer = new Answer();
                            answer.AnswerValue = reader["Answer"].ToString();
                            var iscorrect = reader["IsCorrectAnswer"];
                            answer.IsCorrect = (bool)reader["IsCorrectAnswer"];
                            answers.Add(answer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return answers;
        }
    }
}
