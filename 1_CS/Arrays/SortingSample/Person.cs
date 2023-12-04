﻿namespace SortingSample;

public record Person(string FirstName, string LastName) : IComparable<Person>
{
    public int CompareTo(Person? other)
    {
        ArgumentNullException.ThrowIfNull(other);

        int result = LastName.CompareTo(other.LastName);
        if (result == 0)
        {
            result = FirstName.CompareTo(other.FirstName);
        }

        return result;
    }

    public override string ToString() => $"{FirstName} {LastName}";
}
