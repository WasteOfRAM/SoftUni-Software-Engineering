namespace TeisterMask;

public static class ValidationConstraints
{
    // Employee

    public const int EmployeeUsernameMinLength = 3;
    public const int EmployeeUsernameMaxLength = 40;
    public const string EmployeeUsernameRegexPatern = @"^[a-zA-Z0-9]+$";
    public const string EmployeePhoneRegexPatern = @"^\d{3}-\d{3}-\d{4}$";

    // Project

    public const int ProjectNameMinLength = 2;
    public const int ProjectNameMaxLength = 40;

    // Task

    public const int TaskNameMinLength = 2;
    public const int TaskNameMaxLength = 40;
}
