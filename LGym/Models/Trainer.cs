﻿namespace LGym.Models
{
    public class Trainer
    {
        public int TrainerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Speciality { get; set; }
        public decimal FeePer30Minutes { get; set; }
        public DateTime HireDate { get; set; }

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
