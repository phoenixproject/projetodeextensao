using System;
using System.Collections.Generic;

namespace ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;

public partial class TipoUsuario
{
    public int TpUsuario { get; set; }

    public string DsTpUsuario { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
