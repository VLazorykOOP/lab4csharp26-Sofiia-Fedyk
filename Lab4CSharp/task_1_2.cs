// ============================================================
// ЗАВДАННЯ 1.2 — Клас Triangle
// ============================================================
class Triangle
{
    public double a, b, c;
    public string color;

    public Triangle(double a, double b, double c, string color)
    {
        this.a = a; this.b = b; this.c = c; this.color = color;
    }

    // Індексатор
    public object this[int index]
    {
        get => index switch
        {
            0 => a, 1 => b, 2 => c, 3 => color,
            _ => throw new IndexOutOfRangeException($"Index {index} out of range (0..3)")
        };
        set
        {
            switch (index)
            {
                case 0: a = Convert.ToDouble(value); break;
                case 1: b = Convert.ToDouble(value); break;
                case 2: c = Convert.ToDouble(value); break;
                case 3: color = value?.ToString() ?? ""; break;
                default: throw new IndexOutOfRangeException($"Index {index} out of range (0..3)");
            }
        }
    }

    public override string ToString() =>
        $"Triangle(a={a}, b={b}, c={c}, color={color})";

    // ++ / --
    public static Triangle operator ++(Triangle t) => new Triangle(t.a + 1, t.b + 1, t.c + 1, t.color);
    public static Triangle operator --(Triangle t) => new Triangle(t.a - 1, t.b - 1, t.c - 1, t.color);

    // true/false: трикутник існує
    public static bool operator true(Triangle t) =>
        t.a + t.b > t.c && t.a + t.c > t.b && t.b + t.c > t.a;
    public static bool operator false(Triangle t) =>
        !(t.a + t.b > t.c && t.a + t.c > t.b && t.b + t.c > t.a);

    // Множення на скаляр
    public static Triangle operator *(Triangle t, double scalar) =>
        new Triangle(t.a * scalar, t.b * scalar, t.c * scalar, t.color);

    // implicit: Triangle → string
    public static implicit operator string(Triangle t) => $"{t.a};{t.b};{t.c};{t.color}";

    // implicit: string → Triangle
    public static implicit operator Triangle(string s)
    {
        var parts = s.Split(';');
        if (parts.Length < 4) throw new FormatException("String must be in format: a;b;c;color");
        return new Triangle(
            double.Parse(parts[0]),
            double.Parse(parts[1]),
            double.Parse(parts[2]),
            parts[3]
        );
    }
}