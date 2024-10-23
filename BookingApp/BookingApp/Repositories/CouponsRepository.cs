﻿using BookingApp.Models;

namespace BookingApp.Repositories;

public class CouponsRepository() : AbstractRepository<Coupon>(_filePath)
{
    private static readonly string _filePath = "C:\\Users\\HARDPC\\RiderProjects\\BYT-project\\BookingApp\\BookingApp\\Repositories\\Files\\service.json";
}