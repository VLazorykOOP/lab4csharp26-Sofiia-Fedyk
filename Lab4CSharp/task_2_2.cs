// ============================================================
// ЗАВДАННЯ 2.2 — Клас VectorUInt
// ============================================================
class VectorUInt
{
    protected uint[] IntArray;
    protected uint size;
    protected int codeError;
    protected static uint num_vec = 0;

    // Конструктор без параметрів
    public VectorUInt()
    {
        size = 1;
        IntArray = new uint[1];
        IntArray[0] = 0;
        num_vec++;
    }

    // Конструктор з розміром
    public VectorUInt(uint size)
    {
        this.size = size;
        IntArray = new uint[size];
        num_vec++;
    }

    // Конструктор з розміром та початковим значенням
    public VectorUInt(uint size, uint initVal)
    {
        this.size = size;
        IntArray = new uint[size];
        for (int i = 0; i < size; i++) IntArray[i] = initVal;
        num_vec++;
    }

    ~VectorUInt()
    {
        Console.WriteLine($"[~VectorUInt] Destroyed vector of size {size}");
    }

    // Властивості
    public uint Size => size;
    public int CodeError { get => codeError; set => codeError = value; }

    // Індексатор
    public uint this[uint index]
    {
        get
        {
            if (index >= size) { codeError = -1; return 0; }
            return IntArray[index];
        }
        set
        {
            if (index >= size) { codeError = -1; return; }
            IntArray[index] = value;
        }
    }

    public void Input()
    {
        Console.WriteLine($"Enter {size} vector elements (uint):");
        for (uint i = 0; i < size; i++)
        {
            Console.Write($"  [{i}]: ");
            if (uint.TryParse(Console.ReadLine(), out uint val))
                IntArray[i] = val;
        }
    }

    public void Print(string name)
    {
        Console.Write($"{name} = [ ");
        for (uint i = 0; i < size; i++) Console.Write($"{IntArray[i]} ");
        Console.WriteLine("]");
    }

    public void Fill(uint val)
    {
        for (uint i = 0; i < size; i++) IntArray[i] = val;
    }

    public static uint GetCount() => num_vec;

    public VectorUInt Clone()
    {
        var v = new VectorUInt(size);
        Array.Copy(IntArray, v.IntArray, size);
        return v;
    }

    // ++ / --
    public static VectorUInt operator ++(VectorUInt v)
    {
        var res = new VectorUInt(v.size);
        for (uint i = 0; i < v.size; i++) res.IntArray[i] = v.IntArray[i] + 1;
        return res;
    }
    public static VectorUInt operator --(VectorUInt v)
    {
        var res = new VectorUInt(v.size);
        for (uint i = 0; i < v.size; i++) res.IntArray[i] = v.IntArray[i] > 0 ? v.IntArray[i] - 1 : 0;
        return res;
    }

    // true/false: всі елементи != 0
    public static bool operator true(VectorUInt v)
    {
        if (v.size == 0) return false;
        for (uint i = 0; i < v.size; i++) if (v.IntArray[i] == 0) return false;
        return true;
    }
    public static bool operator false(VectorUInt v)
    {
        if (v.size == 0) return true;
        for (uint i = 0; i < v.size; i++) if (v.IntArray[i] == 0) return true;
        return false;
    }

    // !: size != 0
    public static bool operator !(VectorUInt v) => v.size != 0;

    // ~: побітове заперечення
    public static VectorUInt operator ~(VectorUInt v)
    {
        var res = new VectorUInt(v.size);
        for (uint i = 0; i < v.size; i++) res.IntArray[i] = ~v.IntArray[i];
        return res;
    }

    // Допоміжний: бінарна операція двох векторів
    private static VectorUInt BinaryOp(VectorUInt a, VectorUInt b, Func<uint, uint, uint> op)
    {
        uint maxSize = Math.Max(a.size, b.size);
        var res = new VectorUInt(maxSize);
        for (uint i = 0; i < maxSize; i++)
        {
            uint va = i < a.size ? a.IntArray[i] : 0;
            uint vb = i < b.size ? b.IntArray[i] : 0;
            res.IntArray[i] = op(va, vb);
        }
        return res;
    }

    // Арифметичні: вектор op вектор
    public static VectorUInt operator +(VectorUInt a, VectorUInt b) => BinaryOp(a, b, (x, y) => x + y);
    public static VectorUInt operator -(VectorUInt a, VectorUInt b) => BinaryOp(a, b, (x, y) => x >= y ? x - y : 0);
    public static VectorUInt operator *(VectorUInt a, VectorUInt b) => BinaryOp(a, b, (x, y) => x * y);
    public static VectorUInt operator /(VectorUInt a, VectorUInt b) => BinaryOp(a, b, (x, y) => y != 0 ? x / y : 0);
    public static VectorUInt operator %(VectorUInt a, VectorUInt b) => BinaryOp(a, b, (x, y) => y != 0 ? x % y : 0);

    // Арифметичні: вектор op скаляр
    public static VectorUInt operator +(VectorUInt a, uint s) { var r = a.Clone(); for (uint i = 0; i < r.size; i++) r.IntArray[i] += s; return r; }
    public static VectorUInt operator -(VectorUInt a, uint s) { var r = a.Clone(); for (uint i = 0; i < r.size; i++) r.IntArray[i] = r.IntArray[i] >= s ? r.IntArray[i] - s : 0; return r; }
    public static VectorUInt operator *(VectorUInt a, uint s) { var r = a.Clone(); for (uint i = 0; i < r.size; i++) r.IntArray[i] *= s; return r; }
    public static VectorUInt operator /(VectorUInt a, uint s) { var r = a.Clone(); if (s != 0) for (uint i = 0; i < r.size; i++) r.IntArray[i] /= s; return r; }
    public static VectorUInt operator %(VectorUInt a, uint s) { var r = a.Clone(); if (s != 0) for (uint i = 0; i < r.size; i++) r.IntArray[i] %= s; return r; }

    // Побітові: вектор op вектор
    public static VectorUInt operator |(VectorUInt a, VectorUInt b) => BinaryOp(a, b, (x, y) => x | y);//or
    public static VectorUInt operator ^(VectorUInt a, VectorUInt b) => BinaryOp(a, b, (x, y) => x ^ y); //xor
    public static VectorUInt operator &(VectorUInt a, VectorUInt b) => BinaryOp(a, b, (x, y) => x & y); //and
    public static VectorUInt operator >>(VectorUInt a, int shift) { var r = a.Clone(); for (uint i = 0; i < r.size; i++) r.IntArray[i] >>= shift; return r; }
    public static VectorUInt operator <<(VectorUInt a, int shift) { var r = a.Clone(); for (uint i = 0; i < r.size; i++) r.IntArray[i] <<= shift; return r; }

    // Побітові: вектор op скаляр
    public static VectorUInt operator |(VectorUInt a, uint s) { var r = a.Clone(); for (uint i = 0; i < r.size; i++) r.IntArray[i] |= s; return r; }
    public static VectorUInt operator ^(VectorUInt a, uint s) { var r = a.Clone(); for (uint i = 0; i < r.size; i++) r.IntArray[i] ^= s; return r; }
    public static VectorUInt operator &(VectorUInt a, uint s) { var r = a.Clone(); for (uint i = 0; i < r.size; i++) r.IntArray[i] &= s; return r; }

    // == та !=
    public static bool operator ==(VectorUInt a, VectorUInt b)
    {
        if (a.size != b.size) return false;
        for (uint i = 0; i < a.size; i++) if (a.IntArray[i] != b.IntArray[i]) return false;
        return true;
    }
    public static bool operator !=(VectorUInt a, VectorUInt b) => !(a == b);

    // Порівняння
    public static bool operator >(VectorUInt a, VectorUInt b)
    {
        uint len = Math.Min(a.size, b.size);
        for (uint i = 0; i < len; i++) if (!(a.IntArray[i] > b.IntArray[i])) return false;
        return true;
    }
    public static bool operator >=(VectorUInt a, VectorUInt b)
    {
        uint len = Math.Min(a.size, b.size);
        for (uint i = 0; i < len; i++) if (!(a.IntArray[i] >= b.IntArray[i])) return false;
        return true;
    }
    public static bool operator <(VectorUInt a, VectorUInt b) => b > a;
    public static bool operator <=(VectorUInt a, VectorUInt b) => b >= a;

    public override bool Equals(object? obj) => obj is VectorUInt v && this == v;
    public override int GetHashCode() => IntArray.GetHashCode();

    // Доступ до внутрішнього масиву для MatrixUint
    public uint[] GetArray() => IntArray;
}