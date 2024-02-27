using SimpleStringifier;

var stringifier = new Stringifier();

var p = new Person
{
    Name = new Name
    {
        FirstName = "Nick",
        LastName = "Kvizhinadze"
    },
    Age = 31
};

Console.WriteLine(stringifier.Stringify(p));
