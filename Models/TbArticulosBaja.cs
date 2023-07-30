using System;
using System.Collections.Generic;

namespace MVCCRUD.Models;

public partial class TbArticulosBaja
{
    public string CodArt { get; set; } = null!;

    public DateTime FechaBaja { get; set; }

    public virtual TbArticulo CodArtNavigation { get; set; } = null!;
}
