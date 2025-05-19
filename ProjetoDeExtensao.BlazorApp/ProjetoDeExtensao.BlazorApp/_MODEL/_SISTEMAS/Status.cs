using System;
using System.Collections.Generic;

namespace ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;

public partial class Status
{
    public int CdStatus { get; set; }

    public string DsStatus { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
