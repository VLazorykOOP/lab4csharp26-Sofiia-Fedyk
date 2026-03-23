// ЗАВДАННЯ 3.7 — Студент (struct, tuple, record)

//Реалізація 1: struct
struct StudentStruct
{
    public string FullName;
    public string Address;
    public string Group;
    public double Rating;

    public StudentStruct(string fullName, string address, string group, double rating)
    {
        FullName = fullName; Address = address; Group = group; Rating = rating;
    }

    public override string ToString() =>
        $"  [{FullName}] | {Address} | {Group} | Rating: {Rating}";
}

//Реалізація 3: record
record StudentRecord(string FullName, string Address, string Group, double Rating)
{
    public override string ToString() =>
        $"  [{FullName}] | {Address} | {Group} | Rating: {Rating}";
}