using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


 
 namespace ActivityCenter.Models
{
 public class Activity
    {
        public int ActivityId {get; set;}

        [Required]
        public string Title {get; set;}

        [Required]
        public DateTime Date {get; set;}

        [Required]
        [RegularExpression(@"\b((1[0-2]|0?[1-9]):([0-5][0-9])([AaPp][Mm]))", ErrorMessage="Please provide time in HOURS:MINS and AM or PM")]
        public string Time {get; set;}

        [Required]
        [RegularExpression(@"^[+]?\d+([.]\d+)?$", ErrorMessage = "You can spend negative time!")]
        public string Duration {get; set;}

        [Required]
        [MinLength(10, ErrorMessage= "Description must be at leat 10 characters long")]
        public string Description {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        [ForeignKey("Coordinator")]
        public int UserId {get; set;}
        public MainUser Coordinator {get; set;}

        public List<JoinActivity> JoinActivities {get; set;}


    }
}