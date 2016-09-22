using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    public static class Tuple
    {
        public static Tuple<T1> Create<T1>(T1 item1)
        {
            return new Tuple<T1>(item1);
        }

        public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new Tuple<T1, T2>(item1, item2);
        }

        public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            return new Tuple<T1, T2, T3>(item1, item2, item3);
        }

        public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
        }

    }

    public class Tuple<T1> : IEquatable<Tuple<T1>>, IComparable<Tuple<T1>>
    {
        public T1 Item1 { get; set; }

        public Tuple(T1 item1)
        {
            Item1 = item1;
        }

        public static bool operator ==(Tuple<T1> t1, Tuple<T1> t2)
        {
            if (ReferenceEquals(t1, t2))
                return true;
            if ((object)t1 == null || (object)t2 == null)
                return false;
            return t1.Equals(t2);
        }

        public static bool operator !=(Tuple<T1> t1, Tuple<T1> t2)
        {
            return !(t1 == t2);
        }

        public override int GetHashCode()
        {
            return 0 ^ Item1.GetHashCode();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("(");
            stringBuilder.Append(Item1);
            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        public string ToString(string pFormat)
        {
            return string.Format(pFormat, Item1);
        }

        public override bool Equals(object t)
        {
            if (t == null || !(t is Tuple<T1>))
            {
                return false;
            }
            return Equals((Tuple<T1>)t);
        }

        public bool Equals(Tuple<T1> t)
        {
            if (t == null)
            {
                return false;
            }
            return EqualityComparer<T1>.Default.Equals(Item1, t.Item1);
        }

        public int CompareTo(Tuple<T1> t)
        {
            return Comparer<T1>.Default.Compare(Item1, t.Item1);
        }
    }

    public class Tuple<T1, T2> : Tuple<T1>, IEquatable<Tuple<T1, T2>>, IComparable<Tuple<T1, T2>>
    {
        public T2 Item2 { get; set; }

        public Tuple(T1 item1, T2 item2)
            : base(item1)
        {
            Item2 = item2;
        }

        public static bool operator ==(Tuple<T1, T2> t1, Tuple<T1, T2> t2)
        {
            if (ReferenceEquals(t1, t2))
                return true;
            if ((object)t1 == null || (object)t2 == null)
                return false;
            return t1.Equals(t2);
        }

        public static bool operator !=(Tuple<T1, T2> t1, Tuple<T1, T2> t2)
        {
            return !(t1 == t2);
        }

        public override int GetHashCode()
        {
            return 0 ^ Item1.GetHashCode() ^ Item2.GetHashCode();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("(");
            stringBuilder.Append(Item1);
            stringBuilder.Append(", ");
            stringBuilder.Append(Item2);
            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        public new string ToString(string pFormat)
        {
            return string.Format(pFormat, Item1, Item2);
        }

        public override bool Equals(object t)
        {
            if (t == null || !(t is Tuple<T1, T2>))
                return false;
            return Equals((Tuple<T1, T2>)t);
        }

        public bool Equals(Tuple<T1, T2> t)
        {
            if (t == null)
            {
                return false;
            }
            return EqualityComparer<T1>.Default.Equals(Item1, t.Item1) && EqualityComparer<T2>.Default.Equals(Item2, t.Item2);
        }

        public int CompareTo(Tuple<T1, T2> pOther)
        {
            return Comparer<T1>.Default.Compare(Item1, pOther.Item1);
        }
    }

    public class Tuple<T1, T2, T3> : Tuple<T1, T2>, IEquatable<Tuple<T1, T2, T3>>, IComparable<Tuple<T1, T2, T3>>
    {
        public T3 Item3 { get; set; }

        public Tuple(T1 item1, T2 item2, T3 item3)
            : base(item1, item2)
        {
            Item3 = item3;
        }

        public static bool operator ==(Tuple<T1, T2, T3> t1, Tuple<T1, T2, T3> t2)
        {
            if (ReferenceEquals(t1, t2))
                return true;
            if ((object)t1 == null || (object)t2 == null)
                return false;
            return t1.Equals(t2);
        }

        public static bool operator !=(Tuple<T1, T2, T3> t1, Tuple<T1, T2, T3> t2)
        {
            return !(t1 == t2);
        }

        public override int GetHashCode()
        {
            return 0 ^ Item1.GetHashCode() ^ Item2.GetHashCode() ^ Item3.GetHashCode();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("(");
            stringBuilder.Append(Item1);
            stringBuilder.Append(", ");
            stringBuilder.Append(Item2);
            stringBuilder.Append(", ");
            stringBuilder.Append(Item3);
            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        public new string ToString(string pFormat)
        {
            return string.Format(pFormat, Item1, Item2, Item3);
        }

        public override bool Equals(object t)
        {
            if (t == null || !(t is Tuple<T1, T2, T3>))
                return false;
            return Equals((Tuple<T1, T2, T3>)t);
        }

        public bool Equals(Tuple<T1, T2, T3> t)
        {
            if (t == null)
                return false;
            return EqualityComparer<T1>.Default.Equals(Item1, t.Item1) && EqualityComparer<T2>.Default.Equals(Item2, t.Item2) && EqualityComparer<T3>.Default.Equals(Item3, t.Item3);
        }

        public int CompareTo(Tuple<T1, T2, T3> t)
        {
            return Comparer<T1>.Default.Compare(Item1, t.Item1);
        }
    }

    public class Tuple<T1, T2, T3, T4> : Tuple<T1, T2, T3>, IEquatable<Tuple<T1, T2, T3, T4>>, IComparable<Tuple<T1, T2, T3, T4>>
    {
        public T4 Item4 { get; set; }

        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
            : base(item1, item2, item3)
        {
            Item4 = item4;
        }

        public static bool operator ==(Tuple<T1, T2, T3, T4> t1, Tuple<T1, T2, T3, T4> t2)
        {
            if (ReferenceEquals(t1, t2))
                return true;
            if ((object)t1 == null || (object)t2 == null)
                return false;
            return t1.Equals(t2);
        }

        public static bool operator !=(Tuple<T1, T2, T3, T4> t1, Tuple<T1, T2, T3, T4> t2)
        {
            return !(t1 == t2);
        }

        public override int GetHashCode()
        {
            return 0 ^ Item1.GetHashCode() ^ Item2.GetHashCode() ^ Item3.GetHashCode() ^ Item4.GetHashCode();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("(");
            stringBuilder.Append(Item1);
            stringBuilder.Append(", ");
            stringBuilder.Append(Item2);
            stringBuilder.Append(", ");
            stringBuilder.Append(Item3);
            stringBuilder.Append(", ");
            stringBuilder.Append(Item4);
            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        public new string ToString(string pFormat)
        {
            return string.Format(pFormat, Item1, Item2, Item3, Item4);
        }

        public override bool Equals(object t)
        {
            if (t == null || !(t is Tuple<T1, T2, T3, T4>))
                return false;
            return Equals((Tuple<T1, T2, T3, T4>)t);
        }

        public bool Equals(Tuple<T1, T2, T3, T4> t)
        {
            if (t == null)
                return false;
            return EqualityComparer<T1>.Default.Equals(Item1, t.Item1) && EqualityComparer<T2>.Default.Equals(Item2, t.Item2) && EqualityComparer<T3>.Default.Equals(Item3, t.Item3) && EqualityComparer<T4>.Default.Equals(Item4, t.Item4);
        }

        public int CompareTo(Tuple<T1, T2, T3, T4> t)
        {
            return Comparer<T1>.Default.Compare(Item1, t.Item1);
        }
    }
}
