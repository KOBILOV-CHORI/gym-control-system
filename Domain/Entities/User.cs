﻿namespace Domain.Entities;

public abstract class User : BaseEntity
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string? ProfileImage { get; set; }
}