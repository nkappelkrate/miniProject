using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MoviesRatings.Data
{
    public class Actor
    {
        public ObjectId Id { get; set; } 
        //public ObjectId CastId { get; set; }
        [Required]
        [StringLength(50, MinimumLength =2, ErrorMessage = "First Name must be between 2 and 50 characters long")]
        //[MinLength(2, ErrorMessage = "First Name must be between 2 and 50 characters long")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters long")]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}
