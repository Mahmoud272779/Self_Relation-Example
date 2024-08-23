using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicroTech.Models;

public partial class Account
{
    public string AccNumber { get; set; } = null!;

    public string? AccParent { get; set; }

    public decimal? Balance { get; set; }

    [JsonIgnore]
    public virtual Account? AccParentNavigation { get; set; }

    public virtual ICollection<Account> childAccounts { get; set; } = new List<Account>();
}
