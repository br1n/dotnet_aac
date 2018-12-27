using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ActivityCenter.Models
{
    public class JoinActivity
    {
        public int JoinActivityId {get; set;}
        public int UserId {get; set;}
        public int ActivityId {get; set;}
        public MainUser User {get; set;}
        public Activity Activity {get; set;}
    }
}