using System;
using System.Collections.Generic;

namespace MVCCRUD.Models;

public partial class TbArticulo
{
    public string CodArt { get; set; } = null!;

    public string? NomArt { get; set; }

    public string? UniMed { get; set; }

    public decimal? PreArt { get; set; }

    public int? StkArt { get; set; }

    public string? DeBaja { get; set; }

    public virtual ICollection<TbArticulosBaja> TbArticulosBajas { get; set; } = new List<TbArticulosBaja>();

    public virtual ICollection<TbArticulosLiquidacion> TbArticulosLiquidacions { get; set; } = new List<TbArticulosLiquidacion>();
}
