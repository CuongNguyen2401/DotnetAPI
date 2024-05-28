﻿namespace MyApiNetCore8.Model
{
    public class User : BaseDTO
    {
        public long id { get; set; }
        public DateTime date_of_birth { get; set; }
        public string first_name { get; set; }

        public Gender gender { get; set; }
        public string last_name { get; set; }
        public string password { get; set; }
        public string phone_number { get; set; }
        public string username { get; set; }
    }
    public enum Gender
    {
        FEMALE, MALE, OTHER
    }
}