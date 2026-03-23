using System.Collections.Generic;
class Program
{
    static void Header(string title)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 60));
        Console.WriteLine($"  {title}");
        Console.WriteLine(new string('=', 60));
    }

    static void Main()
    {
        // TASK 1: Triangle
        Header("TASK 1.2 — Triangle");

        Triangle t1 = new Triangle(3, 4, 5, "red");
        Triangle t2 = new Triangle(1, 2, 10, "blue"); // does not exist

        Console.WriteLine($"t1: {t1}");
        Console.WriteLine($"t2: {t2}");

        // true/false
        Console.WriteLine($"t1 exists: {(t1 ? "yes" : "no")}");
        Console.WriteLine($"t2 exists: {(t2 ? "yes" : "no")}");

        // ++ / --
        Console.WriteLine($"t1 before ++:  {t1}");
        t1++;
        Console.WriteLine($"t1 after ++: {t1}");
        t1--;
        Console.WriteLine($"t1 after --: {t1}");

        // * скаляр
        Triangle t5 = t1 * 2.0;
        Console.WriteLine($"t1 * 2.0: {t5}");

        // implicit: Triangle → string
        string s = t1;
        Console.WriteLine($"implicit Triangle→string: \"{s}\"");

        // implicit: string → Triangle
        Triangle t6 = "6;8;10;green";
        Console.WriteLine($"implicit string→Triangle: {t6}");

        // Індексатор
        Console.WriteLine($"t1[0]={t1[0]}, t1[1]={t1[1]}, t1[2]={t1[2]}, t1[3]={t1[3]}");
        try { var bad = t1[5]; }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"Indexer [5]: Exception — {ex.Message}");
        }


        // TASK 2: VectorUInt
        Header("TASK 2.2 — VectorUInt");

        VectorUInt v1 = new VectorUInt(4, 5);
        VectorUInt v2 = new VectorUInt(4, 3);

        v1.Print("v1");
        v2.Print("v2");
        Console.WriteLine($"Vector count: {VectorUInt.GetCount()}");

        v1.Print("v1 before ++");
        v1++;
        v1.Print("v1 after ++");
        v1--;
        v1.Print("v1 after --");

        Console.WriteLine($"v1 true (all != 0): {(v1 ? "yes" : "no")}");
        Console.WriteLine($"!v1 (size != 0): {!v1}");

        VectorUInt vBit = new VectorUInt(3, 0b1010u);
        vBit.Print("vBit");
        (~vBit).Print("~vBit");

        (v1 + v2).Print("v1 + v2");
        (v1 - v2).Print("v1 - v2");
        (v1 * v2).Print("v1 * v2");
        (v1 / v2).Print("v1 / v2");
        (v1 % v2).Print("v1 % v2");

        (v1 + 10u).Print("v1 + 10");
        (v1 * 2u).Print("v1 * 2");

        (v1 | v2).Print("v1 | v2");
        (v1 ^ v2).Print("v1 ^ v2");
        (v1 & v2).Print("v1 & v2");
        (v1 >> 1).Print("v1 >> 1");
        (v1 << 1).Print("v1 << 1");
        (v1 | 1u).Print("v1 | 1");
        (v1 ^ 3u).Print("v1 ^ 3");
        (v1 & 7u).Print("v1 & 7");

        VectorUInt vEq = new VectorUInt(4, 5);
        Console.WriteLine($"v1 == vEq: {v1 == vEq}");
        Console.WriteLine($"v1 != v2:  {v1 != v2}");
        Console.WriteLine($"v1 > v2:  {v1 > v2}");
        Console.WriteLine($"v1 >= v2: {v1 >= v2}");
        Console.WriteLine($"v2 < v1:  {v2 < v1}");
        Console.WriteLine($"v2 <= v1: {v2 <= v1}");

        uint badVal = v1[100u];
        Console.WriteLine($"v1[100] → {badVal}, codeError = {v1.CodeError}");

        // TASK 3: Student
        Header("TASK 3.7 — Student (struct, tuple, record)");

        double threshold = 50.0;

        // struct
        Console.WriteLine("\nStruct");
        List<StudentStruct> studs = new List<StudentStruct>
        {
            new StudentStruct("Ivanenko Ivan Ivanovych",       "Kyiv, Khreshchatyk St. 1",      "CN-11", 75.0),
            new StudentStruct("Petrenko Petro Petrovych",       "Lviv, Franka St. 5",            "CN-11", 45.0),
            new StudentStruct("Sydorenko Oleksii Mykolaiovych", "Kharkiv, Sumska St. 10",        "CN-12", 90.0),
            new StudentStruct("Kovalenko Mariia Vasylivna",     "Dnipro, Gagarina Ave. 15",      "CN-12", 30.0),
            new StudentStruct("Melnyk Olena Petrivna",          "Odesa, Derybasivska St. 2",     "CN-13", 88.0),
        };
        Console.WriteLine($"Removing rating < {threshold}");
        studs.RemoveAll(st => st.Rating < threshold);
        studs.Add(new StudentStruct("New Student Struct", "Zaporizhzhia, Kozatska St. 3", "CN-14", 95.0));
        Console.WriteLine("Result:");
        foreach (var st in studs) Console.WriteLine(st);

        // ValueTuple
        Console.WriteLine("\nValueTuple");
        var tupls = new List<(string fullName, string address, string group, double rating)>
        {
            ("Ivanenko Ivan Ivanovych",       "Kyiv, Khreshchatyk St. 1",      "CN-11", 75.0),
            ("Petrenko Petro Petrovych",       "Lviv, Franka St. 5",            "CN-11", 45.0),
            ("Sydorenko Oleksii Mykolaiovych", "Kharkiv, Sumska St. 10",        "CN-12", 90.0),
            ("Kovalenko Mariia Vasylivna",     "Dnipro, Gagarina Ave. 15",      "CN-12", 30.0),
            ("Melnyk Olena Petrivna",          "Odesa, Derybasivska St. 2",     "CN-13", 88.0),
        };
        Console.WriteLine($"Removing rating < {threshold}");
        tupls.RemoveAll(t => t.rating < threshold);
        tupls.Add(("New Student Tuple", "Mykolaiv, Admiralska St. 7", "CN-14", 92.0));
        Console.WriteLine("Result:");
        foreach (var t in tupls)
            Console.WriteLine($"  [{t.fullName}] | {t.address} | {t.group} | Rating: {t.rating}");

        // record 
        Console.WriteLine("\nRecord");
        List<StudentRecord> recs = new List<StudentRecord>
        {
            new StudentRecord("Ivanenko Ivan Ivanovych",       "Kyiv, Khreshchatyk St. 1",      "CN-11", 75.0),
            new StudentRecord("Petrenko Petro Petrovych",       "Lviv, Franka St. 5",            "CN-11", 45.0),
            new StudentRecord("Sydorenko Oleksii Mykolaiovych", "Kharkiv, Sumska St. 10",        "CN-12", 90.0),
            new StudentRecord("Kovalenko Mariia Vasylivna",     "Dnipro, Gagarina Ave. 15",      "CN-12", 30.0),
            new StudentRecord("Melnyk Olena Petrivna",          "Odesa, Derybasivska St. 2",     "CN-13", 88.0),
        };
        Console.WriteLine($"Removing rating < {threshold}");
        recs.RemoveAll(r => r.Rating < threshold);
        recs.Add(new StudentRecord("New Student Record", "Poltava, Sobornosti St. 9", "CN-14", 81.0));
        Console.WriteLine("Result:");
        foreach (var r in recs) Console.WriteLine(r);

        // TASK 4: MatrixUint
        Header("TASK 4.2 — MatrixUint");

        MatrixUint m1 = new MatrixUint(2, 3);
        m1[0, 0] = 1; m1[0, 1] = 2; m1[0, 2] = 3;
        m1[1, 0] = 4; m1[1, 1] = 5; m1[1, 2] = 6;
        MatrixUint m2 = new MatrixUint(2, 3, 2u);

        m1.Print("m1");
        m2.Print("m2");
        Console.WriteLine($"Matrix count: {MatrixUint.GetCount()}");

        m1.Print("m1 before ++");
        m1++;
        m1.Print("m1 after ++");
        m1--;
        m1.Print("m1 after --");

        Console.WriteLine($"m1 true (all != 0): {(m1 ? "yes" : "no")}");
        Console.WriteLine($"!m1 (n,m != 0): {!m1}");

        MatrixUint mSmall = new MatrixUint(1, 2, 0b1100u);
        mSmall.Print("mSmall");
        (~mSmall).Print("~mSmall");

        (m1 + m2).Print("m1 + m2");
        (m1 - m2).Print("m1 - m2");
        (m1 / m2).Print("m1 / m2");
        (m1 % m2).Print("m1 % m2");

        MatrixUint m3 = new MatrixUint(3, 2);
        m3[0, 0] = 1; m3[0, 1] = 2;
        m3[1, 0] = 3; m3[1, 1] = 4;
        m3[2, 0] = 5; m3[2, 1] = 6;
        m3.Print("m3 (3x2)");
        (m1 * m3).Print("m1 * m3 (2x3 × 3x2 = 2x2)");

        VectorUInt colVec = new VectorUInt(3);
        colVec[0u] = 1; colVec[1u] = 2; colVec[2u] = 3;
        colVec.Print("colVec");
        (m1 * colVec).Print("m1 * colVec");

        (m1 * 3u).Print("m1 * 3");
        (m1 + 10u).Print("m1 + 10");
        (m1 - 1u).Print("m1 - 1");

        (m1 | m2).Print("m1 | m2");
        (m1 ^ m2).Print("m1 ^ m2");
        (m1 & m2).Print("m1 & m2");
        (m1 >> 1).Print("m1 >> 1");
        (m1 << 1).Print("m1 << 1");
        (m1 | 1u).Print("m1 | 1");
        (m1 ^ 3u).Print("m1 ^ 3");
        (m1 & 7u).Print("m1 & 7");

        MatrixUint mEq = new MatrixUint(2, 3);
        mEq[0, 0] = 1; mEq[0, 1] = 2; mEq[0, 2] = 3;
        mEq[1, 0] = 4; mEq[1, 1] = 5; mEq[1, 2] = 6;
        Console.WriteLine($"m1 == mEq: {m1 == mEq}");
        Console.WriteLine($"m1 != m2:  {m1 != m2}");

        MatrixUint mBig = new MatrixUint(2, 3, 100u);
        Console.WriteLine($"mBig > m1:  {mBig > m1}");
        Console.WriteLine($"mBig >= m1: {mBig >= m1}");
        Console.WriteLine($"m1 < mBig:  {m1 < mBig}");
        Console.WriteLine($"m1 <= mBig: {m1 <= mBig}");

        Console.WriteLine($"m1[4] (= m1[1,1]): {m1[4]}");
        uint badM = m1[999];
        Console.WriteLine($"m1[999] → {badM}, codeError = {m1.CodeError}");

        Console.WriteLine();
        Console.WriteLine("=== Program finished ===");
    }
}