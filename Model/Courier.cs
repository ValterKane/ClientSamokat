using System;
using System.Collections.Generic;

namespace ClientSamokat.Model;

public partial class Courier
{
    public int PersonId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public bool? Stat { get; set; }

    public virtual ICollection<Journal> Journals { get; set; } = new List<Journal>();

    public virtual Person Person { get; set; } = null!;

    // override object.Equals
    public override bool Equals(object obj)
    {
        if (obj is not Courier)
            return false;
        else
        {
            Courier b1 = obj as Courier;

            if (b1.GetHashCode() == this.GetHashCode())
            {
                return true;
            }
        }
        return false;
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        return PersonId.GetHashCode() ^ PhoneNumber.GetHashCode() ^ Stat.GetHashCode();
    }
}
