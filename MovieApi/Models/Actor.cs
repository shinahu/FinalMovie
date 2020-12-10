using System;

namespace MovieApi.Models
{
    public class Actor
    {
        public int ActorNo {get; set;}

        public string FullName {get; set;}
        public string GivenName {get; set;} 
        public string SurName {get; set;}


        public Actor()
        {
          this.ActorNo = 0;
          this.FullName = "";
          this.GivenName = "";
          this.SurName = "";
        }

        public Actor(int actorno, string fullname, string givenname, string surname)
        {
            this.ActorNo = actorno;
            this.FullName = fullname;
            this.GivenName = givenname;
            this.SurName = surname;
        }


        public string setFullName(string givenname, string surname){
           return (givenname + " " + surname);
        }



        }
       
    }


