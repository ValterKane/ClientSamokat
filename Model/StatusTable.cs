using System;
using System.Collections.Generic;

namespace ClientSamokat.Model;

public partial class StatusTable : ObservableModel
{
    private int statusID;
    private string descr;

    public int StatusId { get => statusID; set => SetProperty(ref statusID, value); }

    public string Descr
    {
        get => descr;
        set => SetProperty(ref descr, value);
    }

    public virtual ICollection<Journal> Journals { get; set; } = new List<Journal>();
}
