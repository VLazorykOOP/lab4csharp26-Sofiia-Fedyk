// ЗАВДАННЯ 4.2 — Клас MatrixUint
class MatrixUint
{
    protected uint[,] IntArray;
    protected int n, m;
    protected int codeError;
    protected static int num_m = 0;

    public MatrixUint()
    {
        n = 1; m = 1;
        IntArray = new uint[1, 1];
        num_m++;
    }

    public MatrixUint(int n, int m)
    {
        this.n = n; this.m = m;
        IntArray = new uint[n, m];
        num_m++;
    }

    public MatrixUint(int n, int m, uint initVal)
    {
        this.n = n; this.m = m;
        IntArray = new uint[n, m];
        Fill(initVal);
        num_m++;
    }

    ~MatrixUint()
    {
        Console.WriteLine($"[~MatrixUint] Destroyed matrix {n}x{m}");
    }

    public int N => n;
    public int M => m;
    public int CodeError { get => codeError; set => codeError = value; }

    public uint this[int i, int j]
    {
        get
        {
            if (i < 0 || i >= n || j < 0 || j >= m) { codeError = -1; return 0; }
            return IntArray[i, j];
        }
        set
        {
            if (i < 0 || i >= n || j < 0 || j >= m) { codeError = -1; return; }
            IntArray[i, j] = value;
        }
    }

    // Індексатор [k] → [k/m, k%m]
    public uint this[int k]
    {
        get
        {
            if (m == 0 || k < 0 || k >= n * m) { codeError = -1; return 0; }
            return IntArray[k / m, k % m];
        }
        set
        {
            if (m == 0 || k < 0 || k >= n * m) { codeError = -1; return; }
            IntArray[k / m, k % m] = value;
        }
    }

    public void Input()
    {
        Console.WriteLine($"Enter matrix {n}x{m} (uint):");
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                Console.Write($"  [{i},{j}]: ");
                if (uint.TryParse(Console.ReadLine(), out uint val))
                    IntArray[i, j] = val;
            }
    }

    public void Print(string name)
    {
        Console.WriteLine($"{name} ({n}x{m}):");
        for (int i = 0; i < n; i++)
        {
            Console.Write("  [ ");
            for (int j = 0; j < m; j++) Console.Write($"{IntArray[i, j],4} ");
            Console.WriteLine("]");
        }
    }

    public void Fill(uint val)
    {
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                IntArray[i, j] = val;
    }

    public static int GetCount() => num_m;

    public MatrixUint Clone()
    {
        var res = new MatrixUint(n, m);
        Array.Copy(IntArray, res.IntArray, n * m);
        return res;
    }

    // ++ / --
    public static MatrixUint operator ++(MatrixUint mx)
    {
        var res = mx.Clone();
        for (int i = 0; i < res.n; i++)
            for (int j = 0; j < res.m; j++)
                res.IntArray[i, j]++;
        return res;
    }
    public static MatrixUint operator --(MatrixUint mx)
    {
        var res = mx.Clone();
        for (int i = 0; i < res.n; i++)
            for (int j = 0; j < res.m; j++)
                if (res.IntArray[i, j] > 0) res.IntArray[i, j]--;
        return res;
    }

    // true/false
    public static bool operator true(MatrixUint mx)
    {
        if (mx.n == 0 || mx.m == 0) return false;
        for (int i = 0; i < mx.n; i++)
            for (int j = 0; j < mx.m; j++)
                if (mx.IntArray[i, j] == 0) return false;
        return true;
    }
    public static bool operator false(MatrixUint mx)
    {
        if (mx.n == 0 || mx.m == 0) return true;
        for (int i = 0; i < mx.n; i++)
            for (int j = 0; j < mx.m; j++)
                if (mx.IntArray[i, j] == 0) return true;
        return false;
    }

    // !
    public static bool operator !(MatrixUint mx) => mx.n != 0 && mx.m != 0;

    // ~
    public static MatrixUint operator ~(MatrixUint mx)
    {
        var res = mx.Clone();
        for (int i = 0; i < res.n; i++)
            for (int j = 0; j < res.m; j++)
                res.IntArray[i, j] = ~res.IntArray[i, j];
        return res;
    }

    // Допоміжний: бінарна операція двох матриць
    private static MatrixUint MatOp(MatrixUint a, MatrixUint b, Func<uint, uint, uint> op)
    {
        var res = a.Clone();
        int rn = Math.Min(a.n, b.n), rm = Math.Min(a.m, b.m);
        for (int i = 0; i < rn; i++)
            for (int j = 0; j < rm; j++)
                res.IntArray[i, j] = op(a.IntArray[i, j], b.IntArray[i, j]);
        return res;
    }

    // Арифметичні: матриця op матриця
    public static MatrixUint operator +(MatrixUint a, MatrixUint b) => MatOp(a, b, (x, y) => x + y);
    public static MatrixUint operator -(MatrixUint a, MatrixUint b) => MatOp(a, b, (x, y) => x >= y ? x - y : 0);
    public static MatrixUint operator /(MatrixUint a, MatrixUint b) => MatOp(a, b, (x, y) => y != 0 ? x / y : 0);
    public static MatrixUint operator %(MatrixUint a, MatrixUint b) => MatOp(a, b, (x, y) => y != 0 ? x % y : 0);

    // Множення: матриця * матриця (стандартне)
    public static MatrixUint operator *(MatrixUint a, MatrixUint b)
    {
        if (a.m != b.n) return a.Clone();
        var res = new MatrixUint(a.n, b.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < b.m; j++)
                for (int k = 0; k < a.m; k++)
                    res.IntArray[i, j] += a.IntArray[i, k] * b.IntArray[k, j];
        return res;
    }

    // Множення: матриця * вектор
    public static VectorUInt operator *(MatrixUint a, VectorUInt v)
    {
        if (a.m != (int)v.Size) return new VectorUInt((uint)a.n);
        var res = new VectorUInt((uint)a.n);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                res[(uint)i] += a.IntArray[i, j] * v[(uint)j];
        return res;
    }

    // Множення: матриця * скаляр
    public static MatrixUint operator *(MatrixUint a, uint s)
    {
        var res = a.Clone();
        for (int i = 0; i < res.n; i++)
            for (int j = 0; j < res.m; j++)
                res.IntArray[i, j] *= s;
        return res;
    }

    // Арифметичні: матриця op скаляр
    public static MatrixUint operator +(MatrixUint a, uint s) { var r = a.Clone(); for (int i = 0; i < r.n; i++) for (int j = 0; j < r.m; j++) r.IntArray[i, j] += s; return r; }
    public static MatrixUint operator -(MatrixUint a, uint s) { var r = a.Clone(); for (int i = 0; i < r.n; i++) for (int j = 0; j < r.m; j++) r.IntArray[i, j] = r.IntArray[i, j] >= s ? r.IntArray[i, j] - s : 0; return r; }
    public static MatrixUint operator /(MatrixUint a, uint s) { var r = a.Clone(); if (s != 0) for (int i = 0; i < r.n; i++) for (int j = 0; j < r.m; j++) r.IntArray[i, j] /= s; return r; }
    public static MatrixUint operator %(MatrixUint a, uint s) { var r = a.Clone(); if (s != 0) for (int i = 0; i < r.n; i++) for (int j = 0; j < r.m; j++) r.IntArray[i, j] %= s; return r; }

    // Побітові: матриця op матриця
    public static MatrixUint operator |(MatrixUint a, MatrixUint b) => MatOp(a, b, (x, y) => x | y);
    public static MatrixUint operator ^(MatrixUint a, MatrixUint b) => MatOp(a, b, (x, y) => x ^ y);
    public static MatrixUint operator &(MatrixUint a, MatrixUint b) => MatOp(a, b, (x, y) => x & y);
    public static MatrixUint operator >>(MatrixUint a, int s) { var r = a.Clone(); for (int i = 0; i < r.n; i++) for (int j = 0; j < r.m; j++) r.IntArray[i, j] >>= s; return r; }
    public static MatrixUint operator <<(MatrixUint a, int s) { var r = a.Clone(); for (int i = 0; i < r.n; i++) for (int j = 0; j < r.m; j++) r.IntArray[i, j] <<= s; return r; }

    // Побітові: матриця op скаляр
    public static MatrixUint operator |(MatrixUint a, uint s) { var r = a.Clone(); for (int i = 0; i < r.n; i++) for (int j = 0; j < r.m; j++) r.IntArray[i, j] |= s; return r; }
    public static MatrixUint operator ^(MatrixUint a, uint s) { var r = a.Clone(); for (int i = 0; i < r.n; i++) for (int j = 0; j < r.m; j++) r.IntArray[i, j] ^= s; return r; }
    public static MatrixUint operator &(MatrixUint a, uint s) { var r = a.Clone(); for (int i = 0; i < r.n; i++) for (int j = 0; j < r.m; j++) r.IntArray[i, j] &= s; return r; }

    // == та !=
    public static bool operator ==(MatrixUint a, MatrixUint b)
    {
        if (a.n != b.n || a.m != b.m) return false;
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                if (a.IntArray[i, j] != b.IntArray[i, j]) return false;
        return true;
    }
    public static bool operator !=(MatrixUint a, MatrixUint b) => !(a == b);

    // Порівняння (поелементно)
    public static bool operator >(MatrixUint a, MatrixUint b)
    {
        int rn = Math.Min(a.n, b.n), rm = Math.Min(a.m, b.m);
        for (int i = 0; i < rn; i++)
            for (int j = 0; j < rm; j++)
                if (!(a.IntArray[i, j] > b.IntArray[i, j])) return false;
        return true;
    }
    public static bool operator >=(MatrixUint a, MatrixUint b)
    {
        int rn = Math.Min(a.n, b.n), rm = Math.Min(a.m, b.m);
        for (int i = 0; i < rn; i++)
            for (int j = 0; j < rm; j++)
                if (!(a.IntArray[i, j] >= b.IntArray[i, j])) return false;
        return true;
    }
    public static bool operator <(MatrixUint a, MatrixUint b) => b > a;
    public static bool operator <=(MatrixUint a, MatrixUint b) => b >= a;

    public override bool Equals(object? obj) => obj is MatrixUint mx && this == mx;
    public override int GetHashCode() => IntArray.GetHashCode();
}