﻿using MyApiNetCore8.Enums;
using static MyApiNetCore8.Model.Product;

namespace MyApiNetCore8.DTO.Request
{
    public class CategoryRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        
        public Status status { get; set; }
    }
}