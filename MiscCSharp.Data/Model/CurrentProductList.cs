﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MiscCSharp.Data.Model
{
    [Keyless]
    public partial class CurrentProductList
    {
        [Column("ProductID")]
        public int ProductId { get; set; }
        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }
    }
}
