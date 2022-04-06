﻿namespace BMS.Company.Domain;

public class User : Entity
{
    public string Email { get; init; }

    public Company Company { get; set; }

#pragma warning disable CS8618
    private User() {}
#pragma warning restore CS8618

    public User(Guid guid)
    {
        Guid = guid;
    }
}
