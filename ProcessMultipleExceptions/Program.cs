// This code will not compile!
using ProcessMultipleExceptions;

Console.WriteLine("***** Handling Multiple Exceptions *****\n");

Car myCar = new Car("Rusty", 90);

try
{
    // Trigger an argument out of range exception.
    myCar.Accelerate(50);
}
catch (CarIsDeadException e) when (e.ErrorTimeStamp.DayOfWeek != DayOfWeek.Friday)
{
    // This new line will only print if the when clause evaluates to true.
    Console.WriteLine("Catching car is dead!");
    Console.WriteLine(e.Message);
    try
    {
        FileStream fs = File.Open(@"C:\carErrors.txt", FileMode.Open);
    }
    catch (Exception e2)
    {
        //This causes a compile error-InnerException is read only
        //e.InnerException = e2;
        // Throw an exception that records the new exception,
        // as well as the message of the first exception.
        throw new CarIsDeadException(e.CauseOfError, e.ErrorTimeStamp, e.Message, e2);
    }
}
catch (ArgumentOutOfRangeException e)
{
    Console.WriteLine(e.Message);
}
// This will catch any other exception
// beyond CarIsDeadException or
// ArgumentOutOfRangeException.
catch (Exception e)
{
    // Process all other exceptions?
    Console.WriteLine(e.Message);
}
finally
{
    // This will always occur. Exception or not.
    myCar.CrankTunes(false);
}
Console.ReadLine();