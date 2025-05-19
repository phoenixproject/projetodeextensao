using System;
using System.Collections.Generic;

namespace ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;

public partial class TipoServico
{
    public int TpServico { get; set; }

    public string DsTpServico { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
