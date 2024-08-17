using System;
using System.Collections.Generic;

namespace ClientSamokat.Model;

public partial class Journal
{
    public int JournalId { get; set; }

    public DateTime JournalDate { get; set; }

    public int BicycleId { get; set; }

    public int LockId { get; set; }

    public int PersonId { get; set; }

    public int StatusId { get; set; }

    public DateTime? DateOfSing { get; set; }

    public int OccId { get; set; }

    public virtual Bicycle Bicycle { get; set; } = null!;

    public virtual BicyclesLock Lock { get; set; } = null!;

    public virtual Occ Occ { get; set; } = null!;

    public virtual Courier Person { get; set; } = null!;

    public virtual StatusTable Status { get; set; } = null!;

    // override object.Equals
    public override bool Equals(object obj)
    {
        if (obj is not Journal)
            return false;
        else
        {
            Journal b1 = obj as Journal;

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
        return JournalId.GetHashCode() ^ BicycleId.GetHashCode() ^ LockId.GetHashCode() ^ PersonId.GetHashCode() + StatusId.GetHashCode() ^ DateOfSing.GetHashCode() ^ OccId.GetHashCode();
    }
}
