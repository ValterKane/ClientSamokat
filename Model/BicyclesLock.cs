namespace ClientSamokat.Model;

public partial class BicyclesLock : ObservableModel
{
    private int lockId;
    private DateTime lastDateInspect;
    private bool? stat;
    private int occ_id;
    private double latitude;
    private double longitude;


    public int LockId
    {
        get => lockId;
        set
        {
            SetProperty(ref lockId, value);
        }
    }

    public DateTime LastDateInspect
    {
        get => lastDateInspect;
        set
        {
            SetProperty(ref lastDateInspect, value);
        }
    }

    public bool? Stat
    {
        get => stat;
        set
        {
            SetProperty(ref stat, value);
        }
    }

    public int Occ_Id
    {
        get => occ_id;
        set
        {
            SetProperty(ref occ_id, value);
        }
    }

    public double Latitude
    {
        get => latitude;
        set => SetProperty(ref latitude, value);
    }

    public double Longitude
    {
        get => longitude;
        set => SetProperty(ref longitude, value);
    }

    public virtual ICollection<Journal> Journals { get; set; } = new List<Journal>();

    // override object.Equals
    public override bool Equals(object obj)
    {
        if (obj is not BicyclesLock)
            return false;
        else
        {
            BicyclesLock b1 = obj as BicyclesLock;

            if (b1.GetHashCode() == GetHashCode())
            {
                return true;
            }
        }
        return false;
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        return LockId.GetHashCode() ^ Occ_Id.GetHashCode() ^ LastDateInspect.GetHashCode() ^ Stat.GetHashCode();
    }




}
