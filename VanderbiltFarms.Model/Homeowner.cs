﻿namespace VanderbiltFarms.Model
{
    public class Homeowner
    {
        public int HomeownerId { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Description { get; set; }
    }
}