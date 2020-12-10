using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
       SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
        IConfiguration configuration;
        string connectionString = "";

        public MoviesController(IConfiguration iConfig) {

            this.configuration = iConfig;
            //use the SqlConnectionStringBuilder to create our connection string

            this.stringBuilder.DataSource = this.configuration.GetSection("DBConnectionString").GetSection("Url").Value;
            this.stringBuilder.InitialCatalog = this.configuration.GetSection("DBConnectionString").GetSection("Database").Value;
            this.stringBuilder.UserID = this.configuration.GetSection("DBConnectionString").GetSection("User").Value;
            this.stringBuilder.Password = this.configuration.GetSection("DBConnectionString").GetSection("Password").Value;
            this.connectionString = stringBuilder.ConnectionString;


        }


                

      //Read Task List all Movies
        
        [HttpGet("listallmovies")]

        public List<Movies> ListMovies(){

            List<Movies> Movies = new List<Movies>();

            SqlConnection conn = new SqlConnection(connectionString);

            string queryString = "Select * From Movie";

            SqlCommand command = new SqlCommand( queryString, conn);

            conn.Open();


            string result = "";

            using(SqlDataReader reader = command.ExecuteReader())

            {
                
                while (reader.Read())

                {

                    result += reader[0] + " | " + reader[1] + reader[2] + reader[3] + "\n";

                    

                    // ORM - Object Relation Mapping

                    Movies.Add(

                        new Movies() { MovieNo = Convert.ToInt32(reader[0]), Title = reader[1].ToString(), RelYear = Convert.ToInt32(reader[2]), Runtime = Convert.ToInt32(reader[3])});                

                }
                
             }



            return Movies;



        }

        //List all movies with the 
    
       [HttpGet("listtitlethe")]

        public List<Movies> ListMovieTitle(){


            List<Movies> Movies = new List<Movies>();



            SqlConnection conn = new SqlConnection(connectionString);



            string queryString = "Select * From Movie WHERE Title LIKE 'The%'";



            SqlCommand command = new SqlCommand( queryString, conn);

            conn.Open();

        

            string result = "";

            using(SqlDataReader reader = command.ExecuteReader())

            {

                while (reader.Read())

                {

                    result += reader[0] + " | " + reader[1] + reader[2] + reader[3] + "\n";

                    

                    // ORM - Object Relation Mapping

                    Movies.Add(

                        new Movies() { MovieNo = Convert.ToInt32(reader[0]), Title = reader[1].ToString(), RelYear = Convert.ToInt32(reader[2]), Runtime = Convert.ToInt32(reader[3])});                

                }

            }



            return Movies;



        }

//list all the titles that has Luke Wilson as cast member
          [HttpGet("ListLukeWilsonMovies")]

          public List<Movies> ListLukeWilsonMovies(){



            List<Movies> Movies = new List<Movies>();



            SqlConnection conn = new SqlConnection(connectionString);



            string queryString = @" SELECT * 

                                    FROM Movie M

                                    INNER JOIN CASTING C

                                    ON M.MOVIENO = C.MOVIENO

                                    WHERE ACTORNO = (SELECT ACTORNO FROM ACTOR WHERE FULLNAME = 'Luke Wilson');";



            SqlCommand command = new SqlCommand( queryString, conn);

            conn.Open();

        

            string result = "";

            using(SqlDataReader reader = command.ExecuteReader())

            {  

                while (reader.Read())

                {

                    result += reader[0] + " | " + reader[1] + reader[2] + reader[3] + "\n";

                    

                    // ORM - Object Relation Mapping

                    Movies.Add(

                        new Movies() { MovieNo = Convert.ToInt32(reader[0]), Title = reader[1].ToString(), RelYear = Convert.ToInt32(reader[2]), Runtime = Convert.ToInt32(reader[3])});                

                }

            }



            return Movies;



          }

//List Total running time of all movies
             [HttpGet("ListAllMoviesRunTime")]

             public string ListAllMoviesRunTime(){



            string Movielist = "";



            SqlConnection conn = new SqlConnection(connectionString);



            string queryString = @" SELECT (SUM(RUNTIME)) AS 'TOTAL RUNTIME OF ALL MOVIES'

                                    FROM MOVIE";



            SqlCommand command = new SqlCommand( queryString, conn);

            conn.Open();

    

            using(SqlDataReader reader = command.ExecuteReader())

            {

                while (reader.Read())

                {

                    Movielist = "The Total Number of Movie Run Time is: " + (reader[0]).ToString();

                }
            }

            return Movielist;

        }

        [HttpPost("uRuntime/{movieTitle}/{newRunTime}")]

        public List<Movies> UpdateRuntime(string movieTitle, int newRunTime){

            List<Movies> Movies = new List<Movies>();
            SqlConnection conn = new SqlConnection(connectionString);
            string runtimeUpdater = @" UPDATE MOVIE 
                                    SET RUNTIME = " + newRunTime + @"   
                                    WHERE TITLE = '" + movieTitle + "';";            

            string displayResult = @" SELECT *
                                        FROM MOVIE
                                        WHERE TITLE = '" + movieTitle + "';";

            // SQL Command to update
            SqlCommand command1 = new SqlCommand( runtimeUpdater, conn);
            conn.Open();
            command1.ExecuteNonQuery();

            // To display the results
            SqlCommand command2 = new SqlCommand( displayResult,conn);
            string result = "";
            using(SqlDataReader reader = command2.ExecuteReader())

            {  

                while (reader.Read())

                {
                    result += reader[0] + " | " + reader[1] + reader[2] + reader[3] + "\n";

                    // ORM - Object Relation Mapping
                    Movies.Add(

                        new Movies() { MovieNo = Convert.ToInt32(reader[0]), Title = reader[1].ToString(), RelYear = Convert.ToInt32(reader[2]), Runtime = Convert.ToInt32(reader[3])});                
                }

            }

            conn.Close();

            return Movies;

        }

       [HttpPost("ChangeActorName/{givenname}/{surname}/{newsurname}")]
        public List<Actor> ChangeActorName(string givenname, string surname, string newsurname){
            List<Actor> actor = new List<Actor>();
            string space = " ";
            SqlConnection conn = new SqlConnection(connectionString);
            string changesurname = " UPDATE ACTOR SET SURNAME = '" + newsurname +"', FULLNAME = '"+ givenname + space + newsurname+"' WHERE GIVENNAME ='" + givenname + "' AND SURNAME = '" + surname + "'";            
            string displayResult = @" SELECT *
                                        FROM ACTOR
                                        WHERE GIVENNAME = '"+ givenname +"' AND SURNAME='"+ newsurname +"'";
            // SQL Command to update
            SqlCommand command1 = new SqlCommand( changesurname, conn);
            conn.Open();
            command1.ExecuteNonQuery();
            // To display the results
            SqlCommand command2 = new SqlCommand( displayResult,conn);
            string result = "";
            using(SqlDataReader reader = command2.ExecuteReader())
            {   
                while (reader.Read())
                {
                    result += reader[0] + " | " + reader[1] + reader[2] + reader[3] + "\n";
                    // ORM - Object Relation Mapping
                    actor.Add(
                        new Actor() { ActorNo = Convert.ToInt32(reader[0]), FullName = reader[1].ToString(), GivenName = reader[2].ToString(), SurName = reader[3].ToString()});                
                }
            }
            conn.Close();
            return actor;
        }

        [HttpPost("addmovie")]
        public string AddMovie(Movies m){

            SqlConnection conn = new SqlConnection(connectionString);

            string queryString = "INSERT INTO MOVIE (MOVIENO, TITLE, RELYEAR, RUNTIME) VALUES (@mNo, @mTitle , @mRelyear ,@mRuntime)";

            SqlCommand command = new SqlCommand(queryString, conn);
            command.Parameters.AddWithValue("@mNo",  m.MovieNo);
            command.Parameters.AddWithValue("@mTitle", m.Title);
            command.Parameters.AddWithValue("@mRelyear", m.RelYear);
            command.Parameters.AddWithValue("@mRuntime", m.Runtime);

            conn.Open();
            var result = command.ExecuteNonQuery();

            return "Success! " + result + " row of data added into the Movie table";
        }

       /* {
           "MovieNo":1000,
           "Title":"This Idiot",
           "RelYear":1999,
           "Runtime":50
          }
          */
     
        [HttpPost("addActor")]
        public string AddActor(Actor m){

            SqlConnection conn = new SqlConnection(connectionString);

            string queryString = "INSERT INTO ACTOR (ACTORNO, FULLNAME, GIVENNAME, SURNAME) VALUES (@mNo, @mFullName , @mGivenName ,@mSurName)";

            SqlCommand command = new SqlCommand(queryString, conn);
            command.Parameters.AddWithValue("@mNo",  m.ActorNo);
            command.Parameters.AddWithValue("@mFullName", m.FullName);
            command.Parameters.AddWithValue("@mGivenName", m.GivenName);
            command.Parameters.AddWithValue("@mSurName", m.SurName);

            conn.Open();
            var result = command.ExecuteNonQuery();

            return "Success! " + result + " row of data added into the Actor table";
        }
         
         /*{

             
            "ActorNo": 1000,
            "FullName": "MISTER James",
            "GivenName": "MISTER”,
            "Surname": "James"
        
         }*/


           [HttpPost("castActor")]
           public string CastActor(Cast c){

            SqlConnection conn = new SqlConnection(connectionString);

            string queryString = "INSERT INTO CASTING (CASTID, ACTORNO, MOVIENO) VALUES (@cNo, @cActorNo , @cMovieNo)";

            SqlCommand command = new SqlCommand(queryString, conn);
            command.Parameters.AddWithValue("@cNo",  c.CastId);
            command.Parameters.AddWithValue("@cActorNo", c.ActorNo);
            command.Parameters.AddWithValue("@cMovieNo", c.MovieNo);
            

            conn.Open();
            var result = command.ExecuteNonQuery();

            return "Success! " + result + " row of data added into the Casting Table";
        }


        /*{
            "CastID": 4555,
            "ActorNo": 1234,
            "MovieNo": 268
        }*/

// Exception task. redirects to the correct database
      [HttpGet("exception")]

        public string exception(){



            string connectionStringx = @"Data Source=no.database.here.com;User ID=Wally;Password=Where;Initial Catalog=Is";

            SqlConnection connx = new SqlConnection(connectionStringx);



            string queryString = @" SELECT (SUM(RUNTIME)) AS 'TOTAL RUNTIME OF ALL MOVIES'

                                    FROM MOVIE";



            try {
            
                SqlCommand command = new SqlCommand( queryString, connx);

                connx.Open();

                var result = command.ExecuteNonQuery();

                return result.ToString();
                
                
                
                } catch (SqlException ex) 
                
                {
                string Movielist = "";



                SqlConnection conn = new SqlConnection(connectionString);

                conn.Open();

                SqlCommand command = new SqlCommand( queryString, conn);



                 using(SqlDataReader reader = command.ExecuteReader())

           {            
            while (reader.Read())

                {Movielist = "The Total Number of Movie Run Time is: " + (reader[0]).ToString();

                }

           }

                return "Sorry, the DATABASE you are trying to connect is not available at the moment, You are being directed to a new DATABASE \n\n" + ex.Message + "\n\n Connection to new DATABASE successfull\n\n" + Movielist;

            }

        }
    


    }
}