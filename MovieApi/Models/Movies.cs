using System;
using System.Data.SqlClient;

namespace MovieApi.Models
{
    public class Movies
    {
        public int MovieNo {get; set;}
        public string Title {get; set;}
        public int RelYear {get; set;} 
        public int Runtime {get; set;}

      

        public Movies()
        {
            this.MovieNo = 0;
            this.Title = "";
            this.RelYear = 0;
            this.Runtime = 0;

        }

        public Movies(int movieno, string title, int relyear, int runtime)
        {
            this.MovieNo = movieno;
            this.Title = title;
            this.RelYear = relyear;
            this.Runtime = runtime;
        }
        

        //Pre Task 3 i. NumActors – returns the number of actors cast in the movie as an int
        public int NumActors(int movieno)
        {  
        string connectionString = @"Data Source=csharp.czit4bgdokjy.us-east-1.rds.amazonaws.com;Initial Catalog=Movies;User ID=admin;Password=databaseconnection";
        SqlConnection conn = new SqlConnection(connectionString);
         
        string queryString = @" SELECT COUNT(MOVIENO)
                                    FROM CASTING
                                    WHERE MOVIENO ='" + movieno + "' GROUP BY MOVIENO;" ;
                                    
        SqlCommand command = new SqlCommand( queryString, conn);
            conn.Open();

            int actorCount = 0;

            using(SqlDataReader reader = command.ExecuteReader())

            {
            
              while (reader.Read())

                {

                    actorCount = Convert.ToInt32(reader[0]);                

                }

            }

            return actorCount;

         }



        // Pre Task 3 ii. GetAge – returns how old the movie is from the current year as an int

        public int GetAge(int movieno){

            string connectionString = @"Data Source=csharp.czit4bgdokjy.us-east-1.rds.amazonaws.com;Initial Catalog=Movies;User ID=admin;Password=databaseconnection";

            SqlConnection conn = new SqlConnection(connectionString);



            string queryString = @" SELECT (YEAR(GETDATE()) - RELYEAR) AS GetAge

                                    FROM MOVIE

                                    WHERE MOVIENO ='" + movieno + "';" ;



            SqlCommand command = new SqlCommand( queryString, conn);

            conn.Open();

        

            int movieAge = 0;

            using(SqlDataReader reader = command.ExecuteReader())
            
            {                while (reader.Read())

                {
                movieAge = Convert.ToInt32(reader[0]);                

                }
            }

            return movieAge;

        }
    }



}
