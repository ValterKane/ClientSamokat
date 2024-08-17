using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientSamokat.Model;

public partial class Bicycle : ObservableModel
{
    private int bicycleId1;
    private string bicycleName1 = null!;
    private DateTime lastTiDate1;
    private DateTime checkInDate1;
    private string bicycleProducer1 = null!;
    private bool? stat1;
    private int occ_id;

    public int BicycleId
    {
        get => bicycleId1;
        set 
        {
            SetProperty(ref bicycleId1, value);
        }
    }

    public string BicycleName
    { 
        get => bicycleName1; 
        set 
        {
            SetProperty(ref bicycleName1, value);
        } 
    }

    public DateTime LastTiDate 
    { 
        get => lastTiDate1; 
        set 
        {
            SetProperty(ref lastTiDate1, value);
        } 
    }

    public DateTime CheckInDate 
    { 
        get => checkInDate1; 
        set 
        {
            SetProperty(ref checkInDate1, value);
        } 
    }

    public string BicycleProducer
    { 
        get => bicycleProducer1; 
        set
        {
            SetProperty(ref bicycleProducer1, value);
        } 
    }

    public bool? Stat 
    { 
        get => stat1; 
        set 
        {
            SetProperty(ref stat1, value);
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

    public virtual ICollection<Journal> Journals { get; set; } = new List<Journal>();
    public virtual Occ Occ { get; set; } = null!;

    // override object.Equals
    public override bool Equals(object obj)
    {
        if (obj is not Bicycle)
            return false;
        else
        { 
            Bicycle b1 = obj as Bicycle;

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
        return bicycleId1.GetHashCode() ^ bicycleName1.GetHashCode() ^ lastTiDate1.GetHashCode() ^ checkInDate1.GetHashCode() ^ bicycleProducer1.GetHashCode() ^ stat1.GetHashCode() ^ occ_id.GetHashCode();
    }
}
