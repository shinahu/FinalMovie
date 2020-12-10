using System;
using System.Data.SqlClient;

namespace MovieApi.Models
{
    public class Cast
    {

        public int CastId { get; set; }
        
        public int ActorNo { get; set;}

        public int MovieNo { get; set; }

    }

}