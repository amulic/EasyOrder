﻿namespace EasyOrder.API.Models.DTO
{
    public class FoodDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string? ImageLink { get; set; }
    }
}
