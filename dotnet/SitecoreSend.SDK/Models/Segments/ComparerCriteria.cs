namespace SitecoreSend.SDK;

public enum ComparerCriteria
{
    Is = 0, // Finds subscribers where the Field is exactly equal to the specified Value.
    IsNot = 1, // Finds subscribers where the Field is not equal to the specified Value.
    Contains = 2, // Finds subscribers where the Field contains the specified Value.
    DoesNotContain = 3, // Finds subscribers where the Field does not contain the specified Value.
    StartsWith = 4, // Finds subscribers where the Field starts with the specified Value.
    DoesNotStartWith = 5, // Finds subscribers where the Field does not start with the specified Value.
    EndsWith = 6, // Finds subscribers where the Field ends with the specified Value.
    DoesNotEndWith = 7, // Finds subscribers where the Field does not end with the specified Value.
    IsGreaterThan = 8, // Finds subscribers where the Field is greater than the specified Value.
    IsGreaterThanOrEqualTo = 9, // Finds subscribers where the Field is greater than or equal to the specified Value.
    IsLessThan = 10, // Finds subscribers where the Field is less than the specified Value.
    IsLessThanOrEqualTo = 11, // Finds subscribers where the Field is less than or equal to the specified Value.
    IsBefore = 12, // Finds subscribers where the Field is before the specified Value.
    IsAfter = 13, // Finds subscribers where the Field is after the specified Value.
    IsEmpty = 14, // Finds subscribers where the Field has no Value.
    IsNotEmpty = 15, // Finds subscribers where the Field contains a Value.
    IsTrue = 16, // Finds subscribers where the condition defined by the Field is true.
    IsFalse = 17, // Finds subscribers where the condition defined by the Field is false.
    IsBetween = 24, // Finds subscribers where the numeric value of a criterion is between two defined numbers.
    IsNotBetween = 25, // Finds subscribers where the numeric value of a criterion is not between two defined numbers.
}
